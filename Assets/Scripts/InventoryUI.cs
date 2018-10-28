using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory playerInventory;
    [SerializeField] private GameObject item1UI;
    List<Image> imgs;
    private int numBatteries = 0;


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
        Debug.Log(numBatteries);
        for (int i = 0; i < numBatteries; i++){
            imgs[i].color = UnityEngine.Color.green;
        }
  

    }
}
