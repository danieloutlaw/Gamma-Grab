#pragma strict

var clearTime : int = 4;
var isShuttingDown = false;
var dir : Vector3;
var playerVar : GameObject;
var isGrabbed = false;
var grabMul : int = 200;

function Start() {
	playerVar = shipControl.thisShip;
	yield WaitForSeconds (clearTime);
	if(!isGrabbed)
		transform.rigidbody.velocity.z = 100;
}

function Update() {
	if(Menu.playerDeath)
  		Destroy(gameObject);   		
}

function OnTriggerEnter(other : Collider) 
{
	if(other.gameObject.tag == "LootGrabber") 
	{
		isGrabbed = true;
    	dir = transform.position - playerVar.transform.position; // calculate the target direction...
    	transform.rigidbody.velocity.x = dir.x;
        transform.rigidbody.velocity.y = dir.y;
        transform.rigidbody.velocity.z = -200;
        
    }
	if(other.gameObject.tag == "KillZone")
	{
		Scorekeeper.multiplier += 1;
		Menu.permMult += 1;
    	Destroy(gameObject);
    	Scorekeeper.permMult++;
	}
	if(other.gameObject.tag == "DropOutZone")
    		Destroy(gameObject);
}

function OnTriggerStay(other : Collider)
{
	if(other.gameObject.tag == "LootGrabber") 
	{
    	dir = playerVar.transform.position - transform.position; // calculate the target direction...
    	transform.rigidbody.velocity.x = dir.x;
        transform.rigidbody.velocity.y = dir.y;
        transform.rigidbody.velocity.z = -200;
    }
}