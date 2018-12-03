using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSanityEffects : MonoBehaviour {

	[SerializeField] private float distanceCameraCanShakeFromOrigin; 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cube_prefab;
    [SerializeField] private Light camera_light;
	[SerializeField] private GameObject colorHaze;
    [SerializeField] private GameObject level_one;
    //audio
    private AudioSource breathing_source;
    private AudioSource whisper_source;
    private AudioSource tinnitus_source;
    private AudioSource ambience_source;
    public AudioClip heavy_breathing;
    public AudioClip whisper;
    public AudioClip tinnitus;
    public AudioClip general_ambience;

	private GameObject camera;
	[SerializeField] private GameObject cameraReference;
    private List<GameObject> current_cubes;
    private float current_level = 50f;
    private float sanity;

	// Use this for initialization
	void Start () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
        current_cubes = new List<GameObject>();
        breathing_source = GetComponent<AudioSource>();
        whisper_source = player.AddComponent<AudioSource>();
        tinnitus_source = player.AddComponent<AudioSource>();
        ambience_source = player.AddComponent<AudioSource>();
		camera = GameObject.Find("Camera");
	}
	
	// Update is called once per frame
	void Update () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
        float magnitude = 0f;
        if(!ambience_source.isPlaying)
        {
            ambience_source.PlayOneShot(general_ambience, 1f);
        }
        if(sanity < 60f) {
            if (!breathing_source.isPlaying)
            {
                breathing_source.PlayOneShot(heavy_breathing, (1f- sanity/100f));
            }
            StartCoroutine(Shake(0.1f, magnitude + (sanity - 60f) * 0.0005f));
        }

		if (sanity > 60f && camera.transform.position != cameraReference.transform.position) {
			camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraReference.transform.position, Time.deltaTime/20);	
		}



        if(sanity < 40f) {
            if (!whisper_source.isPlaying)
            {
                whisper_source.PlayOneShot(whisper, (1f-(sanity / 100f)) * 0.6f);
            }
            float current_index = sanity / 40f;
            changeSightRange(current_index * 10f);
            changeSightIntensity(current_index * 3f);
            changeSightAngle(current_index * 70);
        }

        if (sanity < 50f)
        {
            if (sanity <= current_level)
            {
                //generateRandomCubes(Mathf.RoundToInt(2f));
                current_level -= 10f;
            }
            
        }
        else if (sanity >= 50f && current_cubes.Count >= 0)
        {
            foreach(GameObject cube in current_cubes) {
                cube.GetComponent<ShadowMonsterController>().die(false);
            }
            current_cubes = new List<GameObject>();
            current_level = 50f;
        }
        if (sanity < 20f) {
            if (!tinnitus_source.isPlaying)
            {
                tinnitus_source.PlayOneShot(tinnitus, (1f-(sanity / 100f)) * 0.2f);
            }
        }
        /*
        if (sanity < 10f) {
			colorHaze.SetActive (true);
		} else {
			colorHaze.SetActive (false);
		}
        */
    }

    //camera shaking when sanity is lower than 70
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = camera.transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
			if (Mathf.Abs (camera.transform.position.x - cameraReference.transform.position.x) < distanceCameraCanShakeFromOrigin && Mathf.Abs (camera.transform.position.y - cameraReference.transform.position.y) < distanceCameraCanShakeFromOrigin && Mathf.Abs (camera.transform.position.z - cameraReference.transform.position.z) < distanceCameraCanShakeFromOrigin) {
				camera.transform.Translate (Random.insideUnitCircle * magnitude);
			} else {
				camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraReference.transform.position, Time.deltaTime);
			//	camera.transform.Translate (player.transform.position);
			}

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
		int count = 0;
		while (count < number){
            //x ranges from -24 to 24 in Zone 1 and 2 (excluding elevator and Zone 3)
            float x = Random.Range(-24f, 24f);
            //-1f ensures the cube was invisible when instantiated
            float y = -1f;
            //z ranges from 24 to -24
            float z = Random.Range(-24f, 24f);
			Vector3 proposedCubePosition = new Vector3(x, y, z);
			float sphereRadius = 2f;
			int layerMask = ~(1 << 9);
			// check to see that there is no other object that collides with the where the cube will be placed
			if (true && !Physics.CheckSphere(proposedCubePosition, sphereRadius, layerMask)) {
				// create cube
				GameObject cube = Instantiate(cube_prefab, proposedCubePosition, Quaternion.Euler(0, Random.value * 360, 0)) as GameObject;
				current_cubes.Add(cube);
				cube.transform.parent = level_one.transform;
				count++;
			}
        }
    }

    //generate the number of cubes appearing
    public void generateRandomCubesByPlayer(int number)
    {
        int count = 0;
        while (count < number)
        {
            //x ranges from -24 to 24 in Zone 1 and 2 (excluding elevator and Zone 3)
            float x = Random.value * 4 - 2;
            //-1f ensures the cube was invisible when instantiated
            float y = -1f;
            //z ranges from 24 to -24
            float z = Random.value * 4 - 2;
            Vector3 proposedCubePosition = new Vector3(this.player.transform.position.x + x, y, this.player.transform.position.z + z);
            float sphereRadius = 2f;
            int layerMask = ~(1 << 9);
            // check to see that there is no other object that collides with the where the cube will be placed
            if (true && !Physics.CheckSphere(proposedCubePosition, sphereRadius, layerMask))
            {
                // create cube
                GameObject cube = Instantiate(cube_prefab, proposedCubePosition, Quaternion.Euler(0, Random.value * 360, 0)) as GameObject;
                current_cubes.Add(cube);
                cube.transform.parent = level_one.transform;
                count++;
            }
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

    //AUDIO
}
