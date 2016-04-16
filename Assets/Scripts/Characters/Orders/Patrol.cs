using UnityEngine;
using System.Collections;
using System;

public class Patrol : ComplexOrder
{
	public Transform[]				waypoints;
	public bool                     moveToRandomWaypoints		= false;

	private int                     __waypointNumber            = 0;

	public override void PreformOrder()
	{
		
	}
}
