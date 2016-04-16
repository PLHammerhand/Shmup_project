using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public int              collisionDamage;
	public int              startHealth;
	public bool             openFire;

	[HideInInspector]
	public List<Weapon>     weaponsList					= new List<Weapon>();

	protected int           _maxHealth;

	private int             __currentHealth;

	public int currentHealth
	{
		get
		{
			return __currentHealth;
		}

		protected set
		{
			__currentHealth = value;
		}
	}


	protected virtual void Awake()
	{
		_maxHealth = startHealth;
		currentHealth = startHealth;

		weaponsList.AddRange(gameObject.transform.GetComponentsInChildren<Weapon>());
	}

	public virtual void Heal(int healingAmount)
	{
		if(currentHealth < _maxHealth)
		{
			currentHealth += healingAmount;

			if(currentHealth > _maxHealth)
				currentHealth = _maxHealth;
		}
	}

	public virtual void Damage(int damageAmout)
	{
		currentHealth -= damageAmout;

		if(currentHealth <= 0)
			_DestroyCharacter();
	}

	void OnCollisionEnter(Collision other)
	{
		//	TODO	calculate collision damage based on the speed of the colliding objects
		Character character = other.gameObject.GetComponent<Character>();
		
		if(character != null)
			character.Damage(collisionDamage);
	}

	protected virtual void _DestroyCharacter()
	{
		Debug.Log(">>>> Character " + gameObject.name + " (ID: " + gameObject.GetInstanceID() + ") destroyed.");
		gameObject.SetActive(false);
	}
}
