#pragma strict

var target: GameObject; // drag the target here
var speed: float = 250.0; // object speed
private var moving = false; // object initially stopped
private var ready = false;
private var dir: Vector3;
var explosion: GameObject;
var drop: GameObject;
var isShuttingDown = false;
var pointValue : int = 30;
var test = true;
var directionPointer: GameObject;
var xPosition : int = 0;
var	yPosition : int = 0;
var isHit = false;

var isReady = false;

var startingSpoty : float = 0;
var startingSpotx : float = 0;
var velocityRoll : float = 0;
var needsRestart = false;

function Start() {
	target = shipControl.thisShip;
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	
	yield WaitForSeconds (2.0);
	
	ready = true;
	velocityRoll = Random.Range(150f,200f);
	transform.rigidbody.velocity = transform.up * velocityRoll;
	for( ; ; ) {
		yield WaitForSeconds (1.0);
		if(test == false) {
			yield WaitForSeconds (1.0);
			test = true;
		}
	}
}

function FindPlayer() {
	target = shipControl.thisShip;
}

function FixedUpdate(){
	if(!Menu.timeStopped && needsRestart) {
		transform.rigidbody.velocity = transform.up * velocityRoll;
		needsRestart = false;
	}
	if(transform.position.z < 0) {
		transform.rigidbody.velocity = Vector3(dir.x, dir.y, 100);
	}
	else if(Menu.timeStopped) {
		transform.rigidbody.velocity = Vector3.zero;
		needsRestart = true;
	}
	else if (transform.position.z > 0) {
		transform.rigidbody.velocity = Vector3.zero;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		transform.position = Vector3(transform.position.x, transform.position.y, 0);
		isReady = true;
	}
	else
		isReady = true;
  	if(Menu.newGame)
  		Destroy(gameObject);
  /*	if((transform.position.x <= -495 || transform.position.x >= 495 || transform.position.y <= -495 || transform.position.y >= 495) && test == true) {
  		test = false;
  		transform.Rotate(0,0,180);
  		transform.rigidbody.velocity *= -1;
  	}  */
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Projectile" && !isShuttingDown && !isHit && transform.position.z > -10) {
		isHit = true;
		xPosition = transform.position.x;
		yPosition = transform.position.y;
		LootManager.LootRoll(xPosition + 2, yPosition + 2);  // Create three random drops
		LootManager.LootRoll(xPosition + 2, yPosition - 2);
		LootManager.LootRoll(xPosition, yPosition);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Diagnaler Killed you.");
	}
/*	if(collision.gameObject.tag == "Border" && !isShuttingDown) {
		transform.Rotate(0,0,180);
		test = false;
	} */
	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	}
}

