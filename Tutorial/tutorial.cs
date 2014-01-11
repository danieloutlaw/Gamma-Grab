using UnityEngine;
using System.Collections;


public class tutorial : MonoBehaviour {
	
	GameObject playerVar;
	bool isInstruction1 = false;
	bool isInstruction2 = false;
	bool isInstruction3 = false;
	bool isInstruction4 = false;
	bool isInstruction5 = false;
	bool isInstruction6 = false;
	bool isInstruction7 = false;
	bool isInstruction8 = false;
	bool isInstruction9 = false;
	bool isInstruction10 = false;
	bool isInstruction11 = false;
	bool isInstruction12 = false;
	bool isInstruction13 = false;
	bool isInstruction14 = false;
	bool isInstruction15 = false;
	
	public static bool showScore = false;
	public static bool showMultiplier = false;
	public static bool showLevel = false;
	public static bool showExperience = false;
	public static bool showGrab = false;
	
	bool gotoDone = false;
	bool deathInstruct = false;
	
	gotospot mySpot1;
	gotospot mySpot2;
	gotospot mySpot3;
	gotospot mySpot4;
	
	GameObject goToSpot1;
	GameObject goToSpot2;
	GameObject goToSpot3;
	GameObject goToSpot4;
	
	GameObject enemy1;
	GameObject enemy2;
	GameObject enemy3;
	GameObject enemy4;
	
	bool wave1Spawned = false;
	bool wave2Spawned = false;
	bool wave3Spawned = false;
	bool wave4Spawned = false;
	bool wave5Spawned = false;
	
	public GameObject goToSpot;
	public GameObject enemyType1;
	public GameObject enemyType2;
	public GameObject enemyType3;
	public GameObject enemyType4;
	public GameObject enemyType5;
	public GameObject enemyType6;
	
	private Vector3 velocity1 = Vector3.zero;
	private Vector3 velocity2 = Vector3.zero;
	private Vector3 velocity3 = Vector3.zero;
	private Vector3 velocity4 = Vector3.zero;
	
	
	public GUIStyle instruction;
	public GUIStyle textBox;
	
	void Update () {
		if(mySpot1 && mySpot2 && mySpot3 && mySpot4) {
			if(mySpot1.isTouched && mySpot2.isTouched && mySpot3.isTouched && mySpot4.isTouched && !gotoDone) {
				Vector3 targetPosition = new Vector3(0,0,0);
				goToSpot1.transform.position = Vector3.SmoothDamp(goToSpot1.transform.position, targetPosition, ref velocity1, .15f);
				goToSpot2.transform.position = Vector3.SmoothDamp(goToSpot2.transform.position, targetPosition, ref velocity2, .15f);
				goToSpot3.transform.position = Vector3.SmoothDamp(goToSpot3.transform.position, targetPosition, ref velocity3, .15f);
				goToSpot4.transform.position = Vector3.SmoothDamp(goToSpot4.transform.position, targetPosition, ref velocity4, .15f);
			}
			if(goToSpot1.transform.position == goToSpot2.transform.position) {
				gotoDone = true;
				goToSpot1.transform.rigidbody.velocity = new Vector3(0,0,-200);
				goToSpot2.transform.rigidbody.velocity = new Vector3(0,0,-200);
				goToSpot3.transform.rigidbody.velocity = new Vector3(0,0,-200);
				goToSpot4.transform.rigidbody.velocity = new Vector3(0,0,-200);
				isInstruction1 = false;
				isInstruction2 = false;
				
			}
		}
	}
	
