// Initializes the cooldown from the inventory
#pragma strict

public var cooldownObject : GameObject;

function Start () {
	if(Menu.level >= 20) {
		cooldownObject = Resources.Load(Menu.proTexturesCooldown);
		var cooldownObject1 = Instantiate(cooldownObject, transform.position, Quaternion.identity);
		cooldownObject1.transform.parent = transform;
		cooldownObject1.transform.up = transform.up;
	}
}

function Update () {
	
}