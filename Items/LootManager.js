#pragma strict

static var dropMultiplier : GameObject;
static var dropBasicWeapon : GameObject;
static var dropGrabber : GameObject;
static var permDropMultiplier : GameObject;
static var permDropGrabber : GameObject;

static var clearTime : int = 6;

function start()
{

}

static public function LootMultiplier(xPosition, yPosition) {
	dropMultiplier = Instantiate(Resources.Load("DropMultiplier"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
	dropMultiplier.transform.rigidbody.velocity.x = Random.Range(-16,16) * Time.deltaTime * 60;
   	dropMultiplier.transform.rigidbody.velocity.y = Random.Range(-16,16) * Time.deltaTime * 60;
}

static public function LootGrabber(xPosition, yPosition) {
	dropMultiplier = Instantiate(Resources.Load("DropGrabber"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
	dropMultiplier.transform.rigidbody.velocity.x = Random.Range(-16,16) * Time.deltaTime * 60;
   	dropMultiplier.transform.rigidbody.velocity.y = Random.Range(-16,16) * Time.deltaTime * 60;
}

static public function LootRedline(xPosition, yPosition) {
	dropMultiplier = Instantiate(Resources.Load("IntroRedline"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
	dropMultiplier.transform.rigidbody.velocity.x = Random.Range(-16,16) * Time.deltaTime * 60;
   	dropMultiplier.transform.rigidbody.velocity.y = Random.Range(-16,16) * Time.deltaTime * 60;
}

static public function LootYellowArrow(xPosition, yPosition) {
	dropMultiplier = Instantiate(Resources.Load("IntroYellowArrow"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
	dropMultiplier.transform.rigidbody.velocity.x = Random.Range(-16,16) * Time.deltaTime * 60;
   	dropMultiplier.transform.rigidbody.velocity.y = Random.Range(-16,16) * Time.deltaTime * 60;
}

static public function LootRoll(xPosition, yPosition) {
	var lootRoll1 : int = Random.Range(0,10000);
	if(lootRoll1 > 150) {
		dropMultiplier = Instantiate(Resources.Load("DropMultiplier"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
		dropMultiplier.transform.rigidbody.velocity.x = Random.Range(-16,16) * Time.deltaTime * 60;
   	 	dropMultiplier.transform.rigidbody.velocity.y = Random.Range(-16,16) * Time.deltaTime * 60;
		yield WaitForSeconds (clearTime);
		Destroy(dropMultiplier.gameObject);
	}
	else if(lootRoll1 > 80) {
		dropGrabber = Instantiate(Resources.Load("DropGrabber"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
		dropGrabber.transform.rigidbody.velocity.x = Random.Range(-16,16) * Time.deltaTime * 60;
   	 	dropGrabber.transform.rigidbody.velocity.y = Random.Range(-16,16) * Time.deltaTime * 60;
		yield WaitForSeconds (clearTime);
		Destroy(dropMultiplier.gameObject);
	}
	else if(lootRoll1 > 60) {
		permDropMultiplier = Instantiate(Resources.Load("DropPermanentMultiplier"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
		permDropMultiplier.transform.rigidbody.velocity.x = Random.Range(-32,32 * Time.deltaTime * 60);
   	 	permDropMultiplier.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
		yield WaitForSeconds (clearTime + 4);
		Destroy(permDropMultiplier.gameObject);
	}
	else if(lootRoll1 > 50) {
		permDropGrabber = Instantiate(Resources.Load("DropPermanentGrabber"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
		permDropGrabber.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
   	 	permDropGrabber.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
		yield WaitForSeconds (clearTime + 4);
		Destroy(permDropGrabber.gameObject);
	}
	else {
		clearTime += 6;
		
		/*
		dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
    			*/
    			
		var itemRoll1 : int = Random.Range(0,1000);
		if(Menu.level < 5) {
			if(itemRoll1 >= 400) {
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else {
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
		}
		else if(Menu.level < 10) {
			if(itemRoll1 >= 700) {
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if(itemRoll1 >= 350) {
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 150) {
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else {
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
		}
		else if(Menu.level < 15){
			if(itemRoll1 >= 800) {
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if(itemRoll1 >= 650) {
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 500) {
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 400) {
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 200) {
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 100) {
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else {
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
		}
		else if(Menu.level < 20){
			if(itemRoll1 >= 800) {
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if(itemRoll1 >= 600) {
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 450) {
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 350) {
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 200) {
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 133) {
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 66){
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else {
				dropBasicWeapon = Instantiate(Resources.Load("BoosterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
		}
		else if(Menu.level < 35) {
			if(itemRoll1 >= 800) {
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if(itemRoll1 >= 600) {
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 450) {
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 350) {
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 200) {
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 150) {
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 100){
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 50){
				dropBasicWeapon = Instantiate(Resources.Load("BoosterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else {
				dropBasicWeapon = Instantiate(Resources.Load("CooldownDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
		}
		else {
			if(itemRoll1 >= 800) {
				dropBasicWeapon = Instantiate(Resources.Load("YellowTriangleDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if(itemRoll1 >= 600) {
				dropBasicWeapon = Instantiate(Resources.Load("RedLineDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 450) {
				dropBasicWeapon = Instantiate(Resources.Load("RepeaterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 350) {
				dropBasicWeapon = Instantiate(Resources.Load("DozerDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 250) {
				dropBasicWeapon = Instantiate(Resources.Load("BoomerangDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 200) {
				dropBasicWeapon = Instantiate(Resources.Load("ShieldDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 150) {
				dropBasicWeapon = Instantiate(Resources.Load("BombDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 100) {
				dropBasicWeapon = Instantiate(Resources.Load("BoosterDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else if (itemRoll1 >= 50) {
				dropBasicWeapon = Instantiate(Resources.Load("CooldownDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
			else {
				dropBasicWeapon = Instantiate(Resources.Load("PassiveDrop"), Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				dropBasicWeapon.transform.rigidbody.velocity.x = Random.Range(-32,32) * Time.deltaTime * 60;
    			dropBasicWeapon.transform.rigidbody.velocity.y = Random.Range(-32,32) * Time.deltaTime * 60;
			}
		} 
		yield WaitForSeconds (clearTime);
		Destroy(dropBasicWeapon.gameObject);
	}
	//	GenerateItem(xPosition, yPosition);
		
}
