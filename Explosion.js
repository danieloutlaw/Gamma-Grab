#pragma strict
var explodeTime : float = 0.5;

function Start () {
	yield WaitForSeconds(explodeTime);
	Destroy(gameObject);
}
