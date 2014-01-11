using UnityEngine;
using System.Collections;

public class MasterSpawner : MonoBehaviour {
	public GameObject playerVar;
	public GameObject[] enemies = new GameObject[11];
	bool[] areActive = new bool[11];
	bool[] canGo = new bool[11];
	float[] enemyRolls = new float[11];
	int[] enemyTimeTicks = new int[11];
	float[] timeDelay = new float[11];
	
	public GameObject special;
	int timeTillSpecial;
	bool specialOut = false;
	bool difficultySet = false;
	float difficultyMult = 0;
	int specialTicks;
	UnityThreading.ActionThread myThread;
	
	void Update () {
		if(Menu.playerDeath)
  			Destroy(gameObject); 
	}
	
	IEnumerator Start () {
		playerVar = ShipControlCS.thisShip;
		
		timeTillSpecial = Random.Range (500,6500);
		specialTicks = 0;
		specialOut = false;
		
		areActive[0] = false;
		areActive[1] = false;
		areActive[2] = false;
		areActive[3] = false;
		areActive[4] = false;
		areActive[5] = false;
		areActive[6] = false;
		areActive[7] = false;
		areActive[8] = false;
		areActive[9] = false;
		areActive[10] = false;
		
		canGo[0] = true;
		canGo[1] = true;
		canGo[2] = true;
		canGo[3] = true;
		canGo[4] = true;
		canGo[5] = true;
		canGo[6] = true;
		canGo[7] = true;
		canGo[8] = true;
		canGo[9] = true;
		canGo[10] = true;
		
		int firstChoice = 0;
		int secondChoice = 0;
		int thirdChoice = 0;
		int fourthChoice = 0;
		int fifthChoice = 0;
		int sixthChoice = 0;
		int seventhChoice = 0;
		int eighthChoice = 0;
		int ninthChoice = 0;
		int tenthChoice = 0;
		int eleventhChoice = 0;
		
		if(!Menu.singleGame) {
		enemyRolls[0] = Random.Range(0,1000);	// Seeker
		enemyRolls[1] = Random.Range(0,1000);	// Launcher
		enemyRolls[2] = Random.Range(0,1000);	//
		enemyRolls[3] = Random.Range(0,1000);	//
		enemyRolls[4] = Random.Range(0,1000);	//
		enemyRolls[5] = Random.Range(0,1000);	//
		enemyRolls[6] = -1; 					// Quad Enemy
		enemyRolls[7] = -1; 					// Shielder
		enemyRolls[8] = Random.Range(0,1000);   //
		enemyRolls[9] = Random.Range(0,1000);	// Poofer
		enemyRolls[10] = Random.Range(0,1000);	// StraightlineShifted
		
		float biggest = 0;
	
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				firstChoice = i;
			}
		enemyRolls[firstChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++) 
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				secondChoice = i;
			}
		enemyRolls[secondChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				thirdChoice = i;
			}
		enemyRolls[thirdChoice] = 0;
		biggest = 0;
		enemyRolls[6] = Random.Range(0,1000);
		enemyRolls[7] = Random.Range(0,1000);
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				fourthChoice = i;
			}
		enemyRolls[fourthChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				fifthChoice = i;
			}
		enemyRolls[fifthChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				sixthChoice = i;
			}
		enemyRolls[sixthChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				seventhChoice = i;
			}
		enemyRolls[seventhChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				eighthChoice = i;
			}
		enemyRolls[eighthChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				ninthChoice = i;
			}
		enemyRolls[ninthChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				tenthChoice = i;
			}
		enemyRolls[tenthChoice] = 0;
		biggest = 0;
		for(int i = 0; i <= 10; i++)
			if(enemyRolls[i] > biggest) {
				biggest = enemyRolls[i];
				eleventhChoice = i;
			}
		enemyRolls[eleventhChoice] = 0;
		biggest = 0;
		
		areActive[firstChoice] = true;
		areActive[secondChoice] = true;
		if(Menu.difficulty >= 3)
			areActive[eleventhChoice] = true;
		if(Menu.difficulty >= 4)
			areActive[tenthChoice] = true;
		}
		if(Menu.singleGame)
			areActive[Menu.singleSelection] = true;
		yield return new WaitForSeconds (0.5f);
		
		for( ; ; ) {	if(playerVar) {		
			if(areActive[0] && canGo[0]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0;  	
				xRoll = Random.Range (-400f, 400f);
				yRoll = Random.Range (-400f, 400f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				int num = 1;
				int spot = 1;
				bool isX = true;
				bool isP = true;
				int enemyCap = Menu.currentLevel * 2;
				for(int i = 0; i < enemyCap; i++) {
					GameObject seeker01 = Instantiate(enemies[0], placement, Quaternion.identity) as GameObject;
					if(isX && isP) {
						placement.x += 16;
						if(spot >= num) {
							isX = false;
							isP = false;
							spot = 0;
						}
					}
					else if(!isX && !isP) {
						placement.y -= 16;
						if(spot >= num) {
							isX = true;
							spot = 0;
							num++;
						}
					}
					else if(isX && !isP) {
						placement.x -= 16;
						if(spot >= num) {
							isX = false;
							isP = true;
							spot = 0;
						}
					}
					else if(!isX && isP) {
						placement.y += 16;
						if(spot >= num) {
							isX = true;
							num++;
							spot = 0;
						}
					}
					spot++;
				}
				canGo[0] = false;
			}
			
			if(areActive[1] && canGo[1]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0;  	
				
				xRoll = Random.Range (-400f, 400f);
				yRoll = Random.Range (-400f, 400f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				int num = 1;
				int spot = 1;
				bool isX = true;
				bool isP = true;
				int enemyCap = Menu.currentLevel;
				for(int i = 0; i <= enemyCap; i++) {
					GameObject seeker01 = Instantiate(enemies[1], placement, Quaternion.identity) as GameObject;
					if(isX && isP) {
						placement.x += 16;
						if(spot >= num) {
							isX = false;
							isP = false;
							spot = 0;
						}
					}
					else if(!isX && !isP) {
						placement.y -= 16;
						if(spot >= num) {
							isX = true;
							spot = 0;
							num++;
						}
					}
					else if(isX && !isP) {
						placement.x -= 16;
						if(spot >= num) {
							isX = false;
							isP = true;
							spot = 0;
						}
					}
					else if(!isX && isP) {
						placement.y += 16;
						if(spot >= num) {
							isX = true;
							num++;
							spot = 0;
						}
					}
					spot++;
				}
				canGo[1] = false;
			}
			
			if(areActive[2] && canGo[2]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 	
				
				xRoll = Random.Range (-480f, 480f);
				yRoll = Random.Range (-480f, 480f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler01 = Instantiate(enemies[2], placement, Quaternion.identity) as GameObject;
				canGo[2] = false;
			}
			
			if(areActive[3] && canGo[3]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 	
				
				xRoll = Random.Range (-480f, 480f);
				yRoll = Random.Range (-480f, 480f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler02 = Instantiate(enemies[3], placement, Quaternion.identity) as GameObject;
				canGo[3] = false;
			}
			
			if(areActive[4] && canGo[4]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				
				xRoll = Random.Range (-480f, 480f);
				yRoll = Random.Range (-480f, 480f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler02 = Instantiate(enemies[4], placement, Quaternion.identity) as GameObject;
				canGo[4] = false;				
			}
			
			if(areActive[5] && canGo[5]) {
				int xOrY = Random.Range (0, 10);
				int leftOrRight = Random.Range (0, 10);
				int downOrUp = Random.Range (0, 10);
				bool goingDown = true;
				if(downOrUp < 5)
					goingDown = false;
				int highCap = 450 - (Menu.currentLevel * 16);
				int lowCap = -450 + (Menu.currentLevel * 16);
				float roll = Random.Range (lowCap, highCap);
				if(roll > 0)
					leftOrRight = 0;
				else
					leftOrRight = 10;
				if(xOrY > 5) {
					if(leftOrRight > 5) {
						Vector3 placement = new Vector3(roll, -480, -150);
						Vector3 startingPlacement = placement;
    					for(int i = -4; i <= Menu.currentLevel; i++) {
							GameObject straightLine01 = Instantiate(enemies[5], placement, Quaternion.identity) as GameObject;
							if(placement.x <= 484 && !goingDown)
								placement.x += 16;
							else if(!goingDown) {
								placement.x = startingPlacement.x - 16;
								goingDown = true;
							}
							else if(placement.x >= -484 && goingDown) 
								placement.x -= 16;
							else {
								placement.x = startingPlacement.x + 16;
								goingDown = false;
							}
						}
					}
					else {
						Vector3 placement = new Vector3(roll, 480, -150);
						Vector3 startingPlacement = placement;
    					for(int i = -4; i <= Menu.currentLevel; i++) {
							GameObject straightLine01 = Instantiate(enemies[5], placement, Quaternion.identity) as GameObject;
							if(placement.x <= 484 && !goingDown)
								placement.x += 16;
							else if(!goingDown) {
								placement.x = startingPlacement.x - 16;
								goingDown = true;
							}
							else if(placement.x >= -484 && goingDown) 
								placement.x -= 16;
							else {
								placement.x = startingPlacement.x + 16;
								goingDown = false;
							}
						}
					}
				}
				else {
					if(leftOrRight > 5) {
						Vector3 placement = new Vector3(-480, roll, -150);
						Vector3 startingPlacement = placement;
    					for(int i = -4; i <= Menu.currentLevel; i++) {
							GameObject straightLine01 = Instantiate(enemies[5], placement, Quaternion.identity) as GameObject;
							straightLine01.transform.Rotate(0,0,90);
							if(placement.y <= 484 && !goingDown)
								placement.y += 16;
							else if(!goingDown) {
								placement.y = startingPlacement.y - 16;
								goingDown = true;
							}
							else if(placement.y >= -484 && goingDown) 
								placement.y -= 16;
							else {
								placement.y = startingPlacement.y + 16;
								goingDown = false;
							}
						}
					}
					else {
						Vector3 placement = new Vector3(480, roll, -150);
						Vector3 startingPlacement = placement;
    					for(int i = -4; i <= Menu.currentLevel; i++) {
							GameObject straightLine01 = Instantiate(enemies[5], placement, Quaternion.identity) as GameObject;
							straightLine01.transform.Rotate(0,0,90);
							if(placement.y <= 484 && !goingDown)
								placement.y += 16;
							else if(!goingDown) {
								placement.y = startingPlacement.y - 16;
								goingDown = true;
							}
							else if(placement.y >= -484 && goingDown) 
								placement.y -= 16;
							else {
								placement.y = startingPlacement.y + 16;
								goingDown = false;
							}
						}
					}
				}
				canGo[5] = false;
			}
			if(areActive[6] && canGo[6]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				
				xRoll = Random.Range (-440f, 440f);
				yRoll = Random.Range (-440f, 440f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler02 = Instantiate(enemies[6], placement, Quaternion.identity) as GameObject;
				canGo[6] = false;				
			}
			if(areActive[7] && canGo[7]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				
				xRoll = Random.Range (-420f, 420f);
				yRoll = Random.Range (-420f, 420f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler02 = Instantiate(enemies[7], placement, Quaternion.identity) as GameObject;
				canGo[7] = false;				
			}
			if(areActive[8] && canGo[8]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				
				xRoll = Random.Range (-480f, 480f);
				yRoll = Random.Range (-480f, 480f);
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler02 = Instantiate(enemies[8], placement, Quaternion.identity) as GameObject;
				canGo[8] = false;				
			}
			if(areActive[9] && canGo[9]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				
				xRoll = Random.Range (-480f, 480f);
				yRoll = Random.Range (-480f, 480f);				
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject crawler02 = Instantiate(enemies[9], placement, Quaternion.identity) as GameObject;
				canGo[9] = false;				
			}
			if(areActive[10] && canGo[10]) {
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				
				xRoll = Random.Range (-300f, 300f);
				yRoll = Random.Range (-300f, 300f);				
				
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				int directionRoll = Random.Range (0,10);
				int amount = (Menu.currentLevel / 2) + 1;
				for(int i = 0; i <= amount; i++) {
					GameObject straightlineshifted02 = Instantiate(enemies[10], placement, Quaternion.identity) as GameObject;
					if(placement.x < -480 || placement.x > 480 || placement.y < -480 || placement.y > 480)
							directionRoll = directionRoll % 10;
					if(directionRoll > 5) {
						straightlineshifted02.transform.Rotate(0,0,45);
						if(xRoll < 0) {
							placement.x += 10;
							placement.y += 10;
						}
						else {
							placement.x -= 10;
							placement.y -= 10;
						}
					}
					else {
						straightlineshifted02.transform.Rotate(0,0,-45);
						if(xRoll < 0) {
							placement.x += 10;
							placement.y -= 10;
						}
						else {
							placement.x -= 10;
							placement.y += 10;
						}
					}
				}
				canGo[10] = false;				
			}
			
			if(specialTicks >= timeTillSpecial && !specialOut) {
				specialOut = true;
				specialTicks = 0;
				timeTillSpecial = Random.Range (500,7000);
				
				int highLow = 0;
    			float xRoll = 0;
    			float yRoll = 0; 
    			float waitTime = 0.0f;  	
				if(Random.Range(0,10) > 5) {
					xRoll = Random.Range(-200,0);
					xRoll += playerVar.transform.position.x;
				}
				else {
					xRoll = Random.Range(0,200);
					xRoll += playerVar.transform.position.x;
				} 
				if(Random.Range(0,10) > 5) {
					yRoll = Random.Range(-200,0);
					yRoll += playerVar.transform.position.y; 
				}
				else {
					yRoll = Random.Range(0,200);
					yRoll += playerVar.transform.position.y;
				}
				if( xRoll >= 430 )
					xRoll = Random.Range(300,430);
				else if( xRoll <= -430 )
					xRoll = Random.Range(-430,-300);
				if ( yRoll >= 430 )
					yRoll = Random.Range(300,430);
				else if( yRoll <= -430)
					yRoll = Random.Range(-430,-300);
				Vector3 placement = new Vector3(xRoll, yRoll, -150);
				GameObject special1 = Instantiate(special, placement, Quaternion.identity) as GameObject;
			}
			
			if(!Menu.singleGame) {
				if(Menu.currentLevel >= 5)
					areActive[thirdChoice] = true;
				if(Menu.currentLevel >= 10)
					areActive[fourthChoice] = true;
				if(Menu.currentLevel >= 15)
					areActive[fifthChoice] = true;
				if(Menu.currentLevel >= 20)
					areActive[sixthChoice] = true;
				if(Menu.currentLevel >= 25)
					areActive[seventhChoice] = true;
				if(Menu.currentLevel >= 30)
					areActive[eighthChoice] = true;
				if(Menu.currentLevel >= 35)
					areActive[ninthChoice] = true;
				if(Menu.currentLevel >= 40)
					areActive[tenthChoice] = true;
				if(Menu.currentLevel >= 45)
					areActive[eleventhChoice] = true;
			}
			}
			yield return new WaitForSeconds (.01f);
			enemyTimeTicks[0]++;
			enemyTimeTicks[1]++;
			enemyTimeTicks[2]++;
			enemyTimeTicks[3]++;
			enemyTimeTicks[4]++;
			enemyTimeTicks[5]++;
			enemyTimeTicks[6]++;
			enemyTimeTicks[7]++;
			enemyTimeTicks[8]++;
			enemyTimeTicks[9]++;
			enemyTimeTicks[10]++;
			specialTicks++;
		}
		
	}
	
	void FixedUpdate() {
		myThread = UnityThreadHelper.CreateThread(DoThreadWork);
	}
	
	void DoThreadWork() {
		if(!difficultySet) {
			difficultyMult= Menu.difficulty * 0.5f;
			if(Menu.singleGame) {
				difficultyMult *= 4;
			if(Menu.currentLevel >= 5)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 10)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 15)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 20)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 25)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 30)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 35)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 40)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 45)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 50)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 55)
				difficultyMult *= 2f;
			if(Menu.currentLevel >= 60)
				difficultyMult *= 2f;
			}
			difficultySet = true;
		}
		timeDelay[0] = (375 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[0] >= timeDelay[0]) {
			canGo[0] = true;
			enemyTimeTicks[0] = 0;
		}
		timeDelay[1] = (350 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[1] >= timeDelay[1]) {
			canGo[1] = true;
			enemyTimeTicks[1] = 0;
		}
		timeDelay[2] = (50 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[2] >= timeDelay[2]) {
			canGo[2] = true;
			enemyTimeTicks[2] = 0;
		}
		timeDelay[3] = (205 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[3] >= timeDelay[3]) {
			canGo[3] = true;
			enemyTimeTicks[3] = 0;
		}
		timeDelay[4] = (175 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[4] >= timeDelay[4]) {
			canGo[4] = true;
			enemyTimeTicks[4] = 0;
		}
		timeDelay[5] = (350 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[5] >= timeDelay[5]) {
			canGo[5] = true;
			enemyTimeTicks[5] = 0;
		}
		timeDelay[6] = (325 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[6] >= timeDelay[6]) {
			canGo[6] = true;
			enemyTimeTicks[6] = 0;
		}
		timeDelay[7] = (325 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[7] >= timeDelay[7]) {
			canGo[7] = true;
			enemyTimeTicks[7] = 0;
		}
		timeDelay[8] = (150 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[8] >= timeDelay[8]) {
			canGo[8] = true;
			enemyTimeTicks[8] = 0;
		}
		timeDelay[9] = (120 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[9] >= timeDelay[9]) {
			canGo[9] = true;
			enemyTimeTicks[9] = 0;
		}
		timeDelay[10] = (200 / Mathf.Sqrt (Menu.currentLevel + 2)) / difficultyMult;
		if(enemyTimeTicks[10] >= timeDelay[10]) {
			canGo[10] = true;
			enemyTimeTicks[10] = 0;
		}
	}
}