	void OnGUI() {
		if(isInstruction1)
			GUI.Button(new Rect(Screen.width/2, 50, 0, 25), "Move with the left joystick.", instruction);
		if(isInstruction2) 
			GUI.Button(new Rect(Screen.width/2, 80, 0, 25), "Move to each of the white circles.", instruction);
		if(isInstruction3) {
			GUI.Button(new Rect(Screen.width/2, 50, 0, 25), "Load a weapon by holding down either the left or right trigger.", instruction);
			GUI.Button(new Rect(Screen.width/2, 80, 0, 25), "With the trigger held down, point at our target with the right joystick.", instruction);
		}
		if(isInstruction4) {
			GUI.Button(new Rect(Screen.width/2, 110, 0, 25), "Kill all four paricles.", instruction);
			GUI.Button(new Rect(Screen.width/2, 140, 0, 25), "Notice our score in the lower left corner.", instruction);
		}
		if(isInstruction5) {
			GUI.Button(new Rect(Screen.width/2, 50, 0, 25), "Kill the next four particles and collect their essence.", instruction);	
			GUI.Button(new Rect(Screen.width/2, 80, 0, 25), "Their essence will increase our multiplier.", instruction);
			GUI.Button(new Rect(Screen.width/2, 110, 0, 25), "Points for new kills will be multiplied by this amount, shown in the lower right corner.", instruction);
		}
		if(isInstruction6) {
			GUI.Button(new Rect(Screen.width/2, 50, 0, 25), "Some enemies drop red essence.", instruction);	
			GUI.Button(new Rect(Screen.width/2, 80, 0, 25), "These will increase the radius from our center that can grab essence.", instruction);
			GUI.Button(new Rect(Screen.width/2, 110, 0, 25), "We can see our grab radius in the top right corner.", instruction);
		}
		if(isInstruction7) {
			GUI.Button(new Rect(Screen.width/2, 50, 0, 25), "The essence will last until we are forced into another dimension.", instruction);	
			GUI.Button(new Rect(Screen.width/2, 80, 0, 25), "Some particles contain canisters of pure essence that will travel with us through dimensions.", instruction);
			GUI.Button(new Rect(Screen.width/2, 110, 0, 25), "Collecting these will permanently increase our multiplier or grab radius.", instruction);
		}
		if(isInstruction8) {
			GUI.Button(new Rect(Screen.width/2, 140, 0, 25), "Some particles will contain essence that can be used for upgrades.", instruction);	
			GUI.Button(new Rect(Screen.width/2, 170, 0, 25), "These upgrades are square shaped. Pick up the two firepower upgrades.", instruction);
		}
		if(isInstruction9) {
			GUI.Button(new Rect(Screen.width/2, 50, 0, 25), "We can only upgrade while we are between dimensions.", instruction);	
			GUI.Button(new Rect(Screen.width/2, 80, 0, 25), "Any interaction with a particle will result in immediate banishment from the current dimension.", instruction);
			GUI.Button(new Rect(Screen.width/2, 110, 0, 25), "There are infinite dimensions. Accept your fate.", instruction);
		}
		if(isInstruction10) {
			GUI.Button(new Rect(50, Screen.height - 125, Screen.width - 100, 120), "", textBox);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 100, 0, 25), "We can see how well we harvested a dimension after we have been banished.", instruction);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 70, 0, 25), "Hit the top button (Y on an X-box controller) to see our inventory.", instruction);
			GUI.Button(new Rect(Screen.width/2, Screen.height - 40, 0, 25), "There we will see our upgrades.", instruction);
		}
		if(isInstruction11) {
			GUI.Button(new Rect(50, Screen.height - 155, Screen.width - 100, 150), "", textBox);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 130, 0, 25), "On the left side we see the items that are equipped.", instruction);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 100, 0, 25), "On the right side we see the items in our inventory.", instruction);
			GUI.Button(new Rect(Screen.width/2, Screen.height - 70, 0, 25), "To swap items, scroll over one item and hit the bottom button (A on an X-box controller).", instruction);
			GUI.Button(new Rect(Screen.width/2, Screen.height - 40, 0, 25), "Then go to the other side and hit the bottom button again over the item you want to swap.", instruction);
		}
		if(isInstruction12) {
			GUI.Button(new Rect(50, Screen.height - 125, Screen.width - 100, 90), "", textBox);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 100, 0, 25), "We start with one basic type of dimension we can enter, but others will be unlocked.", instruction);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 70, 0, 25), "Hit New Game to enter.", instruction);
		}
		if(isInstruction13) {
			GUI.Button(new Rect(50, Screen.height - 125, Screen.width - 100, 90), "", textBox);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 100, 0, 25), "This concludes the tutorial.", instruction);	
			GUI.Button(new Rect(Screen.width/2, Screen.height - 70, 0, 25), "There is no limit to the amount of power we can achieve.", instruction);
		}
		
	}
	
	IEnumerator Start () {
		playerVar = GameObject.FindWithTag ("PlayerShip");
		
		// Audio instructions and text instructions
		yield return new WaitForSeconds(2.0f);
		isInstruction1 = true;
		yield return new WaitForSeconds(1.0f);

		isInstruction2 = true;
		goToSpot1 = Instantiate(goToSpot, new Vector3(playerVar.transform.position.x + 100,playerVar.transform.position.y,0), Quaternion.identity) as GameObject;
		goToSpot2 = Instantiate(goToSpot, new Vector3(playerVar.transform.position.x - 100,playerVar.transform.position.y,0), Quaternion.identity) as GameObject;
		goToSpot3 = Instantiate(goToSpot, new Vector3(playerVar.transform.position.x ,playerVar.transform.position.y + 100,0), Quaternion.identity) as GameObject;
		goToSpot4 = Instantiate(goToSpot, new Vector3(playerVar.transform.position.x ,playerVar.transform.position.y - 100,0), Quaternion.identity) as GameObject;		
		
		mySpot1 = goToSpot1.GetComponent(typeof(gotospot)) as gotospot;
		mySpot2 = goToSpot2.GetComponent(typeof(gotospot)) as gotospot;
		mySpot3 = goToSpot3.GetComponent(typeof(gotospot)) as gotospot;
		mySpot4 = goToSpot4.GetComponent(typeof(gotospot)) as gotospot;
		
		for( ; ; ) {
			if(gotoDone && !wave1Spawned) {
				yield return new WaitForSeconds(2.0f);
				wave1Spawned = true;
				isInstruction3 = true;
				showScore = true;
				enemy1 = Instantiate(enemyType1, new Vector3(playerVar.transform.position.x + 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy2 = Instantiate(enemyType1, new Vector3(playerVar.transform.position.x - 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy3 = Instantiate(enemyType1, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y + 100,-200), Quaternion.identity) as GameObject;
				enemy4 = Instantiate(enemyType1, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y - 100,-200), Quaternion.identity) as GameObject;

				yield return new WaitForSeconds(3.0f);
				isInstruction4 = true;
			}
			if(wave1Spawned && enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null && !wave2Spawned) {
				wave2Spawned = true;
				showMultiplier = true;
				isInstruction3 = false;
				isInstruction4 = false;
				isInstruction5 = true;
				enemy1 = (GameObject)Instantiate(enemyType2, new Vector3(playerVar.transform.position.x + 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy2 = (GameObject)Instantiate(enemyType2, new Vector3(playerVar.transform.position.x - 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy3 = (GameObject)Instantiate(enemyType2, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y + 100,-200), Quaternion.identity) as GameObject;
				enemy4 = (GameObject)Instantiate(enemyType2, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y - 100,-200), Quaternion.identity) as GameObject;
			}
			if(Menu.currentMult == 9 && !wave3Spawned) {
				wave3Spawned = true;
				isInstruction5 = false;
				isInstruction6 = true;
				showGrab = true;
				enemy1 = Instantiate(enemyType2, new Vector3(playerVar.transform.position.x + 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy2 = Instantiate(enemyType2, new Vector3(playerVar.transform.position.x - 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy3 = Instantiate(enemyType2, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y + 100,-200), Quaternion.identity) as GameObject;
				enemy4 = Instantiate(enemyType3, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y - 100,-200), Quaternion.identity) as GameObject;
			}
			if(Menu.currentGrabberRadius == 21.0f && !wave4Spawned) {
				wave4Spawned = true;
				isInstruction6 = false;
				isInstruction7 = true;
				yield return new WaitForSeconds(4.0f);
				isInstruction8 = true;
				enemy1 = Instantiate(enemyType4, new Vector3(playerVar.transform.position.x + 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy2 = Instantiate(enemyType2, new Vector3(playerVar.transform.position.x - 100,playerVar.transform.position.y,-200), Quaternion.identity) as GameObject;
				enemy3 = Instantiate(enemyType5, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y + 100,-200), Quaternion.identity) as GameObject;
				enemy4 = Instantiate(enemyType2, new Vector3(playerVar.transform.position.x, playerVar.transform.position.y - 100,-200), Quaternion.identity) as GameObject;
			}
			if(wave4Spawned && Menu.currentItemsFound == 2 && !wave5Spawned){
				wave5Spawned = true;
				isInstruction7 = false;
				isInstruction8 = false;
				isInstruction9 = true;
				yield return new WaitForSeconds(3.5f);
			}
			if(wave5Spawned && !Menu.playerDeath) {
				float dropSpotX = 0;
				float dropSpotY = 0;
				for(int i = 0; i <= 20; i++) {
					dropSpotX = Random.Range (-400,400);
					dropSpotY = Random.Range (-400,400);
					enemy1 = Instantiate(enemyType6, new Vector3(dropSpotX,dropSpotY,-200), Quaternion.identity) as GameObject;
				}
				yield return new WaitForSeconds(0.2f);
			}
			if(Menu.playerDeath && !deathInstruct && isInstruction9) {
				deathInstruct = true;
				isInstruction9 = false;
				isInstruction10 = true;
			}
			if(deathInstruct && isInstruction10 && Menu.showInventory) {
				isInstruction10 = false;
				isInstruction11 = true;
			}
			else {
				isInstruction11 = false;
			}
			if(isInstruction11 && Menu.showMenu) {
				isInstruction11 = false;
				isInstruction12 = true;
			}
			if(Menu.newGame) {
				isInstruction12 = false;
				isInstruction13 = true;
				yield return new WaitForSeconds(8.0f);
				isInstruction13 = false;
				Menu.isTutorial = false;
				Destroy(gameObject);
			}
			yield return new WaitForSeconds(0.01f);
		}
		
		
	//	isInstruction1 = false;
		
		
	}
	
}