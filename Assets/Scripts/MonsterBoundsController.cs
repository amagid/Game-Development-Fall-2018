using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoundsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerExit(Collider other)
    {
        ShadowMonsterController monster = other.GetComponent<ShadowMonsterController>();
        if (monster != null && monster.bounds == this.gameObject)
        {
            monster.movingTo = monster.transform.position + (this.GetComponent<Collider>().bounds.center - monster.transform.position) / 5f;
            monster.movingFrom = monster.transform.position;
            monster.resetMovementTimer();
            monster.movementTime = monster.interval;
            monster.inBounds = false;
            Invoke("setMovementTarget", monster.interval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ShadowMonsterController monster = other.GetComponent<ShadowMonsterController>();
        if (monster != null && monster.bounds == this.gameObject)
        {
            monster.inBounds = true;
        }
    }
}
