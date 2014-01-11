/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class PlayerDeath : MonoBehaviour {
	
//	public SimpleSQL.SimpleSQLManager manager;
	public static bool playerDeath = false;
	
	static public void PlayerDied () {
		Debug.Log("Player died");
		playerDeath = true;
		
		/*
		foreach(GameObject obj in entityList) {
			if(obj.gameObject.tag != "AlwaysKeep")
				Destroy(obj);
				*/
	//	manager.Commit();
	//	}
	
		
		
/*	var killSpawner : GameObject = GameObject.FindWithTag("Spawner");
	while( killSpawner != null)
	{
		Destroy( killSpawner.gameObject );
		killSpawner = GameObject.FindWithTag("Spawner");
	}
	var killDrops : GameObject = GameObject.FindWithTag("Drop");
	while( killDrops != null)
	{
		Destroy( killDrops.gameObject );
		killDrops = GameObject.FindWithTag("Drop");
	}
	var killEnemies : GameObject = GameObject.FindWithTag("Seeker");
	while( killEnemies != null)
	{
		Destroy( killEnemies.gameObject );
		killEnemies = GameObject.FindWithTag("Seeker");
	}
	
	var killPlayer : GameObject = GameObject.FindWithTag("PlayerVar");
	Destroy( killPlayer.gameObject ); */
	
//	Menu.showMenu = true;
//	manager.Commit();
//}