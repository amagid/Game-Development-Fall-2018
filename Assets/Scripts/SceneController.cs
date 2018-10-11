using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject elevator_light;
    [SerializeField] private GameObject elevator_door;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    public bool lvl1_complete = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        yield return new WaitForSeconds(6f);
        level1.SetActive(false);
        level2.SetActive(true);
        lvl1_complete = false;
        yield return new WaitForSeconds(3f);
        elevator_door.GetComponent<DoorController>().StartCoroutine("openDoor");
    }
}
