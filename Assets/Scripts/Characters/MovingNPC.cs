using UnityEngine;
using System.Collections;

public class MovingNPC : NPC
{
	public float			    forwardSpeed;
	public float			    sideSpeed;
	public float				backwardSpeed;

	protected float             _fireRange;
	protected float             _baseDrag;
	protected Rigidbody         _selfRigidbody;


	void Start()
	{
		_fireRange = gameObject.GetComponent<SphereCollider>().radius;
		_selfRigidbody = gameObject.GetComponent<Rigidbody>();
		_baseDrag = _selfRigidbody.drag;
	}

	void Update()
	{
		if(target != null)
		{
			if(Vector3.Angle(gameObject.transform.forward, target.transform.position - gameObject.transform.position) > _FireAngle)
			{
				_Rotate();
				HoldFire();
			}
			else
			{
				if(Vector3.Angle(gameObject.transform.forward, target.transform.position - gameObject.transform.position) < _FireAngle)
					_Rotate();

				_Fire();
			}
		}
		else
			HoldFire();
	}

	void FixedUpdate()
	{
		if(target != null)
		{
			//if(Vector3.Angle(gameObject.transform.forward, target.transform.position - gameObject.transform.position) < _FireAngle)
			//{
			if(Vector3.Distance(gameObject.transform.position, target.transform.position) > (3 *_fireRange / 4))
				Move();
			else if(Vector3.Distance(gameObject.transform.position, target.transform.position) < (3 * _fireRange / 4) &&
					Vector3.Distance(gameObject.transform.position, target.transform.position) > (_fireRange / 2))
				Stop();
			else
				Move(MoveDirection.BACKWARD);
		}
		//}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
			target = other.gameObject;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			target = null;
			HoldFire();
		}
	}

	public void Move(MoveDirection direction = MoveDirection.FORWARD)
	{
		Vector3 dir;

		Debug.Log(">>> Moving: " + direction.ToString());

		switch(direction)
		{
			case MoveDirection.LEFT:
				dir = -gameObject.transform.right * sideSpeed;
				break;
			case MoveDirection.RIGHT:
				dir = gameObject.transform.right * sideSpeed;
				break;
			case MoveDirection.BACKWARD:
				dir = -gameObject.transform.forward * backwardSpeed;
				break;
			default:
				dir = gameObject.transform.forward * forwardSpeed;
				break;
		}

		_selfRigidbody.AddForce(dir);
	}

	public void Move(Transform movePosition)
	{
		if(Vector3.Distance(gameObject.transform.position, movePosition.position) > 1f)
			Move();
	}

	public void Stop()
	{
		_selfRigidbody.AddForce(-_selfRigidbody.velocity, ForceMode.Impulse);
	}
}
