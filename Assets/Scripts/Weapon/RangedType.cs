using UnityEngine;
using System.Collections;

public abstract class RangedType : Weapon
{
    public GameObject		bulletPrefab;
	public Transform        muzzle;
	public float            range;

	protected override void Awake()
	{
		base.Awake();

		muzzle = transform.FindChild("Muzzle");
	}
}
