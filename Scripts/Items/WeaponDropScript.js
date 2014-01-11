#pragma strict

var clearTime : int = 3;
var isShuttingDown = false;
var dir : Vector3;
var playerVar : GameObject;
var isGrabbed = false;
var grabMul : int = 1;

function Start() {
	playerVar = GameObject.FindWithTag("PlayerShip");
	yield WaitForSeconds (clearTime);
	if(!isGrabbed)
		Destroy(gameObject);
}

/*
function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnCollisionEnter( collision : Collision ) {
	if(collision.gameObject.tag == "PlayerShip" && !isShuttingDown)
	{
		Debug.Log("Collision");
		Destroy(gameObject);
	}
}
*/

function OnTriggerEnter(other : Collider) 
{
	if(other.gameObject.tag == "LootGrabber") 
	{
		isGrabbed = true;
    	dir = playerVar.transform.position - transform.position; // calculate the target direction...
    	transform.rigidbody.velocity.x = dir.x * grabMul;
        transform.rigidbody.velocity.y = dir.y * grabMul;
   
    }
	if(other.gameObject.tag == "LootAccept")
	{
    	Destroy(gameObject);
	//	Scorekeeper.multiplier += 1;
	}
}

function OnTriggerStay(other : Collider)
{
	if(other.gameObject.tag == "LootGrabber") 
	{
    	dir = playerVar.transform.position - transform.position; // calculate the target direction...
    	transform.rigidbody.velocity.x = dir.x * grabMul;
        transform.rigidbody.velocity.y = dir.y * grabMul;
    }
}

/*
static var isShuttingDown = false;

function Start () {

}

function Update () {

}

function OnApplicationQuit() {
	isShuttingDown = true;
}

function OnDestroy() {
	if ( !isShuttingDown )
	{
		var xPosition : int = transform.position.x;
		var yPosition : int = transform.position.y;
		LootManager.LootRoll(xPosition + 20, yPosition);
		LootManager.LootRoll(xPosition - 20, yPosition);
		Scorekeeper.score += 10 * Scorekeeper.multiplier;
	}
}
*/
/*
function OnTriggerEnter(other : Collider) 
{
	if(other.gameObject == lootGrabber.gameObject) 
	{
    	dir = playerVar.position - transform.position; // calculate the target direction...
    	transform.rigidbody.velocity.x = dir.x * Scorekeeper.grabberSpeed;
        transform.rigidbody.velocity.y = dir.y * Scorekeeper.grabberSpeed;
   
    }
	if(other.gameObject == lootAccept.gameObject)
	{
    	Destroy(gameObject);
		Scorekeeper.multiplier += 1;
	}
}

function OnTriggerStay(other : Collider)
{
	if(other.gameObject == lootGrabber.gameObject) 
	{
    	dir = playerVar.position - transform.position; // calculate the target direction...
    	transform.rigidbody.velocity.x = dir.x * Scorekeeper.grabberSpeed;
        transform.rigidbody.velocity.y = dir.y * Scorekeeper.grabberSpeed;
    }
} */