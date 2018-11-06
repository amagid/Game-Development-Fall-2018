using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Operator : MonoBehaviour {
	[SerializeField] private GameObject level1;
	[SerializeField] private GameObject level2;
	[SerializeField] private GameObject finalLevel;
	[SerializeField] private Text operatorText;
	[SerializeField] private GameObject sceneController;

	private float wordsPerSecond = 5f;
	private float timeElapsed = 0f;
	private const string level1Text = "Hello.\nGoal: get out.\nFind 3 batteries.";
	private const string level2Text = "";
	private const string finalText = "";
	private float wordCount = 0f;

	// Update is called once per frame
	void FixedUpdate () {
		timeElapsed += Time.fixedDeltaTime;

		wordCount = timeElapsed * wordsPerSecond;
		if (level1.activeSelf && !sceneController.GetComponent<SceneController> ().lvl1_complete) {
			animateDisplayMessage (level1Text, wordCount); 
		} else if (level2.activeSelf && !sceneController.GetComponent<SceneController> ().lvl2_complete) {
			animateDisplayMessage (level2Text, wordCount); 
		} else if (finalLevel.activeSelf && !sceneController.GetComponent<SceneController> ().game_complete) {
			animateDisplayMessage (finalText, wordCount); 
		} else {
			operatorText.text = string.Empty;
		}
	}

	private void animateDisplayMessage(string completeMessage, float wordCount){
		// full message has already been displayed - stop animation
		if (operatorText.text.Equals (completeMessage)) {
			return;
		}
		float words = wordCount;
		StringBuilder builder = new StringBuilder (); 
		foreach (char c in completeMessage) {
			// word count limit has been reached
			if (words < 0) {
				operatorText.text = builder.ToString ();
				return;
			} else {
				builder.Append (c);
				if (c != ' ') {
					words--;
				}
			}
		}
		operatorText.text = builder.ToString ();
	}
}
