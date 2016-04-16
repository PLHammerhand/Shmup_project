using UnityEngine;
using System.Collections;

public class Initializator : MonoBehaviour
{
	//		Map variables
	[Range(200,2000)]
	public int				mapHeight					= 200;
	[Range(200,2000)]
	public int				mapWidth					= 200;
	public int				maxEnemiesOnMap;
	public int              maxBonusesOnMap;


	void Start()
	{
		//	TODO:	Generate map
		//MapGeneratorManager.Instance.Initialize(this);
		//	TODO:	Loading GUI scene after map is generated
		//	TODO:	Initialize LevelMaster when GUI is ready
		GUIManager.Instance.Initialize();
		LevelMaster.Instance.Initialize();
	}
}
