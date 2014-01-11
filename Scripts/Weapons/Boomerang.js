#pragma strict

var lives : int = 1;
var isShuttingDown = false;
var explosion : Transform;
var explosionSound : Transform;
var size : float = 0.0;
var speed : float = 0.0;
var growthRate : float = 1.0;
var scale : float = 1.0;
var startingLightRange : float = 0;
var startingSpot : Vector3;
var returnSpot : Vector3;
var hasReturned = false;
var hitWall = false;
var playerVar : GameObject;
var distance : float;
var rotateMultiplier : float;
var distanceTraveled : float;
var previousPosition : Vector3;
var returning = false;
var hasSounded = false;
var explosiveRoll : float;

function Start () {
	rotateMultiplier = Random.Range(0,10);
	explosiveRoll = Random.Range(0,100);
	playerVar = shipControl.thisShip;
	startingSpot = transform.position;
	growthRate = 3;
	startingLightRange = light.range;
	if(Input.GetAxis("Triggers_1") > 0.5) {
		lives = Menu.damageLeft;
		size = Menu.sizeLeft;
		speed = Menu.speedLeft;		
	}
	else if(Input.GetAxis("Triggers_1") < -0.5) {
		lives = Menu.damageRight;
		size = Menu.sizeRight;	
		speed = Menu.speedRight;	
	}
	else if(Input.GetButton("LB_1")) {
		lives = Menu.damageLeftBumper;
		size = Menu.sizeLeftBumper;
		speed = Menu.speedLeftBumper;
	}
	else if(Input.GetButton("RB_1")) {
		lives = Menu.damageRightBumper;
		size = Menu.sizeRightBumper;
		speed = Menu.speedRightBumper;
	}
	rotateMultiplier *= speed;
	lives += Menu.damageShield;
	lives += Menu.bombDamage;
	lives += Menu.damageFirstPassive;
	lives += Menu.damageSecondPassive;
	yield WaitForSeconds(.75);
	returning = true;
	yield WaitForSeconds(.75);
	Destroy(gameObject);
	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Seeker" && !isShuttingDown) {
		lives -= 1;
//		var explosion1 = Instantiate(explosion, transform.position, Quaternion.identity);
		if(!hasSounded)
			var explosionSound1 = Instantiate(explosionSound, transform.position, Quaternion.identity);
		hasSounded = true;
	}
	if(lives == 0) {
//		var explosion2 = Instantiate(explosion, transform.position, Quaternion.identity);
//		var explosionSound2 = Instantiate(explosionSound, transform.position, Quaternion.identity);
		Destroy(gameObject);
//		hasSounded = true;
	}
	if(collision.gameObject.tag == "Border" && !isShuttingDown && !hitWall) {
		hasReturned = true;
		var direction : Vector3;
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	}
	if(collision.gameObject.tag == "BorderEnemy" && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.up);
		transform.up = transform.rigidbody.velocity.normalized;

/*		if(explosiveRoll > explosive) {
			var largeExplosion2 : GameObject = Resources.Load("ExplosiveExplosion");
			var largeExplosion3 = Instantiate(largeExplosion2, transform.position, Quaternion.identity);
			explosive = 0;
		}  */
	}
	if(collision.gameObject == playerVar && hasReturned)
		Destroy(gameObject);
}

function FixedUpdate() {
	if(returning && playerVar != null) {
	//	if(!hasReturned)
	//		transform.up = playerVar.transform.position - transform.position;
		var postDirection : Vector3 = playerVar.transform.position - transform.position;
		postDirection = postDirection.normalized;
		transform.rigidbody.velocity = postDirection * 250 * speed;
		hasReturned = true;
	}
	transform.Rotate(0,0,rotateMultiplier);
//	if(transform.position.x > 510 || transform.position.x < -510 || transform.position.y > 510 || transform.position.y < -510 && !hitWall) {
//		hitWall = true;
//		transform.rigidbody.velocity *= -1;
//	}
//	else if(transform.position.x > 510 || transform.position.x < -510 || transform.position.y > 510 || transform.position.y < -510 && hitWall) {
//		var explosion4 = Instantiate(explosion, transform.position, Quaternion.identity);
//		var explosionSound4 = Instantiate(explosionSound, transform.position, Quaternion.identity);
//		Destroy(gameObject);
//	}
	
	
/*	if(!hasReturned)
		distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - startingSpot.x),2) + Mathf.Pow((transform.position.y - startingSpot.y),2));
	else
		distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - returnSpot.x),2) + Mathf.Pow((transform.position.y - returnSpot.y),2));
	if(distance >= 350 && !hasReturned && playerVar != null) {
		hasReturned = true;
		transform.up = playerVar.transform.position - transform.position;
		transform.rigidbody.velocity = transform.up * 250 * speed;
		returnSpot = playerVar.transform.position;
//		transform.rigidbody.velocity *= -1;
	}
	if(hasReturned && distance <= 10) {
		var explosion4 = Instantiate(explosion, transform.position, Quaternion.identity);
//		var explosionSound4 = Instantiate(explosionSound, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
//	if(hasReturned) {
//		transform.rigidbody.velocity = transform.position - playerVar.transform.position * 250 * speed;
//	}
		*/
		
	transform.localScale = Vector3.one * scale;
	light.range = startingLightRange * scale;
	if(scale < size)
		scale += growthRate;
	if(scale > size)
		scale = size;
	
}