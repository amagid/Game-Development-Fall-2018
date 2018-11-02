using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private Inventory playerInventory;
    [SerializeField] private GameObject item1UI;
    List<Image> imgs;
    int selectedIndex = -1; 
    private int numBatteries = 0;


    public void highLight(int indes) {
        if(selectedIndex == indes) {
            selectedIndex = -1;
        } else {
            selectedIndex = indes;
        }
    }

    // Use this for initialization
    void Start()
    {
        imgs = new List<Image>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        for (int i = 0; i < 8; i++) {
            int j = i + 1;
            string tag = "Slot" + j;
            imgs.Add(GameObject.Find(tag).GetComponent<Image>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        numBatteries = playerInventory.batteryCount();
        for (int i = 0; i < numBatteries; i++){
            imgs[i].color = UnityEngine.Color.green;
            int j = i + 1; 
            GameObject.Find("Slot" + j).GetComponent<Outline>().effectColor = UnityEngine.Color.black;
        }
        for (int i = numBatteries; i < 8; i++) {
            int j = i + 1; 
            imgs[i].color = UnityEngine.Color.red;
            GameObject.Find("Slot" + j).GetComponent<Outline>().effectColor = UnityEngine.Color.black;
        }
        if (selectedIndex >= numBatteries)
        {
            selectedIndex = -1;
        }
        if(selectedIndex != -1) {
            int index = selectedIndex + 1;
            GameObject.Find("Slot" + index).GetComponent<Outline>().effectColor = UnityEngine.Color.white;
        }
        playerInventory.selectedBatteryIndex = selectedIndex;
  

    }
}
