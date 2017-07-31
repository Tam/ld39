using UnityEngine;

public class Pushable : MonoBehaviour
{
	private Rigidbody _rigidbody;
	private AudioSource _audio;

	private bool _isDead;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_audio = GetComponent<AudioSource>();
		_audio.volume = 0f;
	}

	private void FixedUpdate()
	{
		if (_isDead) return;
		
		float v = _rigidbody.velocity.magnitude / 10f;
		_audio.volume = Mathf.Clamp(v, 0f, 1f);
	}

	private void OnTriggerEnter(Collider trigger)
	{
		if (trigger.tag.Contains("KillBox"))
		{
			_isDead = true;
			StartCoroutine(AudioManager.FadeOut(_audio, 0.5f));
		}
	}
}
