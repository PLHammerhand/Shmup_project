using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public int					damage;
    public GameObject			fireParticles;
	public ObjectPool           objectPool;
	public bool					openFire			= false;

	[HideInInspector]
    public GameObject			target;

    protected float				_refireTime;
	protected float				_nextFireTime;



	protected abstract void _CalculateRefireTime();

	protected virtual void Awake()
	{
		objectPool = gameObject.AddComponent<ObjectPool>();
	}

    public virtual void Fire()
	{
		_ShowParticles();
	}

	protected virtual void _ShowParticles()
	{
		//	TODO
	}

	protected bool _CheckPlayerFirePermission()
	{
		if(PlayerWeaponsManager.Instance.allowFire && openFire && TimeManager.Instance.gameplayState)
			return true;
		else
			return false;
	}
}
