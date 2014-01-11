using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class AssignAffixes : MonoBehaviour	{
	public SimpleSQL.SimpleSQLManager manager;
		
	public bool isGrabbed = false;
	public GameObject playerVar;
	public int clearTime = 10;
	public Vector3 dir;
	public int grabMul = 200;
		
	public static string weaponName = "";
	public float damage = 0;
	public float cost = 0;
	public float speed = 0;
	public int weaponTypeID = 1;
	public int rarity = 1;
	public int projectiles = 0;
	public float size = 1.0f;
	public float grabberAdd = 0;
	public int multiplierAdd = 0;
	public static string proTextures = "";
	public static string affix1 = null;
	public static string affix2 = null;
	public static string affix3 = null;
	public static string affix4 = null;
	public static string affix5 = null;
	public static string affix6 = null;
	
	public int rearProjectiles = 0;
	public int explosive = 0;
	public int fragmenting = 0;
	public int ricochet = 0;
	public float chaotic = 0f;
	
	public bool hasSuffix = false;
	public bool hasPrefix = false;
	
	public int itemLevel = 1;
	
	// Select the affix (this is the function that most scripts should call
	public static string ChooseAffix(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
		int affixRoll = Random.Range (0, 10000);
		if(weaponTypeID < 10) {
			if(affixRoll <= 600)
				return Chaotic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 1200)
				return Rapid(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 2200)
				return Large(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 2400)
				return Explosive(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 2600)
				return Fragmenting(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 3600)
				return Deadly(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 4200)
				return Magnetic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 4800)
				return Scorekeeper(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 5400)
				return Multi(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 6000)
				return Lethal(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 6700)
				return Achievement(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
	//		else if(affixRoll <= 7300)
	//			return Annihilation(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 7900)
				return Piercing(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 8700)
				return Ricochet(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 9100)
				return Unstable(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else
				return RearProjectile(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else if(weaponTypeID > 30 && weaponTypeID < 41) {
			if(affixRoll <= 1250)
				return Scorekeeper(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 2500)
				return Magnetic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 3750)
				return Achievement(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 5000)
				return Recharging(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 6250)
				return Siphoning(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 8000)
				return Hungering(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else
				return Empowering(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else if(weaponTypeID < 31 && weaponTypeID > 20) {
			if(affixRoll <= 2500)
				return Scorekeeper(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 5000)
				return Magnetic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 7500)
				return Achievement(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else
				return Empowering(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else if(weaponTypeID < 51 && weaponTypeID > 40) {
			if(affixRoll <= 2500)
				return Scorekeeper(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 5000)
				return Magnetic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 7500)
				return Achievement(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else if(weaponTypeID < 21 && weaponTypeID > 10) {
			if(affixRoll <= 2500)
				return Scorekeeper(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 5000)
				return Magnetic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 7500)
				return Achievement(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else
				return Empowering(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		else {
			if(affixRoll <= 2000)
				return Scorekeeper(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 4000)
				return Magnetic(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else if(affixRoll <= 7000)
				return Achievement(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
			else
				return Empowering(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		}
		return "error";
	}
	
	// ***** WeaponAffixes *****
	public static string Chaotic(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Projectile bounces randomly upon impact with an enemy.
		if(chaotic > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		chaotic = Random.Range (2f,3f);
		chaotic *= itemLevel;
		if(chaotic > 100)
			chaotic = 100f;
		string defineAffix = "Weapon has a " + chaotic.ToString( "n0" ) + "% chance to bounce off enemies.";
		return defineAffix;
	}
	
	public static string Rapid(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased rate of fire.
		float speedIncrease = Random.Range (0.1f,0.2f);
		float temp = itemLevel / 5;
		speedIncrease *= temp;
		if(speedIncrease < .3f)
			speedIncrease = .3f;
		speed += speedIncrease;
		string defineAffix = "Speed increased by " + speedIncrease.ToString ("n2") + ".";
		return defineAffix;
	}
	
	public static string Large(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Larger projectiles.
		float sizeIncrease = Random.Range (0.1f,0.2f);
		float temp = itemLevel / 5;
		sizeIncrease *= temp;
		if(sizeIncrease < .3f)
			sizeIncrease = .3f;
		size += sizeIncrease;
		string defineAffix = "Size increased by " + sizeIncrease.ToString ("n2") + ".";
		return defineAffix;
	}
	
	public static string Explosive(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Projectile has a mini explosion( bomb effect) upon impact.
		if(explosive > 0 || proTextures == "repeater")
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float explodeRoll = Random.Range (.5f,.75f);
		explodeRoll *= itemLevel;
		explosive = (int)explodeRoll;
		if(explosive < 1)
			explosive = 1;
		if(explosive > 100)
			explosive = 100;
		string defineAffix = "Projectiles have a " + explosive + "% chance to detonate on impact.";
		return defineAffix;
	}
	
	public static string Fragmenting(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Projectile breaks into a set of smaller versions upon impact.
		if(fragmenting > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float fragmentRoll = Random.Range (0f, 10f);
		if(fragmentRoll > 5)
			fragmenting = 2;
		else
			fragmenting = 1;
		int temp = itemLevel / 10;
		if(temp > 1)
			fragmenting *= temp;
		string defineAffix = "";
		if(fragmenting == 1)
			defineAffix = "Projectiles break into " + fragmenting.ToString ("n0") + " piece on impact.";
		else
			defineAffix = "Projectiles break into " + fragmenting.ToString ("n0") + " pieces on impact.";
		return defineAffix;
	}
	
	public static string Deadly(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased damage and size.
		float sizeIncrease = Random.Range (0.05f,0.1f);
		float temp = itemLevel / 5;
		sizeIncrease *= temp;
		if(sizeIncrease < .2f)
			sizeIncrease = .2f;
		size += sizeIncrease;
		float damageIncrease = Random.Range (.2f,.4f);
		temp = itemLevel / 10;
		damageIncrease *= temp;
		if(damageIncrease > 1)
			damage += damageIncrease;
		else {
			damage += 1;
			damageIncrease = 1;
		}
		string defineAffix = "Size increased by " + sizeIncrease.ToString ("n2") + " and damage increase by " + damageIncrease.ToString ("n0") + ".";
		return defineAffix;
	}
	
	public static string Magnetic(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased grab radius.
		float grabIncrease = Random.Range (.1f,.2f);
		float temp = itemLevel / 2 + 1;
		grabIncrease *= temp;
		if(grabIncrease < .3f)
			grabIncrease = .3f;
		grabberAdd += grabIncrease;
		string defineAffix = "Grab radius increased by " + grabIncrease.ToString ("n1") + ".";
		return defineAffix;	
	}
	
	public static string Scorekeeper(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased multiplier.
		int multIncrease = Random.Range (10,20);
		int temp = itemLevel / 2 + 1;
		multIncrease *= temp;
		multiplierAdd += multIncrease;
		string defineAffix = "Multiplier increased by " + multIncrease.ToString ("n0") + ".";
		return defineAffix;	
	}
	
	public static string Multi(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Multiple projectiles.
		float projRoll = Random.Range (1f,2f);
		int projectilesIncrease = 0;
		if(projRoll >= 1.5f)
			projectilesIncrease = 2;
		else
			projectilesIncrease = 1;
		int temp = itemLevel / 15;
		projectilesIncrease *= temp;
		if(projectilesIncrease < 1 || proTextures == "dozer" || proTextures == "boomerang" || proTextures == "repeater")
			projectilesIncrease = 1;
		projectiles += projectilesIncrease;
		string defineAffix = "Projectiles increased by " + projectilesIncrease.ToString ("n0") + ".";
		return defineAffix;
	}
	
	public static string Lethal(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased damage and rate.
		float speedIncrease = Random.Range (0.05f,0.1f);
		float temp = itemLevel / 5 + 1;
		speedIncrease *= temp;
		if(speedIncrease < .2f)
			speedIncrease = .2f;
		speed += speedIncrease;
		float damageIncrease = Random.Range (.1f,.2f);
		temp = itemLevel / 10;
		damageIncrease *= temp;
		if(damageIncrease > 1)
			damage += damageIncrease;
		else {
			damage += 1;
			damageIncrease = 1;
		}
		string defineAffix = "Speed increased by " + speedIncrease.ToString ("n2") + " and damage increase by " + damageIncrease.ToString ("n0") + ".";
		return defineAffix;
		
	}
	
	public static string Achievement(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased grab radius and multiplier.
		int multIncrease = Random.Range (7,15);
		float grabIncrease = Random.Range (.1f,.15f);
		float temp = itemLevel / 2 + 1;
		int temp2 = itemLevel / 2 + 1;
		multIncrease *= temp2;
		grabIncrease *= temp;
		if(grabIncrease < .2f)
			grabIncrease = .2f;
		multiplierAdd += multIncrease;
		grabberAdd += grabIncrease;
		string defineAffix = "Multiplier increased by " + multIncrease.ToString ("n0") + ", and grab radius increase by " + grabIncrease.ToString ("n1") + ".";
		return defineAffix;	
		
	}

	public static string Annihilation(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Fragmenting and Explosive.
		if(explosive > 0 || fragmenting > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float fragmentRoll = Random.Range (0f, 8f);
		if(fragmentRoll > 5)
			fragmenting = 3;
		else
			fragmenting = 2;
		int temp = itemLevel / 10;
		if(temp > 1)
			fragmenting *= temp;
		float explodeRoll = Random.Range (.25f,.5f);
		explodeRoll *= itemLevel;
		explosive = (int)explodeRoll;
		if(explosive < 1)
			explosive = 1;
		if(explosive > 100)
			explosive = 100;
		string defineAffix = "Projectiles break into " + fragmenting.ToString ("n0") + " pieces and have a " + explosive + "% chance to explode.";
		return defineAffix;
	}
	
	public static string Piercing(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increased damage.
		float damageIncrease = Random.Range (1f,2f);
		float temp = itemLevel / 10;
		damageIncrease *= temp;
		if(damageIncrease > 1)
			damage += damageIncrease;
		else {
			damage += 1;
			damageIncrease += 1;
		}
		string defineAffix = "Damage increased by " + damageIncrease.ToString ("n0") + ".";
		return defineAffix;
	}
	
	public static string Ricochet(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Projectiles can bounce off walls.
		if(ricochet > 0 || proTextures == "boomerang")
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float ricochetRoll = Random.Range (1f,1.5f);
		ricochetRoll *= itemLevel;
		ricochet = (int)ricochetRoll;
		if(ricochet > 100)
			ricochet = 100;
		string defineAffix = "Projectiles have a " + ricochet + "% chance to bounce off walls.";
		return defineAffix;
	}
	
	public static string Unstable(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Ricochet and Chaotic.
		if(ricochet > 0 || chaotic > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		chaotic = Random.Range (1f,1.5f);
		chaotic *= itemLevel;
		if(chaotic > 100)
			chaotic = 100f;
		ricochet = (int)chaotic;
		string defineAffix = "Weapon has a " + chaotic.ToString( "n0" ) + "% chance to bounce off enemies and walls.";
		return defineAffix;
	} 
	
	public static string RearProjectile(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Fires extra projectiles out the back.
		if(rearProjectiles > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float rearProjectilesRoll = Random.Range(0f,10f);
		int addProjectiles = 1;
		if(rearProjectilesRoll > 7)
			addProjectiles++;
		int temp = itemLevel / 10;
		if(temp > 1)
			addProjectiles *= temp;
		if(proTextures == "dozer" || proTextures == "boomerang" || proTextures == "repeater")
			addProjectiles = 1;
		rearProjectiles += addProjectiles;
		string defineAffix = "Weapon shoots " + addProjectiles.ToString( "n0" ) + " projectiles out the back.";
		return defineAffix;
	}
	
	
	//***** Shield Affixes *****
	public static string Recharging(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Slowly recharges shield percent.
		if(proTextures == "intercepting")
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float rechargeRate = Random.Range (.1f, .5f);
		float temp = itemLevel / 5;
		rechargeRate *= temp;
		chaotic = rechargeRate;
		string defineAffix = "Shield regenerates at the rate of " + rechargeRate.ToString ("n2") + "% per second.";
		return defineAffix;
	}
	
	public static string Siphoning(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Shield recharges as you pick up multipliers.
		if(proTextures == "intercepting")
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float chargePercent = Random.Range (.2f,.4f);
		float temp = itemLevel / 10;
		chargePercent *= temp;
		string defineAffix = "Each multiplier recharges your shield by " + chargePercent.ToString("n4") +"%.";
		chargePercent *= 1000;
		fragmenting += (int)chargePercent;
		return defineAffix;
	}
	
	public static string Hungering(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Shield recharges as you kill enemies.
		if(proTextures == "intercepting")
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float chargePercent = Random.Range (.4f,.8f);
		float temp = itemLevel / 10;
		chargePercent *= temp;
		string defineAffix = "Killing enemies recharges your shield by " + chargePercent.ToString("n4") +"%.";
		chargePercent *= 1000;
		explosive += (int)chargePercent;
		return defineAffix;
	}
	
	public static string Hard(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Shield lasts much longer, but you cannot fire while active.
		if(proTextures == "intercepting")
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		float hard = Random.Range (10f,30f);
		hard *= itemLevel;
		hard += 100;
		string defineAffix = "Shield lasts " + hard.ToString("n1") +"% as long but deactivates weapons.";
		hard *= .01f;
		speed *= hard;
		rearProjectiles = 1;
		return defineAffix;
	}
	
	public static string Empowering(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increases damage caused by all weapons to increase by 1.
		if(damage > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		damage = 1;
		string defineAffix = "Damage increased by 1 for all weapons.";
		return defineAffix;
	}
	
	// *****Other Affixes*****
	public static string Arming(ref string weaponName, ref float damage, ref float cost, ref float speed, ref int weaponTypeID, ref int rarity, ref int projectiles, ref float size, ref float grabberAdd, ref int multiplierAdd, ref string proTextures, ref int rearProjectiles, ref int explosive, ref int fragmenting, ref int ricochet, ref float chaotic, ref int itemLevel) {
	// Increases damage caused by all weapons to increase by 1.
		if(damage > 0)
			return ChooseAffix(ref weaponName, ref damage, ref cost, ref speed, ref weaponTypeID, ref rarity, ref projectiles, ref size, ref grabberAdd, ref multiplierAdd, ref proTextures, ref rearProjectiles, ref explosive, ref fragmenting, ref ricochet, ref chaotic, ref itemLevel);
		projectiles = 1;
		string defineAffix = "Projectiles increased by 1 for all weapons.";
		return defineAffix;
	}
	
}

