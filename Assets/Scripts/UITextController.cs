using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour {

    public GameObject sanityTextObject;
    public GameObject batteryTextObject;
    public GameObject powerTextObject;
    public GameObject player; 

    private TMP_Text sanityText;
    private TMP_Text batteryText;
    private TMP_Text powerText;

    private int sanityValue;
    private int powerValue;
    private int batteryAmount; 

	// Use this for initialization
	void Start () {
        sanityText = sanityTextObject.GetComponent<TMP_Text>();
        powerText = powerTextObject.GetComponent<TMP_Text>();
        batteryText = batteryTextObject.GetComponent<TMP_Text>();
	}
	
	// Update is called once per frame
	void Update () {
        this.sanityValue = player.GetComponent<PlayerCharacter>().GetSanity();
        this.powerValue = player.GetComponent<PlayerCharacter>().GetPower();
        this.batteryAmount = player.GetComponent<PlayerCharacter>().GetInventory()
            .itemCount();
        this.sanityText.text = "Sanity: " + sanityValue; 
        this.powerText.text = "Power: " + powerValue; 
        this.batteryText.text = "Battery: " + batteryAmount; 
	}
}
