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
var isAlone = false;
var isAloneStart = false;
var startingLocalPosition : Vector3;


function Start() {
	target = shipControl.thisShip;
	startingLocalPosition = transform.localPosition;
	
	ready = true;
}

function Update(){
	transform.localPosition = startingLocalPosition;
	if(Menu.newGame)
		Destroy(gameObject);
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
		LootManager.LootRoll(xPosition - 2, yPosition - 2);
		LootManager.LootRoll(xPosition - 2, yPosition + 2);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

function OnCollisionEnter(collision : Collision) {
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown) {
		Menu.PlayerDied();
		Debug.Log("Single Shielder Killed you.");
	}
	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	}
}

