using UnityEngine;
using System.Collections;

public class SuicideNPC : MovingNPC
{
	public int              damageAmount;
	public bool             damageMultipleTargets       = false;


	void OnCollisionEnter(Collision col)
	{
		Character collisionTarget = col.gameObject.GetComponent<Character>();
		
    }

	protected override void _DestroyCharacter()
	{


		base._DestroyCharacter();
	}

	private void __DealDamage()
	{

	}
}
