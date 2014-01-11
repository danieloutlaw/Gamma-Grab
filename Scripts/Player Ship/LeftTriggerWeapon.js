// Movement for the player's ship

var speed : float = 50.0;
var projectile : GameObject;
var projectileNum : int = 0;
var rearProjectileNum : int = 0;
var speedMultiplier : double;
var gun1 : GameObject;
var gun2 : GameObject;
var gun3 : GameObject;
var gun4 : GameObject;
var gun5 : GameObject;
var gun6 : GameObject;
var gun7 : GameObject;
var gun8 : GameObject;
var gun9 : GameObject;
var gun10 : GameObject;
var gun11 : GameObject;
var gun12 : GameObject;
var gun13 : GameObject;
var gun14 : GameObject;
var gun15 : GameObject;
var reverseGun1 : GameObject;
var reverseGun2 : GameObject;
var reverseGun3 : GameObject;
var reverseGun4 : GameObject;
var reverseGun5 : GameObject;
//var soundEffect : GameObject;
var weaponType : String;
var weaponSpeed : float;
var speedMult : float = 1;

function Start () 
{
	for(var i = 0; i <= 5; i++ )
	{
		if(!Input.GetButton("A_1") && !Input.GetButton("A_2")){
		var verticalTranslation : float = 0;
		var horizontalTranslation : float = 0;
		if(Input.GetAxisRaw ("R_YAxis_1") >= .05 || Input.GetAxisRaw ("R_YAxis_1") <= -.5)
			verticalTranslation = Input.GetAxisRaw ("R_YAxis_1");
		else if(Input.GetAxisRaw ("R_YAxis_2") >= .05 || Input.GetAxisRaw ("R_YAxis_2") <= -.5)
			verticalTranslation = Input.GetAxisRaw ("R_YAxis_2");
    	if(Input.GetAxisRaw ("R_XAxis_1") >= .05 || Input.GetAxisRaw ("R_XAxis_1") <= -.5)
			horizontalTranslation = Input.GetAxisRaw ("R_XAxis_1");
		else if(Input.GetAxisRaw ("R_XAxis_2") >= .05 || Input.GetAxisRaw ("R_XAxis_2") <= -.5)
			horizontalTranslation = Input.GetAxisRaw ("R_XAxis_2");
		if(Input.GetAxis("Triggers_1") > 0.5 && (verticalTranslation >= .05 || horizontalTranslation >= .05 || verticalTranslation <= -.05 || horizontalTranslation <= -.05)) {
			projectile = Resources.Load(Menu.proTexturesLeft);
			projectileNum = Menu.projectilesLeft + Menu.boosterProjectiles + Menu.projectilesCooldown + Menu.projectilesFirstPassive + Menu.projectilesSecondPassive;
			rearProjectileNum = Menu.rearProjectilesLeft + Menu.boosterRearProjectiles + Menu.rearProjectilesCooldown + Menu.rearProjectilesFirstPassive + Menu.rearProjectilesSecondPassive;
			weaponType = Menu.proTexturesLeft;
			weaponSpeed = Menu.speedLeft;
			Fire();
			if(Menu.proTexturesLeft == "repeater") {
				yield WaitForSeconds(.02 / Menu.speedLeft );
			}
			else {
				yield WaitForSeconds(.5 / Menu.speedLeft );
			}
		}
		else if(Input.GetAxis("Triggers_1") < -0.5 && (verticalTranslation >= .05 || horizontalTranslation >= .05 || verticalTranslation <= -.05 || horizontalTranslation <= -.05)) {
			projectile = Resources.Load(Menu.proTexturesRight);
			projectileNum = Menu.projectilesRight + Menu.boosterProjectiles + Menu.projectilesCooldown + Menu.projectilesFirstPassive + Menu.projectilesSecondPassive;
			rearProjectileNum = Menu.rearProjectilesRight + Menu.boosterRearProjectiles + Menu.rearProjectilesCooldown + Menu.rearProjectilesFirstPassive + Menu.rearProjectilesSecondPassive;
			weaponType = Menu.proTexturesRight;
			weaponSpeed = Menu.speedRight;
			Fire();
			if(Menu.proTexturesRight == "repeater") {
				yield WaitForSeconds(.02 / Menu.speedRight );
			}
			else {
				yield WaitForSeconds(.5 / Menu.speedRight );
			}
		}
		else if(Input.GetButton("LB_1") && Menu.level >= 25 && (verticalTranslation >= .05 || horizontalTranslation >= .05 || verticalTranslation <= -.05 || horizontalTranslation <= -.05)) {
			projectile = Resources.Load(Menu.proTexturesLeftBumper);
			projectileNum = Menu.projectilesLeftBumper + Menu.boosterProjectiles + Menu.projectilesCooldown + Menu.projectilesFirstPassive + Menu.projectilesSecondPassive;
			rearProjectileNum = Menu.rearProjectilesLeftBumper + Menu.boosterRearProjectiles + Menu.rearProjectilesCooldown + Menu.rearProjectilesFirstPassive + Menu.rearProjectilesSecondPassive;
			weaponType = Menu.proTexturesLeftBumper;
			weaponSpeed = Menu.speedLeftBumper;
			Fire();
			if(Menu.proTexturesLeftBumper == "repeater") {
				yield WaitForSeconds(.02 / Menu.speedLeftBumper );
			}
			else {
				yield WaitForSeconds(.5 / Menu.speedLeftBumper );
			}
		}
		else if(Input.GetButton("RB_1") && Menu.level >= 30 && (verticalTranslation >= .05 || horizontalTranslation >= .05 || verticalTranslation <= -.05 || horizontalTranslation <= -.05)) {
			projectile = Resources.Load(Menu.proTexturesRightBumper);
			projectileNum = Menu.projectilesRightBumper + Menu.boosterProjectiles + Menu.projectilesCooldown + Menu.projectilesFirstPassive + Menu.projectilesSecondPassive;
			rearProjectileNum = Menu.rearProjectilesRightBumper + Menu.boosterRearProjectiles + Menu.rearProjectilesCooldown + Menu.rearProjectilesFirstPassive + Menu.rearProjectilesSecondPassive;
			weaponType = Menu.proTexturesRightBumper;
			weaponSpeed = Menu.speedRightBumper;
			Fire();
			if(Menu.proTexturesRightBumper == "repeater") {
				yield WaitForSeconds(.02 / Menu.speedRightBumper );
			}
			else {
				yield WaitForSeconds(.5 / Menu.speedRightBumper );
			}
		}
		}
		yield WaitForSeconds(.001);
		i--;
	}
//	}
}

