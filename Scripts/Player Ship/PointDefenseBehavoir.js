var isShuttingDown = false;
var laser : GameObject;

function Start() {
	var shieldColor : Color;
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
	laser.light.color = shieldColor;
	var waitTime : float = .2 / Menu.sizeShield;
	for( ; ; ) {
		transform.Rotate(0,0,Random.Range(0,360));
		var projectile = Instantiate(laser, transform.position, Quaternion.identity);
		projectile.transform.up = transform.up;
    	projectile.rigidbody.velocity = transform.up * 250;
		yield WaitForSeconds(waitTime);
	}
	
}

function OnApplicationQuit() {
	isShuttingDown = true;
}
	
function FixedUpdate () {
	if(!Scorekeeper.shieldOut)
		Destroy(gameObject);
}
