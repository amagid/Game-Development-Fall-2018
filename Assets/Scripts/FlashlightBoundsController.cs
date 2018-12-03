using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBoundsController : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        ShadowMonsterController monster = other.GetComponent<ShadowMonsterController>();
        if (monster != null)
        {
            monster.startStun();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShadowMonsterController monster = other.GetComponent<ShadowMonsterController>();
        if (monster != null)
        {
            monster.endingStun = true;
            monster.Invoke("endStun", 1f);
        }
    }
}
