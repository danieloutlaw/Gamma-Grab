#pragma strict

var target: GameObject; // drag the target here
var speed: float = 60.0; // object speed
private var ready = false;
private var dir: Vector3;
var explosion: GameObject;
var drop: GameObject;
var isShuttingDown = false;
var pointValue : int = 15;
var isHit = false;

var playerFront : GameObject;
var playerBehind : GameObject;
var playerLeft : GameObject;
var playerRight : GameObject;

var startingSpoty : float = 0;
var startingSpotx : float = 0;

var isReady = true;
var isReversed = false;

function Start() {
	target = shipControl.thisShip;
	startingSpoty = transform.position.y;
	startingSpotx = transform.position.x;
	
//	transform.position = Vector3(startingSpotx, startingSpoty, -500);
	var dir : Vector3 = Vector3(startingSpotx, startingSpoty, 0) - transform.position;
	
	if(Menu.newGame)
  		Destroy(gameObject);
  		
	yield WaitForSeconds (2.0);  // wait before the object starts to follow.
	
	playerFront = GameObject.FindWithTag ("TargetSpotFront");
	playerBehind = GameObject.FindWithTag ("TargetSpotBehind");
	playerLeft = GameObject.FindWithTag ("TargetSpotLeft");
	playerRight = GameObject.FindWithTag ("TargetSpotRight");
	ready = true;
	if(Menu.newGame)
  		Destroy(gameObject);
	for( ; ; ) {
		if(!Menu.timeStopped) {
		if(ready && !Menu.newGame && isReady && playerFront != null) {
			var playerBehindDistance : float = Mathf.Abs( playerBehind.transform.position.x - transform.position.x) + Mathf.Abs( playerBehind.transform.position.y - transform.position.y);
			var playerFrontDistance : float = Mathf.Abs( playerFront.transform.position.x - transform.position.x) + Mathf.Abs( playerFront.transform.position.y - transform.position.y);
			var playerLeftDistance : float = Mathf.Abs( playerLeft.transform.position.x - transform.position.x) + Mathf.Abs( playerLeft.transform.position.y - transform.position.y);
			var playerRightDistance : float = Mathf.Abs( playerRight.transform.position.x - transform.position.x) + Mathf.Abs( playerRight.transform.position.y - transform.position.y);
		}
		if( playerFront != null && playerBehindDistance >= playerFrontDistance && playerBehindDistance >= playerLeftDistance && playerBehindDistance >= playerRightDistance && ready && isReady && !Menu.newGame) {
			transform.up = playerBehind.transform.position - transform.position;
			transform.rigidbody.velocity = transform.up * speed * 4;
    	}
    	else if( playerFront != null && playerFrontDistance >= playerBehindDistance && playerFrontDistance >= playerLeftDistance && playerFrontDistance >= playerRightDistance && ready && isReady && !Menu.newGame) {
			transform.up = playerFront.transform.position - transform.position;
			transform.rigidbody.velocity = transform.up * speed * 4;
    	}
    	else if( playerFront != null && playerLeftDistance >= playerFrontDistance && playerLeftDistance >= playerBehindDistance && playerLeftDistance >= playerRightDistance && ready && isReady && !Menu.newGame) {
			transform.up = playerLeft.transform.position - transform.position;
			transform.rigidbody.velocity = transform.up * speed * 4;
    	}
    	else if ( playerFront != null && playerRightDistance >= playerFrontDistance && playerRightDistance >= playerBehindDistance && playerRightDistance >= playerLeftDistance && ready && isReady && !Menu.newGame){
			transform.up = playerRight.transform.position - transform.position;
			transform.rigidbody.velocity = transform.up * speed * 4;
    	}
		if(Menu.newGame)
  			Destroy(gameObject);
        yield WaitForSeconds (2.5);
        if(Menu.newGame)
  			Destroy(gameObject);
 	}
 	else
 		yield WaitForSeconds (0.1);
 	}

}

function FixedUpdate(){
	if(Menu.newGame)
  		Destroy(gameObject);
	if(transform.position.z < 0) {
		transform.rigidbody.velocity = Vector3(dir.x, dir.y, 100);
	}
	else if (transform.position.z > 0) {
		transform.rigidbody.velocity = Vector3.zero;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		transform.position = Vector3(transform.position.x, transform.position.y, 0);
		isReady = true;
	}
	else if(!isReady)
		isReady = true;
 /*   if((transform.position.x <= -495 || transform.position.x >= 495 || transform.position.y <= -495 || transform.position.y >= 495) && !isReversed) {
  		transform.Rotate(0,0,180);
  		transform.rigidbody.velocity *= -1;
  		isReversed = true;
  	} */
  	else if((transform.position.x > -495 && transform.position.x < 495 && transform.position.y > -495 && transform.position.y < 495) && isReversed)
  	if(Menu.timeStopped)
  		transform.rigidbody.velocity *= 0;
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
		LootManager.LootRoll(xPosition + 2, yPosition);  // Create two random drops
		LootManager.LootRoll(xPosition - 2, yPosition);
		LootManager.LootRoll(xPosition, yPosition);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		Destroy(gameObject);
	}
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown && !Menu.newGame) {
		Menu.PlayerDied();
		Debug.Log("Attacker Killed you.");
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