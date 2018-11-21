using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformedDeath : MonoBehaviour {
	[SerializeField] Text typeOfDeath;
	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		typeOfDeath.text = PlayerPrefs.GetString ("endGame");
	}

	void Update(){
		Debug.Log (Time.timeSinceLevelLoad);
	}
}
