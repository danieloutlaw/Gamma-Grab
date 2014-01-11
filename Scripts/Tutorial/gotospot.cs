using UnityEngine;
using System.Collections;


public class gotospot : MonoBehaviour {
	
	GameObject playerVar;
	public bool isTouched = false;
	exSprite mySprite;
	//public GameObject goToSpot;
	
	public GUIStyle instruction;
	
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "PlayerShip")
			isTouched = true;
		if(collider.gameObject.tag == "KillZone")
			Destroy (gameObject);
	}
	void Update () {
		
			
		if(isTouched)
			mySprite.color = Color.green;
	}
	
	void Start () {
		playerVar = GameObject.FindWithTag ("PlayerShip");
		mySprite = gameObject.GetComponent<exSprite>();
	}
	
}