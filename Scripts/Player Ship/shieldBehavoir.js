var isShuttingDown = false;

function Start() {
	var shieldColor : Color;
	if(Menu.projectilesShield >= 90)
		shieldColor = Color.white;
	else if(Menu.projectilesShield >= 80)
		shieldColor = Color.cyan;
	else if(Menu.projectilesShield >= 70)
		shieldColor = Color.green;
	else if(Menu.projectilesShield >= 60)
		shieldColor = Color.magenta;
	else if(Menu.projectilesShield >= 50)
		shieldColor = Color.red;
	else if(Menu.projectilesShield >= 40)
		shieldColor = Color.yellow;
	else if(Menu.projectilesShield >= 30)
		shieldColor = new Color(1f,.5f,0f,1f);
	else if(Menu.projectilesShield >= 20)
		shieldColor = Color.black;
	else if(Menu.projectilesShield >= 10)
		shieldColor = Color.blue;
	else 
		shieldColor = Color.gray;
	light.color = shieldColor;
	light.range *= Menu.sizeShield;
	transform.localPosition.y += (Menu.sizeShield - 1);
	transform.localScale += Vector3.one * (Menu.sizeShield / 2);
	transform.localScale -= (Vector3.one / 2);
}

function OnApplicationQuit() {
	isShuttingDown = true;
}
	
function FixedUpdate () {
	if(!Scorekeeper.shieldOut)
		Destroy(gameObject);
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Seeker" && !isShuttingDown)
		Scorekeeper.shieldPercent -= 100 / Menu.speedShield;
}