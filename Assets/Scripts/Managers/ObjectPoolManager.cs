using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalObjectPoolManager : Singleton<GlobalObjectPoolManager>
{
	//	TODO
	//	Finish

	public Vector3                                          position			= new Vector3(0, 0, -50f);
	public Dictionary<GameObject, List<GameObject>>			objects				= new Dictionary<GameObject, List<GameObject>>();

	public GameObject GetGameObject(GameObject pooled)
	{	
		List<GameObject> outList;

		if(objects.TryGetValue(pooled, out outList))
		{
			GameObject go;
			for(int i = 0; i < outList.Count; i++)
			{
				if(!outList[i].activeInHierarchy)
					return outList[i];
			}

			go = Instantiate(pooled) as GameObject;

			return go;
		}
		else return __CreateKeyListPair(pooled);
	}

	public void AddGameObject(GameObject go)
	{
		List<GameObject> list;
		go.SetActive(false);

		if(objects.TryGetValue(go, out list))
			list.Add(go);
		else
		{
			__CreateKeyListPair(go);
		}
	}

	private GameObject __CreateKeyListPair(GameObject go)
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(go);
		objects.Add(go, list);

		return go;
	}
}
