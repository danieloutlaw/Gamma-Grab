using UnityEngine;
using System.Collections;
using SimpleSQL;

public class PlayerInformation
{
	// The WeaponID field is set as the primary key in the SQLite database,
	// so we reflect that here with the PrimaryKey attribute
	[PrimaryKey, AutoIncrement]
	public int PlayerID { get; set; }
	
	public string PlayerName { get; set; }
	
	public int TotalScore { get; set; }
	
	public int HighScore { get; set; }
	
	public int StartingMultiplier { get; set; }
	
	public float StartingRadius { get; set; }
	
	public int Money {get; set; }
}