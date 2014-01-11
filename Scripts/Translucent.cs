using UnityEngine;
using System.Collections;

public class Translucent : MonoBehaviour {
	exSprite mySprite;
	bool switchedUp = false;
	bool switchedDown = false;

	// Use this for initialization
	void Start () {
		mySprite = gameObject.GetComponent<exSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z < -9 && mySprite && !switchedUp) {
			mySprite.color =  new Color(1f,1f,1f,.2f);
			switchedUp = true;
		}
		if(transform.position.z > -8 && mySprite && !switchedDown) {
			mySprite.color =  new Color(1f,1f,1f,1f);
			switchedDown = true;
		}
	} 
}
