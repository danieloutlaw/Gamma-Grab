#pragma strict

static var lives : int = 1;
var isShuttingDown = false;
var explosion : Transform;
var explosionSound : Transform;
var wallClickSound : Transform;
var explosiveExplosionSound : Transform;
var size : float = 0.0;
var growthRate : float = 1.0;
var scale : float = 1.0;
var startingLightRange : float = 0;
var ricochet : int = 0;
var hasBounced = false;
var chaoticRoll : int = 0;
var explosiveRoll : int = 0;
var chaotic : float = 0; 
var speed : float = 0.0;
var explosive : int = 0;
var fragmenting : int = 0;
var playerVar : GameObject;
var fragmented = false;
var hasSounded = false;

function Start () {
	fragmented = false;
	playerVar = shipControl.thisShip;
	growthRate = 3;
	startingLightRange = light.range;
	if(Input.GetAxis("Triggers_1") > 0.5) {
		lives = Menu.damageLeft;
		size = Menu.sizeLeft;
		ricochet = Menu.ricochetLeft;
		chaotic = Menu.chaoticLeft;
		speed = Menu.speedLeft;
		explosive = Menu.explosiveLeft;
		fragmenting = Menu.fragmentingLeft;
	}
	else if(Input.GetAxis("Triggers_1") < -0.5) {
		lives = Menu.damageRight;
		size = Menu.sizeRight;
		ricochet = Menu.ricochetRight;
		chaotic = Menu.chaoticRight;
		speed = Menu.speedRight;
		explosive = Menu.explosiveRight;
		fragmenting = Menu.fragmentingRight;
	}
	else if(Input.GetButton("LB_1")) {
		lives = Menu.damageLeftBumper;
		size = Menu.sizeLeftBumper;	
		ricochet = Menu.ricochetLeftBumper;
		chaotic = Menu.chaoticLeftBumper;
		speed = Menu.speedLeftBumper;
		explosive = Menu.explosiveLeftBumper;
		fragmenting = Menu.fragmentingLeftBumper;
	}
	else if(Input.GetButton("RB_1")) {
		lives = Menu.damageRightBumper;
		size = Menu.sizeRightBumper;
		ricochet = Menu.ricochetRightBumper;
		chaotic = Menu.chaoticRightBumper;
		speed = Menu.speedRightBumper;
		explosive = Menu.explosiveRightBumper;
		fragmenting = Menu.fragmentingRightBumper;
	}
	lives += Menu.damageShield;
	lives += Menu.bombDamage;
	lives += Menu.damageFirstPassive;
	lives += Menu.damageSecondPassive;
	chaoticRoll = Random.Range(0,100);
	explosiveRoll = Random.Range(0,100);
	if(playerVar != null) {
		var distance : float = Vector3.Distance(transform.position, playerVar.transform.position);
		if(distance >= 50) {
			size *= .5;
			lives = 1;
			explosive = 0;
			fragmenting = 0;
		}
	}
	else {
		size *= .5;
		lives = 1;
		explosive = 0;
		fragmenting = 0;
	}
	
	yield WaitForSeconds(2);
	Destroy(gameObject);
	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Seeker" && !isShuttingDown && !fragmented) {
		lives -= 1;
//		var explosion1 = Instantiate(explosion, transform.position, Quaternion.identity);
		if(!hasSounded)
			var explosionSound1 = Instantiate(explosionSound, transform.position, Quaternion.identity);
		hasSounded = true;
		if(chaoticRoll < chaotic) {
			transform.Rotate(0,0, Random.Range(-90,90));
			transform.rigidbody.velocity = transform.up * -1 * 250 * speed;
			transform.up = transform.rigidbody.velocity.normalized;
			chaoticRoll = Random.Range(0,100);
		}
		else if(chaotic > 0)
			chaoticRoll = Random.Range(0,100);
		if(explosiveRoll < explosive) {
			var largeExplosion : GameObject = Resources.Load("ExplosiveExplosion");
			var largeExplosion1 = Instantiate(largeExplosion, transform.position, Quaternion.identity);
			var explosionSound6 = Instantiate(explosiveExplosionSound, transform.position, Quaternion.identity);
			explosive = 0;
		}
		if(!fragmented)
			for(var i : int = 0; i < fragmenting; i++) {
				var newProjectile = Instantiate(gameObject, transform.position, Quaternion.identity);
				newProjectile.transform.Rotate(0,0, Random.Range(-90,90));
				newProjectile.transform.rigidbody.velocity = transform.up * -1 * 250 * speed;
				newProjectile.transform.up = transform.rigidbody.velocity.normalized;
				fragmented = true;
			}
		if(lives == 0 || fragmented) {
			var explosionSound2 = Instantiate(explosionSound, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	if(collision.gameObject.tag == "Border" && !isShuttingDown) {
		if(ricochet == 0) {
	//		var explosion3 = Instantiate(explosion, transform.position, Quaternion.identity);
			var explosionSound3 = Instantiate(wallClickSound, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		else {
			var ricochetRoll : float = Random.Range(0,100);
			if(ricochetRoll < ricochet) {
				transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
				transform.up = transform.rigidbody.velocity.normalized;
			}
			else {
	//			var explosion8 = Instantiate(explosion, transform.position, Quaternion.identity);
				var explosionSound8 = Instantiate(wallClickSound, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
		if(explosiveRoll < explosive) {
			var largeExplosion2 : GameObject = Resources.Load("ExplosiveExplosion");
			var largeExplosion3 = Instantiate(largeExplosion2, transform.position, Quaternion.identity);
			if(!hasSounded)
				var explosionSound7 = Instantiate(explosiveExplosionSound, transform.position, Quaternion.identity);
			hasSounded = true;
			explosive = 0;
		}
	}
	if(collision.gameObject.tag == "BorderEnemy" && !isShuttingDown) {
		if(ricochet == 0) {
		//	var explosion4 = Instantiate(explosion, transform.position, Quaternion.identity);
			var explosionSound4 = Instantiate(wallClickSound, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		else {
			var ricochetRoll2 : float = Random.Range(0,100);
			if(ricochetRoll2 < ricochet) {
				transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.up);
				transform.up = transform.rigidbody.velocity.normalized;
			}
			else {
	//			var explosion9 = Instantiate(explosion, transform.position, Quaternion.identity);
				var explosionSound9 = Instantiate(wallClickSound, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
		if(explosiveRoll < explosive) {
			var largeExplosion5 : GameObject = Resources.Load("ExplosiveExplosion");
			var largeExplosion6 = Instantiate(largeExplosion5, transform.position, Quaternion.identity);
			var explosionSound5 = Instantiate(explosiveExplosionSound, transform.position, Quaternion.identity);
			explosive = 0;
		}
	}
}

function OnCollisionEnter(collision : Collision) {
/*	if(collision.gameObject.tag == "Border" && !isShuttingDown) {
		if(ricochet == 0) {
			var explosion3 = Instantiate(explosion, transform.position, Quaternion.identity);
	//		var explosionSound3 = Instantiate(explosionSound, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		else {
			transform.rigidbody.velocity = Vector3.Reflect(transform.position, collision.gameObject.transform.forward);
			transform.up = transform.rigidbody.velocity.normalized;
		}
	} */
}

function FixedUpdate() {
	transform.localScale = Vector3.one * scale;
	light.range = startingLightRange * scale;
	if(scale < size)
		scale += growthRate;
	if(scale > size)
		scale = size;
}