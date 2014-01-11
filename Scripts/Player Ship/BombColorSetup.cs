using UnityEngine;
using System.Collections;

public class BombColorSetup : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Color bombColor;
		if(Menu.bombSpeed >= 90)
			bombColor = Color.blue;
		else if(Menu.bombSpeed >= 80)
			bombColor = Color.cyan;
		else if(Menu.bombSpeed >= 70)
			bombColor = Color.green;
		else if(Menu.bombSpeed >= 60)
			bombColor = Color.magenta;
		else if(Menu.bombSpeed >= 50)
			bombColor = Color.red;
		else if(Menu.bombSpeed >= 40)
			bombColor = Color.yellow;
		else if(Menu.bombSpeed >= 30)
			bombColor = new Color(1f,.5f,0f,1f);
		else if(Menu.bombSpeed >= 20)
			bombColor = Color.black;
		else if(Menu.bombSpeed >= 10)
			bombColor = Color.white;
		else 
		bombColor = Color.gray;
		DetonatorBomb.color = bombColor;
	//	float durationMult = 1f;
	//	DetonatorBomb.size = Menu.bombSize * 3;
	//	DetonatorBomb.duration = durationMult;
	//	DetonatorBomb.destroyTime = durationMult * 4;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

