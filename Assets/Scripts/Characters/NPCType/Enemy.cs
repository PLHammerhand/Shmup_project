using UnityEngine;
using System.Collections;

public class Enemy : NPCType
{
    public NPC          npc;
    public GameObject   target;

	void Awake()
    {
        npc = gameObject.GetComponent<NPC>();

        if(npc != null)
            gameObject.tag = "Enemy";
	}
	
	void Update()
    {
	
	}

    private void __Attack()
    {

    }
	
	void OnTriggerEnter(Collider other)
	{

	}
}
