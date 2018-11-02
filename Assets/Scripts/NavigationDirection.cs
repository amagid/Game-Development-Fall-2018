using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationDirection : MonoBehaviour {
	[SerializeField] private GameObject player;
	[SerializeField] private Text directionText;
	private enum CardinalDirection
	{
		North, South, East, West
	}
	
	// Update is called once per frame
	void Update () {
		var v = player.transform.forward;
		v.y = 0;
		v.Normalize ();

		if (Vector3.Angle (v, Vector3.forward) <= 45.0) {
			directionText.text = CardinalDirection.North.ToString();
		} else if (Vector3.Angle (v, Vector3.right) <= 45.0) {
			directionText.text = CardinalDirection.East.ToString();
		} else if (Vector3.Angle (v, Vector3.back) <= 45.0) {
			directionText.text = CardinalDirection.South.ToString();
		} else {
			directionText.text = CardinalDirection.West.ToString();
		}
	}
}
