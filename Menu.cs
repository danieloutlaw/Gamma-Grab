using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SimpleSQL;
using System.IO;

public class Menu : MonoBehaviour {
	
	public SimpleSQL.SimpleSQLManager manager;
	public GameObject introGuy;
	
	public static bool craftingItems = false;
	
	public static string currentPlayerName = "";
	
	// For loading players
	public static bool showLoadPlayer = false;
	public int loadSelection = 0;
	public string playerToLoad = "";
	
	// Menu switches
	public static bool showMenu = false;
	public static bool showHighScores = false;
	public static bool showPlayerStats = false;
	public static bool showMenuCheck = true;
	public static bool showPostGame = false;
	public static bool showOptions = false;
	public static bool showInventory = false;
	public static bool showStash = false;
	public static bool showStore = false;
	public bool canMove = true;
	public static bool isInitialized = false;
	public static bool isIntro = false;
	public static bool isHolding1 = false;
	public static bool isHolding3 = false;
	public static bool endPlayerLife = false;
	
	// Prefabs to load
	public GameObject playerPrefab;
	public GameObject spawnerPrefab;
	public GameObject inventoryPrefab;
	public GameObject stashPrefab;
	public GameObject storePrefab;
	
	// GUI stuff
	public GUIStyle menuText;
	public GUIStyle menuTextSelect;
	public GUIStyle menuBox;
	public GUIStyle textBox;
	public GUIStyle ABox;
	public GUIStyle YBox;
	public int selection = 0;
	public Texture2D expTexture;
	public Texture2D greenExpBar;
	public GUIStyle aButton;
	
	// Switches for what behavior to follow
	public static bool playerDeath = false;
	public static bool startCopy = false;
	public static bool doExecute = false;
	public static bool doExecuteCost = false;
	public static bool newGame = false;
	public static bool resetScore = false;
	public static bool singleGame = false;
	
	// All these variables are extracted from the databases and made static to
	// easily accessed by other scripts
	public static string sql = "";
	public static string weaponName = "";
	public static float damage = 0;
	public static float cost = 0;
	public static float speed = 0;
	public static int weaponTypeID = 1;
	public static int rarity = 1;
	public static int projectiles = 0;
	public static float size = 1.0f;
	public static float grabberAdd = 0;
	public static int multiplierAdd = 0;
	public static string proTextures = "";
	public static string affix1 = null;
	public static string affix2 = null;
	public static string affix3 = null;
	public static string affix4 = null;
	public static string affix5 = null;
	public static string affix6 = null;
	public static int rearProjectiles = 0;
	public static int explosive = 0;
	public static int fragmenting = 0;
	public static int ricochet = 0;
	public static float chaotic = 0f;
	
	public static string weaponNameLeft = "";
	public static float damageLeft = 0;
	public static float costLeft = 0;
	public static float speedLeft = 0;
	public static int weaponTypeIDLeft = 1;
	public static int rarityLeft = 1;
	public static int projectilesLeft = 0;
	public static float sizeLeft = 1.0f;
	public static float grabberAddLeft = 0;
	public static int multiplierAddLeft = 0;
	public static string proTexturesLeft = "";
	public static string affix1Left = null;
	public static string affix2Left = null;
	public static string affix3Left = null;
	public static string affix4Left = null;
	public static string affix5Left = null;
	public static string affix6Left = null;
	public static int rearProjectilesLeft = 0;
	public static int explosiveLeft = 0;
	public static int fragmentingLeft = 0;
	public static int ricochetLeft = 0;
	public static float chaoticLeft = 0f;
	
	
	public static string weaponNameRight = "";
	public static float damageRight = 0;
	public static float costRight = 0;
	public static float speedRight = 0;
	public static int weaponTypeIDRight = 1;
	public static int rarityRight = 1;
	public static int projectilesRight = 0;
	public static float sizeRight = 1.0f;
	public static float grabberAddRight = 0;
	public static int multiplierAddRight = 0;
	public static string proTexturesRight = "";
	public static string affix1Right = null;
	public static string affix2Right = null;
	public static string affix3Right = null;
	public static string affix4Right = null;
	public static string affix5Right = null;
	public static string affix6Right = null;
	public static int rearProjectilesRight = 0;
	public static int explosiveRight = 0;
	public static int fragmentingRight = 0;
	public static int ricochetRight = 0;
	public static float chaoticRight = 0f;
	
	public static string weaponNameLeftBumper = "";
	public static float damageLeftBumper = 0;
	public static float costLeftBumper = 0;
	public static float speedLeftBumper = 0;
	public static int weaponTypeIDLeftBumper = 1;
	public static int rarityLeftBumper = 1;
	public static int projectilesLeftBumper = 0;
	public static float sizeLeftBumper = 1.0f;
	public static float grabberAddLeftBumper = 0;
	public static int multiplierAddLeftBumper = 0;
	public static string proTexturesLeftBumper = "";
	public static string affix1LeftBumper = null;
	public static string affix2LeftBumper = null;
	public static string affix3LeftBumper = null;
	public static string affix4LeftBumper = null;
	public static string affix5LeftBumper = null;
	public static string affix6LeftBumper = null;
	public static int rearProjectilesLeftBumper = 0;
	public static int explosiveLeftBumper = 0;
	public static int fragmentingLeftBumper = 0;
	public static int ricochetLeftBumper = 0;
	public static float chaoticLeftBumper = 0f;
	
	public static string weaponNameRightBumper = "";
	public static float damageRightBumper = 0;
	public static float costRightBumper = 0;
	public static float speedRightBumper = 0;
	public static int weaponTypeIDRightBumper = 1;
	public static int rarityRightBumper = 1;
	public static int projectilesRightBumper = 0;
	public static float sizeRightBumper = 1.0f;
	public static float grabberAddRightBumper = 0;
	public static int multiplierAddRightBumper = 0;
	public static string proTexturesRightBumper = "";
	public static string affix1RightBumper = null;
	public static string affix2RightBumper = null;
	public static string affix3RightBumper = null;
	public static string affix4RightBumper = null;
	public static string affix5RightBumper = null;
	public static string affix6RightBumper = null;
	public static int rearProjectilesRightBumper = 0;
	public static int explosiveRightBumper = 0;
	public static int fragmentingRightBumper = 0;
	public static int ricochetRightBumper = 0;
	public static float chaoticRightBumper = 0f;
	
	public static string nameFirstPassive = "";
	public static float damageFirstPassive = 0;
	public static float costFirstPassive = 0;
	public static float speedFirstPassive = 0;
	public static int weaponTypeIDFirstPassive = 1;
	public static int rarityFirstPassive = 1;
	public static int projectilesFirstPassive = 0;
	public static float sizeFirstPassive = 1.0f;
	public static float grabberAddFirstPassive = 0;
	public static int multiplierAddFirstPassive = 0;
	public static string proTexturesFirstPassive = "";
	public static string affix1FirstPassive = null;
	public static string affix2FirstPassive = null;
	public static string affix3FirstPassive = null;
	public static string affix4FirstPassive = null;
	public static string affix5FirstPassive = null;
	public static string affix6FirstPassive = null;
	public static int rearProjectilesFirstPassive = 0;
	public static int explosiveFirstPassive = 0;
	public static int fragmentingFirstPassive = 0;
	public static int ricochetFirstPassive = 0;
	public static float chaoticFirstPassive = 0f;
	
	public static string nameSecondPassive = "";
	public static float damageSecondPassive = 0;
	public static float costSecondPassive = 0;
	public static float speedSecondPassive = 0;
	public static int weaponTypeIDSecondPassive = 1;
	public static int raritySecondPassive = 1;
	public static int projectilesSecondPassive = 0;
	public static float sizeSecondPassive = 1.0f;
	public static float grabberAddSecondPassive = 0;
	public static int multiplierAddSecondPassive = 0;
	public static string proTexturesSecondPassive = "";
	public static string affix1SecondPassive = null;
	public static string affix2SecondPassive = null;
	public static string affix3SecondPassive = null;
	public static string affix4SecondPassive = null;
	public static string affix5SecondPassive = null;
	public static string affix6SecondPassive = null;
	public static int rearProjectilesSecondPassive = 0;
	public static int explosiveSecondPassive = 0;
	public static int fragmentingSecondPassive = 0;
	public static int ricochetSecondPassive = 0;
	public static float chaoticSecondPassive = 0f;
	
	public static string nameShield = "";
	public static float damageShield = 0;
	public static float costShield = 0;
	public static float speedShield = 0;
	public static int weaponTypeIDShield = 1;
	public static int rarityShield = 1;
	public static int projectilesShield= 0;
	public static float sizeShield = 1.0f;
	public static float grabberAddShield = 0;
	public static int multiplierAddShield = 0;
	public static string proTexturesShield = "";
	public static string affix1Shield = null;
	public static string affix2Shield = null;
	public static string affix3Shield = null;
	public static string affix4Shield = null;
	public static string affix5Shield = null;
	public static string affix6Shield = null;
	public static int rearProjectilesShield = 0;
	public static int explosiveShield = 0;
	public static int fragmentingShield = 0;
	public static int ricochetShield = 0;
	public static float chaoticShield = 0f;
	
	public static string nameCooldown = "";
	public static float damageCooldown = 0;
	public static float costCooldown = 0;
	public static float speedCooldown = 0;
	public static int weaponTypeIDCooldown = 1;
	public static int rarityCooldown = 1;
	public static int projectilesCooldown= 0;
	public static float sizeCooldown = 1.0f;
	public static float grabberAddCooldown = 0;
	public static int multiplierAddCooldown = 0;
	public static string proTexturesCooldown = "";
	public static string affix1Cooldown = null;
	public static string affix2Cooldown = null;
	public static string affix3Cooldown = null;
	public static string affix4Cooldown = null;
	public static string affix5Cooldown = null;
	public static string affix6Cooldown = null;
	public static int rearProjectilesCooldown = 0;
	public static int explosiveCooldown = 0;
	public static int fragmentingCooldown = 0;
	public static int ricochetCooldown = 0;
	public static float chaoticCooldown = 0f;
	
