using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeaponsManager : PlayerManager<PlayerWeaponsManager>
{
	public bool                 allowFire           = true;


	void Update()
	{
		if(TimeManager.Instance.gameplayState && allowFire)
		{
			if(Input.GetButtonDown("Fire"))
			{
				foreach(Weapon w in player.weaponsList)
					w.openFire = true;
			}
			if(Input.GetButtonUp("Fire"))
			{
				foreach(Weapon w in player.weaponsList)
					w.openFire = false;
			}
		}
	}
}
