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

var singleEnemy1 : Transform;
var singleEnemy2 : Transform;
var singleEnemy3 : Transform;

var rotateAmount : Vector3;
var smooth = 6.0;

private var velocity = Vector3.zero;

function Start() {
	target = shipControl.thisShip;
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;
	
	
//	transform.position = Vector3(startingSpotx, startingSpoty, -500);
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	rotateAmount = Vector3(0,0,Random.Range(-2,2));
	yield WaitForSeconds (2.0);  // wait before the object starts to follow.
	
	ready = true;
	velocityRollX = Random.Range(20,40);
//	velocityRollY = Random.Range(-60,60);
//	transform.rigidbody.velocity.x = velocityRollX;
//	transform.rigidbody.velocity.y = velocityRollY;
	transform.rigidbody.velocity = Vector3.zero;
}

function FixedUpdate(){
//	transform.Rotate(rotateAmount);
	if(singleEnemy1 != null)
		singleEnemy1.transform.rigidbody.velocity = transform.rigidbody.velocity;
	if(singleEnemy2 != null)
		singleEnemy2.transform.rigidbody.velocity = transform.rigidbody.velocity;
	if(singleEnemy3 != null)
		singleEnemy3.transform.rigidbody.velocity = transform.rigidbody.velocity;
	if(Menu.newGame)
  		Destroy(gameObject);
  	if(Menu.timeStopped) {
  		transform.rigidbody.velocity = Vector3.zero;
  		needsRestart = true;	
  	}
  	
  	if(target && !Menu.timeStopped && transform.position.z == 0) {
  		var rotation = Quaternion.identity;
  	//	transform.rotation.SetFromToRotation(transform.position, target.transform.position);
  	//	rotation.Normalize();
//  		Debug.Log(rotation);
	//	transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.SetFromToRotation(transform.position, target.transform.position), smooth);
	//	var rotation = Quaternion.LookAt(target.transform.position - transform.position, Vector3.left);
		var newDirection = transform.position - target.transform.position;
		newDirection.Normalize();
		newDirection *= -1;
	//	var dampenedDirection = Vector3.SmoothDamp(transform.up, newDirection, Vector3.zero, smooth);
		transform.up = Vector3.SmoothDamp(transform.up, newDirection, velocity, smooth);
	//	transform.rigidbody.velocity = transform.up * velocityRollX;
		
		
		//	transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smooth);
	//	transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, smooth);
	}
	
	
	
  //	targetPosition = target.transform.position;
 // 	transform.position = Vector3.SmoothDamp(transform.position, targetPosition, Vector3.zero, smooth);
  	
  	
  //	if(needsRestart && !Menu.timeStopped) {
  	//	transform.rigidbody.velocity.x = velocityRollX * Time.deltaTime * speed;
	//	transform.rigidbody.velocity.y = velocityRollY * Time.deltaTime * speed;
	//	needsRestart = false;
 // 	} 
  	
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
  	
  	if( singleEnemy1 == null && singleEnemy2 == null && singleEnemy3 == null) {
  		Destroy(gameObject);
  		var explosionPosition : Vector3 = Vector3(transform.localPosition.x,transform.localPosition.y, 0) + Vector3(-12,-66,0);
  		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
  	}
  	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnCollisionEnter(collision : Collision) {
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Shielder Killed you.");
	}
	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	}
}

function OnDestroy() {
	
}