	public static string boosterName = "";
	public static float boosterSpeed = 1.0f;
	public static int boosterMultiplierAdd = 0;
	public static float boosterGrabberAdd = 0;
	public static string boosterProTextures = "";
	public static float boosterCost = 0;
	public static int boosterRarity = 1;
	public static int boosterProjectiles = 0;
	public static string boosterAffix1 = null;
	public static string boosterAffix2 = null;
	public static string boosterAffix3 = null;
	public static string boosterAffix4 = null;
	public static string boosterAffix5 = null;
	public static string boosterAffix6 = null;
	public static int boosterRearProjectiles = 0;
	public static int boosterExplosive = 0;
	public static int boosterFragmenting = 0;
	public static int boosterricochet = 0;
	public static float boosterChaotic = 0f;
	
	public static string bombName = "";
	public static float	bombSpeed = 0;
	public static float	bombSize = 0;
	public static float bombDamage = 0;
	public static int bombMultiplierAdd = 0;
	public static float	bombGrabberAdd = 0;
	public static string bombProTextures = "";
	public static float	bombCost = 0;
	public static int bombProjectiles = 0;
	public static int bombRarity = 0;
	public static string bombAffix1 = null;
	public static string bombAffix2 = null;
	public static string bombAffix3 = null;
	public static string bombAffix4 = null;
	public static string bombAffix5 = null;
	public static string bombAffix6 = null;
	public static int bombRearProjectiles = 0;
	public static int bombExplosive = 0;
	public static int bombFragmenting = 0;
	public static int bombricochet = 0;
	public static float bombChaotic = 0f;
		
	public static string playerName = null;
	public static decimal totalScore = 0;
	public static decimal money = 0;
	public static int startingMultiplier = 0;
	public static float startingRadius = 0f;
	public static decimal highScore1 = 0;
	public static decimal highScore2 = 0;
	public static decimal highScore3 = 0;
	public static decimal highScore4 = 0;
	public static decimal highScore5 = 0;
	public static decimal highScore6 = 0;
	public static decimal highScore7 = 0;
	public static decimal highScore8 = 0;
	public static decimal highScore9 = 0;
	public static decimal highScore10 = 0;
	public static int level = 1;
	public static decimal enemiesKilled = 0;
	
	// Current score taken from the Scorekeeper.js script
	public static decimal currentScore = 0;
	public static int currentEnemiesKilled = 0;
	public static int currentMult = 0;
	public static int currentGrabberRadius = 0;
	public static int currentItemsFound = 0;
	public static int currentLevel = 1;
	public static int currentPermMult = 0;
	public static int currentPermGrab = 0;
	public int previousLevel = 1;
	
	public static int permMult = 0;
	public static float permGrab = 0;
	
	public static bool firstTimePlayer = false;
	
	public string playerInput = "";
	
	public float[] levelRange;
	public static int addedLevel = 0;
	
	public static float playerSpeed = 1.0f;
	
	public static bool isHolding = false;
	
	public static bool timeStopped = false;
	
	public static int backProjectiles = 0;
	
	public string sqlOptions = null;
	public static bool runSQLOptions = false;
	public static int autoSellBelow = 1;
	
	public static bool changeBackground;
	public static int backgroundCount = 1;
	public int optionsSelection = 0;
	
	public GameObject backgroundObject;
	
	public static bool updateCamera = false;
	public int addMultAmount = 0;
	
	public decimal testInt = 0;
	
	public static bool isTutorial = false;
	public GameObject tutorialPrefab;
	
	Vector2 scrollPosition = Vector2.zero;
	float yCheck = 0f;
	public static float difficulty = 2.0f;
	
	public static bool showChooseSingle = false;
	public static int singleSelection = 0;
	public GUIStyle singleEnemyTexture;
	public static bool showPauseGUI = false;
	
	public static bool isMazeGame = false;
	public GameObject mazePrefab;
	
	public bool mazeGame = false;
	public static bool destroyMaze = false;
	public static bool stillMazeGame = false;
	public GameObject nextMazePrefab;
	
	public int totalBackgrounds = 4;
	public int numberOfEnemies = 11;
	
