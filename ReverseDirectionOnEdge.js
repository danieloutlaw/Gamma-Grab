#pragma strict

public var rotationAmount : float = 0;

function Start () {
	rotationAmount = Random.Range(-7f,7f);
}

function FixedUpdate () {
	transform.Rotate(0,0,rotationAmount);
}

function OnTriggerEnter(collision : Collider) {
	if(collision.gameObject.tag == "Border") {
		var direction : Vector3;
		transform.rigidbody.velocity = Vector3.Reflect(transform.rigidbody.velocity, collision.gameObject.transform.forward);
		transform.up = transform.rigidbody.velocity.normalized;
	}
}