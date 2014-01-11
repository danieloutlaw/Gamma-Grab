using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class YellowTriangle : MonoBehaviour	{
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
	public string proTextures = "yellowtriangle";
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
	public bool crafted = false;
	public bool variablesAssigned = false;
		
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
	
	public void Update () {
		if(Menu.newGame)
  			Destroy(gameObject);
		if(Menu.craftingItems && crafted && variablesAssigned) {
			Menu.AddItemStore(weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			Destroy (gameObject);
		}
	}
		
	public void OnTriggerEnter(Collider collision) {	
		if(collision.gameObject.tag == "LootGrabber" && playerVar != null) {
		isGrabbed = true;
    	dir = transform.position - playerVar.transform.position; // calculate the target direction...
    	transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, -200 ); 
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
			dir = transform.position - playerVar.transform.position; // calculate the target direction...
    		transform.rigidbody.velocity = new Vector3 (dir.x, dir.y, -200 );
   		} 
	}
	
	public void GetVariables() {
		// Initialize values
		damage = 1;
		speed = Random.Range (1.0f, 1.5f);
		projectiles = Random.Range (2,5);
		size = Random.Range(1.0f, 1.5f);
		int maxLevelSize = Menu.level / 5;
		maxLevelSize += Menu.currentLevel;
		int finalLevelSize = Random.Range (maxLevelSize - 4, maxLevelSize + 4);
		if(finalLevelSize < 1)
			finalLevelSize = 1;
		if(crafted)
			finalLevelSize = Menu.level;
		
		// Upgrade the weapon depending on the current score.
		for(int i = 1; i < finalLevelSize; i++) {
			itemLevel++;
			if(i % 2 == 0) {
				int upgradeRoll1 = Random.Range (1,32);
				if(upgradeRoll1 > 30 && damage < (itemLevel / 9))
					damage += 1;
				else if(upgradeRoll1 > 20)
					speed += Random.Range(0.1f, 0.12f);
				else if(upgradeRoll1 > 10 && projectiles <= (itemLevel / 6) + 3)
					projectiles += 1;
				else
					size += Random.Range (0.1f, 0.12f);
			}
		}
		
		int rarityRoll = Random.Range(0,1000);
		if(rarityRoll <= 800) {		 // Normal, no affixes
			rarity = 1;
			weaponName = "Level " + itemLevel + " Arrow";
		}
		else if(rarityRoll <= 960) { // Magic, 1-2 affixes
			rarity = 2;
			weaponName = "Level " + itemLevel + " Arrow";
			int affixNumber = Random.Range (1,10);
			affix1 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			if(affixNumber >= 7)
				affix2 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else if(rarityRoll <= 996) { // Rare, 3-4 affixes
			rarity = 3;
			weaponName = "Level " + itemLevel + " Arrow (Rare)";
			affix1 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix2 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			affix3 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			int affixNumber = Random.Range (1,10);
			if(affixNumber >= 7)
				affix4 = AssignAffixes.ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else { 						// Legendary, 5-6 affixes
			rarity = 4;
			damage += 1;
			speed += 1;
			projectiles += 1;
			size += 1;
			weaponName = "Level " + itemLevel + " Arrow (Legendary)";
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
		light.intensity *= rarity;
												
		cost += damage;
		cost += speed;
		cost *= speed;
		cost *= projectiles;
		cost *= size;
		int temp = multiplierAdd / 10;
		cost += temp;
		cost += grabberAdd;
		variablesAssigned = true;
	}
/*	
	public string GetAffix() {
		string defineAffix = "";
		int selectionRoll1 = 0;
		if(hasSuffix)
			selectionRoll1 = Random.Range (1,499);
		else if(hasPrefix)
			selectionRoll1 = Random.Range (500,1000);
		else
			selectionRoll1 = Random.Range (1,1000);
		if(selectionRoll1 >= 900) {
			int damageMult = Random.Range (10,20);
			if(damageMult > 10)
				damageMult = 2;
			else
				damageMult = 1;
			defineAffix = "Damage increased by " + damageMult.ToString( "n0" ) + ".";
			damage += damageMult;
			if(rarity == 2) {
				weaponName += " of Power";
				hasSuffix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 800) {
			float speedMult = Random.Range (1.1f,1.5f);
			defineAffix = "Speed multiplied by " + speedMult.ToString( "n2" ) + ".";
			speed *= speedMult;
			if(rarity == 2) {
				weaponName += " of Speed";
				hasSuffix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 700) {
			float sizeMult = Random.Range (1.1f,1.5f);
			defineAffix = "Size multiplied by " + sizeMult.ToString( "n2" ) + ".";
			size *= sizeMult;
			if(rarity == 2) {
				weaponName += " of Size";
				hasSuffix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 600) {
			int projectilesMult = Random.Range (1,2);
			defineAffix = "Projectiles increased by " + projectilesMult.ToString( "n0" ) + ".";
			projectiles += projectilesMult;
			if(rarity == 2) {
				weaponName += " of Quantity";
				hasSuffix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 600) {
			float grabberAddMult = Random.Range (1,4);
			defineAffix = "Loot grabber radius increased by " + grabberAddMult.ToString( "n1" ) + ".";
			grabberAdd += grabberAddMult;
			if(rarity == 2) {
				weaponName += " of Grabbing";
				hasSuffix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 500) {
			int multiplierAddMult = Random.Range (10,20);
			defineAffix = "Starting multiplier increased by " + multiplierAddMult.ToString( "n0" ) + ".";
			multiplierAdd += multiplierAddMult;
			if(rarity == 2) {
				weaponName += " of Added Bonus";
				hasSuffix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 400) {
			int damageMult = Random.Range (10,20);
			if(damageMult > 15)
				damageMult = 2;
			else
				damageMult = 1;
			defineAffix = "Damage increased by " + damageMult.ToString( "n0" ) + ".";
			damage += damageMult;
			if(rarity == 2) {
				weaponName = "Powerful " + weaponName;
				hasPrefix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 300) {
			float speedMult = Random.Range (1.2f,2.0f);
			defineAffix = "Speed multiplied by " + speedMult.ToString( "n2" ) + ".";
			speed *= speedMult;
			if(rarity == 2) {
				weaponName = "Quick " + weaponName;
				hasPrefix = true;
			}
			return defineAffix;
		}
		else if(selectionRoll1 >= 200) {
			float sizeMult = Random.Range (1.1f,1.5f);
			defineAffix = "Size multiplied by " + sizeMult.ToString( "n2" ) + ".";
			size *= sizeMult;
			if(rarity == 2) {
				weaponName = "Huge " + weaponName;
				hasPrefix = true;
			}
			return defineAffix;
		}
		else {
			int multiplierAddMult = Random.Range (15,30);
			defineAffix = "Starting multiplier increased by " + multiplierAddMult.ToString( "n0" ) + ".";
			multiplierAdd += multiplierAddMult;
			if(rarity == 2) {
				weaponName = "Champion's " + weaponName;
				hasPrefix = true;
			}
			return defineAffix;
		}		
	}
	
	public void Update () {
		if(Menu.playerDeath)
  			Destroy(gameObject);	
	} */
}
	 
