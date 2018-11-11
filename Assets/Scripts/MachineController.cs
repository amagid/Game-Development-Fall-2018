using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour {

    [SerializeField] private Light light;
    [SerializeField] private GameObject player;
    private Inventory inventory;
    //the number of parts that are needed to activate the machine
    private bool atMachine;
	public static bool activated = false;
    public int numOfParts;

	// Use this for initialization
	void Start () {
        numOfParts = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;
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
        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            if(light.range < 30f)
            {
                light.intensity += 0.8f;
                light.range += 0.2f;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.0f);

        //add an indicator that the machine has been built
        player.GetComponent<PlayerCharacter>().pauseSanityLoss();

        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            if (light.range > 8f)
            {
                light.intensity -= 0.5f;
                light.range -= 0.2f;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
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
