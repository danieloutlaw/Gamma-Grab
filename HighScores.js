#pragma strict

function Start ()
{
	guiText.text = PlayerPrefs.GetInt("High Score") + "\n" + PlayerPrefs.GetInt("2 Score") + "\n" + PlayerPrefs.GetInt("3 Score") + 
	"\n" + PlayerPrefs.GetInt("4 Score") + "\n" + PlayerPrefs.GetInt("5 Score") + "\n" + PlayerPrefs.GetInt("6 Score") + "\n" + 
	PlayerPrefs.GetInt("7 Score") + "\n" + PlayerPrefs.GetInt("8 Score") + "\n" + PlayerPrefs.GetInt("9 Score") + "\n" + PlayerPrefs.GetInt("10 Score");
}