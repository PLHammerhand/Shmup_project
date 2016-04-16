using UnityEngine;
using System.Collections;
using System;

public class Guard : SimpleOrder
{
	public bool         randomGuardTime;
	public bool         clockwiseTurning;
	public float        guardTime;
	public float        guardTimeRandomSpan;

	public override void PreformOrder()
	{
		NPC npc = gameObject.GetComponent<NPC>();

		if(npc.GetType().Equals(typeof(StationaryNPC)))
		{
			if(clockwiseTurning)
			{
				//	TODO
			}
			else
			{
				//	TODO
			}
		}
		else if(npc.GetType().Equals(typeof(MovingNPC)) || npc.GetType().Equals(typeof(SuicideNPC)))
		{
			//	TODO
		}
	}
}
