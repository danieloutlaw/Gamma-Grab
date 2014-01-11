
function Start () {
	for( ; ; ) {
		yield WaitForSeconds(.1);
		if(Scorekeeper.cooldownTime > 0)
			Scorekeeper.cooldownTime -= .1;
		if(Menu.timeStopped) {
			var waitTime : float = Menu.sizeCooldown;
			yield WaitForSeconds(waitTime);
			Menu.timeStopped = false;
		}
	}
}
	
function FixedUpdate () {
	if(Input.GetButtonDown("X_1") /*&& Menu.level >= 1*/ && Scorekeeper.cooldownTime <= 0) {
		Scorekeeper.cooldownTime = Menu.speedCooldown;
		Menu.timeStopped = true;
	}
}