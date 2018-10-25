using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory playerInventory;
    [SerializeField] private GameObject item1UI;




    // Use this for initialization
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInventory.getFirstItem() != null)
        {
            item1UI.SetActive(true);
        
        }
        else
        {
            item1UI.SetActive(false);
        }


        item1UI.SetActive(playerInventory.containItem("Battery(1)"));
        if (playerInventory.containItem("Battery(1)")) {
            Debug.Log("abcde");
        }
        item1UI.SetActive(!item1UI.activeInHierarchy);


    }
}
