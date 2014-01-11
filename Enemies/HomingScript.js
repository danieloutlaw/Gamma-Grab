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
var	yPosition : int = 0;
var isHit = false;

var isReady = false;

var startingSpoty : float = 0;
var startingSpotx : float = 0;
var velocitySet = false;

var myThread : UnityThreading.ActionThread;



function Start() {
	myThread = UnityThreadHelper.CreateThread(FindPlayer);
	
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;
	
//	transform.position = Vector3(startingSpotx, startingSpoty, -200);
	

	yield WaitForSeconds (2.0);  // wait before the object starts to follow.
	
	ready = true;
}


function FixedUpdate(){
	//UnityThreadHelper.TaskDistributor.Dispatch(DoOnUpdate());
	if(Menu.timeStopped)
		transform.rigidbody.velocity *= 0;
	if(transform.position.z < 0) {
		transform.rigidbody.velocity = Vector3(dir.x, dir.y, 100);
//		velocitySet = true;
	}
	else if (transform.position.z > 0) {
		transform.rigidbody.velocity = Vector3.zero;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		transform.position = Vector3(startingSpotx, startingSpoty, 0);
		isReady = true;
	}
	else if (transform.position.z == 0)
		isReady = true;
	if(transform.position.z == 0 && target != null) {
		transform.up = target.transform.position - transform.position;
		transform.rigidbody.velocity = transform.up * 2.5 * speed;
	}
  	if(Menu.newGame)
  		Destroy(gameObject);
}

function FindPlayer() {
	target = shipControl.thisShip;
}

/*function DoOnUpdate() {
	
} */

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Projectile" && !isShuttingDown && !isHit && transform.position.z > -10) {
		isHit = true;
		xPosition = transform.position.x;
		yPosition = transform.position.y;
		LootManager.LootRoll(xPosition + 2, yPosition);  // Create two random drops
		LootManager.LootRoll(xPosition - 2, yPosition);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Seeker Killed you.");
	}
	
	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown && transform.position.z < 0) {
		//transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		//transform.up = transform.rigidbody.velocity.normalized;
		transform.position.x += 5;
		transform.position.y += 5;
		if(transform.position.x >= 490)
			transform.position.x = 400;
		if(transform.position.y >= 490)
			transform.position.y = 400;
	} 
}

function OnCollisionEnter(collision : Collision) {
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Seeker Killed you.");
	} 
}
