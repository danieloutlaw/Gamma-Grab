using UnityEngine;
using System.Collections;
using SimpleSQL;

public class SecondPassiveSlot
{
	// The WeaponID field is set as the primary key in the SQLite database,
	// so we reflect that here with the PrimaryKey attribute
	[PrimaryKey]
	public int WeaponID { get; set; }
	
	public string WeaponName { get; set; }
	
	public float Damage { get; set; }
	
	public float Cost { get; set; }
	
	public float Speed { get; set; }	
	
	public int WeaponTypeID { get; set; }
	
	public int Rarity { get; set; }
	
	public int Projectiles { get; set; }
	
	public float Size { get; set; }
	
	public int MultiplierAdd { get; set; }
	
	public float GrabberAdd { get; set; }
	
	public string ProTextures { get; set; }
	
	public string Affix1 { get; set; }
	
	public string Affix2 { get; set; }
	
	public string Affix3 { get; set; }
	
	public string Affix4 { get; set; }
	
	public string Affix5 { get; set; }
	
	public string Affix6 { get; set; }
	
	public int RearProjectiles { get; set; }
	
	public int Explosive { get; set; }
	
	public int Fragmenting { get; set; }
	
	public int Ricochet { get; set; }
	
	public float Chaotic { get; set; }
}