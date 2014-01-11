// Controls the bomb item (B button)

var shieldObject : GameObject;
var playerVar : GameObject;

function Start () {
	
	if(Menu.proTexturesShield == "backshield") {
		transform.localPosition *= -1;
		transform.Rotate(0,0,180);
		shieldObject = Resources.Load("basicshield");
	}
	else
		shieldObject = Resources.Load(Menu.proTexturesShield);
	playerVar = GameObject.FindWithTag("PlayerShip");
}
	
function Update () {
	if((Input.GetButtonDown("Y_1") || Input.GetButtonDown("Y_2")) && Menu.level >= 5 && Scorekeeper.shieldPercent > 0 && !Scorekeeper.shieldOut && !Scorekeeper.waitOnShield) {
		if(Menu.proTexturesShield == "basicshield" || Menu.proTexturesShield == "backshield") {
			var shieldObject1 = Instantiate(shieldObject, transform.position, Quaternion.identity);
			shieldObject1.transform.parent = transform;
			shieldObject1.transform.up = transform.up;
			Scorekeeper.waitOnShield = true;
			Scorekeeper.shieldOut = true;
		}
		else if (Menu.proTexturesShield == "pointdefense") {
			var shieldPointDefense1 = Instantiate(shieldObject, transform.position, Quaternion.identity);
			shieldPointDefense1.transform.parent = transform;
			shieldPointDefense1.transform.up = transform.up;
			Scorekeeper.waitOnShield = true;
			Scorekeeper.shieldOut = true;
		}
		else if (Menu.proTexturesShield == "intercepting") {
			var shieldIntercepting1 = Instantiate(shieldObject, transform.position, Quaternion.identity);
			shieldIntercepting1.transform.parent = playerVar.transform;
			shieldIntercepting1.transform.up = playerVar.transform.up;
			Scorekeeper.waitOnShield = true;
			Scorekeeper.shieldOut = true;
		}
	}
	else if((Input.GetButtonDown("Y_1") || Input.GetButtonDown("Y_2")) && Menu.level >= 5 && Scorekeeper.shieldPercent > 0 && Scorekeeper.shieldOut && !Scorekeeper.waitOnShield && Menu.proTexturesShield != "intercepting") {
		Scorekeeper.waitOnShield = true;
		Scorekeeper.shieldOut = false;
	}
	if(Menu.playerDeath) {
		Scorekeeper.shieldOut = false;
	}
}