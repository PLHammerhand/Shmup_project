using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMaster : Singleton<LevelMaster>
{
	public List<EnemySpawn>						enemiesSpawnsList		= new List<EnemySpawn>();
	public List<BonusSpawn>						bonusesSpawnsList		= new List<BonusSpawn>();

	private int									__score;
	private int									__kills;

	private int									__maxBonusesOnMap;
	private int									__maxEnemiesOnMap;
	private float								__mapHeight;
	private float								__mapWidth;

	private List<NPC>							__enemies				= new List<NPC>();

	public int Kills
	{
		get
		{
			return __kills;
		}
		set
		{
			__kills = value;

			GUIManager.Instance.Kills = __kills;
		}
	}

	public int Score
	{
		get
		{
			return __score;
		}
		set
		{
			__score = value;

			//	TODO:	check if score counting works
			//GUIManager.Instance.Score = __score;
		}
	}


	void Start()
	{
		
	}

	void Update()
	{
		if(GUIManager.Instance.ready && !ready)
			__Init();
	}

	private void __Init()
	{
		enemiesSpawnsList.AddRange(GameObject.FindObjectsOfType<EnemySpawn>());
		bonusesSpawnsList.AddRange(GameObject.FindObjectsOfType<BonusSpawn>());

		GameObject[] en = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject o in en)
			__enemies.Add(o.GetComponent<NPC>());

		ready = true;
	}

	public void Initialize()
	{
		if(GUIManager.Instance.ready)
		{
			//	TODO

		//__maxBonusesOnMap = bonuses;
		//__maxEnemiesOnMap = enemies;

		//__mapHeight = height;
		//__mapWidth = width;

		//if(__mapHeight > 0f && __mapWidth > 0f)
		//	__InitializeMapBounds();

			__Init();
		}
	}

	public void PlayerDestroyed()
	{
		Debug.Log("\t\t>>>> FAIL <<<<");
		foreach(NPC npc in __enemies)
		{
			if(npc.target == PlayerMovementManager.Instance.player.gameObject)
				npc.target = null;
		}

		GUIManager.Instance.EndGame();
	}

	public void EnemyDestroyed(GameObject enemy)
	{
		Debug.Log(">> Enemy down");
		//	TODO:	Insert enemy to the object pool
		//	Not final version
		__enemies.Remove(enemy.GetComponent<NPC>());

		if(__enemies.Count == 0)
		{
			Debug.Log("\t\t>>>> WIN <<<<");
			PlayerMovementManager.Instance.enablePlayerControl = false;
			GUIManager.Instance.EndGame(true);
		}
	}
}
