#pragma strict

var target: GameObject; // drag the target here
var speed: float = 60.0; // object speed
private var moving = false; // object initially stopped
private var ready = false;
private var dir: Vector3;
var explosion: GameObject;
var drop: GameObject;
var isShuttingDown = false;
var pointValue : int = 10;
var xPosition : int = 0;
var yPosition : int = 0;
var isHit = false;

var isReady = false;

var startingSpoty : float = 0;
var startingSpotx : float = 0;
var velocityRollX : float = 0;
var velocityRollY : float = 0;
var needsRestart = false;

var targetSpot : Vector3;

function Start() {
	target = GameObject.FindWithTag ("PlayerShip");
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;

	transform.position = Vector3(startingSpotx, startingSpoty, -200);
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	targetSpot.x = transform.position.x - target.transform.position.x;
	targetSpot.y = transform.position.y - target.transform.position.y;
	targetSpot.z = 0;
	yield WaitForSeconds (2.0);  // wait before the object starts to follow.
	
	ready = true;
	velocityRollX = Random.Range(-20,20);
	velocityRollY = Random.Range(-20,20);
	transform.rigidbody.velocity.x = velocityRollX;
	transform.rigidbody.velocity.y = velocityRollY;
}

function FixedUpdate(){
	dir = target.transform.position - targetSpot - transform.position;
	if(Menu.newGame)
  		Destroy(gameObject);
  	if(Menu.timeStopped) {
  		transform.rigidbody.velocity = Vector3.zero;
  		needsRestart = true;	
  	}
  	if(needsRestart && !Menu.timeStopped) {
  		transform.rigidbody.velocity.x = velocityRollX;
		transform.rigidbody.velocity.y = velocityRollY;
		needsRestart = false;
  	}
	if(transform.position.z < 0) {
		transform.rigidbody.velocity = Vector3(dir.x, dir.y, 100);
	}
	else if (transform.position.z > 0) {
		transform.rigidbody.velocity = Vector3.zero;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		transform.position = Vector3(transform.position.x, transform.position.y, 0);
		isReady = true;
	}
	else
		isReady = true;
  	if(transform.position.x < -500 || transform.position.x > 500 || transform.position.y < -500 || transform.position.y > 500) {
  		transform.rigidbody.velocity.x *= -1;
  		transform.rigidbody.velocity.y *= -1;
  	}
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Projectile" && !isShuttingDown && !isHit && transform.position.z > -10) {
		isHit = true;
		xPosition = transform.position.x;
		yPosition = transform.position.y;
		LootManager.LootMultiplier(xPosition + 2, yPosition + 2);  // Create two multipliers
		LootManager.LootYellowArrow(xPosition - 2, yPosition - 2);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
/*   if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Mover Killed you.");
	} */
}

function OnDestroy() {
	
}
