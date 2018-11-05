using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour {

    [SerializeField] private GameObject tablet;
    
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            this.tablet.SetActive(!this.tablet.activeInHierarchy);
        }
    }
}
