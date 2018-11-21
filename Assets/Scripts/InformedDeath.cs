using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformedDeath : MonoBehaviour {
	// stores the type of player death - no power, insane
	[SerializeField] Text typeOfDeath;
	// stores the time in seconds the player survived
	[SerializeField] Text timeOfDeath;

	void Start () {
		Cursor.visible = true;
		typeOfDeath.text = PlayerPrefs.GetString ("endGame");
		timeOfDeath.text = string.Format ("You survived for {0:0.0} seconds.", PlayerPrefs.GetFloat ("totalTime"));
	}
}
