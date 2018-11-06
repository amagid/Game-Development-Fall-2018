using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour {

    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject inventoryBar;


    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            tablet.SetActive(true);
            inventoryBar.SetActive(false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                tablet.SetActive(false);
                inventoryBar.SetActive(true);
            }
        }
    }
}
