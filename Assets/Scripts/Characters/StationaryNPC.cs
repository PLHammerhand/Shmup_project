using UnityEngine;
using System.Collections;

public class StationaryNPC : NPC
{
    [Range(20,90)]
    public float			fireRange;          //	Percentage range of trigger to fire after detecting target
	public bool             angleDetection		= false;
	[Range(1,180)]
	public float            detectionAngle		= 180f;
	public float            warnedTime;
	public Transform        gunMounting;

	private float           __timer;
	private float           __fireRange;
	private bool            __attacking         = false;


	protected override void Awake()
	{
		fireRange = fireRange / 100f;
		__fireRange = gameObject.GetComponent<SphereCollider>().radius * fireRange;

		if(gunMounting == null)
			gunMounting = transform.FindChild("Mounting");
	}

	void Update()
	{
		if(target != null)
		{
			if(Vector3.Angle(gunMounting.transform.forward, target.transform.position - gameObject.transform.position) < 1f)
			{
				if(Vector3.Distance(gameObject.transform.position, target.transform.position) <= __fireRange)
					_Fire();
			}
			else
				_Rotate(gunMounting);
		}

		if(__timer > 0f)
			__timer -= Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(!angleDetection)
				target = other.gameObject;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(angleDetection && Vector3.Angle(gunMounting.transform.forward, other.transform.position - gameObject.transform.position) <= detectionAngle)
		{
			target = other.gameObject;
			//other.gameObject == target && !__attacking && Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position) <= __fireRange)
			//_Fire();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			target = null;
			HoldFire();
			__timer = warnedTime;
		}
	}
}
