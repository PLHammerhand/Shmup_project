using UnityEngine;
using System.Collections;

public class GunType : RangedType
{
    public int					firerate;
	public int					muzzleVelocity;

	private Transform           __direction;

	protected override void Awake()
	{
		base.Awake();

       _refireTime = (60f / firerate);
		__direction = transform.FindChild("Direction");
	}
	
	void Start()
	{
		_CalculateRefireTime();
		objectPool.count = (int)((range / muzzleVelocity ) / _refireTime);
		objectPool.objectPrefab = bulletPrefab;
		objectPool.Initialize();

		if(muzzle == null)
			muzzle = transform;
	}

    void Update()
    {
		if(_CheckPlayerFirePermission() && _nextFireTime <= 0)
			Fire();

		if(TimeManager.Instance.gameplayState && _nextFireTime > 0)
			_nextFireTime -= Time.deltaTime;
	}

    public override void Fire()
    {
		Debug.Log("Fire!");
		GameObject bullet = objectPool.GetObject();
		bullet.transform.position = muzzle.position;
		bullet.transform.rotation = muzzle.rotation;
		__SetBulletProperties(bullet.GetComponent<Bullet>());
		_CalculateRefireTime();
		_ShowParticles();
		bullet.SetActive(true);
    }

	protected override void _CalculateRefireTime()
	{
		_nextFireTime = _refireTime;
	}

	private void __SetBulletProperties(Bullet bullet)
	{
        bullet.bulletSpeed = muzzleVelocity;
		bullet.bulletDamage = damage;
		bullet.bulletLifetime = range / muzzleVelocity;
	}
}
