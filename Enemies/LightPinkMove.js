#pragma strict

var target: GameObject; // drag the target here
var speed: float = 60.0; // object speed
private var moving = false; // object initially stopped
private var ready = false;
private var dir: Vector3;
var explosion: GameObject;
var drop: GameObject;
var isShuttingDown = false;
var pointValue : int = 50;
var isHit = false;

public var moveRoll : int = 0;
public var velocityRoll : int = 0;
public var goingLeft = false;
public var isWaitingNX = false;
public var isWaitingPX = false;
public var isWaitingNY = false;
public var isWaitingPY = false;
public var sinWave : float = 0;
public var sinWaveIncrease : float = 0.1;

var isReady = false;

var startingSpoty : float = 0;
var startingSpotx : float = 0;
var needsRestart = false;

function Start() {
	target = shipControl.thisShip;
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;
	
//	transform.position = Vector3(startingSpotx, startingSpoty, -500);
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	sinWaveIncrease = Random.Range(0.01, 0.2);
	yield WaitForSeconds (2.0);  // wait before the object starts to follow.
	ready = true;
	moveRoll = Random.Range(0,100);
	velocityRoll = Random.Range(100,200);
	transform.Rotate(0,0,Random.Range(0,360));
	
	transform.rigidbody.velocity = transform.up * velocityRoll;
	if(moveRoll > 50)
		goingLeft = true;
}

function FixedUpdate(){
	if(needsRestart && !Menu.timeStopped) {
		transform.rigidbody.velocity = transform.up * velocityRoll;
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
  	if(Menu.newGame)
  		Destroy(gameObject);
  	sinWave += sinWaveIncrease;
	if(goingLeft && isReady && !Menu.timeStopped) {
		var equalizer1 : float = 1 * (velocityRoll / 100) * Mathf.Sin(sinWave);
		transform.Rotate(0,0,equalizer1);
		transform.rigidbody.velocity = transform.up * velocityRoll;
	}
	else if (isReady && !Menu.timeStopped) {
		var equalizer2 : float = 1 * (velocityRoll / 100) * Mathf.Sin(sinWave);
		transform.Rotate(0,0,equalizer2);
		transform.rigidbody.velocity = transform.up * velocityRoll * -1;
	}
	else if(Menu.timeStopped) {
		transform.rigidbody.velocity *= 0;
		needsRestart = true;
	}
  	if((transform.position.x < -495 || transform.position.x > 495 || transform.position.y < -495 || transform.position.y > 495) && !isWaitingNX && !isWaitingPX && !isWaitingNY && !isWaitingPY) {
  		if(goingLeft)
  			goingLeft = false;
  		else
  			goingLeft = true;
  		if(transform.position.x < -495)
  			isWaitingNX = true;
  		else if(transform.position.x > 495)
  			isWaitingPX = true;
  		else if(transform.position.y < -495)
  			isWaitingNY = true;
  		else if(transform.position.y > 495)
  			isWaitingPY = true;
  	}
  	if(isWaitingNX && transform.position.x > -495)
  		isWaitingNX = false;
  	if(isWaitingPX && transform.position.x < 495)
  		isWaitingPX = false;
  	if(isWaitingNY && transform.position.y > -495)
  		isWaitingNY = false;
  	if(isWaitingPY && transform.position.y < 495)
  		isWaitingPY = false; 
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Projectile" && !isShuttingDown && !isHit && transform.position.z > -10) {
		isHit = true;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		var xPosition : int = transform.position.x;
		var yPosition : int = transform.position.y;
		LootManager.LootRoll(xPosition + 2, yPosition + 2);  // Create three random drops
		LootManager.LootRoll(xPosition + 2, yPosition - 2);
		LootManager.LootRoll(xPosition, yPosition);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		Destroy(gameObject);	
	}
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Siner Killed you.");
	}
/*	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	} */
}
