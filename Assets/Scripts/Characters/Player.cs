using UnityEngine;
using System.Collections;

public class Player : Character
{
	protected override void Awake()
	{
		base.Awake();

		if(gameObject.GetComponent<PlayerMovementManager>() == null)
			gameObject.AddComponent<PlayerMovementManager>();

		if(gameObject.GetComponent<PlayerWeaponsManager>() == null)
			gameObject.AddComponent<PlayerWeaponsManager>();
	}

	public override void Heal(int healingAmount)
	{
		base.Heal(healingAmount);

		GUIManager.Instance.PlayerHealthChanged();
	}

	public override void Damage(int damageAmout)
	{
		base.Damage(damageAmout);

		GUIManager.Instance.PlayerHealthChanged();
	}

	protected override void _DestroyCharacter()
	{
		LevelMaster.Instance.PlayerDestroyed();

		base._DestroyCharacter();
	}
}
