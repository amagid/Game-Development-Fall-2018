using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour {

    [SerializeField] private GameObject Tablet;
    private bool isOn;
    // Use this for initialization
    void Start () {
        isOn = false;
    }
    
    // Update is called once per frame
    void Update () {
        if (isOn == false && Input.GetKeyDown(KeyCode.I)) {
            Tablet.SetActive(true);
            isOn = true;
       
        }
        else {
            if (Input.GetKeyDown(KeyCode.I)) {
                Tablet.SetActive(false);
                isOn = false;
                //comment
            }
        }
    }
}
