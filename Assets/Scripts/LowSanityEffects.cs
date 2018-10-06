using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSanityEffects : MonoBehaviour {

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject camera_light;

    private float sanity;

	// Use this for initialization
	void Start () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
	}
	
	// Update is called once per frame
	void Update () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();

        //sanity checks to see which effects will take place
        if(sanity < 70f) {
            StartCoroutine(Shake(1f, 0.01f));
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

    //decrease range of sight
    public void decreaseSightRange(float difference) {

    }

    //decrease the angle of sight
    public void decreaseSightAngle(float difference) {

    }

    //change player's self emitting light's color
    public void changeSightColor(Color color) {

    }
    //player will be able to solve a level easier if his sanity level is high

    //cubes appearing in random places
    public void randomCubes() {

    }
}
