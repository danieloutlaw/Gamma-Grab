#pragma strict

//var direction : GameObject;
//private var velocity = Vector3.zero;
var smooth = 0.2;
//var targetPosition = Vector3.zero;
var rotateAmount : float;

function FixedUpdate () {
	gameObject.transform.Rotate(0,0,rotateAmount);
//	if(Input.GetAxis ("L_XAxis_1") != 0 || Input.GetAxis ("L_YAxis_1") != 0 )
//		transform.up = Vector3(Input.GetAxis ("L_XAxis_1"), Input.GetAxis ("L_YAxis_1"), 0);

	//	transform.up = Vector3.SmoothDamp(transform.up, targetPosition, velocity, smooth);
}