function Fire() {
	if(Menu.rearProjectilesShield == 0 || !Scorekeeper.shieldOut) {
	if(weaponType == "repeater")
		speedMult = .2;
	else
		speedMult = 1;
	if(projectileNum == 1) {
		var projectile11 = Instantiate(projectile, gun1.transform.position, Quaternion.identity);
    	projectile11.rigidbody.velocity = gun1.transform.up * 250 * weaponSpeed * speedMult;
		projectile11.transform.up = gun1.transform.up;
	}
	else {
		var adder : int = 0;
		if(projectileNum % 2 != 0) {
			var projectile1 = Instantiate(projectile, gun1.transform.position, Quaternion.identity);
			projectile1.transform.up = gun1.transform.up;
    		projectile1.rigidbody.velocity = gun1.transform.up * 250 * weaponSpeed * speedMult;
			adder = 1;
		}
		if(weaponType == "boomerang" && projectileNum > 1)
			yield WaitForSeconds((.5 / weaponSpeed) / projectileNum);
		var rotateAmount : int = 0;
		for(var i : int = 1; i <= projectileNum - adder; i++) {
			if(i % 2 == 1) {
				if(weaponType == "boomerang" || weaponType == "dozer" || (projectileNum >= 3 && projectileNum % 2 == 1))
					rotateAmount += 5;
				if(weaponType == "dozer")
					rotateAmount += 5;
				var projectile2 = Instantiate(projectile, gun2.transform.position, Quaternion.identity);
				projectile2.transform.up = gun2.transform.up;
				projectile2.transform.Rotate(0,0,-rotateAmount);
    			projectile2.rigidbody.velocity = projectile2.transform.up * 250 * weaponSpeed * speedMult;
    			if(weaponType == "boomerang" && projectileNum - adder >= i + 1)
					yield WaitForSeconds((.5 / weaponSpeed) / projectileNum);
			}
			else {
				var projectile3 = Instantiate(projectile, gun3.transform.position, Quaternion.identity);
				projectile3.transform.up = gun3.transform.up;
				projectile3.transform.Rotate(0,0,rotateAmount);
				rotateAmount += 5;
    			projectile3.rigidbody.velocity = projectile3.transform.up * 250 * weaponSpeed * speedMult;
    			if(weaponType == "boomerang" && projectileNum - adder >= i + 1)
					yield WaitForSeconds((.5 / weaponSpeed) / projectileNum);
			}
		}
	}
	if(rearProjectileNum > 0) {
		var rearAdder : int = 0;
		if(rearProjectileNum == 1 || rearProjectileNum % 2 != 0) {
			var rearProjectile11 = Instantiate(projectile, reverseGun1.transform.position, Quaternion.identity);
    		rearProjectile11.rigidbody.velocity = reverseGun1.transform.up * 250 * weaponSpeed * speedMult;
			rearProjectile11.transform.up = reverseGun1.transform.up;
			rearAdder = 1;
		}
		
		var rearRotateAmount : int = 0;
		for(var j : int = 1; j <= rearProjectileNum - rearAdder; j++) {
			if(j % 2 == 1) {
				if(weaponType == "boomerang" || weaponType ==  "dozer")
					rearRotateAmount += 5;
				var rearProjectile2 = Instantiate(projectile, reverseGun2.transform.position, Quaternion.identity);
				rearProjectile2.transform.up = reverseGun2.transform.up;
				rearProjectile2.transform.Rotate(0,0,-rearRotateAmount);
    			rearProjectile2.rigidbody.velocity = rearProjectile2.transform.up * 250 * weaponSpeed * speedMult;
			}
			else {
				var rearProjectile3 = Instantiate(projectile, reverseGun3.transform.position, Quaternion.identity);
				rearProjectile3.transform.up = reverseGun3.transform.up;
				rearProjectile3.transform.Rotate(0,0,rearRotateAmount);
				rearRotateAmount += 5;
    			rearProjectile3.rigidbody.velocity = rearProjectile3.transform.up * 250 * weaponSpeed * speedMult;
			}
		}
	}
    }
}