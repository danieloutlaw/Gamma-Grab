var scale : float = 1.0;
var growthRate : float = 202.0;
var size : float = 100.0;


function Start () {
	size = Menu.bombSize;
/*	for( ; ; ) {
		transform.localScale = Vector3.one * scale;
		if(scale >= size)
			Destroy(gameObject);
		if(scale < size)
			scale += growthRate;
			yield WaitForSeconds(.01);
		} */
}
	
function FixedUpdate () {
	transform.localScale = Vector3.one * scale;
		if(scale >= size)
			Destroy(gameObject);
		if(scale < size)
			scale += growthRate;
	
}