using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class IntroRedLine : MonoBehaviour	{
	public SimpleSQL.SimpleSQLManager manager;
		
	public bool isGrabbed = false;
	public GameObject playerVar;
	public int clearTime = 10;
	public Vector3 dir;
	public int grabMul = 200;
		
	public string weaponName = "";
	public float damage = 0;
	public float cost = 0;
	public float speed = 0;
	public int weaponTypeID = 1;
	public int rarity = 1;
	public int projectiles = 0;
	public float size = 1.0f;
	public float grabberAdd = 0;
	public int multiplierAdd = 0;
	public string proTextures = "redline";
	public string affix1 = null;
	public string affix2 = null;
	public string affix3 = null;
	public string affix4 = null;
	public string affix5 = null;
	public string affix6 = null;
	
	public int rearProjectiles = 0;
	public int explosive = 0;
	public int fragmenting = 0;
	public int ricochet = 0;
	public float chaotic = 0f;
	
	public bool hasSuffix = false;
	public bool hasPrefix = false;
	
	public int itemLevel = 1;
		
	void Start () {
		GetVariables ();
		playerVar = GameObject.FindWithTag("PlayerShip");
	//	yield return new WaitForSeconds (clearTime);
	//	if(!isGrabbed)
	//		transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, 100);
	}
	
	public void OnTriggerEnter(Collider collision) {	
		if(collision.gameObject.tag == "LootGrabber") {
		isGrabbed = true;
    	dir = transform.position - playerVar.transform.position; // calculate the target direction...
    	transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, -200);
    	}
		if(collision.gameObject.tag == "KillZone") {
			if(Menu.autoSellBelow > itemLevel)
				Menu.AddCost(cost);
			else
				Menu.AddItem(weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
    		Menu.currentItemsFound += 1;
			Destroy(gameObject);
		}
		if(collision.gameObject.tag == "DropOutZone")
    		Destroy(gameObject);
	}
	
	public void OnTriggerStay(Collider other) {
		if(other.gameObject.tag == "LootGrabber") {
			dir = playerVar.transform.position - transform.position; // calculate the target direction...
    		transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, -200);
   		}
	}
	
	public void GetVariables() {
		// Initialize values
		damage = 2;
		speed = 1.2f;
		projectiles = 2;
		size = 1.2f;
		weaponName = "Free Laser";		
		rarity = 1;
		light.color = Color.white;
		
		cost += damage;
		cost += speed;
		int temp = multiplierAdd / 10;
		cost += temp;
		cost += grabberAdd;
		cost *= speed;
		cost *= projectiles;
		cost *= size;
		
	}

	
	public void Update () {
		if(Menu.playerDeath)
  			Destroy(gameObject);	
	}
}

