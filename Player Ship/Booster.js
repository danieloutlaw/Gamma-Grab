// Controls the booster item (A button)

var speed : float = 50.0;
var boomObject : GameObject;
var boostTrailObject : GameObject;
var trailObjectHQ : GameObject;
var trailObject : GameObject;
var speedMultiplier : double;
var booster : GameObject;
static var isBoosted = false;

var isReady = false;

function Start () {
	yield WaitForSeconds(.1);
	isReady = true;
}
	
function FixedUpdate () {
	if(isReady && !Menu.noGame) {
		if(Input.GetButton("A_1") && Menu.level >= 15 && (Input.GetAxisRaw ("L_YAxis_1") != 0 || Input.GetAxisRaw ("L_XAxis_1") != 0)) {
			var boostTrailObject1 = Instantiate(boostTrailObject, booster.transform.position, Quaternion.identity);
			Menu.playerSpeed = Menu.boosterSpeed;
			isBoosted = true;
			Time.timeScale = 0.5F;
		}
		else if(Input.GetAxisRaw ("L_YAxis_1") != 0 || Input.GetAxisRaw ("L_XAxis_1") != 0){
			Menu.playerSpeed = 1.0;
			var trailObject1 = Instantiate(trailObject, booster.transform.position, Quaternion.identity);
			isBoosted = false;
			if(Time.timeScale != 1.0f)
				Time.timeScale = 1.0f;
		}
		else {
			Menu.playerSpeed = 1.0;
			isBoosted = false;
			if(Time.timeScale != 1.0f)
				Time.timeScale = 1.0f;
		}
		if(Input.GetButtonUp("A_1"))
			Time.timeScale = 1.0f;
	}
}