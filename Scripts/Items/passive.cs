using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class passive : MonoBehaviour	{
		
	public bool isGrabbed = false;
	public GameObject playerVar;
	public int clearTime = 10;
	public Vector3 dir;
	public int grabMul = 200;
		
	public string weaponName = "";
	public float damage = 0;
	public float cost = 0;
	public float speed = 0;
	public int weaponTypeID = 21;
	public int rarity = 1;
	public int projectiles = 1;
	public float size = 1.0f;
	public float grabberAdd = 0;
	public int multiplierAdd = 0;
	public string proTextures = "passive";
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
	public Color bombColor;
	public bool crafted = false;
	public bool variablesAssigned = true;
		
	IEnumerator Start () {
		if(Menu.craftingItems)
			crafted = true;
		else
			crafted = false;
		GetVariables ();
		playerVar = ShipControlCS.thisShip;
		yield return new WaitForSeconds (clearTime);
		if(!isGrabbed)
			transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, 100);
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
    		transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, -100);
   		}
	}
	
	public void GetVariables() {
		// Initialize values
		projectiles = 0;
		itemLevel = 1;
		weaponTypeID = 51;
		int maxLevelSize = Menu.level / 5;
		maxLevelSize += Menu.currentLevel;
		int finalLevelSize = Random.Range (maxLevelSize - 4, maxLevelSize + 4);
		if(finalLevelSize < 1)
			finalLevelSize = 1;
		if(crafted)
			finalLevelSize = Menu.level;
		itemLevel = finalLevelSize;
		
		int rarityRoll = Random.Range(0,1000);
		if(rarityRoll <= 700) { // Magic, 1-2 affixes
			rarity = 2;
			weaponName = "Level " + itemLevel + " Passive";
			int affixNumber = Random.Range (1,10);
			affix1 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			if(affixNumber >= 7)
				affix2 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else if(rarityRoll <= 990) { // Rare, 3-4 affixes
			rarity = 3;
			weaponName = "Level " + itemLevel + " Passive (Rare)";
			affix1 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix2 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix3 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			int affixNumber = Random.Range (1,10);
			if(affixNumber >= 7)
				affix4 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else { 						// Legendary, 5-6 affixes
			rarity = 4;
			size += 1;
			projectiles += 1;
			damage += 1;
			weaponName = "Level " + itemLevel + " Passive (Legendary)";
			affix1 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix2 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix3 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix4 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix5 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			
			int affixNumber = Random.Range (1,10);
			if(affixNumber >= 7)
				affix6 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
												
		if(rarity == 1)
			light.color = Color.white;
		else if(rarity == 2)
			light.color = Color.green;
		else if(rarity == 3)
			light.color = Color.blue;
		else if(rarity == 4)
			light.color = Color.magenta;
		
		cost = 100 / speed;
		cost *= 10 / size;
		cost += multiplierAdd;
		cost += grabberAdd * 10;
		variablesAssigned = true;
	}
	
	public void Update () {
		if(Menu.newGame)
  			Destroy(gameObject);
		if(Menu.craftingItems && crafted && variablesAssigned) {
			Menu.AddItemStore(weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			Destroy (gameObject);
		}		
	}
}

