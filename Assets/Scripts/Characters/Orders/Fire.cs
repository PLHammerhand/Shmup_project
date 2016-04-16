using UnityEngine;
using System.Collections;
using System;

public class Fire : SimpleOrder
{
	public override void PreformOrder()
	{
		gameObject.GetComponent<NPC>().openFire = true;
	}

	public void OnDestroy()
	{
		gameObject.GetComponent<NPC>().openFire = false;
	}
}
