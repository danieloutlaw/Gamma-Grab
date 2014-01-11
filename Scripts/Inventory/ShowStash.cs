using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;

public class ShowStash : MonoBehaviour {
	
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
	public bool[] isThereS;
	public int lastNum = 0;
	public int lastNumS = 0;
	
	void OnGUI () {
		GUI.Button(new Rect(30, 30, Screen.width - 60, Screen.height - 60), "", backgroundBox);
		// Menu button
		if(GUI.Button(new Rect(40, 40, 100, 25), "Menu", menuBox)) {
			Menu.showMenu = true;
			Menu.showMenuCheck = true;
			Menu.showStash = false;
			Destroy(gameObject);
		}
		GUI.Button(new Rect(170, 40, 100, 25), " Swap", AButton);
		GUI.Button(new Rect(280, 40, 100, 25), "Sell", XButton);
		
		// Get the Money from the Player Information database
		string sqlMoney = "SELECT * FROM PlayerInformation5" + Menu.currentPlayerName + "";
		List<PlayerInformation5> playerInfo = manager.Query<PlayerInformation5>(sqlMoney);
		
		GUI.TextField(new Rect(Screen.width / 4, 75, 0, 140), "Stash", invTextCenter);
		foreach (PlayerInformation5 player in playerInfo)
			GUI.TextField(new Rect((Screen.width - 60) / 2, Screen.height - 80, 0, 140), "Money: " + player.Money.ToString ("n1"), invTextCenter);
		GUI.TextField(new Rect((Screen.width / 4) * 3, 75, 0, 140), "Inventory", invTextCenter);
		GUI.Button(new Rect(30, 96, Screen.width - 60, 3), "", borderLine);
		// Get and display the item in the Left Trigger Slot database
		
		scrollPosition = GUI.BeginScrollView(new Rect(30, 100, (Screen.width - 60) / 2, Screen.height - 200), scrollPosition, new Rect(0, 0, 0, countMultiplier), false, false);
		scrollPosition = new Vector2(0, 0);
		int left = 0;
		int top = 0;
		if(totalCount == 0)
			countMultiplier = 0;
		// Get and display the entire Inventory on the right side of the screen
		string sqlStash = "SELECT * FROM Stash" + Menu.currentPlayerName + "";
		List<Stash> stash = manager.Query<Stash>(sqlStash);
		int countS = 0;
		foreach (Stash inv in stash)
		{
			countS++;
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
			if( currentSelection == 1 && countS == 1)
				scrollPosition = new Vector2(left, top);
			else if( currentSelection == inv.WeaponID)
				scrollPosition = new Vector2(left, top - 200);
			// Draw the box first, then the picture, then print the stats, followed by the affixes
			if( currentSelection == inv.WeaponID && onLeft && pickLeft && currentPick == inv.WeaponID ) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBoxSelectedPicked);
		//		Vector2 newPosition = new Vector2(left, top);
				
				pickedItemType = inv.WeaponTypeID;
			}
			else if( ( currentSelection != inv.WeaponID || !onLeft ) && currentPick == inv.WeaponID && pickLeft ) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBoxPicked);
				pickedItemType = inv.WeaponTypeID;
			}
			else if( currentSelection == inv.WeaponID && onLeft ) {
				GUI.Button(new Rect(left,top,(Screen.width - 60) / 2 - 20,120 + affixes + 30), "", invBoxSelected);	
		//		Vector2 newPosition = new Vector2(left, top);
		//		scrollPosition = new Vector2(left, top-200);
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
		//		GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Defense Rating: " + inv.Damage.ToString( "n2" ), invTextLeft);
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
			
			if(countS > totalCount) {
				countMultiplier += 150;
				totalCount += 1;
				countMultiplier += affixes;
			}
			top += 150 + affixes;
			lastNumS = inv.WeaponID;
			isThereS[inv.WeaponID] = true;
		}
		
        GUI.EndScrollView();
		
		scrollPosition2 = GUI.BeginScrollView(new Rect((Screen.width - 60) / 2 + 30, 100, (Screen.width - 60) / 2, Screen.height - 200), scrollPosition2, new Rect(0, 0, 0, countMultiplier2), false, false);
		scrollPosition2 = new Vector2(0, 0);
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
				scrollPosition2 = new Vector2(left, top-200);
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
				scrollPosition2 = new Vector2(left, top-200);
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
		//		GUI.TextField(new Rect(left + 40 + ((Screen.width - 60) / 2 - 10) / 7,top + 55,(Screen.width - 60) / 2 - 20,140), "Defense Rating: " + inv.Damage.ToString( "n2" ), invTextLeft);
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
		isThereS = new bool[50000];
		for(int i = 0; i < 50000; i++)
			isThere[i] = false;
		for(int i = 0; i < 50000; i++)
			isThereS[i] = false;


		yield return new WaitForSeconds(.1f);
		currentPick = 0;
		currentSelection = lastNumS;
		currentSelectionRight = lastNum;
		int highest = 0;
		// Checks for movement on the left joystick and navigates to the next selection
		for( ; ; ) {
			Debug.Log ("lastNum = " + lastNum + ", lastNumS = " + lastNumS);
			if(canMove) {
				if(lastNum > lastNumS)
					highest = lastNum;
				else
					highest = lastNumS;
				if(!isThere[currentSelectionRight])
					currentSelectionRight = lastNum;
				if(!isThereS[currentSelection])
					currentSelection = lastNumS;
				
				//string sqlInv = "SELECT * FROM Inv6" + Menu.currentPlayerName + "";
				//List<Inv6> inventory = manager.Query<Inv6>(sqlInv);
				
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5 && onLeft) {
					canMove = false;
					if(currentSelection < lastNumS) {
						currentSelection++;
						while(!isThereS[currentSelection] && currentSelection < lastNumS)
							currentSelection++;
					}
					else {
						currentSelection = 1;
						while(!isThereS[currentSelection] && currentSelection < lastNumS)
							currentSelection++;
					}
					yield return new WaitForSeconds(.2f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5 && onLeft) {
					canMove = false;
					if(currentSelection > 1) {
						currentSelection--;
						while(!isThereS[currentSelection] && currentSelection > 1)
							currentSelection--;
						if(currentSelection == 1 && isThereS[1] == false)
							currentSelection = lastNumS;
					}
					else {
						currentSelection = lastNumS;
						while(!isThereS[currentSelection] && currentSelection > 1)
							currentSelection--;
					}
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
					yield return new WaitForSeconds(.2f);
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
					// Begin a database transaction
					manager.BeginTransaction();
											
					// copy the data to backup table
					string sql1 = "UPDATE Stash" + Menu.currentPlayerName +
						" SET WeaponID=" + (highest + 1) +
						" WHERE WeaponID=" + currentSelection;
					manager.Execute(sql1);
					string sql2 = "INSERT INTO \"Inv6" + Menu.currentPlayerName + "\" " +
						"SELECT * " + "" + 
						"FROM \"Stash" + Menu.currentPlayerName + "\"" +
						" WHERE WeaponID = " +
						(highest + 1);
					manager.Execute(sql2);						
					string slqD3 = "DELETE FROM \"Stash" + Menu.currentPlayerName + "\" " +
						"WHERE WeaponID =" +
						(highest + 1);
					manager.Execute (slqD3);	
					// commit the transaction and run all the commands
					manager.Commit();
					currentSelectionRight = currentSelection;
					isThereS[currentSelection] = false;
					currentSelection = highest + 1;
					totalCount2 = 0;
					totalCount = 0;
					yield return new WaitForSeconds(.2f);
					canMove = true;
				}
				if(Input.GetButton("A_1") && !onLeft) {
					canMove = false;
					// Begin a database transaction
					manager.BeginTransaction();
											
					// copy the data to backup table
					string sql1 = "UPDATE Inv6" + Menu.currentPlayerName +
						" SET WeaponID=" + (highest + 1) +
						" WHERE WeaponID=" + currentSelectionRight;
					manager.Execute(sql1);
					string sql2 = "INSERT INTO \"Stash" + Menu.currentPlayerName + "\" " +
						"SELECT * " + "" + 
						"FROM \"Inv6" + Menu.currentPlayerName + "\"" +
						" WHERE WeaponID=" +
						(highest + 1);
					manager.Execute(sql2);						
					string slqD3 = "DELETE FROM \"Inv6" + Menu.currentPlayerName + "\" " +
						"WHERE WeaponID =" +
						(highest + 1);
					manager.Execute (slqD3);
						
					// commit the transaction and run all the commands
					manager.Commit();
					currentSelection = currentSelectionRight;
					isThere[currentSelectionRight] = false;
					currentSelectionRight = highest + 1;
					totalCount2 = 0;
					totalCount = 0;
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
							"WHERE WeaponID=" +
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
					Menu.showStash = false;
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
