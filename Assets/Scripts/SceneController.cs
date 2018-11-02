using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject elevator_light;
    [SerializeField] private GameObject elevator_door;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    //[SerializeField]
    [SerializeField] private GameObject elevator_outside_lights;
    public bool lvl1_complete = false;


    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
        if(lvl1_complete) {
            elevator_light.SetActive(false);
        }
        //light flicker
        if (Random.value > 0.9)
        {
            if (elevator_light.active == true)
            {
                elevator_light.active = false;
            }
            else
            {
                elevator_light.active = true;
            }
        }
    }

    public IEnumerator loadLevel2()
    {
        elevator_light.SetActive(false);
        yield return new WaitForSeconds(4f);
        level1.SetActive(false);
        level2.SetActive(true);
        StartCoroutine("elevatorMovingAnimation");
        yield return new WaitForSeconds(6f);
        elevator_door.GetComponent<DoorController>().StartCoroutine("openDoor");
    }

    public IEnumerator elevatorMovingAnimation() {
        for (int i = 0; i < 3; i++)
        {
            elevator_outside_lights.SetActive(true);
            Vector3 initPos = elevator_outside_lights.transform.position;
            float x = elevator_outside_lights.transform.position.x;
            float y = elevator_outside_lights.transform.position.y - 20f;
            float z = elevator_outside_lights.transform.position.z;
            Vector3 finalPos = new Vector3(x, y, z);
            for (float t = 0f; t < 1; t += Time.deltaTime / 1f)
            {
                elevator_outside_lights.transform.position = Vector3.Lerp(initPos, finalPos, t);
                yield return null;
            }
            //moving the lights back to init position after it finished moving up
            elevator_outside_lights.transform.position = initPos;
            elevator_outside_lights.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
