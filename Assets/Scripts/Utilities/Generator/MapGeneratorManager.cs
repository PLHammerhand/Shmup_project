using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapGeneratorManager : Singleton<MapGeneratorManager>
{
	public static Dictionary<string, Object[]>      Assets;

	public float									levelHeight;
	public float									levelWidth;

	public Vector3[]								enemySpawns;
	public Vector3[]								bonusSpawns;

	private bool									__mapGenerated;
	private SpawnObject						        __currentSpawnType;
	private Initializator							__init;
	
	private bool[,]									__map;								//	false if empty
	private List<string>							__spawns = new List<string>();

	public bool mapGenerated
	{
		get;
		private set;
	}




	public void Initialize(Initializator init)
	{
		__init = init;
		__map = new bool[__init.mapWidth / 5, __init.mapHeight / 5];
		__currentSpawnType = SpawnObject.BONUS;

		__InitializeFoldersList();

		enemySpawns = __GeneratePositions(__init.maxEnemiesOnMap);
		bonusSpawns = __GeneratePositions(__init.maxBonusesOnMap);

		__GenerateMap();
	}

	private void __GenerateMap()
	{
		__InitializeMapBounds();

	}

	private Vector3[] __GeneratePositions(int number)
	{
		Vector3[] spawns = new Vector3[number];

		int x;
		int y;

		for(int i = 0; i < number; i++)
		{
			bool searching = true;
			do
			{
				x = UnityEngine.Random.Range(0, __init.mapWidth);
				y = UnityEngine.Random.Range(0, __init.mapHeight);

				if(__CheckPositionStatus(x, y))
				{
					__InsertSpawn(x, y);
					searching = false;
				}

			} while(searching);
		}

		return spawns;
	}

	private bool __CheckPositionStatus(int x, int y)
	{
		//	false = empty
		if(__map[x, y])
			return false;
		else
			return true;
	}

	/*
	private GameObject __GenerateGameObject()
	{
		//	THIS SHOULD BE IN LEVEL MASTER

		string filepath = "";
		
		filepath += __folders[(int)__currentSpawnType];

		Object[] loadedAssets;

		if(!Assets.ContainsKey(filepath))
		{
			loadedAssets = Resources.LoadAll(filepath);
			Assets.Add(filepath, loadedAssets);
		}
		else
			Assets.TryGetValue(filepath, out loadedAssets);

		int i = UnityEngine.Random.Range(0, loadedAssets.Length - 1);

		return loadedAssets[i] as GameObject;
	}
	*/

	private void __InsertSpawn(int x, int y)
	{
		GameObject go;

		go = Resources.Load("Spawns" + Path.PathSeparator + __spawns[(int)__currentSpawnType]) as GameObject;

		float posX = -((float)__init.mapWidth / 2) + x;
		float posY = -((float)__init.mapHeight / 2) + y;

		go.transform.position = new Vector3(posX, posY, 0);

		Vector2 size = go.GetComponent<ObjectGenerationInfo>().size;

		__LockPositions(x, y, size);
	}

	private void __LockPositions(int x, int y, Vector2 size)
	{
		//	TODO:
		//	Next version - more advanced generation
		//	Zastanowić się, w jaki sposób ograniczać wielkość ObjectGenerationInfo -
		//		- czy wyliczać środek (krótszy kod) czy podwójnie liczyć (dłuższy)

		__map[x / 5 , y / 5] = true;
		//int sizeX = (size.x < 1 ? 1 : (int)size.x);
		//int sizeY = (size.y < 1 ? 1 : (int)size.y);

		//if(sizeX > 1 || sizeY > 1)
		//{
		//	int horizontalStart;
		//	int verticalStart;

		//	for(int i = -sizeX; i < sizeX; i++)
		//	{

		//	}
		//}
	}

	private void __InitializeFoldersList()
	{
		__spawns.Add("Bonus");
		__spawns.Add("Enemy");
	}

	private void __InitializeMapBounds()
	{
		GameObject go;

		//	NORTH
		go = new GameObject();
		go.name = "NORTH_BOUND";
		go.AddComponent<BoxCollider>().size = new Vector3(__init.mapWidth, 1f, 1f);
		go.transform.position = new Vector3(0, __init.mapHeight / 2f, 0);

		//	EAST
		go = new GameObject();
		go.name = "EAST_BOUND";
		go.AddComponent<BoxCollider>().size = new Vector3(1f, __init.mapHeight, 1f);
		go.transform.position = new Vector3(__init.mapWidth / 2f, 0, 0);

		//	WEST
		go = new GameObject();
		go.name = "WEST_BOUND";
		go.AddComponent<BoxCollider>().size = new Vector3(1f, __init.mapHeight, 1f);
		go.transform.position = new Vector3(-__init.mapWidth / 2f, 0, 0);

		//	SOUTH
		go = new GameObject();
		go.name = "SOUTH_BOUND";
		go.AddComponent<BoxCollider>().size = new Vector3(__init.mapWidth, 1f, 1f);
		go.transform.position = new Vector3(0, -__init.mapHeight / 2f, 0);

		__map = new bool[__init.mapWidth, __init.mapHeight];
	}
}
