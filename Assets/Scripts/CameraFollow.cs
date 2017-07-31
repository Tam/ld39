using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public GameObject Player;
	private Player _player;

	private float _speed = 5f;
	private Vector3 _offset;

	private Vector3 _nextPos;

	void Start ()
	{
		_player = Player.GetComponent<Player>();
		_offset = transform.position - Player.transform.position;
	}

	private void Update()
	{
		if (_player.IsDead)
			return;
		
		_nextPos = Player.transform.position + _offset;
	}

	void LateUpdate ()
	{
		transform.position = Vector3.Lerp(
			transform.position,
			_nextPos,
			_speed * Time.deltaTime
		);
	}
	
}
