// Movement for the player's ship

var speed : float = 3.2;
private var velocity = Vector3.zero;
var smooth = .10;
var isConstrained = false;
var playerExplosion : GameObject;
var isMazed = false;
static var thisShip : GameObject;

function Start() {
	thisShip = gameObject;
	if(Menu.isMaze)
		speed = 170;
	else
		speed = 55;
}

function Update () {
	light.range = 150 + (Menu.level * 3);
	var controller : CharacterController = GetComponent(CharacterController);
	
	if(transform.position.z > 0) {
		transform.position.z = 0;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		isConstrained = true;
	}
	else if(transform.position.z == 0 && !isConstrained){
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		isConstrained = true;
	}
	if(transform.position.z < 0)
		transform.transform.position.z = 0;
		
		var verticalTranslation : float = Input.GetAxisRaw ("L_YAxis_1");
    	var horizontalTranslation : float = Input.GetAxisRaw ("L_XAxis_1");
   		var transformPosition : Vector3;
    	if(!Menu.isMaze) {
			transformPosition = Vector3(horizontalTranslation, verticalTranslation, 0);
			transformPosition.Normalize();
			transformPosition *= speed * Menu.playerSpeed;
			transformPosition += transform.position;
			transform.position = Vector3.SmoothDamp(transform.position, transformPosition, velocity, smooth);
		}
    	else {
			transformPosition = Vector3(horizontalTranslation, verticalTranslation, 0);
			transformPosition *= speed * Menu.playerSpeed;
			transformPosition += transform.position;
			transformPosition -= transform.position;
			transformPosition *= Time.deltaTime;
			controller.Move(transformPosition); 
		}
		
		if(transform.position.x > 492)
			transform.position.x = 492;
		if(transform.position.x < -492)
			transform.position.x = -492;
		if(transform.position.y > 492)
			transform.position.y = 492;
		if(transform.position.y < -492)
			transform.position.y = -492;
	
 /*  	if(Menu.playerDeath && Menu.isInitialized) {
  		Destroy(gameObject);
  		var playerExplosion1 : GameObject = Instantiate(playerExplosion,transform.position, Quaternion.identity);	
  	} */
  	if((Input.GetButton("Start_1") || Input.GetButton("Start_2") || Input.GetButton("Start_3") || Input.GetButton("Start_4")) && !Menu.showPauseGUI) {
  		Time.timeScale = 0;
  		Menu.showPauseGUI = true;
  	}
 /* 	if(Menu.endPlayerLife) {
  		Destroy(gameObject);
  		Menu.endPlayerLife = false;
  	} */
}

function OnTriggerEnter(collision : Collider) {
	if(collision.tag == "Maze")
		isMazed = true;
}