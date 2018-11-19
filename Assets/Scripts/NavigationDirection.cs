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
        this.RotateArrowTo(-player.transform.eulerAngles.y);
	}

	private void RotateArrowTo(float angle){
		arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
