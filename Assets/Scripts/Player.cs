using UnityEngine;

public class Player : MonoBehaviour
{
	// Variables
	// =========================================================================
	public GameManger gm;

	private AudioSource _audio;
	public AudioSource JumpLandAudio;
	private bool _wasGrounded = true;

	private bool _isDead;

	public bool IsDead
	{
		get { return _isDead; }
		set
		{
			_isDead = value;
			StartCoroutine(AudioManager.FadeOut(_audio, 0.5f));
		}
	}

	// Movement
	// -------------------------------------------------------------------------
	private Rigidbody _rigidbody;
	private bool _isRight = true;
	private bool _disableMovement;

	private float _acceleration = 7f
				, _jumpForce = 17f
				, _distToGround;

	// Power
	// -------------------------------------------------------------------------
	private float _playerPowerLevel = 1f;
	public float PlayerPowerLevel
	{
		get { return _playerPowerLevel; }
		set
		{
			float v = Mathf.Clamp01(value);
			
			_playerPowerLevel = v;
			Battery.BatteryLevel = v;
		}
	}

	public Battery Battery;

	// Unity
	// =========================================================================
	void Awake ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_audio = GetComponent<AudioSource>();
		_audio.volume = 0f;
	}

	private void Start()
	{
		_distToGround = GetComponentInChildren<Collider>().bounds.extents.y;
	}

	private void Update()
	{
		Quaternion nextRotation;
		float rotationSpeed = 0.1f;
		
		if (_isRight)
		{
			nextRotation = Quaternion.identity;
			rotationSpeed = 10f;
		}
		else
		{
			nextRotation = new Quaternion(0, 180f, 0, 0);
		}
		
		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			nextRotation,
			Time.deltaTime * rotationSpeed
		);
		
		// Move Sound
		// ---------------------------------------------------------------------
		
		bool isGrounded = IsGrounded();

		if (isGrounded && !_disableMovement && !_isDead)
		{
			float v = _rigidbody.velocity.magnitude / 10f;
			_audio.volume = Mathf.Clamp(v, 0f, 0.2f);
			
			if (!_wasGrounded)
			{
				JumpLandAudio.Play();
			}
		}
		else
		{
			StartCoroutine(AudioManager.FadeOut(_audio, 0.1f, false));
		}
		
		_wasGrounded = isGrounded;
	}

	private void FixedUpdate()
	{
		if (_disableMovement) return;
		
		bool isGrounded = IsGrounded();
		
		// Horizontal Movement
		// ---------------------------------------------------------------------
		
		float x = Input.GetAxis("Horizontal") * _acceleration;

		if (!isGrounded)
			x *= 0.9f;

		if (x != 0f)
			_isRight = x > 0;

		Vector3 velocity = new Vector3(x, _rigidbody.velocity.y, 0);
		_rigidbody.velocity = velocity;
		
		// Jumping
		// ---------------------------------------------------------------------

		if (Input.GetButton("Jump") && isGrounded)
		{
			_rigidbody.AddForce(
				new Vector3(0, _jumpForce, 0), 
				ForceMode.Impulse
			);
		}
		
		// Fix: Prevent player sticking to walls
		// ---------------------------------------------------------------------

		Vector3 fwd = transform.TransformDirection(Vector3.right);
		RaycastHit hitPushable;
		
		if (Physics.Raycast(transform.position, fwd, out hitPushable, 1f))
		{
			if (hitPushable.transform.tag.Contains("Pushable") && isGrounded)
				return;
		}
		
		Vector3 horizontalMove = _rigidbody.velocity;
		horizontalMove.y = 0;
		float distance = horizontalMove.magnitude * Time.fixedDeltaTime;
		horizontalMove.Normalize();
		
		RaycastHit hit;
		if (_rigidbody.SweepTest(
			horizontalMove, out hit, distance, QueryTriggerInteraction.Ignore
		)) {
//			float x2 = hit.transform.tag.Contains("Floor") ? x : 0f;
			float x2 = isGrounded ? x : 0f;
			_rigidbody.velocity = new Vector3(x2, _rigidbody.velocity.y, 0);
		}
	}

	private void OnTriggerEnter(Collider trigger)
	{
		switch (trigger.tag)
		{
			case "KillBox":
				_isDead = true;
				gm.OnPlayerDeath();
				return;
				
			case "BatteryInteractable":
				StartObjectInteraction(trigger);
				return;
				
			case "Exit":
				_disableMovement = true;
				gm.EndLevel1();
				return;
		}

		switch (trigger.name)
		{
			case "BayDoorClose":
				trigger.enabled = false;
				gm.am.CloseBayDoor();
				gm.am.ExitBayDoorGuy();
				return;
		}
	}

	private void OnTriggerExit(Collider trigger)
	{
		switch (trigger.tag)
		{
			case "BatteryInteractable":
				EndObjectInteraction(trigger);
				break;
		}
	}
	
	// Object Interaction
	// =========================================================================

	private void StartObjectInteraction(Collider trigger)
	{
		trigger.GetComponent<Interactable.Interactable>().StartInteract(this);
	}

	private void EndObjectInteraction(Collider trigger)
	{
		trigger.GetComponent<Interactable.Interactable>().EndInteract();
	}

	// Helpers
	// =========================================================================
	/// <summary>
	/// Check if the player is on the ground
	/// </summary>
	/// <returns></returns>
	private bool IsGrounded()
	{	
		return Physics.Raycast(
			transform.position, 
			-Vector3.up, 
			_distToGround + 0.1f
		);
	}
	
}
