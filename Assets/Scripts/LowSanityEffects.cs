using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSanityEffects : MonoBehaviour {

    [SerializeField] private GameObject player;

    [SerializeField] private Light camera_light;

    private float sanity;

	// Use this for initialization
	void Start () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
	}
	
	// Update is called once per frame
	void Update () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
        float magnitude = 0f;
        //sanity checks to see which effects will take place
        if(sanity < 60f) {
            StartCoroutine(Shake(0.1f, magnitude + (sanity - 60f) * 0.001f));
        }
        if(sanity < 40f) {
            changeSightRange(-(Time.deltaTime));
            changeSightIntensity(-(Time.deltaTime));
            changeSightAngle(-(Time.deltaTime));
        }
        if(sanity < 20f) {
            changeSightColor(Color.red);
        }
    }

    //camera shaking when sanity is lower than 70
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = player.transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            player.transform.Translate(Random.insideUnitCircle * magnitude);
            elapsed += Time.deltaTime;
            yield return 0;
        }
    }

    //change range of sight
    //default range is 10
    public void changeSightRange(float difference) {
        camera_light.range += difference;
    }

    //change intensity of light
    //default value is 3.0
    public void changeSightIntensity(float difference) {
        camera_light.intensity += difference;
    }

    //change the angle of sight
    //default sightangle is 70
    public void changeSightAngle(float difference) {
        camera_light.spotAngle += difference;
    }

    //change player's self emitting light's color
    public void changeSightColor(Color color) {
        //see if changing the color gradually would work.
        //Color.Lerp wouldn't work.
        camera_light.color = color;
    }

    //player will be able to solve a level easier if his sanity level is high

    //cubes appearing in random places
    public void randomCubes() {

    }
}
