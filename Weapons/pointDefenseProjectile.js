#pragma strict

static var lives : int = 1;
var isShuttingDown = false;
var explosion : Transform;
var explosionSound : Transform;
var size : float = 0.0;
var scale : float = 1.0;
var startingLightRange : float = 0;
var ricochet : int = 0;
var hasBounced = false;
var chaoticRoll : int = 0;
var chaotic : float = 0; 
var speed : float = 0.0;
var explosive : int = 0;
var fragmenting : int = 0;
var playerVar : GameObject;

function Start () {
	playerVar = shipControl.thisShip;
	lives = 1;
	yield WaitForSeconds(2);
	Destroy(gameObject);
	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Seeker" && !isShuttingDown)
		Destroy(gameObject);
	if(collision.gameObject.tag == "Border" && !isShuttingDown) {
	//	var explosion3 = Instantiate(explosion, transform.position, Quaternion.identity);
	//	var explosionSound3 = Instantiate(explosionSound, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	if(collision.gameObject.tag == "BorderEnemy" && !isShuttingDown) {
	//	var explosion4 = Instantiate(explosion, transform.position, Quaternion.identity);
	//	var explosionSound3 = Instantiate(explosionSound, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}