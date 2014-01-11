#pragma strict

var guiTextScore : GUIText;
var guiTextMult : GUIText;
var guiTextGrab : GUIText;
var guiTextLevel : GUIText;
var guiTextBombs : GUIText;
var guiTextCooldown : GUIText;
var guiTextShield : GUIText;
var xpCurrentGame : Texture2D;
var xpCurrentGameGreen : Texture2D;
var xpOverall : Texture2D;
var xpOverallGreen : Texture2D;
public var barText : GUIStyle;

public static var score : System.Decimal = 0;
public static var multiplier : int = 1;
public static var grabberRadius : float = 20;
public static var enemiesKilled : System.Decimal = 0;
public static var levelReached : int = 1;
public static var permMult : int = 0;
public static var permGrab : float = 0;

public var currentDingSound : GameObject;
public var overallDingSound : GameObject;

public var isReady = false;
public var tenCount : int  = 0;

public static var bombCount : int = 0;
public static var shieldPercent : float = 100.0f;
public static var cooldownTime : float = 0;

public static var shieldOut = false;
public static var percentTick : float = 0;
public static var waitOnShield = false;
public var playerVar : GameObject;
public static var inGame = false;
public var levelsGained = 0;
public var keeperScore : System.Decimal = 0;

var enemyKillCheck : int = 0;

public var timeCount : int = 0;
public var myThread1 : UnityThreading.ActionThread;
public var myThread2 : UnityThreading.ActionThread;

public var currentTotalScore : System.Decimal; //+ keeperScore;
public var totalLevelReached : int;
public var nextLevelScoreOverall : System.Decimal;
public var prevLevelScoreOverall : System.Decimal;

function Start () {
	for( ; ; ) {
		yield WaitForSeconds(.1);
		myThread1 = UnityThreadHelper.CreateThread(DoThreadWork1);
		if(playerVar)
			inGame = true;
	}
}

function DoThreadWork1() {
	if(waitOnShield)
		timeCount++;
	if(shieldPercent <= 0){
		shieldOut = false;
		shieldPercent = 0.0;
	}
	if(timeCount >= 5) {
		timeCount = 0;
		waitOnShield = false;
	}
	if(isReady) {
		tenCount++;
	}
	if(isReady && tenCount >= 10) {
		tenCount = 0;
		isReady = false;
		Menu.newGame = false;
	}
	if(Menu.chaoticShield > 0 && shieldPercent < 100) {
		shieldPercent += (Menu.chaoticShield / 10);
	}
}

function OnGUI() {
	if(inGame || tutorial.showExperience) {
		currentTotalScore = Menu.totalScore + score; //+ keeperScore;
    	totalLevelReached = Menu.level;
    	nextLevelScoreOverall = Mathf.Pow(totalLevelReached, 3.2f) * 20000;
    	prevLevelScoreOverall = Mathf.Pow(totalLevelReached - 1, 3.2f) * 20000;
    	Debug.Log(Menu.level);
   // 	Debug.Log ("currentTotalScore = " + currentTotalScore);
   // 	Debug.Log ("nextLevelScoreOverall = " + nextLevelScoreOverall);
    	for(var i : int = 0; currentTotalScore >= nextLevelScoreOverall; totalLevelReached++) {
    		var addMultAmount : int = 0;
    		Menu.level++;
    		totalLevelReached++;
			nextLevelScoreOverall = Mathf.Pow(totalLevelReached, 3.2f) * 20000;
			prevLevelScoreOverall = Mathf.Pow(totalLevelReached - 1, 3.2f) * 20000;
			var overallDingSound1 = Instantiate(overallDingSound, transform.position, Quaternion.identity);
			Debug.Log ("Leveled");
			addMultAmount += 5 + Menu.level;
			Debug.Log ("addMultAmount=" + addMultAmount);
	
			permMult += addMultAmount;
		}
			
		var scoreDifference : System.Decimal = nextLevelScoreOverall - prevLevelScoreOverall;
		var currentScoreDifference : System.Decimal = currentTotalScore - prevLevelScoreOverall;
		
		GUI.DrawTexture(Rect(Screen.width/2 - ((Screen.width/3) / 2) , Screen.height - 18, (Screen.width/3) * (currentScoreDifference / scoreDifference), 18), xpOverallGreen);
		GUI.DrawTexture(Rect(Screen.width/2 - ((Screen.width/3) / 2), Screen.height - 18, Screen.width/3, 18), xpOverall);
		GUI.Button(new Rect(Screen.width/2 , Screen.height - 18, 0, 18), currentScoreDifference.ToString( "n0" ) + "/" + scoreDifference.ToString( "n0" ), barText);
		
    	var nextLevelScore : System.Decimal = Mathf.Pow(levelReached, 3.2f) * 5000;
    	var prevLevelScore : System.Decimal = Mathf.Pow(levelReached - 1, 3.2f) * 5000;
    	for(var k : int = 0; score >= nextLevelScore; levelReached++) {
			nextLevelScore = (Mathf.Pow(levelReached + 1, 3.2f) * 5000);
			prevLevelScore = (Mathf.Pow(levelReached, 3.2f) * 5000);
			var currentDingSound1 = Instantiate(currentDingSound, transform.position, Quaternion.identity);
		}
		var scoreDifferenceSingle : System.Decimal = nextLevelScore - prevLevelScore;
		var currentScoreDifferenceSingle : System.Decimal = score - prevLevelScore;
	
		GUI.DrawTexture(Rect(0, 0, (Screen.width/4 - 75) *(currentScoreDifferenceSingle/scoreDifferenceSingle), 18), xpCurrentGameGreen);
		GUI.DrawTexture(Rect(0, 0, Screen.width/4 - 75, 18), xpCurrentGame);
		GUI.Button(new Rect((Screen.width/4 - 75) / 2, 0, 0, 18), currentScoreDifferenceSingle.ToString( "n0" ) + "/" + scoreDifferenceSingle.ToString( "n0" ), barText);
	}
}

