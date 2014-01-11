var isShuttingDown = false;
var energyBall : GameObject;
var playerVar : GameObject;

function Start() {
	var shieldColor : Color;
	playerVar = shipControl.thisShip;
	transform.position = playerVar.transform.position;
	if(Menu.projectilesShield >= 88)
		shieldColor = Color.white;
	else if(Menu.projectilesShield >= 77)
		shieldColor = Color.cyan;
	else if(Menu.projectilesShield >= 66)
		shieldColor = Color.green;
	else if(Menu.projectilesShield >= 55)
		shieldColor = Color.magenta;
	else if(Menu.projectilesShield >= 44)
		shieldColor = Color.red;
	else if(Menu.projectilesShield >= 33)
		shieldColor = Color.yellow;
	else if(Menu.projectilesShield >= 22)
		shieldColor = new Color(1f,.5f,0f,1f);
	else if(Menu.projectilesShield >= 11)
		shieldColor = Color.gray;
	else
		shieldColor = Color.blue;
	energyBall.light.color = shieldColor;
	
	var ballNum : int = 3 * Menu.sizeShield;
	var changeAmount : int = 360 / ballNum;
	
	for(var i : int = 0; i < ballNum; i++) {
		var projectile = Instantiate(energyBall, transform.position + Vector3(12,0,0), Quaternion.identity);
		projectile.transform.parent = transform;
		gameObject.transform.Rotate(0,0,changeAmount);
	}
	
	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}
	
function FixedUpdate () {
	gameObject.transform.Rotate(0,0,5);
	if(!Scorekeeper.shieldOut)
		Destroy(gameObject);
}

function OnTriggerEnter(collision : Collider) {
//	if(collision.gameObject.tag == "Seeker" && !isShuttingDown)
//		Scorekeeper.shieldPercent -= 100 / Menu.damageShield;
}