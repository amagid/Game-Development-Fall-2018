using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Determines the direction the player is traveling in
public class NavigationDirection : MonoBehaviour {
	// stores the player object
	[SerializeField] GameObject player;
	[SerializeField] Image arrow;
	[SerializeField] float arrowRotationSpeed;

	private enum CardinalDirection{
		North, South, East, West
	}

	private Vector3 target; 
	private Dictionary<CardinalDirection, Vector3> directionToTarget = new Dictionary<CardinalDirection, Vector3>();

	void Start(){
		directionToTarget.Add (CardinalDirection.North, new Vector3 (0, 0, 0));
		directionToTarget.Add (CardinalDirection.South, new Vector3 (0, 0, -180));
		directionToTarget.Add (CardinalDirection.East, new Vector3 (0, 0, -90));
		directionToTarget.Add (CardinalDirection.West, new Vector3 (0, 0, 90));

		if (arrowRotationSpeed == 0) {
			arrowRotationSpeed = 1.5f;
		}
	}

	// Update is called once per frame
	void Update () {
		var v = player.transform.forward;
		v.y = 0;
		v.Normalize ();
		if (Vector3.Angle (v, Vector3.forward) <= 45.0) {
			RotateArrowTo(CardinalDirection.North);
		} else if (Vector3.Angle (v, Vector3.right) <= 45.0) {
			RotateArrowTo(CardinalDirection.East);
		} else if (Vector3.Angle (v, Vector3.back) <= 45.0) {
			RotateArrowTo(CardinalDirection.South);
		} else {
			RotateArrowTo(CardinalDirection.West);
		}
	}

	private void RotateArrowTo(CardinalDirection cardinalDirection){
		directionToTarget.TryGetValue(cardinalDirection, out target);
		arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, Quaternion.Euler(target), Time.deltaTime * arrowRotationSpeed);
	}
}
