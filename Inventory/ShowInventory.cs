using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class ShowInventory : MonoBehaviour {
	
	// reference to our database manager object in the scene
	public SimpleSQL.SimpleSQLManager manager;
	
	// The different GUI styles for the boxes
	public GUIStyle invBox;
	public GUIStyle menuBox;
	public GUIStyle invTextCenter;
	public GUIStyle invTextCenterGreen;
	public GUIStyle invTextCenterBlue;
	public GUIStyle invTextCenterMagenta;
	public GUIStyle invTextLeft;
	public GUIStyle invBoxSelected;
	public GUIStyle invBoxPicked;
	public GUIStyle invBoxSelectedPicked;
	public GUIStyle projectBox;
	public GUIStyle AButton;
	public GUIStyle XButton;
	public GUIStyle borderLine;
	public GUIStyle backgroundBox;
	
	// reference to the gui text object in our scene that will be used for output
	public GUIText outputText;
	public Vector2 scrollPosition = Vector2.zero;
	public Vector2 scrollPosition2 = Vector2.zero;
	public int leftLength = 0;
	public int totalCount = 0;
	public int totalCount2 = 0;
	public int countMultiplier = 0;
	public int countMultiplier2 = 0;
	public int currentPick = 0;	
	public int currentSelection = 1;
	public int currentSelectionRight = 1;
	public int heldSelection = 1;
	bool canMove = true;
	bool onLeft = true;
	bool pickLeft = true;
	public Texture2D showTexture;
	
	public int pickedItemType = 0;
	
	public bool[] isThere;
	public int lastNum = 0;
	
	void OnGUI () {
		GUI.Button(new Rect(30, 30, Screen.width - 60, Screen.height - 60), "", backgroundBox);
		// Menu button
		if(GUI.Button(new Rect(40, 40, 100, 25), "Menu", menuBox)) {
			Menu.showMenu = true;
			Menu.showMenuCheck = true;
			Menu.showInventory = false;
			Destroy(gameObject);
		}
		GUI.Button(new Rect(170, 40, 100, 25), "Select", AButton);
		GUI.Button(new Rect(280, 40, 100, 25), "Sell", XButton);
		
		// Get the Money from the Player Information database
		string sqlMoney = "SELECT * FROM PlayerInformation5" + Menu.currentPlayerName + "";
		List<PlayerInformation5> playerInfo = manager.Query<PlayerInformation5>(sqlMoney);
		
		GUI.TextField(new Rect(Screen.width / 4, 75, 0, 140), "Equipped", invTextCenter);
		foreach (PlayerInformation5 player in playerInfo)
			GUI.TextField(new Rect((Screen.width - 60) / 2, Screen.height - 80, 0, 140), "Money: " + player.Money.ToString ("n1"), invTextCenter);
		GUI.TextField(new Rect((Screen.width / 4) * 3, 75, 0, 140), "Inventory", invTextCenter);
		GUI.Button(new Rect(30, 96, Screen.width - 60, 3), "", borderLine);
		// Get and display the item in the Left Trigger Slot database
		string sql2 = "SELECT * FROM LeftTriggerSlot" + Menu.currentPlayerName + "";
		List<LeftTriggerSlot> leftSlot = manager.Query<LeftTriggerSlot>(sql2);
		
		scrollPosition = GUI.BeginScrollView(new Rect(30, 100, (Screen.width - 60) / 2, Screen.height - 200), scrollPosition, new Rect(0, 0, 0, countMultiplier), false, false);
		scrollPosition = new Vector2(0, 0);
		if(totalCount == 0)
			countMultiplier = 0;
		int left = 0;
		int top = 0;
		int spaceHolder = 0;
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Left Trigger Slot", invTextLeft);

		foreach (LeftTriggerSlot inv in leftSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 1)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 1 && onLeft && pickLeft && currentPick == 1) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 10, 140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 1 || !onLeft) && currentPick == 1 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 1 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Damage: " + inv.Damage.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Speed: " + inv.Speed.ToString( "n2" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Projectiles: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Projectile Size: " + inv.Size.ToString( "n2" ), invTextLeft);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 115,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 255,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
			
			if(1 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
		}
		
		// Get and display the item in the Right Trigger Slot database
		string sqlR = "SELECT * FROM RightTriggerSlot" + Menu.currentPlayerName + "";
		List<RightTriggerSlot> rightSlot = manager.Query<RightTriggerSlot>(sqlR);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Right Trigger Slot", invTextLeft);
		
		foreach (RightTriggerSlot inv in rightSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 2)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 2 && onLeft && pickLeft && currentPick == 2) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 2 || !onLeft) && currentPick == 2 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 2 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Damage: " + inv.Damage.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Speed: " + inv.Speed.ToString( "n2" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Projectiles: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Projectile Size: " + inv.Size.ToString( "n2" ), invTextLeft);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 115,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 255,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(2 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
		}
		if(Menu.level >= 5) {
		
		Color shieldColor;
				
		string sqlShield = "SELECT * FROM ShieldSlot" + Menu.currentPlayerName + "";
		List<ShieldSlot> shieldSlot = manager.Query<ShieldSlot>(sqlShield);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Shield Slot", invTextLeft);

		foreach (ShieldSlot inv in shieldSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 3)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 3 && onLeft && pickLeft && currentPick == 3) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
				
			}
			else if((currentSelection != 3 || !onLeft) && currentPick == 3 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 3 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			if(inv.Projectiles >= 88)
				shieldColor = Color.white;
			else if(inv.Projectiles >= 77)
				shieldColor = Color.cyan;
			else if(inv.Projectiles >= 66)
				shieldColor = Color.green;
			else if(inv.Projectiles >= 55)
				shieldColor = Color.magenta;
			else if(inv.Projectiles >= 44)
				shieldColor = Color.red;
			else if(inv.Projectiles >= 33)
				shieldColor = Color.yellow;
			else if(inv.Projectiles >= 22)
				shieldColor = new Color(1f,.5f,0f,1f);
			else if(inv.Projectiles >= 11)
				shieldColor = Color.gray;
			else
				shieldColor = Color.blue;
				
		//	GUI.color = shieldColor;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Shield Size: " + inv.Size.ToString( "n2" ), invTextLeft);
	//		GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Defense Rating: " + inv.Damage.ToString( "n2" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Defense Rating: " + inv.Speed.ToString( "n2" ), invTextLeft);
			GUI.color = shieldColor;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Color", invTextLeft);
			GUI.color = Color.white;
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 255,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(3 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
			
		}}
		
		if(Menu.level >= 10) {
		Color bombColor;
		
		
		
		string sqlBomb = "SELECT * FROM BombSlot" + Menu.currentPlayerName + "";
		List<BombSlot> bombSlot = manager.Query<BombSlot>(sqlBomb);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Bomb Slot", invTextLeft);

		foreach (BombSlot inv in bombSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 4)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 4 && onLeft && pickLeft && currentPick == 4) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 4 || !onLeft) && currentPick == 4 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 4 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;	
			if(inv.Speed >= 88)
				bombColor = Color.white;
			else if(inv.Speed >= 77)
				bombColor = Color.cyan;
			else if(inv.Speed >= 66)
				bombColor = Color.green;
			else if(inv.Speed >= 55)
				bombColor = Color.magenta;
			else if(inv.Speed >= 44)
				bombColor = Color.red;
			else if(inv.Speed >= 33)
				bombColor = Color.yellow;
			else if(inv.Speed >= 22)
				bombColor = new Color(1f,.5f,0f,1f);
			else if(inv.Speed >= 11)
				bombColor = Color.gray;
			else
				bombColor = Color.blue;
			
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Bomb Radius: " + inv.Size.ToString( "n1" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Total Bombs: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
			GUI.color = bombColor;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Color", invTextLeft);
			GUI.color = Color.white;
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 135,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(4 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
			
		}}
		
		if(Menu.level >= 15) {
		// Get and display the item in the Booster Slot database
		string sqlBoost = "SELECT * FROM BoosterSlot" + Menu.currentPlayerName + "";
		List<BoosterSlot> boosterSlot = manager.Query<BoosterSlot>(sqlBoost);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Booster Slot", invTextLeft);

		foreach (BoosterSlot inv in boosterSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 5)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 5 && onLeft && pickLeft && currentPick == 5) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 5 || !onLeft) && currentPick == 5 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 5 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Speed Boost: " + inv.Speed.ToString( "n2" ), invTextLeft);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 95,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 115,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 135,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(5 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
			
			
	//	}
		}}
		
		if(Menu.level >= 20) {
		string sqlCooldown = "SELECT * FROM CooldownSlot" + Menu.currentPlayerName + "";
		List<CooldownSlot> cooldownSlot = manager.Query<CooldownSlot>(sqlCooldown);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Cooldown Slot", invTextLeft);

		foreach (CooldownSlot inv in cooldownSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 6)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 6 && onLeft && pickLeft && currentPick == 6) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 6 || !onLeft) && currentPick == 6 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 6 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Cooldown Duration: " + inv.Speed.ToString( "n2" ), invTextLeft);
			string cooldownEffect = "";
			if(inv.WeaponTypeID == 41)
				cooldownEffect = "Stops all enemies for " + inv.Size.ToString ("n2") + " seconds.";
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Effect: " + cooldownEffect, invTextLeft);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 115,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 135,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(6 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
			
		}}
		
		if(Menu.level >= 25) {
		
		string sqlBL = "SELECT * FROM LeftBumperSlot" + Menu.currentPlayerName + "";
		List<LeftBumperSlot> leftBumperSlot = manager.Query<LeftBumperSlot>(sqlBL);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Left Bumper Slot", invTextLeft);

		foreach (LeftBumperSlot inv in leftBumperSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 7)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 7 && onLeft && pickLeft && currentPick == 7) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 7 || !onLeft) && currentPick == 7 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 7 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Damage: " + inv.Damage.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Speed: " + inv.Speed.ToString( "n2" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Projectiles: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Projectile Size: " + inv.Size.ToString( "n2" ), invTextLeft);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 115,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 255,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(7 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
		}}
		
		if(Menu.level >= 30) {		
		string sqlBR = "SELECT * FROM RightBumperSlot" + Menu.currentPlayerName + "";
		List<RightBumperSlot> rightBumperSlot = manager.Query<RightBumperSlot>(sqlBR);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Right Bumper Slot", invTextLeft);

		foreach (RightBumperSlot inv in rightBumperSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 8)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 8 && onLeft && pickLeft && currentPick == 8) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 8 || !onLeft) && currentPick == 8 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 8 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Damage: " + inv.Damage.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Speed: " + inv.Speed.ToString( "n2" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Projectiles: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Projectile Size: " + inv.Size.ToString( "n2" ), invTextLeft);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 115,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 255,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(8 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
		}}
		
		if(Menu.level >= 35) {
		string sqlFPS = "SELECT * FROM FirstPassiveSlot" + Menu.currentPlayerName + "";
		List<FirstPassiveSlot> firstPassiveSlot = manager.Query<FirstPassiveSlot>(sqlFPS);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "First Passive Slot", invTextLeft);

		foreach (FirstPassiveSlot inv in firstPassiveSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 9)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 9 && onLeft && pickLeft && currentPick == 9) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 9 || !onLeft) && currentPick == 9 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 9 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 55,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 75,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 95,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 115,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 135,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(9 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
	//	}
		}}
		
		if(Menu.level >= 40) {
		string sqlSPS = "SELECT * FROM SecondPassiveSlot" + Menu.currentPlayerName + "";
		List<SecondPassiveSlot> secondPassiveSlot = manager.Query<SecondPassiveSlot>(sqlSPS);
		
		GUI.TextField(new Rect(left + 10,top + 10,(Screen.width - 60) / 2 - 20,140), "Second Passive Slot", invTextLeft);

		foreach (SecondPassiveSlot inv in secondPassiveSlot)
		{
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;
			
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if(currentSelection == 10)
				scrollPosition = new Vector2(left, top-200);
			if( currentSelection == 10 && onLeft && pickLeft && currentPick == 10) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelectedPicked);
			}
			else if((currentSelection != 10 || !onLeft) && currentPick == 10 && pickLeft)
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxPicked);
			else if( currentSelection == 10 && onLeft) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBoxSelected);
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,140 + affixes + 30), "", invBox);
			top += 20;
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,140), inv.WeaponName, invTextCenterMagenta);
			float buyPrice = inv.Cost / 5;
			GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 55,(Screen.width - 60) / 2 - 20,140), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 75,(Screen.width - 60) / 2 - 20,140), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 95,(Screen.width - 60) / 2 - 20,140), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 115,(Screen.width - 60) / 2 - 20,140), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 135,(Screen.width - 60) / 2 - 20,140), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,140), inv.Affix6, invTextCenter);
		
			if(10 > totalCount) {
				countMultiplier += 170;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
			
			
	//	}
		}}
		
        GUI.EndScrollView();
		
		scrollPosition2 = GUI.BeginScrollView(new Rect((Screen.width - 60) / 2 + 30, 100, (Screen.width - 60) / 2, Screen.height - 200), scrollPosition2, new Rect(0, 0, 0, countMultiplier2), false, false);
		left = 0;
		top = 0;
		if(totalCount2 == 0)
			countMultiplier2 = 0;
		// Get and display the entire Inventory on the right side of the screen
		string sql = "SELECT * FROM Inv6" + Menu.currentPlayerName + "";
		List<Inv6> inventory = manager.Query<Inv6>(sql);
		int count = 0;
		foreach (Inv6 inv in inventory)
		{
			count++;
			// affixes is used to increase the size of the box to carry extra affixes
			int affixes = 0;
			if(inv.Affix1 != null && inv.Affix1 != "")
				affixes += 40;
			if(inv.Affix2 != null && inv.Affix2 != "")
				affixes += 20;
			if(inv.Affix3 != null && inv.Affix3 != "")
				affixes += 20;
			if(inv.Affix4 != null && inv.Affix4 != "")
				affixes += 20;
			if(inv.Affix5 != null && inv.Affix5 != "")
				affixes += 20;
			if(inv.Affix6 != null && inv.Affix6 != "")
				affixes += 20;		
			if( currentSelectionRight == 1 && count == 1)
				scrollPosition2 = new Vector2(left, top);
			else if( currentSelectionRight == inv.WeaponID)
				scrollPosition2 = new Vector2(left, top-200);
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if( currentSelectionRight == inv.WeaponID && !onLeft && !pickLeft && currentPick == inv.WeaponID ) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBoxSelectedPicked);
		//		Vector2 newPosition = new Vector2(left, top);
				
				pickedItemType = inv.WeaponTypeID;
			}
			else if( ( currentSelectionRight != inv.WeaponID || onLeft ) && currentPick == inv.WeaponID && !pickLeft ) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBoxPicked);
				pickedItemType = inv.WeaponTypeID;
			}
			else if( currentSelectionRight == inv.WeaponID && !onLeft ) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBoxSelected);	
		//		Vector2 newPosition = new Vector2(left, top);
		//		scrollPosition2 = new Vector2(left, top-200);
				pickedItemType = inv.WeaponTypeID;
			}
			else
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBox);
			
			showTexture = Resources.Load ("" + inv.ProTextures + "Display") as Texture2D;
			projectBox.normal.background = showTexture;
			GUI.Button(new Rect(left + 20 ,top + 35,((Screen.width - 60) / 2 - 10) / 10,((Screen.width - 60) / 2 - 10) / 10),"", projectBox);
			
			if(inv.Rarity == 1)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,120), inv.WeaponName, invTextCenter);
			else if(inv.Rarity == 2)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,120), inv.WeaponName, invTextCenterGreen);
			else if(inv.Rarity == 3)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,120), inv.WeaponName, invTextCenterBlue);
			else if(inv.Rarity == 4)
				GUI.TextField(new Rect(left + 5,top + 5, (Screen.width - 60) / 2 - 20,120), inv.WeaponName, invTextCenterMagenta);
			if(inv.WeaponTypeID >= 1 && inv.WeaponTypeID < 11) {
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,120), "Damage: " + inv.Damage.ToString( "n0" ), invTextLeft);
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,120), "Speed: " + inv.Speed.ToString( "n2" ), invTextLeft);
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,120), "Projectiles: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,120), "Projectile Size: " + inv.Size.ToString( "n2" ), invTextLeft);
				float buyPrice = inv.Cost / 5;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 115,(Screen.width - 60) / 2 - 20,120), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			}
			else if(inv.WeaponTypeID >= 31 && inv.WeaponTypeID < 41) {
				Color shieldColor;
				if(inv.Projectiles >= 88)
					shieldColor = Color.white;
				else if(inv.Projectiles >= 77)
					shieldColor = Color.cyan;
				else if(inv.Projectiles >= 66)
					shieldColor = Color.green;
				else if(inv.Projectiles >= 55)
					shieldColor = Color.magenta;
				else if(inv.Projectiles >= 44)
					shieldColor = Color.red;
				else if(inv.Projectiles >= 33)
					shieldColor = Color.yellow;
				else if(inv.Projectiles >= 22)
					shieldColor = new Color(1f,.5f,0f,1f);
				else if(inv.Projectiles >= 11)
					shieldColor = Color.gray;
				else
					shieldColor = Color.blue;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Shield Size: " + inv.Size.ToString( "n2" ), invTextLeft);
//				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Defense Rating: " + inv.Damage.ToString( "n2" ), invTextLeft);
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Defense Rating: " + inv.Speed.ToString( "n2" ), invTextLeft);
				GUI.color = shieldColor;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Color", invTextLeft);
				GUI.color = Color.white;
				float buyPrice = inv.Cost / 5;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			}
			else if(inv.WeaponTypeID >= 21 && inv.WeaponTypeID < 31) {
				Color bombColor;
				if(inv.Speed >= 88)
					bombColor = Color.white;
				else if(inv.Speed >= 77)
					bombColor = Color.cyan;
				else if(inv.Speed >= 66)
					bombColor = Color.green;
				else if(inv.Speed >= 55)
					bombColor = Color.magenta;
				else if(inv.Speed >= 44)
					bombColor = Color.red;
				else if(inv.Speed >= 33)
					bombColor = Color.yellow;
				else if(inv.Speed >= 22)
					bombColor = new Color(1f,.5f,0f,1f);
				else if(inv.Speed >= 11)
					bombColor = Color.gray;
				else
					bombColor = Color.blue;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Bomb Radius: " + inv.Size.ToString( "n1" ), invTextLeft);
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Total Bombs: " + inv.Projectiles.ToString( "n0" ), invTextLeft);
				GUI.color = bombColor;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Color", invTextLeft);
				GUI.color = Color.white;
				float buyPrice = inv.Cost / 5;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 95,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			}
			else if(inv.WeaponTypeID >= 11 && inv.WeaponTypeID < 21) {
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Speed Boost: " + inv.Speed.ToString( "n2" ), invTextLeft);
				float buyPrice = inv.Cost / 5;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			}
			else if(inv.WeaponTypeID >= 41 && inv.WeaponTypeID < 51) {
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 35,(Screen.width - 60) / 2 - 20,140), "Cooldown Duration: " + inv.Speed.ToString( "n2" ), invTextLeft);
				string cooldownEffect = "";
				if(inv.WeaponTypeID == 41)
					cooldownEffect = "Stops all enemies for " + inv.Size.ToString ("n2") + " seconds.";
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Effect: " + cooldownEffect, invTextLeft);
				float buyPrice = inv.Cost / 5;
				GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 75,(Screen.width - 60) / 2 - 20,140), "Sell Price: " + buyPrice.ToString( "n0" ), invTextLeft);
			}
			else if(inv.WeaponTypeID >= 51) {
				
			}
			
			if(inv.Affix1 != null)
				GUI.TextField(new Rect(left + 5,top + 155,(Screen.width - 60) / 2 - 20,120), inv.Affix1, invTextCenter);
			if(inv.Affix2 != null)
				GUI.TextField(new Rect(left + 5,top + 175,(Screen.width - 60) / 2 - 20,120), inv.Affix2, invTextCenter);
			if(inv.Affix3 != null)
				GUI.TextField(new Rect(left + 5,top + 195,(Screen.width - 60) / 2 - 20,120), inv.Affix3, invTextCenter);
			if(inv.Affix4 != null)
				GUI.TextField(new Rect(left + 5,top + 215,(Screen.width - 60) / 2 - 20,120), inv.Affix4, invTextCenter);
			if(inv.Affix5 != null)
				GUI.TextField(new Rect(left + 5,top + 235,(Screen.width - 60) / 2 - 20,120), inv.Affix5, invTextCenter);
			if(inv.Affix6 != null)
				GUI.TextField(new Rect(left + 5,top + 255,(Screen.width - 60) / 2 - 20,120), inv.Affix6, invTextCenter);
			
			// Used for debugging. Shows the ID of the item
		//	GUI.TextField(new Rect(left + 100,top + 115,(Screen.width - 60) / 2 - 20,120), "" + inv.WeaponID, invTextCenter);
			
			if(count > totalCount2) {
				countMultiplier2 += 150;
				totalCount2 += 1;
				countMultiplier2 += affixes;
			}
			top += 150 + affixes;
			lastNum = inv.WeaponID;
			isThere[inv.WeaponID] = true;
		}
		
		
        GUI.EndScrollView();
		GUI.Button(new Rect(30, Screen.height - 98, Screen.width - 60, 3), "", borderLine);
	}
	
	// Use this for initialization; must be IEnumerator to use yield commands
	IEnumerator Start () {	
		isThere = new bool[50000];
		for(int i = 0; i < 50000; i++)
			isThere[i] = false;
		int slotsUnlocked = 2;
		for(int i = 5; i <= 40; i += 5)
			if(Menu.level >= i)
				slotsUnlocked++; 

		yield return new WaitForSeconds(.1f);
		currentPick = 0;
		currentSelection = 1;
		currentSelectionRight = lastNum;
		
		// Checks for movement on the left joystick and navigates to the next selection
		for( ; ; ) {
			
			if(canMove) {		
				string sqlInv = "SELECT * FROM Inv6" + Menu.currentPlayerName + "";
				List<Inv6> inventory = manager.Query<Inv6>(sqlInv);
				
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5 && onLeft) {
					canMove = false;
					if(currentSelection >= slotsUnlocked)
						currentSelection = 1;
					else
						currentSelection++;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5 && onLeft) {
					canMove = false;
					if(currentSelection == 1)
						currentSelection = slotsUnlocked;
					else
						currentSelection--;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5 && !onLeft) {
					canMove = false;
					if(currentSelectionRight < lastNum) {
						currentSelectionRight++;
						while(!isThere[currentSelectionRight] && currentSelectionRight < lastNum)
							currentSelectionRight++;
					}
					else {
						currentSelectionRight = 1;
						while(!isThere[currentSelectionRight] && currentSelectionRight < lastNum)
							currentSelectionRight++;
					}
					yield return new WaitForSeconds(.1f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5 && !onLeft) {
					canMove = false;
					if(currentSelectionRight > 1) {
						currentSelectionRight--;
						while(!isThere[currentSelectionRight] && currentSelectionRight > 1)
							currentSelectionRight--;
						if(currentSelectionRight == 1 && isThere[1] == false)
							currentSelectionRight = lastNum;
					}
					else {
						currentSelectionRight = lastNum;
						while(!isThere[currentSelectionRight] && currentSelectionRight > 1)
							currentSelectionRight--;
					}
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
				if(Input.GetAxisRaw ("L_XAxis_1") > .5 && onLeft) {
					canMove = false;
					onLeft = false;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
				if(Input.GetAxisRaw ("L_XAxis_1") < -.5 && !onLeft) {
					canMove = false;
					onLeft = true;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
				// These commands swap items from one database to another, using a temporary holding database
				if(Input.GetButton("A_1") && onLeft) {
					canMove = false;
					if(currentPick == 0) {
						currentPick = currentSelection;
						pickLeft = true;
					}
					else if( currentPick == currentSelection && pickLeft)
						currentPick = 0;
					else if( currentSelection == 1 && pickLeft && currentPick == 2){
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"LeftTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"RightTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if((currentSelection == 1 && pickLeft && currentPick == 7) || (currentSelection == 7 && pickLeft && currentPick == 1)){
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"LeftTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"LeftBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if((currentSelection == 1 && pickLeft && currentPick == 8) || (currentSelection == 8 && pickLeft && currentPick == 1)){
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"LeftTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightBumperSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"RightBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if((currentSelection == 2 && pickLeft && currentPick == 7) || (currentSelection == 7 && pickLeft && currentPick == 2)){
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"RightTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"LeftBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if((currentSelection == 2 && pickLeft && currentPick == 8) || (currentSelection == 8 && pickLeft && currentPick == 2)){
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"RightTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightBumperSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"RightBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if((currentSelection == 7 && pickLeft && currentPick == 8) || (currentSelection == 8 && pickLeft && currentPick == 7)){
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"LeftBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightBumperSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"RightBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if(currentSelection == 2 && pickLeft && currentPick == 1) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql1);
						string slqD1 = "DELETE FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD1);
						string sql2 = "REPLACE INTO \"LeftTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\"";
						manager.Execute(sql2);
						string slqD2 = "DELETE FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);
						string sql3 = "REPLACE INTO \"RightTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\"";
						manager.Execute(sql3);
						string slqD3 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD3);
												
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
					}
					else if(currentSelection == 1 && !pickLeft && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"LeftTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentPick;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentPick;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 2 && !pickLeft && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"RightTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if((currentSelection == 7 && !pickLeft) && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"LeftBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if((currentSelection == 8 && !pickLeft) && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"RightBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 5 && !pickLeft && pickedItemType == 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"BoosterSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"BoosterSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"BoosterSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 4 && !pickLeft && pickedItemType == 21) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"BombSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"BombSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"BombSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 3 && !pickLeft && pickedItemType > 30 && pickedItemType < 40) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"ShieldSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"ShieldSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"ShieldSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 6 && !pickLeft && pickedItemType > 40 && pickedItemType < 50) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"CooldownSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"CooldownSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"CooldownSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 9 && !pickLeft && pickedItemType > 50) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"FirstPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"FirstPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"FirstPassiveSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentSelection == 10 && !pickLeft && pickedItemType > 50) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"SecondPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"SecondPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentPick);						
						string sql2 = "REPLACE INTO \"SecondPassiveSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					pickLeft = true;					
					yield return new WaitForSeconds(.2f);
					canMove = true;
					
				}
					
				if(Input.GetButton("A_1") && !onLeft) {
					canMove = false;
					if(currentPick == 1 && pickLeft && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"LeftTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"LeftTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 2 && pickLeft && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"RightTriggerSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"RightTriggerSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" + currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 7 && pickLeft && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"LeftBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"LeftBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 8 && pickLeft && pickedItemType < 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"RightBumperSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"RightBumperSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 5 && pickLeft && pickedItemType == 11) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"BoosterSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"BoosterSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"BoosterSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 4 && pickLeft && pickedItemType == 21) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"BombSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"BombSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"BombSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 3 && pickLeft && pickedItemType > 30 && pickedItemType < 40) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"ShieldSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"ShieldSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"ShieldSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 6 && pickLeft && pickedItemType > 40 && pickedItemType < 50) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"CooldownSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"CooldownSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"CooldownSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 9 && pickLeft && pickedItemType > 50) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"FirstPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"FirstPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"FirstPassiveSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 10 && pickLeft && pickedItemType > 50) {
						
						// Begin a database transaction
						manager.BeginTransaction();
											
						// copy the data to backup table
						string sql1 = "REPLACE INTO \"HolderDatabase" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"SecondPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql1);						
						string slqD2 = "DELETE FROM \"SecondPassiveSlot" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD2);						
						string sqlS1 = "UPDATE HolderDatabase" + Menu.currentPlayerName + " " +
							"SET WeaponID = ?";
						manager.Execute (sqlS1, currentSelectionRight);						
						string sql2 = "REPLACE INTO \"SecondPassiveSlot" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
							"WHERE WeaponID = " +
							currentSelectionRight;
						manager.Execute(sql2);						
						string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute (slqD3);						
						string sql3 = "REPLACE INTO \"Inv6" + Menu.currentPlayerName + "\" " +
							"SELECT * " + "" + 
							"FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute(sql3);												
						string slqD4 = "DELETE FROM \"HolderDatabase" + Menu.currentPlayerName + "\" ";
						manager.Execute (slqD4);
						
						// commit the transaction and run all the commands
						manager.Commit();
						currentPick = 0;
						totalCount2 = 0;
						totalCount = 0;
					}
					else if(currentPick == 0 || currentPick != currentSelectionRight) {
						pickLeft = false;
						currentPick = currentSelectionRight;
					}
					else
						currentPick = 0;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
			}	
			// This allows the player to sell items from the inventory using 'X'
			if(Input.GetButton("X_1") && !onLeft) {
				canMove = false;
				string inventorySQL = "SELECT * FROM Inv6" + Menu.currentPlayerName + "";
				List<Inv6> invSelect = manager.Query<Inv6>(inventorySQL);
			
				string sqlMoney = "SELECT * FROM PlayerInformation5" + Menu.currentPlayerName + "";
				List<PlayerInformation5> playerInfo = manager.Query<PlayerInformation5>(sqlMoney);
			
				bool invSwitch = false;
				int countInt = 0;
			
				foreach(Inv6 inv in invSelect) {
					countInt++;					
				/*	if(invSwitch) {
						string sqlReorder = "UPDATE Inv6 " +
							"SET WeaponID=" + (inv.WeaponID - 1) +
							" WHERE WeaponID =" + countInt;
						manager.Execute (sqlReorder);
					} */
					
					if(inv.WeaponID == currentSelectionRight && !invSwitch) {
						string sqlAddMoney = "UPDATE PlayerInformation5" + Menu.currentPlayerName + " " +
							"SET Money=Money+" + inv.Cost / 5;
						manager.Execute(sqlAddMoney);
				
						string slqDelete = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
							"WHERE WeaponID =" +
							currentSelectionRight;
						manager.Execute(slqDelete);
						totalCount2 = 0;
						currentPick = 0;
						invSwitch = true;
						isThere[inv.WeaponID] = false;
					}
				}
				yield return new WaitForSeconds(.2f);
				canMove = true;
				if(currentSelectionRight < lastNum) {
					currentSelectionRight++;
					while(!isThere[currentSelectionRight] && currentSelectionRight < lastNum)
							currentSelectionRight++;
				}
				else {
					currentSelectionRight = 1;
					while(!isThere[currentSelectionRight] && currentSelectionRight < lastNum)
							currentSelectionRight++;
				}
			}
			// Simply deselct if the item is currently selected
			if(Input.GetButton("B_1")) {
				canMove = false;
				if(currentPick == 0) {
					Menu.showMenu = true;
					Menu.showInventory = false;
					Destroy(gameObject);
				}
				else {
					currentPick = 0;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
			}
			yield return new WaitForSeconds(.01f); // Without a wait command, multiple actions will carry out with each click
		}
	}	
}
