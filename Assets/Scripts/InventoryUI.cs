using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private Inventory playerInventory;
    [SerializeField] private GameObject item1UI;
    List<Image> imgs;
    List<Image> powerLevels;
    int selectedIndex = 0;
    private bool updateOn;
    private bool charging = false;
    private bool discharging = false;
    private PowerSource playerBattery;


    public void highLight(int index)
    {
        selectedIndex = index;
    }

    // Use this for initialization
    void Start()
    {
        updateOn = true;
        imgs = new List<Image>();
        powerLevels = new List<Image>();
        GameObject player = GameObject.Find("Player"); 
        playerInventory = player.GetComponent<Inventory>();
        playerBattery = player.GetComponent<PowerConsumer>().getPowerSource();
        for (int i = 0; i < playerInventory.inventorySize; i++)
        {
            string tag = "Slot" + (i + 1);
            string powerLevelTag = "Power Level " + (i + 1);
            imgs.Add(GameObject.Find(tag).GetComponent<Image>());
            powerLevels.Add(GameObject.Find(powerLevelTag).GetComponent<Image>());

        }


    }

    // Update is called once per frame
    // TODO: Refactor so that updates are only done on events rather than every frame to improve performance
    void Update()
    {
        if (updateOn)
        {
            for (int i = 0; i < playerInventory.inventorySize; i++)
            {
                if (playerInventory.getItem(i) != null)
                {
                    imgs[i].color = UnityEngine.Color.green; // TODO: Set image to item icon once we have icons
                    powerLevels[i].color = UnityEngine.Color.green;
                    if (playerInventory.getItem(i).GetComponent<Battery>() != null)
                    {
                        float width;
                        if (playerInventory.getItem(i).GetComponent<Battery>().getPowerSource().getPowerLevel() >= 100)
                        {
                            width = 100 / 3;   // Maximum length
                        }
                        else
                        {
                            // Bar width decreases only when powerLeverl < 100
                            width = playerInventory.getItem(i).GetComponent<Battery>().getPowerSource().getPowerLevel() / 3;
                        }
                        RectTransform rt = (RectTransform)powerLevels[i].gameObject.transform;
                        rt.sizeDelta = new Vector2(width, 5);
                    }
                }
                else
                {
                    imgs[i].color = UnityEngine.Color.gray; // TODO: Revisit later to potentially improve look
                    RectTransform rt = (RectTransform)powerLevels[i].gameObject.transform;
                    rt.sizeDelta = new Vector2(0, 0);
                }
                GameObject.Find("Slot" + (i + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.black;
            }
            if (selectedIndex < 0 || selectedIndex > playerInventory.inventorySize)
            {
                selectedIndex = 0;
            }
            GameObject.Find("Slot" + (selectedIndex + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.white;


            // Set selected slot with keys
            if (Input.GetKeyDown(KeyCode.Alpha1))
                selectedIndex = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                selectedIndex = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                selectedIndex = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                selectedIndex = 3;
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                selectedIndex = 4;
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                selectedIndex = 5;
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                selectedIndex = 6;
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                selectedIndex = 7;

            playerInventory.setSelectedItemIndex(selectedIndex);
        }
    }

    public void clickChargePlayer()
    {

        this.discharging = !this.discharging;
        this.dischargeSelectedBattery();
    }

    public void clickDischargePlayer()
    {

        this.charging = !this.charging;
        this.chargeSelectedBattery();
    }

    private void chargeSelectedBattery()
    {
        Debug.Log("playerBattery " + playerBattery != null);
        if (this.charging)
        {
            GameObject selectedItem = this.playerInventory.peekSelectedItem();
            Debug.Log("playerBattery " + playerBattery != null);
            if (selectedItem != null
                && selectedItem.CompareTag("battery")
                && (PowerSource.transferPower(playerBattery, selectedItem.GetComponent<Battery>().getPowerSource(), 20f)
                    || PowerSource.transferPower(playerBattery, selectedItem.GetComponent<Battery>().getPowerSource(), selectedItem.GetComponent<Battery>().getPowerSource().getMaxPower() - selectedItem.GetComponent<Battery>().getPowerSource().getPowerLevel())))
            {
                Invoke("chargeSelectedBattery", 1f);
            } else
            {
                this.charging = false;
            }
        }
    }

    private void dischargeSelectedBattery()
    {
        if (this.discharging)
        {
            GameObject selectedItem = this.playerInventory.peekSelectedItem();
            if (selectedItem != null
                && selectedItem.CompareTag("battery")
                && (PowerSource.transferPower(selectedItem.GetComponent<Battery>().getPowerSource(), playerBattery, 20f)
                    || PowerSource.transferPower(selectedItem.GetComponent<Battery>().getPowerSource(), playerBattery, playerBattery.getMaxPower() - playerBattery.getPowerLevel())))
            {
                Invoke("dischargeSelectedBattery", 1f);
            }
            else
            {
                this.discharging = false;
            }
        }
    }
}