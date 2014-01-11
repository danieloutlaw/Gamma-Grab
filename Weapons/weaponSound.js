#pragma strict

var lives : int = 1;
var isShuttingDown = false;
var explosion : Transform;
var size : float = 0.0;
var growthRate : float = 1.0;
var scale : float = 1.0;
var startingLightRange : float = 0;

function Start () {
	growthRate = 3;
	startingLightRange = light.range;
	if(Input.GetAxis("Triggers_1") > 0.5) {
		lives = Menu.damageLeft;
		size = Menu.sizeLeft;		
	}
	else if(Input.GetAxis("Triggers_1") < -0.5) {
		lives = Menu.damageRight;
		size = Menu.sizeRight;		
	}
	else if(Input.GetButton("LB_1")) {
		lives = Menu.damageLeftBumper;
		size = Menu.sizeLeftBumper;	
	}
	else if(Input.GetButton("RB_1")) {
		lives = Menu.damageRightBumper;
		size = Menu.sizeRightBumper;	
	}
	lives += Menu.damageShield;
	lives += Menu.bombDamage;
	yield WaitForSeconds(4);
	Destroy(gameObject);
	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Seeker" && !isShuttingDown)
		lives -= 1;
	if(lives == 0) {
		//for(var i : int = 0; i <= size; i += 2) {
			var explosion1 = Instantiate(explosion, transform.position, Quaternion.identity);
	//	}
		Destroy(gameObject);
	}
	if(collision.gameObject.tag == "Border" && !isShuttingDown) {
//		for(var j : int = 0; j <= size; j += 2)
			var explosion2 = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

function FixedUpdate() {
	if(transform.position.x > 510 || transform.position.x < -510 || transform.position.y > 510 || transform.position.y < -510) {
		var explosion2 = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	transform.localScale = Vector3.one * scale;
	light.range = startingLightRange * scale;
	if(scale < size)
		scale += growthRate;
	if(scale > size)
		scale = size;
	
}