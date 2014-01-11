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

function Start() {
	target = shipControl.thisShip;
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;
	
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	yield WaitForSeconds (2.0);  // wait before the object starts to follow.
	ready = true;
	var xRoll : float = Random.Range(0,10);
	var yRoll : float = Random.Range(0,10);
	var forceAmountX : float = 0;
	var forceAmountY : float = 0;
	if(xRoll >= 6)
		forceAmountX = Random.Range(9000f,12000f);
	else
		forceAmountX = Random.Range(-12000f,-9000f);
	if(yRoll >= 6)
		forceAmountY = Random.Range(9000f,12000f);
	else
		forceAmountY = Random.Range(-12000f,-9000f);
	
	if(!Menu.timeStopped) {
		
		for( ; ; ) {
			transform.rigidbody.AddRelativeForce(forceAmountX,forceAmountY,0);
			yield WaitForSeconds(0.45f);
			transform.rigidbody.velocity = Vector3.zero;
			transform.rigidbody.AddTorque(0,0,Random.Range(0,360));
		//	transform.Rotate(0,0,Random.Range(0,360));
			yield WaitForSeconds(0.15f);
		}
	}
}

function FixedUpdate(){
	if(Menu.newGame)
  		Destroy(gameObject);
  	if(Menu.timeStopped) {
  		transform.rigidbody.velocity = Vector3.zero;
  		needsRestart = true;	
  	}
  	
  /*	if(needsRestart && !Menu.timeStopped) {
  		transform.rigidbody.velocity.x = velocityRollX;
		transform.rigidbody.velocity.y = velocityRollY;
		needsRestart = false;
  	} */
  	
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
  /*	if(transform.position.x < -500 || transform.position.x > 500 || transform.position.y < -500 || transform.position.y > 500) {
  		transform.rigidbody.velocity.x *= -1;
  		transform.rigidbody.velocity.y *= -1;
  	} */
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
		LootManager.LootRoll(xPosition - 2, yPosition - 2);
		LootManager.LootRoll(xPosition , yPosition);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Poofer Killed you.");
	}
	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	}
}

function OnCollisionEnter(collision : Collision) {
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Poofer Killed you.");
	}
}

function OnDestroy() {
	
}
