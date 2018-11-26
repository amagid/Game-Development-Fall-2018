using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelector : MonoBehaviour
{

    [SerializeField] private GameObject selector;
    private bool selected;
    // Use this for initialization
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        Debug.Log("21312312");
    }

    void OnMouseExit()
    {
        if (selected == true) {
            selector.SetActive(false);
        }
    }

}