	void OnGUI () {
		// Intro Screen
		if(isIntro) {
			if(GUI.Button(new Rect((Screen.width - 200), 50, 100, 25), "Press ", aButton )) {
				isInitialized = true;
				showMenu = true;
				isIntro = false;
				isHolding1 = true;
				int addSpace = 0;
				for(int i = 0; i <= 200; i++){
					
		//			GameObject introGuy1 = Instantiate(introGuy, new Vector3(100,-500 + addSpace,0), Quaternion.identity) as GameObject;
					GameObject introGuy2 = Instantiate(introGuy, new Vector3(0,-500 + addSpace,0), Quaternion.identity) as GameObject;
		//			GameObject introGuy3 = Instantiate(introGuy, new Vector3(-100,-500 + addSpace,0), Quaternion.identity) as GameObject;
		//			introGuy1.transform.Rotate(0,0,Random.Range (0,360));
					introGuy2.transform.Rotate(0,0,Random.Range (0,360));
		//			introGuy3.transform.Rotate(0,0,Random.Range (0,360));
					if(i % 10 == 0)
						addSpace += 50;
				}
			}
			if(Input.GetButton("A_1")) {
				isInitialized = true;
				showMenu = true;
				isIntro = false;
				isHolding1 = true;
				int addSpace = 0;
				for(int i = 0; i <= 200; i++) {
					
			//		GameObject introGuy1 = Instantiate(introGuy, new Vector3(100,-500 + addSpace,0), Quaternion.identity) as GameObject;
					GameObject introGuy2 = Instantiate(introGuy, new Vector3(0,-500 + addSpace,0), Quaternion.identity) as GameObject;
			//		GameObject introGuy3 = Instantiate(introGuy, new Vector3(-100,-500 + addSpace,0), Quaternion.identity) as GameObject;
			//		introGuy1.transform.Rotate(0,0,Random.Range (0,360));
					introGuy2.transform.Rotate(0,0,Random.Range (0,360));
			//		introGuy3.transform.Rotate(0,0,Random.Range (0,360));
					if(i % 10 == 0)
						addSpace += 50;
					
				}
			}
		}
		if(showPauseGUI) {
			GUI.Button(new Rect(((Screen.width/2) - 150), ((Screen.height/2) - 100), 300, 200), "", textBox );
			GUI.Button(new Rect((Screen.width/2 ), (Screen.height/2) - 30, 0, 50), "Continue Playing", ABox);
			GUI.Button(new Rect((Screen.width/2 ) - 15, (Screen.height/2) - 20, 150, 30), "", ABox);
			if(Input.GetButton("A_1")) {
				Time.timeScale = 1.0f;
				showPauseGUI = false;
			}
			GUI.Button(new Rect((Screen.width/2 ), (Screen.height/2), 0, 50), "Return to Menu", YBox);
			GUI.Button(new Rect((Screen.width/2 ) - 15, (Screen.height/2) + 10, 150, 30), "", YBox);
			if(Input.GetButton("Y_1")) {
				Time.timeScale = 1.0f;
				endPlayerLife = true;
				PlayerDied();
				showPauseGUI = false;
				showMenu = true;
				foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("PlayerShip")) {
					Destroy (obj);
				}
				foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("Seeker")) {
					Destroy (obj);
				}
				foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("Drop")) {
					Destroy (obj);
				}
			}
		}
		// Display the menu
		if(showMenu){
			GUI.Button(new Rect(((Screen.width/2) - 250), 20, 500, 430), "", textBox );
			if(selection == 0 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "Enter New Dimension", menuTextSelect )) {
					singleGame = false;
					isMazeGame = false;
					NewGame ();
				}
				if(Input.GetButton("A_1")) {
					singleGame = false;
					isMazeGame = false;
					NewGame ();
				}
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "Enter New Dimension", menuText )) {
					singleGame = false;
					isMazeGame = false;
					NewGame ();
				}
			if(selection == 1 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "Enter Targeted Dimension", menuTextSelect )) {
					isHolding1 = true;
					isMazeGame = false;
					NewGameSingle ();
				}
				if(Input.GetButton("A_1") && !isTutorial) {
					isHolding1 = true;
					isMazeGame = false;
					NewGameSingle ();
				}
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "Enter Targeted Dimension", menuText )) {
					isMazeGame = false;
					NewGameSingle ();
				}
			if(selection == 2 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), "Enter Maze Dimension", menuTextSelect )) {
					isHolding1 = true;
					NewGameMaze ();
				}
				if(Input.GetButton("A_1") && !isTutorial) {
					isHolding1 = true;
					NewGameMaze ();
				}
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), "Enter Maze Dimension", menuText ))
					NewGameMaze ();
			if(selection == 3 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "Replay Introduction", menuTextSelect ))
					Tutorial ();
				if(Input.GetButton("A_1") && !isTutorial)
					Tutorial ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "Replay Introduction", menuText ))
					Tutorial ();
			if(selection == 4 && !isHolding && !isHolding1) { 
				if(GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Equip Items", menuTextSelect))
					Inventory ();
				if(Input.GetButton("A_1"))
					Inventory ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Equip Items", menuText))
					Inventory ();
			if(selection == 5 && !isHolding && !isHolding1) { 
				if(GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Stash", menuTextSelect))
					Stash ();
				if(Input.GetButton("A_1") && !isTutorial)
					Stash ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Stash", menuText))
					Stash ();
			if(selection == 6 && !isHolding && !isHolding1) { 
				if(GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "Shop", menuTextSelect))
					Shop ();
				if(Input.GetButton("A_1") && !isTutorial)
					Shop ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "Shop", menuText))
					Shop ();
			if(selection == 7 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "High Scores", menuTextSelect))
					HighScores ();
				if(Input.GetButton("A_1") && !isTutorial)
					HighScores ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "High Scores", menuText))
					HighScores ();
			if(selection == 8 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "Player Stats", menuTextSelect))
					PlayerStats ();
				if(Input.GetButton("A_1") && !isTutorial)
					PlayerStats ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "Player Stats", menuText))
					PlayerStats ();
			if(selection == 9 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 300, 0, 50), "Options", menuTextSelect))
					Options ();
				if(Input.GetButton("A_1"))
					Options ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 300, 0, 50), "Options", menuText))
					InitializePlayer ();
			if(selection == 10 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 330, 0, 50), "New Player", menuTextSelect))
					InitializePlayer ();
				if(Input.GetButton("A_1"))
					InitializePlayer ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 330, 0, 50), "New Player", menuText))
					LoadPlayer ();
			if(selection == 11 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 360, 0, 50), "Load Player", menuTextSelect))
					LoadPlayer ();
				if(Input.GetButton("A_1"))
					LoadPlayer ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 360, 0, 50), "Load Player", menuText))
					LoadPlayer ();
			if(selection == 12 && !isHolding && !isHolding1) {
				if(GUI.Button(new Rect((Screen.width/2 ), 390, 0, 50), "Exit", menuTextSelect))
					ExitGame ();
				if(Input.GetButton("A_1"))
					ExitGame ();
			}
			else 
				if(GUI.Button(new Rect((Screen.width/2 ), 390, 0, 50), "Exit", menuText))
					ExitGame ();
		}
		else if(showChooseSingle) {
			UpdatePlayerInformation();
			GUI.Button(new Rect(((Screen.width/2) - 400), 20, 800, 370), "", textBox );
			// Menu button
			if(GUI.Button(new Rect(((Screen.width/2) - 410), 30, 100, 25), "Menu", menuBox)) {
				showMenu = true;
				showChooseSingle = false;
			}
			if(Input.GetButton("B_1")) {
				showMenu = true;
				showChooseSingle = false;
			}
			// Choose Your Enemy
			Texture2D showTexture;
			showTexture = Resources.Load ("enemy1") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,40, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 0)
				GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "Seeker", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "Seeker", menuText);
			showTexture = Resources.Load ("enemy4") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,70, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 1)
				GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "Launcher" , menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "Launcher" , menuText);
			showTexture = Resources.Load ("enemy16") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,100, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 2)
				GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), "Crawler", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), "Crawler", menuText);
			showTexture = Resources.Load ("enemy6") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,130, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 3)
				GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "Circler", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "Circler", menuText);
			showTexture = Resources.Load ("enemy12") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,160, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 4)
				GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Siner", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Siner", menuText);
			showTexture = Resources.Load ("enemy2") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,190, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 5)
				GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Straightliner", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Straightliner", menuText);
			showTexture = Resources.Load ("whiteenemy3") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,220, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 6)
				GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "Grouper", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "Grouper", menuText);
			showTexture = Resources.Load ("enemy21") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,250, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 7)
				GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "Shielder", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "Shielder", menuText);
			showTexture = Resources.Load ("enemy7") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,280, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 8)
				GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "Duplicator", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "Duplicator", menuText);
			showTexture = Resources.Load ("enemy3") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,310, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 9)
				GUI.Button(new Rect((Screen.width/2 ), 300, 0, 50), "Poofer", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 300, 0, 50), "Poofer", menuText);
			showTexture = Resources.Load ("enemy20") as Texture2D;
			singleEnemyTexture.normal.background = showTexture;
			GUI.Button(new Rect((Screen.width/2 ) + 120 ,340, 30, 30),"", singleEnemyTexture);
			if(singleSelection == 10)
				GUI.Button(new Rect((Screen.width/2 ), 330, 0, 50), "Diagnaler", menuTextSelect);
			else
				GUI.Button(new Rect((Screen.width/2 ), 330, 0, 50), "Diagnaler", menuText);
			if(Input.GetButton ("A_1") && !isHolding1) {
				showChooseSingle = false;
				singleGame = true;
				NewGame();	
			}
		}
		// Display the high score screen
		else if(showHighScores) {
			
			UpdatePlayerInformation();
			GUI.Button(new Rect(((Screen.width/2) - 400), 20, 800, 340), "", textBox );
			// Menu button
			if(GUI.Button(new Rect(((Screen.width/2) - 410), 30, 100, 25), "Menu", menuBox)) {
				showMenu = true;
				showHighScores = false;
			}
			if(Input.GetButton("B_1")) {
				showMenu = true;
				showHighScores = false;
			}
			// Display the top 10 high scores
			GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "1: " + highScore1.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "2: " + highScore2.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), "3: " + highScore3.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "4: " + highScore4.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "5: " + highScore5.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "6: " + highScore6.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "7: " + highScore7.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "8: " + highScore8.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "9: " + highScore9.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 300, 0, 50), "10: " + highScore10.ToString( "n0" ), menuText);
		}
		
		// Display the player stats screen
		else if(showPlayerStats) {
			GUI.Button(new Rect(((Screen.width/2) - 400), 20, 800, 310), "", textBox );
			// Menu button
			if(GUI.Button(new Rect(((Screen.width/2) - 400), 30, 100, 25), "Menu", menuBox)) {
				showMenu = true;
				showPlayerStats = false;
			}
			if(Input.GetButton("B_1")) {
				showMenu = true;
				showPlayerStats = false;
			}
			
			int totalLevelReached = 1;
			decimal previousLevelScore = 0;
			decimal nextTotalLevelScore = (decimal)Mathf.Pow(totalLevelReached, 3.2f) * 20000;
    		for(totalLevelReached = 1; totalScore >= nextTotalLevelScore; totalLevelReached++) {
				nextTotalLevelScore = (decimal)Mathf.Pow(totalLevelReached, 3.2f) * 20000;
				previousLevelScore = (decimal)Mathf.Pow(totalLevelReached - 1, 3.2f) * 20000;
			}
			
			decimal nextScoreDifference = nextTotalLevelScore - previousLevelScore;
			decimal nextScoreProgress = totalScore - previousLevelScore;
			
			// Display the player stats
			GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "Player Name: " + playerName, menuText);
			GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "Level: " + level.ToString( "n0" ), menuText);
			
		//	string tempPercent = "" + (nextScoreProgress / nextScoreDifference) * 1000;
			float tempPercentFloat = (float)nextScoreProgress/(float)nextScoreDifference;
		//	tempPercentFloat /= 1000;
			
			GUI.DrawTexture(new Rect((Screen.width/2 -280), 105, (Screen.width/2 - 280) * tempPercentFloat, 18), greenExpBar);
			GUI.DrawTexture(new Rect((Screen.width/2 -280), 105, Screen.width/2 - 280, 18), expTexture);
			menuText.fontSize = 10;
			GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), nextScoreProgress.ToString( "n0" ) + "/" + nextScoreDifference.ToString( "n0" ), menuText);
			menuText.fontSize = 25;
			GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "Total Score: " + totalScore.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Enemies Killed: " + enemiesKilled.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Starting Multiplier: " + startingMultiplier.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "Starting Grab Radius: " + startingRadius.ToString( "n1" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "Total Money: " + money.ToString( "n0" ), menuText);
			int levelReached = 1;
			decimal nextLevelScore = (decimal)Mathf.Pow(levelReached, 3.2f) * 5000;
    		for(levelReached = 1; highScore1 >= nextLevelScore; levelReached++) {
				nextLevelScore = (decimal)Mathf.Pow(levelReached, 3.2f) * 5000;
			}
			
			GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "Highest Level Reached: " + levelReached.ToString( "n0" ), menuText);
			
		}
		
		else if(showOptions) {
			
			GUI.Button(new Rect(((Screen.width/2) - 620), 140, 1240, 150), "", textBox );
			// Menu button
			if(GUI.Button(new Rect(((Screen.width/2) - 620), 250, 100, 25), "Menu", menuBox)) {
				showMenu = true;
				showOptions = false;
				sqlOptions = "UPDATE PlayerInformation5" + currentPlayerName + " " +
					"SET AUTOSELLBELOW=" + autoSellBelow;
				runSQLOptions = true;
			}
			if(Input.GetButton("B_1")) {
				showMenu = true;
				showOptions = false;
				sqlOptions = "UPDATE PlayerInformation5" + currentPlayerName + " " +
					"SET AUTOSELLBELOW=" + autoSellBelow;
				runSQLOptions = true;
			}
			string difficultyText = "";
			if(difficulty == 1)
				difficultyText = "Beginner";
			else if(difficulty == 2)
				difficultyText = "Intermediate";
			else if(difficulty == 3)
				difficultyText = "Expert";
			else if(difficulty == 4)
				difficultyText = "Insane";
			if(optionsSelection == 0)
				GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Difficulty: " + difficultyText + ".", menuTextSelect);
			else 
				GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Difficulty: " + difficultyText + ".", menuText);
			if(optionsSelection == 1)
				GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Automatically Sell Items lower than level " + autoSellBelow + " (1 means no autosell).", menuTextSelect);
			else 
				GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Automatically Sell Items lower than level " + autoSellBelow + " (1 means no autosell).", menuText);
			string backgroundText = "";
			if(backgroundCount == 0)
					backgroundText = "Random Background";
				else
					backgroundText = "Background " + backgroundCount + ".";
			if(optionsSelection == 2)
				
				GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), backgroundText, menuTextSelect);
			else 
				GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), backgroundText, menuText);
			
			
		}
		
		else if(showPostGame) {
	//		currentScore = 0;
			showPostGame = false;
			//if(showMenu || showInventory)
				
		//	if(!showMenu && !showInventory)
		//		NewGame();
			/*
			GUI.Button(new Rect(30, 20, Screen.width - 60, 600), "", textBox );
			// Menu button
			if(GUI.Button(new Rect(22, 30, 120, 25), "Menu", menuBox) && !isTutorial) {
				showMenu = true;
				showPostGame = false;
			//	isMazeGame = false;
				currentScore = 0;
			}
			if(Input.GetButton("B_1") && !isTutorial) {
				showMenu = true;
				showPostGame = false;
			//	isMazeGame = false;
				currentScore = 0;
			}
			// Inventory button
			if(GUI.Button(new Rect(2, 60, 160, 25), "Equip", YBox)) {
				showMenu = true;
				showPostGame = false;
				currentScore = 0;
			//	isMazeGame = false;
				Inventory ();
			}
			if(Input.GetButton("Y_1")) {
				showMenu = true;
				showPostGame = false;
				currentScore = 0;
			//	isMazeGame = false;
				Inventory ();
			}
			// New Game Button
			menuTextSelect.fontSize = 18;
			if(GUI.Button(new Rect(187, 90, 0, 25), "Enter New Dimension", menuTextSelect) && !isTutorial) {
				showMenu = true;
				showPostGame = false;
				currentScore = 0;
			//	mazeGame = false;
			//	if(isMazeGame) {
			//		isMazeGame = false;
			//		mazeGame = true;
			//	}
				NewGame ();
			}
			menuTextSelect.fontSize = 25;
			GUI.Button(new Rect(200, 90, 160, 25), "", ABox);
			if(Input.GetButton("A_1") && !isHolding && !isHolding1 && !isTutorial) {
				showMenu = true;
				showPostGame = false;
				currentScore = 0;
			//	mazeGame = false;
			//	if(isMazeGame) {
			//		isMazeGame = false;
			//		mazeGame = true;
			//	}
				NewGame ();
			}
			// Display the player stats after a game
			GUI.Button(new Rect((Screen.width/2 ), 30, 0, 50), "Player: " + playerName, menuText);
			GUI.Button(new Rect((Screen.width/2 ), 60, 0, 50), "Score: " + currentScore.ToString( "n0" ), menuText);
			int multTotal = startingMultiplier + multiplierAddLeft + multiplierAddRight + multiplierAddLeftBumper + multiplierAddRightBumper + bombMultiplierAdd + boosterMultiplierAdd + multiplierAddShield + multiplierAddCooldown + multiplierAddFirstPassive + multiplierAddSecondPassive;;
			GUI.Button(new Rect((Screen.width/2 ), 90, 0, 50), "Starting Multiplier: " + multTotal.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 120, 0, 50), "Ending Multiplier: " + currentMult.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 150, 0, 50), "Starting Grab Radius: " + startingRadius.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 180, 0, 50), "Ending Grab Radius: " + currentGrabberRadius.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 210, 0, 50), "Enemies Killed: " + currentEnemiesKilled.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 240, 0, 50), "Items Found: " + currentItemsFound.ToString( "n0" ), menuText);
			GUI.Button(new Rect((Screen.width/2 ), 270, 0, 50), "Multiplier permanently increased by " + currentPermMult.ToString( "n0" ) + ".", menuText);
			GUI.Button(new Rect((Screen.width/2 ), 300, 0, 50), "Grab radius permanently increased by " + currentPermGrab.ToString( "n1" ) + ".", menuText);
			GUI.Button(new Rect((Screen.width/2 ), 330, 0, 50), "Level Reached: " + currentLevel.ToString( "n0" ), menuText);
			
			GUI.Button(new Rect((Screen.width/2 ), 390, 0, 50), "Total Money: " + money.ToString( "n0" ), menuText);
			decimal showTotalScore = totalScore + currentScore;
			GUI.Button(new Rect((Screen.width/2 ), 420, 0, 50), "Total Score: " + showTotalScore.ToString( "n0" ), menuText);
			
			if(addedLevel == 1) {
				GUI.Button(new Rect((Screen.width/2 ), 450, 0, 50), "Congratulations! You have earned a new level!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 480, 0, 50), "Your multiplier is permanently increased by " + addMultAmount.ToString ( "n0" ) + "!", menuText);
			}
			else if(addedLevel > 1) {
				GUI.Button(new Rect((Screen.width/2 ), 450, 0, 50), "Congratulations! You have earned " + addedLevel.ToString( "n0" ) + " new levels!!", menuText);
				int addedLevelMultiplier = addedLevel * 5;
				GUI.Button(new Rect((Screen.width/2 ), 480, 0, 50), "Your multiplier is permanently increased by " + addMultAmount.ToString ( "n0" ) + "!", menuText);
			}
			if(addedLevel >= 1 && level >= 5 && level - addedLevel < 5) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "The shield is now available to you by pressing Y.", menuText);
			}
			if(addedLevel >= 1 && level >= 10 && level - addedLevel < 10) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "The bomb is now available to you by pressing B.", menuText);
			}
			if(addedLevel >= 1 && level >= 15 && level - addedLevel < 15) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "The booster is now available to you by pressing A.", menuText);
			}
			if(addedLevel >= 1 && level >= 20 && level - addedLevel < 20) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "The cooldown is now available to you by pressing X.", menuText);
			}
			if(addedLevel >= 1 && level >= 25 && level - addedLevel < 25) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "Another weapon is now available to you by pressing LB.", menuText);
			}
			if(addedLevel >= 1 && level >= 30 && level - addedLevel < 30) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "Another weapon is now available to you by pressing RB.", menuText);
			}
			if(addedLevel >= 1 && level >= 35 && level - addedLevel < 35) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "A passive upgrade is now available in your inventory.", menuText);
			}
			if(addedLevel >= 1 && level >= 40 && level - addedLevel < 40) {
				GUI.Button(new Rect((Screen.width/2 ), 510, 0, 50), "You have unlocked a new item!", menuText);
				GUI.Button(new Rect((Screen.width/2 ), 540, 0, 50), "A second passive upgrade is now available in your inventory.", menuText);
			} */
		}
		else if (firstTimePlayer) {
			GUI.Button(new Rect((Screen.width/2 ), 30, 350, 50), "Welcome. Please enter your name.", menuText);
			GUI.SetNextControlName ("InputTextField");
			playerInput = GUI.TextField(new Rect((Screen.width/2 ), 90, 350, 50), playerInput, 25, textBox);
			GUI.FocusControl ("InputTextField");
			if(GUI.Button(new Rect((Screen.width/2 ), 180, 150, 25), "Accept", ABox) && playerInput != "") {
				InitializePlayer2();
				isHolding = true;
				firstTimePlayer = false;
				
			}
			if((Input.GetButton("A_1") || Input.GetKey(KeyCode.Return)) && playerInput != "") {
				bool notTaken = true;
				for(int i = 1; i <= PlayerPrefs.GetInt ("Number of Players", 0); i++)
					if(playerInput == PlayerPrefs.GetString ("Player" + i))
						notTaken = false;
				if(notTaken) {
					InitializePlayer2();
					isHolding = true;
					firstTimePlayer = false;
				}
				
			}
			
		}
		else if(showLoadPlayer) {
			GUI.Button(new Rect(250, 20, Screen.width - 500, 600), "", textBox );
			scrollPosition = GUI.BeginScrollView(new Rect(250, 30, Screen.width - 500, 600), scrollPosition, new Rect(0, 0, 0, yCheck), false, false);
			scrollPosition = new Vector2(0, 0);
			
			// Menu button
			if(GUI.Button(new Rect(30, 30, 100, 25), "Menu", menuBox)) {
				showMenu = true;
				showPlayerStats = false;
			}
			if(Input.GetButton("B_1")) {
				showMenu = true;
				showLoadPlayer = false;
			}
			int yPosition = 30;
			for(int i = 1; i <= PlayerPrefs.GetInt ("Number of Players", 0); i++) {
				if(loadSelection == i && !isHolding && !isHolding1) {
					scrollPosition = new Vector2(0, yPosition);
					if(GUI.Button(new Rect(((Screen.width - 500)/2 ), yPosition, 0, 50), PlayerPrefs.GetString ("Player" + i), menuTextSelect)) {
						playerToLoad = PlayerPrefs.GetString ("Player" + i);
						LoadThisPlayer (playerToLoad);
					}
					if(Input.GetButton("A_1")) {
						playerToLoad = PlayerPrefs.GetString ("Player" + i);
						LoadThisPlayer (playerToLoad);
					}
				}
				else 
					if(GUI.Button(new Rect(((Screen.width - 500)/2 ), yPosition, 0, 50), PlayerPrefs.GetString ("Player" + i), menuText)) {
						playerToLoad = PlayerPrefs.GetString ("Player" + i);
						LoadThisPlayer (playerToLoad);
					}
				yPosition += 30;
				if(yPosition > yCheck)
					yCheck = yPosition;
			}
			GUI.EndScrollView();
		}
	}
	
	void LoadPlayer() {
		isHolding = true;
		showMenu = false;
		showLoadPlayer = true;
	}
	
	void LoadThisPlayer(string loadedPlayer) {
		isHolding = true;
		showLoadPlayer = false;
		showMenu = true;
		currentPlayerName = loadedPlayer;
		PlayerPrefs.SetString("Player Name", loadedPlayer);
		PlayerPrefs.Save();
	}
	
	// Runs when "New Maze Dimension" is punched
	void NewGameMaze () {
		showMenu = false;
		isMazeGame = true;
		singleGame = true;
		singleSelection = 0;
		NewGame ();
	}
	
	// Runs when "New Game" is punched
	void NewGame() {
		if(stillMazeGame)
			isMazeGame = true;
		SetupWeapons(); // Takes the weapons from the databases and loads them into variables
		
		float gameTypeRoll = Random.Range (0, 100);
			if(gameTypeRoll >= 90) { 
				singleGame = true;
				int singleTypeRoll = Random.Range (0, 100);
				singleSelection = singleTypeRoll % numberOfEnemies;
			}
			else
				singleGame = false;
		
		// Get the information from the database on the player (to initialize starting multiplier and radius)
		foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("Seeker")) {
			Destroy (obj);
		}
		// Create player and enemy spawner prefabs
		playerDeath = false;
		
		newGame = true;
		showMenu = false;
		GameObject createPlayer;
		if(backgroundCount == 0)
			ChangeBackground(backgroundCount);
		if(isMazeGame) {
			createPlayer = Instantiate (playerPrefab, new Vector3 (-480, 480, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
			GameObject mazePrefab1 = Instantiate (mazePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			GameObject nextMazePrefab1 = Instantiate (nextMazePrefab, new Vector3 (480, -480, 0), Quaternion.identity) as GameObject;
			
		}
		else
			createPlayer = Instantiate (playerPrefab, new Vector3 (0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
		GameObject createSpawner = Instantiate (spawnerPrefab, new Vector3(-5000, -5000, 5000), Quaternion.identity) as GameObject;
		Time.timeScale = 1.0f;
		updateCamera = true;
		UpdatePlayerInformation();
		manager.BeginTransaction(); // Start collecting calls to the database so they all get run at once 
	}
	
	void NewGameInstant() {
		Debug.Log ("NewGameInstant()");
		playerDeath = true;
		newGame = false;
		foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("PlayerShip")) {
			Destroy (obj);
		}
		foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("Seeker")) {
			Destroy (obj);
		}
		foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("Drop")) {
			Destroy (obj);
		}
		if(!showMenu) {
			GameObject createPlayer;
			if(backgroundCount == 0)
				ChangeBackground(backgroundCount);
			if(isMazeGame) {
				createPlayer = Instantiate (playerPrefab, new Vector3 (-480, 480, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
				GameObject mazePrefab1 = Instantiate (mazePrefab, Vector3.zero, Quaternion.identity) as GameObject;
				GameObject nextMazePrefab1 = Instantiate (nextMazePrefab, new Vector3 (480, -480, 0), Quaternion.identity) as GameObject;	
			}
			else
				createPlayer = Instantiate (playerPrefab, new Vector3 (0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
			GameObject createSpawner = Instantiate (spawnerPrefab, new Vector3(-5000, -5000, 5000), Quaternion.identity) as GameObject;
			float gameTypeRoll = Random.Range (0, 100);
			if(gameTypeRoll >= 90) { 
				singleGame = true;
				int singleTypeRoll = Random.Range (0, 100);
				singleSelection = singleTypeRoll % numberOfEnemies;
			}
			else
				singleGame = false;
				
		}
		updateCamera = true;
		Time.timeScale = 1.0f;
		if(currentScore > highScore1) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10=HighScore9, " +
				"HighScore9=HighScore8, " +
				"HighScore8=HighScore7, " +
				"HighScore7=HighScore6, " +
				"HighScore6=HighScore5, " +
				"HighScore5=HighScore4, " +
				"HighScore4=HighScore3, " +
				"HighScore3=HighScore2, " +
				"HighScore2=HighScore1, " +
				"HighScore1=" + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore2) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = HighScore7, " +
				"HighScore7 = HighScore6, " +
				"HighScore6 = HighScore5, " +
				"HighScore5 = HighScore4, " +
				"HighScore4 = HighScore3, " +
				"HighScore3 = HighScore2, " +
				"HighScore2 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore3) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = HighScore7, " +
				"HighScore7 = HighScore6, " +
				"HighScore6 = HighScore5, " +
				"HighScore5 = HighScore4, " +
				"HighScore4 = HighScore3, " +
				"HighScore3 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore4) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = HighScore7, " +
				"HighScore7 = HighScore6, " +
				"HighScore6 = HighScore5, " +
				"HighScore5 = HighScore4, " +
				"HighScore4 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore5) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = HighScore7, " +
				"HighScore7 = HighScore6, " +
				"HighScore6 = HighScore5, " +
				"HighScore5 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore6) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = HighScore7, " +
				"HighScore7 = HighScore6, " +
				"HighScore6 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore7) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = HighScore7, " +
				"HighScore7 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore8) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = HighScore8, " +
				"HighScore8 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore9) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = HighScore9, " +
				"HighScore9 = " + currentScore;
			manager.Execute(sqlScore);
		}
		else if(currentScore > highScore10) {
			string sqlScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET HighScore10 = " + currentScore;
			manager.Execute(sqlScore);
		}
		decimal newTotalScore = currentScore + totalScore;
		string sqlTotalScore = "UPDATE PlayerInformation5" + currentPlayerName + " " +
			"SET TotalScore=" + newTotalScore;
		manager.Execute(sqlTotalScore);
			
		string sqlEnemiesKilled = "UPDATE PlayerInformation5" + currentPlayerName + " " +
			"SET EnemiesKilled=EnemiesKilled+" + currentEnemiesKilled;
		manager.Execute(sqlEnemiesKilled);
		
	//	int levelReached = level;
	//	addMultAmount = 0;
	//	addedLevel = 0;
	//	decimal nextLevelScore = (decimal)Mathf.Pow(levelReached, 3.2f) * 20000;
    /*	for(levelReached = level+1; newTotalScore >= nextLevelScore; levelReached++) {
			Debug.Log ("Leveled");
			nextLevelScore = (decimal)Mathf.Pow(levelReached, 3.2f) * 20000;
			Debug.Log ("nextLevelScore=" + nextLevelScore);
			level++;
			addedLevel++;
			addMultAmount += 5 + level;
			Debug.Log ("addMultAmount=" + addMultAmount);
			craftingItems = true;
			string deleteStoreSql = "DELETE FROM \"Store" + Menu.currentPlayerName + "\" ";
			manager.Execute(deleteStoreSql);
				
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			GetRandomizedItem();
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
			craftingItems = false; 
		}  */
		
		Debug.Log ("currentPermMult=" + currentPermMult);	
		string sqlLevel = "UPDATE PlayerInformation5" + currentPlayerName + " " +
			"SET Level=" + level;
		manager.Execute(sqlLevel);
			
		if(currentPermMult > 0) {
			string sqlPermMult = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET StartingMultiplier=StartingMultiplier+" + currentPermMult;
			manager.Execute (sqlPermMult);
		}
		currentPermMult = 0;
		if(currentPermGrab > 0) {
			string sqlPermGrab = "UPDATE PlayerInformation5" + currentPlayerName + " " +
				"SET StartingRadius=StartingRadius+" + currentPermGrab;
			manager.Execute (sqlPermGrab);
		}
		currentPermGrab = 0;
		manager.Commit ();		

		if(stillMazeGame)
			isMazeGame = true;
		foreach( GameObject obj in GameObject.FindGameObjectsWithTag ("Seeker")) {
			Destroy (obj);
		}
		playerDeath = false;
		if(!showMenu)
			newGame = true;
		//showMenu = false;
		SetupWeapons(); // Takes the weapons from the databases and loads them into variables
		UpdatePlayerInformation();
		previousLevel = level;
		if(!showMenu)
			manager.BeginTransaction(); // Start collecting calls to the database so they all get run at once
	}
	
	void NewGameSingle() {
		showMenu = false;
		showChooseSingle = true;	
	}
	
	// Get the weapons from the databases and load them into static variables
	void SetupWeapons() {
		
		string sqlLeftWeapons = "SELECT * FROM LeftTriggerSlot" + currentPlayerName + "";
		List<LeftTriggerSlot> leftSlot = manager.Query<LeftTriggerSlot>(sqlLeftWeapons);
		
		foreach (LeftTriggerSlot inv in leftSlot) {
			weaponNameLeft = inv.WeaponName;
			damageLeft = inv.Damage;
			costLeft = inv.Cost;
			speedLeft = inv.Speed;
			weaponTypeIDLeft = inv.WeaponTypeID;
			rarityLeft = inv.Rarity;
			projectilesLeft = inv.Projectiles;
			sizeLeft = inv.Size;
			grabberAddLeft = inv.GrabberAdd;
			multiplierAddLeft = inv.MultiplierAdd;			
			proTexturesLeft = inv.ProTextures;
			affix1Left = inv.Affix1;
			affix2Left = inv.Affix2;
			affix3Left = inv.Affix3;
			affix4Left = inv.Affix4;
			affix5Left = inv.Affix5;
			affix6Left = inv.Affix6;
			rearProjectilesLeft = inv.RearProjectiles;
			explosiveLeft = inv.Explosive;
			fragmentingLeft = inv.Fragmenting;
			ricochetLeft = inv.Ricochet;
			chaoticLeft = inv.Chaotic;
		}
		
		string sqlRightWeapons = "SELECT * FROM RightTriggerSlot" + currentPlayerName + "";
		List<RightTriggerSlot> rightSlot = manager.Query<RightTriggerSlot>(sqlRightWeapons);
		
		foreach (RightTriggerSlot inv in rightSlot) {
			weaponNameRight = inv.WeaponName;
			damageRight = inv.Damage;
			costRight = inv.Cost;
			speedRight = inv.Speed;
			weaponTypeIDRight = inv.WeaponTypeID;
			rarityRight = inv.Rarity;
			projectilesRight = inv.Projectiles;
			sizeRight = inv.Size;
			grabberAddRight = inv.GrabberAdd;
			multiplierAddRight = inv.MultiplierAdd;	
			proTexturesRight = inv.ProTextures;
			affix1Right = inv.Affix1;
			affix2Right = inv.Affix2;
			affix3Right = inv.Affix3;
			affix4Right = inv.Affix4;
			affix5Right = inv.Affix5;
			affix6Right = inv.Affix6;
			rearProjectilesRight = inv.RearProjectiles;
			explosiveRight = inv.Explosive;
			fragmentingRight = inv.Fragmenting;
			ricochetRight = inv.Ricochet;
			chaoticRight = inv.Chaotic;
		}
		
		string sqlLeftBumpWeapons = "SELECT * FROM LeftBumperSlot" + currentPlayerName + "";
		List<LeftBumperSlot> leftBumpSlot = manager.Query<LeftBumperSlot>(sqlLeftBumpWeapons);
		
		foreach (LeftBumperSlot inv in leftBumpSlot) {
			weaponNameLeftBumper = inv.WeaponName;
			damageLeftBumper = inv.Damage;
			costLeftBumper = inv.Cost;
			speedLeftBumper = inv.Speed;
			weaponTypeIDLeftBumper = inv.WeaponTypeID;
			rarityLeftBumper = inv.Rarity;
			projectilesLeftBumper = inv.Projectiles;
			sizeLeftBumper = inv.Size;
			grabberAddLeftBumper = inv.GrabberAdd;
			multiplierAddLeftBumper = inv.MultiplierAdd;	
			proTexturesLeftBumper = inv.ProTextures;
			affix1LeftBumper = inv.Affix1;
			affix2LeftBumper = inv.Affix2;
			affix3LeftBumper = inv.Affix3;
			affix4LeftBumper = inv.Affix4;
			affix5LeftBumper = inv.Affix5;
			affix6LeftBumper = inv.Affix6;
			rearProjectilesLeftBumper = inv.RearProjectiles;
			explosiveLeftBumper = inv.Explosive;
			fragmentingLeftBumper = inv.Fragmenting;
			ricochetLeftBumper = inv.Ricochet;
			chaoticLeftBumper = inv.Chaotic;
		}
		
		string sqlRightBumpWeapons = "SELECT * FROM RightBumperSlot" + currentPlayerName + "";
		List<RightBumperSlot> rightBumpSlot = manager.Query<RightBumperSlot>(sqlRightBumpWeapons);
		
		foreach (RightBumperSlot inv in rightBumpSlot) {
			weaponNameRightBumper = inv.WeaponName;
			damageRightBumper = inv.Damage;
			costRightBumper = inv.Cost;
			speedRightBumper = inv.Speed;
			weaponTypeIDRightBumper = inv.WeaponTypeID;
			rarityRightBumper = inv.Rarity;
			projectilesRightBumper = inv.Projectiles;
			sizeRightBumper = inv.Size;
			grabberAddRightBumper = inv.GrabberAdd;
			multiplierAddRightBumper = inv.MultiplierAdd;	
			proTexturesRightBumper = inv.ProTextures;
			affix1RightBumper = inv.Affix1;
			affix2RightBumper = inv.Affix2;
			affix3RightBumper = inv.Affix3;
			affix4RightBumper = inv.Affix4;
			affix5RightBumper = inv.Affix5;
			affix6RightBumper = inv.Affix6;
			rearProjectilesRightBumper = inv.RearProjectiles;
			explosiveRightBumper = inv.Explosive;
			fragmentingRightBumper = inv.Fragmenting;
			ricochetRightBumper = inv.Ricochet;
			chaoticRightBumper = inv.Chaotic;
		}
		
		string sqlFirstPassive = "SELECT * FROM FirstPassiveSlot" + currentPlayerName + "";
		List<FirstPassiveSlot> firstPassiveSlot = manager.Query<FirstPassiveSlot>(sqlFirstPassive);
		
		foreach (FirstPassiveSlot inv in firstPassiveSlot) {
			nameFirstPassive = inv.WeaponName;
			damageFirstPassive = inv.Damage;
			costFirstPassive = inv.Cost;
			speedFirstPassive = inv.Speed;
			weaponTypeIDFirstPassive = inv.WeaponTypeID;
			rarityFirstPassive = inv.Rarity;
			projectilesFirstPassive = inv.Projectiles;
			sizeFirstPassive = inv.Size;
			grabberAddFirstPassive = inv.GrabberAdd;
			multiplierAddFirstPassive = inv.MultiplierAdd;	
			proTexturesFirstPassive = inv.ProTextures;
			affix1FirstPassive = inv.Affix1;
			affix2FirstPassive = inv.Affix2;
			affix3FirstPassive = inv.Affix3;
			affix4FirstPassive = inv.Affix4;
			affix5FirstPassive = inv.Affix5;
			affix6FirstPassive = inv.Affix6;
			rearProjectilesFirstPassive = inv.RearProjectiles;
			explosiveFirstPassive = inv.Explosive;
			fragmentingFirstPassive = inv.Fragmenting;
			ricochetFirstPassive = inv.Ricochet;
			chaoticFirstPassive = inv.Chaotic;
		}
		
		string sqlSecondPassive = "SELECT * FROM SecondPassiveSlot" + currentPlayerName + "";
		List<SecondPassiveSlot> secondPassiveSlot = manager.Query<SecondPassiveSlot>(sqlSecondPassive);
		
		foreach (SecondPassiveSlot inv in secondPassiveSlot) {
			nameSecondPassive = inv.WeaponName;
			damageSecondPassive = inv.Damage;
			costSecondPassive = inv.Cost;
			speedSecondPassive = inv.Speed;
			weaponTypeIDSecondPassive = inv.WeaponTypeID;
			raritySecondPassive = inv.Rarity;
			projectilesSecondPassive = inv.Projectiles;
			sizeSecondPassive = inv.Size;
			grabberAddSecondPassive = inv.GrabberAdd;
			multiplierAddSecondPassive = inv.MultiplierAdd;	
			proTexturesSecondPassive = inv.ProTextures;
			affix1SecondPassive = inv.Affix1;
			affix2SecondPassive = inv.Affix2;
			affix3SecondPassive = inv.Affix3;
			affix4SecondPassive = inv.Affix4;
			affix5SecondPassive = inv.Affix5;
			affix6SecondPassive = inv.Affix6;
			rearProjectilesSecondPassive = inv.RearProjectiles;
			explosiveSecondPassive = inv.Explosive;
			fragmentingSecondPassive = inv.Fragmenting;
			ricochetSecondPassive = inv.Ricochet;
			chaoticSecondPassive = inv.Chaotic;
		}
		
		string sqlShield = "SELECT * FROM ShieldSlot" + currentPlayerName + "";
		List<ShieldSlot> shieldSlot = manager.Query<ShieldSlot>(sqlShield);
		
		foreach (ShieldSlot inv in shieldSlot) {
			nameShield = inv.WeaponName;
			damageShield = inv.Damage;
			costShield = inv.Cost;
			speedShield = inv.Speed;
			weaponTypeIDShield = inv.WeaponTypeID;
			rarityShield = inv.Rarity;
			projectilesShield = inv.Projectiles;
			sizeShield = inv.Size;
			grabberAddShield = inv.GrabberAdd;
			multiplierAddShield = inv.MultiplierAdd;	
			proTexturesShield = inv.ProTextures;
			affix1Shield = inv.Affix1;
			affix2Shield = inv.Affix2;
			affix3Shield = inv.Affix3;
			affix4Shield = inv.Affix4;
			affix5Shield = inv.Affix5;
			affix6Shield = inv.Affix6;
			rearProjectilesShield = inv.RearProjectiles;
			explosiveShield = inv.Explosive;
			fragmentingShield = inv.Fragmenting;
			ricochetShield = inv.Ricochet;
			chaoticShield = inv.Chaotic;
		}
		
		string sqlCooldown = "SELECT * FROM CooldownSlot" + currentPlayerName + "";
		List<CooldownSlot> cooldownSlot = manager.Query<CooldownSlot>(sqlCooldown);
		
		foreach (CooldownSlot inv in cooldownSlot) {
			nameCooldown = inv.WeaponName;
			damageCooldown = inv.Damage;
			costCooldown = inv.Cost;
			speedCooldown = inv.Speed;
			weaponTypeIDCooldown = inv.WeaponTypeID;
			rarityCooldown = inv.Rarity;
			projectilesCooldown = inv.Projectiles;
			sizeCooldown = inv.Size;
			grabberAddCooldown = inv.GrabberAdd;
			multiplierAddCooldown = inv.MultiplierAdd;	
			proTexturesCooldown = inv.ProTextures;
			affix1Cooldown = inv.Affix1;
			affix2Cooldown = inv.Affix2;
			affix3Cooldown = inv.Affix3;
			affix4Cooldown = inv.Affix4;
			affix5Cooldown = inv.Affix5;
			affix6Cooldown = inv.Affix6;
			rearProjectilesCooldown = inv.RearProjectiles;
			explosiveCooldown = inv.Explosive;
			fragmentingCooldown = inv.Fragmenting;
			ricochetCooldown = inv.Ricochet;
			chaoticCooldown = inv.Chaotic;
		}
		
		string sqlBooster = "SELECT * FROM BoosterSlot" + currentPlayerName + "";
		List<BoosterSlot> boosterSlot = manager.Query<BoosterSlot>(sqlBooster);
		
		foreach (BoosterSlot inv in boosterSlot) {
			boosterName = inv.WeaponName;
			boosterSpeed = inv.Speed;
			boosterMultiplierAdd = inv.MultiplierAdd;
			boosterGrabberAdd = inv.GrabberAdd;
			boosterProTextures = inv.ProTextures;
			boosterCost = inv.Cost;
			boosterProjectiles = inv.Projectiles;
			boosterRarity = inv.Rarity;
			boosterAffix1 = inv.Affix1;
			boosterAffix2 = inv.Affix2;
			boosterAffix3 = inv.Affix3;
			boosterAffix4 = inv.Affix4;
			boosterAffix5 = inv.Affix5;
			boosterAffix6 = inv.Affix6;
			boosterRearProjectiles = inv.RearProjectiles;
			boosterExplosive = inv.Explosive;
			boosterFragmenting = inv.Fragmenting;
			boosterricochet = inv.Ricochet;
			boosterChaotic = inv.Chaotic;
		}  
		
		string sqlBomb = "SELECT * FROM BombSlot" + currentPlayerName + "";
		List<BombSlot> bombSlot = manager.Query<BombSlot>(sqlBomb);
		
		foreach (BombSlot inv in bombSlot) {
			bombName = inv.WeaponName;
			bombSpeed = inv.Speed;
			bombSize = inv.Size;
			bombDamage = inv.Damage;
			bombMultiplierAdd = inv.MultiplierAdd;
			bombGrabberAdd = inv.GrabberAdd;
			bombProTextures = inv.ProTextures;
			bombCost = inv.Cost;
			bombProjectiles = inv.Projectiles;
			bombRarity = inv.Rarity;
			bombAffix1 = inv.Affix1;
			bombAffix2 = inv.Affix2;
			bombAffix3 = inv.Affix3;
			bombAffix4 = inv.Affix4;
			bombAffix5 = inv.Affix5;
			bombAffix6 = inv.Affix6;
			bombRearProjectiles = inv.RearProjectiles;
			bombExplosive = inv.Explosive;
			bombFragmenting = inv.Fragmenting;
			bombricochet = inv.Ricochet;
			bombChaotic = inv.Chaotic;
		}
	}
	
	// Loads the tutorial
	void Tutorial() {
		SetupWeapons(); // Takes the weapons from the databases and loads them into variables
		resetScore = true;
		isTutorial = true;
		
		// Get the information from the database on the player (to initialize starting multiplier and radius)
		UpdatePlayerInformation();
		
		playerDeath = false;
		manager.BeginTransaction(); // Start collecting calls to the database so they all get run at once
		showMenu = false;
		
		
		// Create player and enemy spawner prefabs
		GameObject createPlayer = Instantiate (playerPrefab, new Vector3 (0, 0, -1000), Quaternion.Euler(0, 0, 0)) as GameObject;
		GameObject createTutorial = Instantiate (tutorialPrefab, new Vector3(-5000, -5000, 5000), Quaternion.identity) as GameObject;
	}  
	
	// Loads the Inventory screen
	void Inventory() {
		showMenu = false;
		showMenuCheck = false;
		showInventory = true;
		UpdatePlayerInformation();
		GameObject createInventory = Instantiate (inventoryPrefab, new Vector3 (0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
	}
	
	// Loads the stash screen
	void Stash() {
		showMenu = false;
		showStash = true;
		UpdatePlayerInformation();
		GameObject createStash = Instantiate (stashPrefab, new Vector3 (0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
	}
	
	// Loads the shop screen
	void Shop() {
		showMenu = false;
		showStore = true;
		UpdatePlayerInformation();
		GameObject createStore = Instantiate (storePrefab, new Vector3 (0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
	}
	
	// Loads the High Score screen
	void HighScores() {
		showMenu = false;
		showHighScores = true;
		
		UpdatePlayerInformation();
	}
	
	// Loads the Player Stats screen
	void PlayerStats() {
		showMenu = false;
		showPlayerStats = true;
		
		UpdatePlayerInformation();
	}
	
	// Loads the Options screen
	void Options () {
		showMenu = false;
		showOptions = true;
		selection = 0;
		UpdatePlayerInformation();
	}
	
	// Quit the game
	void ExitGame() {
		Application.Quit ();
	}
	
	public static void AddCost(float cost1) {
		cost = cost1 / 5;
		sql = "UPDATE PlayerInformation5" + Menu.currentPlayerName + " " +
			"SET MONEY=MONEY+" + cost;
		doExecuteCost = true;
	}
	
	// When an item is picked up, it runs this function to add the item to the inventory database
	public static void AddItem(string weaponName1, float damage1, float cost1, float speed1, int weaponTypeID1, int rarity1, int projectiles1, float size1, int multiplierAdd1, float grabberAdd1, string proTextures1, string affix11, string affix21, string affix31, string affix41, string affix51, string affix61, int rearProjectiles1, int explosive1, int fragmenting1, int ricochet1, float chaotic1) {
		weaponName = weaponName1;
		damage = damage1;
		cost = cost1;
		speed = speed1;
		weaponTypeID = weaponTypeID1;
		rarity = rarity1;
		projectiles = projectiles1;
		size = size1;
		grabberAdd = grabberAdd1;
		multiplierAdd = multiplierAdd1;
		proTextures = proTextures1;
		affix1 = affix11;
		affix2 = affix21;
		affix3 = affix31;
		affix4 = affix41;
		affix5 = affix51;
		affix6 = affix61;
		rearProjectiles = rearProjectiles1;
		explosive = explosive1;
		fragmenting = fragmenting1;
		ricochet = ricochet1;
		chaotic = chaotic1;
		sql = "INSERT INTO Inv6" + currentPlayerName + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, MultiplierAdd, GrabberAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
		doExecute = true;
	}
	
	// Add an item to the store
	public static void AddItemStore(string weaponName1, float damage1, float cost1, float speed1, int weaponTypeID1, int rarity1, int projectiles1, float size1, int multiplierAdd1, float grabberAdd1, string proTextures1, string affix11, string affix21, string affix31, string affix41, string affix51, string affix61, int rearProjectiles1, int explosive1, int fragmenting1, int ricochet1, float chaotic1) {
		weaponName = weaponName1;
		damage = damage1;
		cost = cost1;
		speed = speed1;
		weaponTypeID = weaponTypeID1;
		rarity = rarity1;
		projectiles = projectiles1;
		size = size1;
		grabberAdd = grabberAdd1;
		multiplierAdd = multiplierAdd1;
		proTextures = proTextures1;
		affix1 = affix11;
		affix2 = affix21;
		affix3 = affix31;
		affix4 = affix41;
		affix5 = affix51;
		affix6 = affix61;
		rearProjectiles = rearProjectiles1;
		explosive = explosive1;
		fragmenting = fragmenting1;
		ricochet = ricochet1;
		chaotic = chaotic1;
		sql = "INSERT INTO Store" + currentPlayerName + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, MultiplierAdd, GrabberAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
		doExecute = true;
	}
	
	// Setup menu switches for when the game is over
	static public void PlayerDied () {	
		startCopy = true;
	}
	
	public void FixedUpdate() {
		// Set the high score(s) and commit database changes
		if(startCopy && !showMenu) {
			startCopy = false;
			NewGameInstant();
		}
		else if(startCopy) {
			playerDeath = true;
			newGame = false;
			startCopy = false;
			NewGameInstant();
		}
	}
	
	IEnumerator Start() {
	//	manager.Commit ();
		// If this is the first time to play, initialize the player
		Screen.showCursor = false;
		FileInfo dbFile;
		if(System.IO.File.Exists("" + Application.persistentDataPath + "/trials81.bytes")) {
			dbFile = new FileInfo("" + Application.persistentDataPath + "/trials81.bytes");
			if(dbFile.Length < 100)
				InitializePlayer();		
		}
		
		if(System.IO.File.Exists("" + Application.persistentDataPath + "/trials81.bytes") == false) {
			InitializePlayer();
			PlayerPrefs.SetInt ("Number of Players", 0);
			PlayerPrefs.Save ();
		}
		else {
			currentPlayerName = PlayerPrefs.GetString("Player Name");
			isIntro = true;
		}
		currentPlayerName = PlayerPrefs.GetString("Player Name");
		
		loadSelection = 1;

		// Check for input from the controller
		for( ; ; ) {
			// Check the switches
		if(doExecute) {
			doExecute = false;
			manager.Execute(sql, weaponName, damage, cost, speed, weaponTypeID, rarity, projectiles, size, multiplierAdd, grabberAdd, proTextures, affix1, affix2, affix3, affix4, affix5, affix6, rearProjectiles, explosive, fragmenting, ricochet, chaotic);
		}
		if(doExecuteCost) {
			doExecuteCost = false;
			manager.Execute (sql);
		}
		if(runSQLOptions) {
			runSQLOptions = false;
			manager.Execute (sqlOptions);
		}			
			// Wait one second before accepting input
			if(isHolding) {
				yield return new WaitForSeconds(2.0f);
				isHolding = false;
			}
			if(isHolding1) {
				yield return new WaitForSeconds(0.5f);
				isHolding1 = false;
			}
			if(isHolding3) {
				yield return new WaitForSeconds(2.0f);
				showMenu = true;
				isHolding3 = false;
			}
			if(showLoadPlayer && canMove && !isHolding) {
				int numOfPlayers = PlayerPrefs.GetInt ("Number of Players");
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5) {
					canMove = false;
					if(loadSelection < numOfPlayers)
						loadSelection += 1;
					else
						loadSelection = 1;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5) {
					canMove = false;
					if(loadSelection > 1)
						loadSelection -= 1;
					else
						loadSelection = numOfPlayers;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}
			}
			if(showMenu && canMove && !isHolding) {
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5) {
					canMove = false;
					if(selection < 12)
						selection += 1;
					else
						selection = 0;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5) {
					canMove = false;
					if(selection > 0)
						selection -= 1;
					else
						selection = 12;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}
			}
			if(showOptions && canMove) {
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5) {
					canMove = false;
					if(optionsSelection < 2)
						optionsSelection += 1;
					else
						optionsSelection = 0;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5) {
					canMove = false;
					if(optionsSelection > 0)
						optionsSelection -= 1;
					else
						optionsSelection = 2;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}
			}
			if(showChooseSingle && canMove) {
				if(Input.GetAxisRaw ("L_YAxis_1") < -.5) {
					canMove = false;
					if(singleSelection < 10)
						singleSelection += 1;
					else
						singleSelection = 0;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				} 
				if(Input.GetAxisRaw ("L_YAxis_1") > .5) {
					canMove = false;
					if(singleSelection > 0)
						singleSelection -= 1;
					else
						singleSelection = 10;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}
			}	
			if(showOptions && canMove  && optionsSelection == 0 ) {
				if(Input.GetAxisRaw ("L_XAxis_1") < -.5) {
					canMove = false;
					if(difficulty == 1)
						difficulty = 4;
					else
						difficulty--;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}	
				if(Input.GetAxisRaw ("L_XAxis_1") > .5) {
					canMove = false;
					if(difficulty == 4)
						difficulty = 1;
					else
						difficulty++;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}	
			}
			if(showOptions && canMove  && optionsSelection == 1 ) {
				if(Input.GetAxisRaw ("L_XAxis_1") < -.5) {
					canMove = false;
					if(autoSellBelow == 1)
						autoSellBelow = level;
					else
						autoSellBelow--;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}	
				if(Input.GetAxisRaw ("L_XAxis_1") > .5) {
					canMove = false;
					if(autoSellBelow == level)
						autoSellBelow = 1;
					else
						autoSellBelow++;
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}	
			}
			if(showOptions && canMove  && optionsSelection == 2 ) {
				if(Input.GetAxisRaw ("L_XAxis_1") < -.5) {
					canMove = false;
					if(backgroundCount == 0) {
						backgroundCount = totalBackgrounds;
						ChangeBackground(backgroundCount);
					}
					else {
						backgroundCount--;
						ChangeBackground(backgroundCount);
					}
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}	
				if(Input.GetAxisRaw ("L_XAxis_1") > .5) {
					canMove = false;
					if(backgroundCount == totalBackgrounds) {
						backgroundCount = 0;
						ChangeBackground(backgroundCount);
					}
					else {
						backgroundCount++;
						ChangeBackground(backgroundCount);
					}
					yield return new WaitForSeconds(.1f);
					canMove = true;
				}	
			}
			if(showMenu || showHighScores || showPlayerStats || showOptions || showPostGame || showInventory || showStore || showStash || showChooseSingle)
				yield return new WaitForSeconds(.001f);
			else
				yield return new WaitForSeconds(2f);
		}
	}
	
	void ChangeBackground(int backgroundNumber) {
		int randLoad = 0;
		if(backgroundNumber == 0)
			backgroundNumber = Random.Range (1,totalBackgrounds + 1);
		Texture backgroundTexture = Resources.Load("background" + backgroundNumber) as Texture;
	//	backgroundObject.renderer.material.SetTexture("_BumpMap", backgroundTexture);
		backgroundObject.renderer.material.SetTexture("_MainTex", backgroundTexture);
	//	backgroundObject.renderer.material.mainTexture = Resources.Load("background" + backgroundNumber) as Texture2D;
	//	backgroundObject.renderer.material.mainTexture = Resources.Load("background" + backgroundNumber) as Texture2D;
	}
	
	void Update() {
		
	}
	
	public void GetRandomizedItem() {
		int itemRoll1 = Random.Range(0,1000);
		GameObject dropBasicWeapon;
		if(Menu.level < 5)
			if(itemRoll1 >= 400) 
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else 
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		else if(Menu.level < 10) 
			if(itemRoll1 >= 700) 
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if(itemRoll1 >= 350) 
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 150) 
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else 
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		else if(Menu.level < 15)
			if(itemRoll1 >= 800) 
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if(itemRoll1 >= 650) 
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 500) 
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 400) 
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 200) 
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 100) 
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else 
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		else if(Menu.level < 20)
			if(itemRoll1 >= 800) 
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if(itemRoll1 >= 600) 
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 450) 
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 350) 
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 200) 
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 133) 
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 66)
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else 
				dropBasicWeapon = Instantiate(Resources.Load("BoosterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		else if(Menu.level < 35) 
			if(itemRoll1 >= 800) 
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if(itemRoll1 >= 600) 
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 450) 
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 350) 
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 200) 
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 150) 
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 100)
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 50)
				dropBasicWeapon = Instantiate(Resources.Load("BoosterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else 
				dropBasicWeapon = Instantiate(Resources.Load("CooldownDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		else 
			if(itemRoll1 >= 800) 
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if(itemRoll1 >= 600) 
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 450) 
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 350) 
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 250) 
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 200) 
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 150) 
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 100) 
				dropBasicWeapon = Instantiate(Resources.Load("BoosterDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else if (itemRoll1 >= 50) 
				dropBasicWeapon = Instantiate(Resources.Load("CooldownDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			else 
				dropBasicWeapon = Instantiate(Resources.Load("PassiveDrop"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
	}
	
	public void UpdatePlayerInformation() {
		string sqlPlayer = "SELECT * FROM PlayerInformation5" + currentPlayerName + "";
			List<PlayerInformation5> getPlayerInfo = manager.Query<PlayerInformation5>(sqlPlayer);
		
		foreach (PlayerInformation5 plr in getPlayerInfo) {
			if(plr.PlayerID == 1) {
				playerName = plr.PlayerName;
				totalScore = plr.TotalScore;
				money = plr.Money;
				highScore1 = plr.HighScore1;
				highScore2 = plr.HighScore2;
				highScore3 = plr.HighScore3;
				highScore4 = plr.HighScore4;
				highScore5 = plr.HighScore5;
				highScore6 = plr.HighScore6;
				highScore7 = plr.HighScore7;
				highScore8 = plr.HighScore8;
				highScore9 = plr.HighScore9;
				highScore10 = plr.HighScore10;
				level = plr.Level;
				enemiesKilled = plr.EnemiesKilled;
			 	startingMultiplier = plr.StartingMultiplier;
			 	startingRadius = plr.StartingRadius;
				autoSellBelow = plr.AutoSellBelow;
			}
		}
	}
	
	// It the player quits manually, the game will commit any changes to the database
	// to avoid errors
	void OnApplicationQuit () {
		FileInfo test;
		if(System.IO.File.Exists("" + Application.persistentDataPath + "/trials81.bytes")) {
			test = new FileInfo("" + Application.persistentDataPath + "/trials81.bytes");
			if(test.Length < 100) 
				InitializePlayer();
			if(playerDeath == false)
				manager.Commit ();
		}
		
	}
	
	void InitializePlayer() {
		showMenu = false;
		firstTimePlayer = true;
	}
	
	void InitializePlayer2() {
			currentPlayerName = playerInput;
			PlayerPrefs.SetString("Player Name", playerInput);
			PlayerPrefs.SetInt ("Number of Players", PlayerPrefs.GetInt("Number of Players") + 1);
			PlayerPrefs.SetString ("Player" + PlayerPrefs.GetInt("Number of Players"), playerInput);
			PlayerPrefs.Save ();
			manager.BeginTransaction();
			string sql1 = "CREATE TABLE \"RightTriggerSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sql1);
			string sql2 = "CREATE TABLE \"LeftTriggerSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sql2);
			string sqlbump1 = "CREATE TABLE \"RightBumperSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlbump1);
			string sqlbump2 = "CREATE TABLE \"LeftBumperSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlbump2);
			string sqlBoostInit = "CREATE TABLE \"BoosterSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlBoostInit);
			string sqlBombInit = "CREATE TABLE \"BombSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlBombInit);
			string sqlShieldInit = "CREATE TABLE \"ShieldSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlShieldInit);
			string sqlCooldownInit = "CREATE TABLE \"CooldownSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlCooldownInit);
			string sqlPassives1 = "CREATE TABLE \"FirstPassiveSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlPassives1);
			string sqlPassives2 = "CREATE TABLE \"SecondPassiveSlot" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlPassives2);
			string sql3 = "CREATE TABLE \"Inv6" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sql3);
			string sqlStash = "CREATE TABLE \"Stash" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlStash);
			string sqlStore = "CREATE TABLE \"Store" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sqlStore);
			string sql4 = "CREATE TABLE \"HolderDatabase" + playerInput + "\" " +
				"(\"WeaponID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"WeaponName\" varchar(60), " +
				"\"Damage\" INTEGER, " +
				"\"Cost\" INTEGER, " +
				"\"Speed\" FLOAT, " +
				"\"WeaponTypeID\" INTEGER, " +
				"\"Rarity\" INTEGER, " +
				"\"Projectiles\" INTEGER, " +
				"\"Size\" INTEGER, " +
				"\"GrabberAdd\" INTEGER, " +
				"\"MultiplierAdd\" INTEGER, " +
				"\"ProTextures\" INTEGER, " +
				"\"Affix1\" varchar(60), " +
				"\"Affix2\" varchar(60), " +
				"\"Affix3\" varchar(60), " +
				"\"Affix4\" varchar(60), " +
				"\"Affix5\" varchar(60), " +
				"\"Affix6\" varchar(60), " +
				"\"RearProjectiles\" INTEGER, " +
				"\"Explosive\" INTEGER, " +
				"\"Fragmenting\" INTEGER, " +
				"\"Ricochet\" INTEGER, " +
				"\"Chaotic\" INTEGER)";
			manager.Execute (sql4);
			string sql5 = "CREATE TABLE \"PlayerInformation5" + playerInput + "\" " +
				"(\"PlayerID\" INTEGER PRIMARY KEY NOT NULL, " + 
				"\"PlayerName\" varchar(60), " +
				"\"TotalScore\" DECIMAL, " +
				"\"StartingMultiplier\" INTEGER, " +
				"\"StartingRadius\" FLOAT, " +
				"\"Money\" DECIMAL, " +
				"\"HighScore1\" DECIMAL, " +
				"\"HighScore2\" DECIMAL, " +
				"\"HighScore3\" DECIMAL, " +
				"\"HighScore4\" DECIMAL, " +
				"\"HighScore5\" DECIMAL, " +
				"\"HighScore6\" DECIMAL, " +
				"\"HighScore7\" DECIMAL, " +
				"\"HighScore8\" DECIMAL, " +
				"\"HighScore9\" DECIMAL, " +
				"\"HighScore10\" DECIMAL, " +
				"\"Level\" INTEGER," +
				"\"EnemiesKilled\" DECIMAL, " +
				"\"AutoSellBelow\" INTEGER)";
			manager.Execute (sql5);
			string sql6 = "INSERT INTO LeftTriggerSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sql6, "Starting Arrow", 1, 1, 1.0, 1, 1, 2, 1, 0, 0, "yellowTriangle", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sql7 = "INSERT INTO RightTriggerSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sql7, "Starting Laser", 2, 1, 1.0, 1, 1, 1, 1, 0, 0, "redline", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlbumpw1 = "INSERT INTO LeftBumperSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sqlbumpw1, "Starting Arrow", 1, 1, 1.0, 1, 1, 2, 1, 0, 0, "yellowTriangle", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlbumpw2 = "INSERT INTO RightBumperSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sqlbumpw2, "Starting Laser", 2, 1, 1.0, 1, 1, 1, 1, 0, 0, "redline", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlBC = "INSERT INTO BoosterSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sqlBC, "Starting Booster", 0, 1, 1.5, 11, 1, 0, 1, 0, 0, "booster", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlBomb = "INSERT INTO BombSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			int bombColorInit = Random.Range (0,100);
			manager.Execute(sqlBomb, "Starting Bomb", 0, 1, bombColorInit, 21, 1, 3, 30, 0, 0, "bomb", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlShield = "INSERT INTO ShieldSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			int shieldColorInit = Random.Range (0,100);
			manager.Execute(sqlShield, "Starting Shield", 0, 1, 2.0, 31, 1, shieldColorInit, 1, 0, 0, "basicshield", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlCooldown = "INSERT INTO CooldownSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			manager.Execute(sqlCooldown, "Starting Cooldown", 0, 1, 80.0, 41, 1, 0, 3.0, 0, 0, "timestopcooldown", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlPassive1 = "INSERT INTO FirstPassiveSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sqlPassive1, "Starting Passive 1", 0, 1, 1.5, 51, 1, 0, 1, 0, 0, "passive", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sqlPassive2 = "INSERT INTO SecondPassiveSlot" + playerInput + " " +
				"(WeaponName, Damage, Cost, Speed, WeaponTypeID, Rarity, Projectiles, Size, GrabberAdd, MultiplierAdd, ProTextures, Affix1, Affix2, Affix3, Affix4, Affix5, Affix6, RearProjectiles, Explosive, Fragmenting, Ricochet, Chaotic) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; 
			manager.Execute(sqlPassive2, "Starting Passive 2", 0, 1, 1.5, 51, 1, 0, 1, 0, 0, "passive", null, null, null, null, null, null, 0, 0, 0, 0, 0);
			string sql8 = "INSERT INTO PlayerInformation5" + playerInput + " " +
				"(PlayerName, TotalScore, StartingMultiplier, StartingRadius, Money, HighScore1, HighScore2, HighScore3, HighScore4, HighScore5, HighScore6, HighScore7, HighScore8," +
				" HighScore9, HighScore10, Level, EnemiesKilled, AutoSellBelow) " +
				"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			manager.Execute(sql8, playerInput, 0, 1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1);
			manager.Commit(); 
			Tutorial ();
	}
}