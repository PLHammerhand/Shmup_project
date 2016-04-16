using UnityEngine;
using System.Collections;

public class RayType : RangedType
{
	private enum RayTypeState
	{
		READY,
		FIRING,
		WAITING,
		COOLING,
		LOCKED
	}

	public static int		HitFramesDelay			= 5;

    public bool				hasCooldown				= false;
    public float			maxFireTime;
    public float			cooldownDelay;
	public float            cooldownTime;


	private int				__bulletsLayer          = 1 << 8;
	private int             __nextDamageFrame;
	private float			__fireTime;
	private LineRenderer    __laser;
	private RayTypeState    __currentState			= RayTypeState.READY;
	private RaycastHit		__hit;

	void Start()
    {
		__bulletsLayer = ~__bulletsLayer;

		__nextDamageFrame = HitFramesDelay;
		__laser = gameObject.AddComponent<LineRenderer>();
		__laser.material = new Material(Shader.Find("Particles/Additive"));
		__laser.SetColors(Color.red, Color.red);
		__laser.SetWidth(0.1f, 0.1f);
		__laser.enabled = false;

		if(muzzle == null)
			muzzle = transform;
	}

	void Update()
	{
		if(TimeManager.Instance.gameplayState)
		{
			if(_CheckPlayerFirePermission() && __currentState != RayTypeState.LOCKED)
				Fire();
			if(!openFire)
				__laser.enabled = false;

			if(hasCooldown)
				__ManageCooldown();
		}
	}

	void FixedUpdate()
	{
		if(TimeManager.Instance.gameplayState && __currentState == RayTypeState.FIRING)
		{
			__nextDamageFrame--;

			if(__nextDamageFrame == 1)
				__DealDamage();
		}
	}

	protected override void _CalculateRefireTime()
	{
		//	STATE LOCKED
		__currentState = RayTypeState.LOCKED;
		__laser.enabled = false;
		__fireTime = 0f;
		_nextFireTime = cooldownTime + cooldownDelay;
	}

	private void __ManageCooldown()
	{
		if(__currentState == RayTypeState.LOCKED)
		{
			if(_nextFireTime <= 0)
				__currentState = RayTypeState.READY;
			else
				_nextFireTime -= Time.deltaTime;
		}
		else if(!openFire && __currentState == RayTypeState.FIRING)
		{
			__CalculateCooldownDelay();
		}
		else if(__currentState == RayTypeState.WAITING)
		{
			if(_refireTime <= 0)
				__CalculateCooldown();
			else
				_refireTime -= Time.deltaTime;
		}
		else if(__currentState == RayTypeState.COOLING)
		{
			if(__fireTime <= 0f)
				__currentState = RayTypeState.READY;
			else
			{
				__fireTime = (_refireTime / maxFireTime);
				_refireTime -= Time.deltaTime;
			}
		}
	}

	private void __CalculateCooldown()
	{
		//	STATE COOLING
		__currentState = RayTypeState.COOLING;
		__laser.enabled = false;
		_refireTime = (__fireTime / maxFireTime) * cooldownTime;
	}

	private void __CalculateCooldownDelay()
	{
		//	STATE WAITING
		__currentState = RayTypeState.WAITING;
		__laser.enabled = false;
		_refireTime = cooldownDelay;
	}

	private void __DrawRay()
    {
		Physics.Raycast(muzzle.position, muzzle.forward, out __hit, range, __bulletsLayer);
		
		__laser.SetPosition(0, muzzle.position);

		if(__hit.collider != null)
			__laser.SetPosition(1, muzzle.position + muzzle.forward * Vector3.Distance(muzzle.position, __hit.point));
		else
			__laser.SetPosition(1, muzzle.position + muzzle.forward * range);

		__laser.enabled = true;
    }

	private void __DealDamage()
	{
		__nextDamageFrame = HitFramesDelay;

		if(__hit.collider != null)
		{
			Character character = __hit.collider.GetComponent<Character>();

			if(__hit.collider.GetComponent<Character>() != null)
				character.Damage(damage);
        }
	}

    public override void Fire()
    {
		//	STATE FIRING
		__currentState = RayTypeState.FIRING;
		__DrawRay();

		if(hasCooldown)
		{
			__fireTime += Time.deltaTime;

			if(__fireTime >= maxFireTime)
				_CalculateRefireTime();
		}
    }
}
