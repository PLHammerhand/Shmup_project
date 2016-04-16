using UnityEngine;
using System.Collections;
using System;

public class Move : SimpleOrder
{
	public Transform        targetPosition;
	public float            precision               = 1f;

	private NPC             __npc;

	void Start()
	{
		__npc = gameObject.GetComponent<NPC>();
	}

	public override void PreformOrder()
	{
		
	}
}
