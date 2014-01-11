var slurpSound : Transform;
var canSlurp = true;

function Update () {
	collider.radius = Scorekeeper.grabberRadius;
}

function OnTriggerEnter(other : Collider) {
	if(other.gameObject.tag == "Drop" && canSlurp) 
	{
		var slurpSound1 = Instantiate(slurpSound, transform.position, Quaternion.identity);
    	canSlurp = false;
    }
}

function Start () {
	for( ; ; ){
		yield WaitForSeconds(.05);
		canSlurp = true;
	}
}