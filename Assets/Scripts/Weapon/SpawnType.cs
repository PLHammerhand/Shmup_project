using UnityEngine;
using System.Collections;
using System;

public class SpawnType : Weapon
{
    public GameObject[]     spawnList;
	public Transform        spawnPosition;
    public bool             randomSpawnFromList;
	public bool             randomSpawnPosition;
	public bool             randomSpawnTime;
	public float            spawnDelay;
	public float            spawnTimeInterval;
    public float            horizontalSpawnRange;							//	X axis
    public float            verticalSpawnRange;								//	Y axis

	private int             __spawnListIterator					= 0;

	void Start()
	{
		//	TODO
		objectPool.Initialize();
	}

    void Update()
    {
		if(_CheckPlayerFirePermission() && _nextFireTime <= 0)
			Fire();

		if(TimeManager.Instance.gameplayState && _nextFireTime > 0)
			_nextFireTime -= Time.deltaTime;
	}

	protected override void _CalculateRefireTime()
	{
		_nextFireTime = spawnDelay;

		if(randomSpawnTime)
			_nextFireTime += UnityEngine.Random.Range(-spawnTimeInterval, spawnTimeInterval);
	}

	public override void Fire()
    {
		if(randomSpawnFromList)
			__spawnListIterator = (int)UnityEngine.Random.Range(0, spawnList.Length - 1);
		else
		{
			__spawnListIterator++;

			if(__spawnListIterator == spawnList.Length)
				__spawnListIterator = 0;
		}

		__Spawn(__spawnListIterator);
    }

	private void __Spawn(int spawnNumber)
	{
		Vector3 spawnPos = spawnPosition.position;

		if(randomSpawnPosition)
			spawnPos += new Vector3(UnityEngine.Random.Range(-horizontalSpawnRange, horizontalSpawnRange), UnityEngine.Random.Range(-verticalSpawnRange, verticalSpawnRange), 0f);

		GameObject go = objectPool.GetObject();

		if(go != null)
		{
			go.transform.position = spawnPos;
			go.transform.rotation = spawnPosition.rotation;
			go.SetActive(true);
		}
	}
}
