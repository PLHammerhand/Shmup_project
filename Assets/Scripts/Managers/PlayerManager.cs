using UnityEngine;
using System.Collections;

public class PlayerManager<T> : Singleton<T> where T : PlayerManager<T>
{
	[HideInInspector]
	public Player               player;


	protected virtual void Awake()
	{
		player = GameObject.FindObjectOfType<Player>();
		//		TODO - DELETE CODE IN LATER VERSIONS
		//		VERSION 1 GAME - MOSTLY DEBUG
		GUIManager.Instance.Initialize();
		LevelMaster.Instance.Initialize();
		//		VERSION 1 GAME - MOSTLY DEBUG
	}
}
