using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSanityEffects : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cube_prefab;
    [SerializeField] private Light camera_light;
	[SerializeField] private GameObject colorHaze;
    [SerializeField] private GameObject level_one;

    private List<GameObject> current_cubes;
    private float current_level = 50f;
    private float sanity;

	// Use this for initialization
	void Start () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
        current_cubes = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
        float magnitude = 0f;

        if(sanity < 60f) {
            StartCoroutine(Shake(0.1f, magnitude + (sanity - 60f) * 0.001f));
        }

        if(sanity < 40f) {
            float current_index = sanity / 40f;
            changeSightRange(current_index * 10f);
            changeSightIntensity(current_index * 3f);
            changeSightAngle(current_index * 70);
        }

        if (sanity < 50f)
        {
            if (sanity < current_level)
            {
                generateRandomCubes(Mathf.RoundToInt((1f - sanity / 50f) * 20f));
                current_level -= 5f;
            }
        }
        else if (sanity >= 50f && current_cubes.Count >= 0)
        {
            foreach(GameObject cube in current_cubes) {
                StartCoroutine(cubeTransform(cube, new Vector3(cube.transform.position.x, cube.transform.position.y - 3f, cube.transform.position.z), 0.5f));
                StartCoroutine(cubeDestroy(cube));
            }
            current_cubes = new List<GameObject>();
            current_level = 50f;
        }

        if (sanity < 10f) {
			colorHaze.SetActive (true);
		} else {
			colorHaze.SetActive (false);
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
    public void changeSightRange(float newValue) {
        camera_light.range = newValue;
    }

    //change intensity of light
    //default value is 3.0
    public void changeSightIntensity(float newValue) {
        camera_light.intensity = newValue;
    }

    //change the angle of sight
    //default sightangle is 70
    public void changeSightAngle(float newValue) {
        camera_light.spotAngle = newValue;
    }

    //generate the number of cubes appearing
    public void generateRandomCubes(int number) {
        for (int i = 0; i < number; i++)
        {
            GameObject cube = Instantiate(cube_prefab) as GameObject;
            current_cubes.Add(cube);
            cube.transform.parent = level_one.transform;
            //x ranges from -24 to 24 in Zone 1 and 2 (excluding elevator and Zone 3)
            float x = Random.Range(-24f, 24f);
            //-1f ensures the cube was invisible when instantiated
            float y = -1f;
            //z ranges from 24 to -24
            float z = Random.Range(-24f, 24f);
            cube.transform.position = new Vector3(x, y, z);
            cube.transform.eulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);
            //make the cube emerge from the ground
            StartCoroutine(cubeTransform(cube, new Vector3(x, y + 3f, z), 1f));
        }
    }

    //cube_prefab will appear from under the floor
    private IEnumerator cubeTransform(GameObject cube, Vector3 finalPos, float time)
    {
        Vector3 currentPos = cube.transform.position;
        for(float t = 0f; t < 1; t += Time.deltaTime/time)
        {
            cube.transform.position = Vector3.Lerp(currentPos, finalPos, t);
            yield return null;
        }
    }

    //a coroutine that will destroy a cube
    private IEnumerator cubeDestroy(GameObject cube)
    {
        yield return new WaitForSeconds(1f);
        Destroy(cube);
        yield return null;
    }
}
