using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformedDeath : MonoBehaviour {
	[SerializeField] Text typeOfDeath;
	[SerializeField] Text timeOfDeath;
	void Start () {
		Cursor.visible = true;
		typeOfDeath.text = PlayerPrefs.GetString ("endGame");
		timeOfDeath.text = string.Format ("You survived for {0:0.0} seconds.", PlayerPrefs.GetFloat ("totalTime"));
	}
}
