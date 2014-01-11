#pragma strict

function FixedUpdate () {

 if(Input.GetAxis ("R_XAxis_1") != 0 || Input.GetAxis ("R_YAxis_1") != 0 )
		transform.up = Vector3(Input.GetAxis ("R_XAxis_1"), Input.GetAxis ("R_YAxis_1"), 0);
}
