#pragma strict

var slurpSound : Transform;

function OnTriggerEnter(other : Collider) {
	if(other.gameObject.tag == "LootGrabber") 
	{
		var slurpSound1 = Instantiate(slurpSound, transform.position, Quaternion.identity);
    }
}