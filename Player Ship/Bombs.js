// Controls the bomb item (B button)

var boomObject : GameObject;

function Start () {
			
}
	
function FixedUpdate () {
	if(Input.GetButtonDown("B_1") && Menu.level >= 10 && Scorekeeper.bombCount > 0) {
		var boomObject1 = Instantiate(boomObject, transform.position, Quaternion.identity);
		Scorekeeper.bombCount--;
	}
}