function Update () {
	if(inGame) {
    	guiTextScore.text = "" + score.ToString( "n0" );
    	guiTextMult.text = "x" + multiplier.ToString( "n0" );
    	guiTextGrab.text = "Grab Radius: " + grabberRadius.ToString( "n1" );
    	guiTextLevel.text = "Level " + levelReached;
    	if(Menu.level >= 10)
    		guiTextBombs.text = "Bombs: " + bombCount;
  		else
    		guiTextBombs.text = null;
    	if(Menu.level >= 5)
    		guiTextShield.text = "Shields at " + shieldPercent.ToString( "n1" ) + "%";
   		else
    		guiTextShield.text = null;
 		if(Menu.level >= 20)
   	 		guiTextCooldown.text = cooldownTime.ToString( "n1" ) + " seconds until cooldown ready.";
   		else
    		guiTextCooldown.text = null;
   // 	var addScoreAmount = score - Menu.currentScore;
   // 	Menu.totalScore += addScoreAmount;
    	Menu.currentScore = score;
    	Menu.currentMult = multiplier;
    	Menu.currentEnemiesKilled = enemiesKilled;
    	Menu.currentGrabberRadius = grabberRadius;
    	Menu.currentLevel = levelReached;
    	Menu.currentPermGrab = permGrab;
    	while(permMult > 0) {
    		Menu.currentPermMult++;
    		permMult--;
    	}
    }
    else if(Menu.isTutorial) {
    	if(tutorial.showScore)
    		guiTextScore.text = "" + score.ToString( "n0" );
    	if(tutorial.showMultiplier)
    		guiTextMult.text = "x" + multiplier.ToString( "n0" );
    	if(tutorial.showGrab)
    		guiTextGrab.text = "Grab Radius: " + grabberRadius.ToString( "n1" );
    	if(tutorial.showLevel)
    		guiTextLevel.text = "Level " + levelReached;
    	Menu.currentScore = score;
    	Menu.currentMult = multiplier;
    	Menu.currentEnemiesKilled = enemiesKilled;
    	Menu.currentGrabberRadius = grabberRadius;
    	Menu.currentLevel = levelReached;
    }
    else {
    	guiTextScore.text = null;
    	guiTextMult.text = null;
    	guiTextGrab.text = null;
    	guiTextLevel.text = null;
    	guiTextBombs.text = null;
    	guiTextBombs.text = null;
    	guiTextShield.text = null;
    	guiTextShield.text = null;
   	 	guiTextCooldown.text = null;
    	guiTextCooldown.text = null;
    }
    if(Menu.newGame && !isReady) {
    	Debug.Log("(Menu.newGame && !isReady)");
		isReady = true;
		inGame = true;
		score = 0;
		enemiesKilled = 0;
		Menu.currentItemsFound = 0;
		levelReached = 1;
		permMult = 0;
		permGrab = 0;
		levelsGained = 0;
		multiplier = Menu.startingMultiplier + Menu.multiplierAddLeft + Menu.multiplierAddRight + Menu.multiplierAddLeftBumper + Menu.multiplierAddRightBumper + Menu.bombMultiplierAdd + Menu.boosterMultiplierAdd + Menu.multiplierAddShield + Menu.multiplierAddCooldown + Menu.multiplierAddFirstPassive + Menu.multiplierAddSecondPassive;
		grabberRadius = Menu.startingRadius + Menu.grabberAddLeft + Menu.grabberAddRight + Menu.grabberAddLeftBumper + Menu.grabberAddRightBumper + Menu.bombGrabberAdd + Menu.boosterGrabberAdd + Menu.grabberAddShield + Menu.grabberAddCooldown + Menu.grabberAddFirstPassive + Menu.grabberAddSecondPassive;
		Menu.addedLevel = 0;
		bombCount = Menu.bombProjectiles;
		shieldPercent = 100.0f;
		percentTick = 10 / Menu.speedShield;
		cooldownTime = Menu.speedCooldown;
		playerVar = shipControl.thisShip;
	}
	if(Menu.explosiveShield > 0 && enemyKillCheck < enemiesKilled) {
		enemyKillCheck++;
		if(shieldPercent < 100) {
			var percentChange : float = Menu.explosiveShield * .0001;
			shieldPercent += percentChange;
		}
	}
    if(playerVar == null) {
    	inGame = false;
    	score = 0;	
    }
}
