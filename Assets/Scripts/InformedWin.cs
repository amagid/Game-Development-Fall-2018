using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformedWin : MonoBehaviour {
    // stores the time in seconds the player survived
    [SerializeField] Text timeOfWin;

    void Start()
    {
        Cursor.visible = true;
        timeOfWin.text = string.Format("You escaped in {0:0.0} seconds.", PlayerPrefs.GetFloat("totalTime"));
    }
}
