using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationDirection : MonoBehaviour {
	private GUIStyle left = new GUIStyle();
	[SerializeField] private GameObject player;
	[SerializeField] private Text directionText;
	private enum CardinalDirection
	{
		North, South, East, West
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		var v = player.transform.forward;
		v.y = 0;
		v.Normalize ();
		Debug.Log ("PAY ATTENTION ********");
		Debug.Log (v);
		Debug.Log (Vector3.forward);
		Debug.Log (Vector3.Angle (v, Vector3.forward));
		Debug.Log ("PAY ATTENTION ********");

		if (Vector3.Angle (v, Vector3.forward) <= 45.0) {
			directionText.text = CardinalDirection.North.ToString();
			Debug.Log ("North");
		} else if (Vector3.Angle (v, Vector3.right) <= 45.0) {
			directionText.text = CardinalDirection.East.ToString();
			Debug.Log ("East");
		} else if (Vector3.Angle (v, Vector3.back) <= 45.0) {
			directionText.text = CardinalDirection.South.ToString();
			Debug.Log ("South");
		} else {
			directionText.text = CardinalDirection.West.ToString();
			Debug.Log ("West");
		}
	}
}
