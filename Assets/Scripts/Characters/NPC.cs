using UnityEngine;
using System.Collections;

public class NPC : Character
{
	protected static float  _FireAngle = 10f;

	public bool             isRotating;
	public float            rotationSpeed;
	public Rotator          rotator;
	public GameObject		target;

	protected void _Fire()
	{
		foreach(Weapon w in weaponsList)
			w.openFire = true;
	}

	public void HoldFire()
	{
		foreach(Weapon w in weaponsList)
			w.openFire = false;
	}

	protected override void _DestroyCharacter()
	{
		base._DestroyCharacter();
		LevelMaster.Instance.EnemyDestroyed(gameObject);
	}

	protected void _Rotate(Transform objectToRotate = null)
	{
		Vector3 dir = gameObject.transform.up;

		if(objectToRotate != null)
		{
			if(Vector3.Cross(objectToRotate.transform.forward, target.transform.position - objectToRotate.transform.position).z > 0f)
				dir = -dir;

			dir = dir * rotationSpeed * Time.deltaTime;

			objectToRotate.transform.Rotate(dir, Space.World);
		}
		else
		{
			if(Vector3.Cross(gameObject.transform.forward, target.transform.position - gameObject.transform.position).z > 0f)
				dir = -dir;

			dir = dir * rotationSpeed * Time.deltaTime;

			transform.Rotate(dir, Space.World);
		}
	}
}
