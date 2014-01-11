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
var hasSploded = false;

var hasSet = false;

function Start() {
	var num : int = Random.Range(1, 22);
	renderer.material.mainTexture = Resources.Load ("enemy" + num);
	if(num == 1)
		light.color = Color(.8, .337, .85, 1);
	else if(num == 2)
		light.color = Color(.92, .11, .14, 1);
	else if(num == 3)
		light.color = Color(.925, .137, .608, 1);
	else if(num == 4)
		light.color = Color(.239, .349, .655, 1);
	else if(num == 5)
		light.color = Color(.545, .239, .702, 1);
	else if(num == 6)
		light.color = Color(.467, .498, .8, 1);
	else if(num == 7)
		light.color = Color(.886, .773, .682, 1);
	else if(num == 8)
		light.color = Color(.325, .784, .8, 1);
	else if(num == 9)
		light.color = Color(.706, .318, .682, 1);
	else if(num == 10)
		light.color = Color(.169, .914, .8, 1);
	else if(num == 11)
		light.color = Color(.545, .839, .698, 1);
	else if(num == 12)
		light.color = Color(.541, .702, .384, 1);
	else if(num == 13)
		light.color = Color(.506, .702, .231, 1);
	else if(num == 14)
		light.color = Color(.541, .686, .243, 1);
	else if(num == 15)
		light.color = Color.yellow;
	else if(num == 16)
		light.color = Color.yellow;
	else if(num == 17)
		light.color = Color(.827, .475, .098, 1);
	else if(num == 18)
		light.color = Color(.824, .494, .247, 1);
	else if(num == 19)
		light.color = Color(.957, .420, .376, 1);
	else if(num == 20)
		light.color = Color(.416, .635, .427, 1);
	else if(num == 21)
		light.color = Color(.827, .475, .098, 1);
	else
		Debug.Log("Became 22");
	if(transform.position.z < -100)
		transform.rigidbody.velocity = Vector3(0,0,200);
	else {
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		rigidbody.drag = .5;
  		velocityRollX = Random.Range(-400,400);
		velocityRollY = Random.Range(-400,400);
		transform.rigidbody.velocity.x = velocityRollX;
		transform.rigidbody.velocity.y = velocityRollY;
		hasSploded = true;
	}
	
}

function Update() {
	if(transform.position.z  > 0)
		transform.position.z = 0;
	if(transform.position.z == 0 && !hasSploded && !hasSet){
		transform.rigidbody.velocity = Vector3.zero;
		hasSet = true;
		rigidbody.drag = .5;
	}
	if(transform.position.z == 0)
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		
	if(Input.GetButton("A_1") && (Menu.isIntro || Menu.firstTimePlayer) && !hasSploded) {
		transform.position.z = 0;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
  	}
}

function FixedUpdate(){
	if(Menu.newGame || Menu.isTutorial) {
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		transform.rigidbody.velocity.z = -200;
	}
	if(transform.position.z < -1000)
  		Destroy(gameObject);
  	if(Menu.timeStopped) {
  		transform.rigidbody.velocity = Vector3.zero;
  		needsRestart = true;	
  	}
  	if(transform.rigidbody.velocity.x > 400)
  		transform.rigidbody.velocity.x = 400;
  	if(transform.rigidbody.velocity.x < -400)
  		transform.rigidbody.velocity.x = -400;
  	if(transform.rigidbody.velocity.y > 400)
  		transform.rigidbody.velocity.y = 400;
  	if(transform.rigidbody.velocity.y < -400)
  		transform.rigidbody.velocity.y = -400;
  	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnCollisionEnter(collision : Collision) {
	if(collision.gameObject.tag == "Projectile" && !isShuttingDown && !isHit && transform.position.z > -10) {
		isHit = true;
		xPosition = transform.position.x;
		yPosition = transform.position.y;
		LootManager.LootRoll(xPosition + 2, yPosition + 2);  // Create four random drops
		LootManager.LootRoll(xPosition - 2, yPosition - 2);
		LootManager.LootRoll(xPosition + 2, yPosition - 2);
		LootManager.LootRoll(xPosition - 2, yPosition + 2);
		Scorekeeper.score += pointValue * Scorekeeper.multiplier;
		Scorekeeper.enemiesKilled++;
		var explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	if((collision.gameObject.tag == "Border" || collision.gameObject.tag == "BorderEnemy") && !isShuttingDown) {
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
//		transform.rigidbody.velocity *= -1;
	}
}

function OnDestroy() {
	
}
