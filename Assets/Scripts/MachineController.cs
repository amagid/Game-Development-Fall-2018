using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour {

    [SerializeField] private Light light;
    [SerializeField] private GameObject player;
    private Inventory inventory;
    //the number of parts that are needed to activate the machine
    private bool atMachine;
    private bool activated = false;
    public int numOfParts;

	// Use this for initialization
	void Start () {
        numOfParts = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(atMachine)
        {
            if(Input.GetKeyDown(KeyCode.E) && player.GetComponent<Inventory>().itemCount() == numOfParts && !activated)
            {
                StartCoroutine("activate");
                activated = true;
            }
        }
	}

    public IEnumerator activate()
    {
        for (float t = 0f; t < 1; t += Time.deltaTime / 1.5f)
        {
            if(light.range < 25f)
            {
                //light.intensity += 1f;
                light.range += 1f;
            }
        }
        yield return new WaitForSeconds(1.5f);
        for (float t = 0f; t < 1; t += Time.deltaTime / 1.5f)
        {
            if (light.range > 5f)
            {
                //light intenstiy -= 1f;
                light.range -= 1f;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            atMachine = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        atMachine = false;
    }


}
