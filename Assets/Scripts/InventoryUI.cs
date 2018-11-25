﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private Inventory playerInventory;
    [SerializeField] private GameObject item1UI;
    List<Image> imgs;
    List<Image> powerLevels;
    List<Image> noteImgs;
    int selectedIndex = 0;
    int noteSelectedIndex = 0;
    private bool updateOn;
    private bool charging = false;
    private bool discharging = false;
    private bool dataTabOn = false;
    private PowerSource playerBattery;


    public void highLight(int index)
    {
        selectedIndex = index;
    }

    // Use this for initialization
    void Start()
    {
        dataTabOn = GameObject.Find("DataPanel");
        updateOn = true;
        imgs = new List<Image>();
        powerLevels = new List<Image>();
        noteImgs = new List<Image>();
        GameObject player = GameObject.Find("Player"); 
        playerInventory = player.GetComponent<Inventory>();
        playerBattery = player.GetComponent<PowerConsumer>().getPowerSource();
        for (int i = 0; i < playerInventory.inventorySize; i++)
        {
            string tag = "Slot" + (i + 1);
            string powerLevelTag = "Power Level " + (i + 1);
            string noteTag = "Note" + (i + 1);
            if (!dataTabOn)
            {
                imgs.Add(GameObject.Find(tag).GetComponent<Image>());
                powerLevels.Add(GameObject.Find(powerLevelTag).GetComponent<Image>());
            } else {
                noteImgs.Add(GameObject.Find(noteTag).GetComponent<Image>());
            }

        }


    }

    // Update is called once per frame
    // TODO: Refactor so that updates are only done on events rather than every frame to improve performance
    void Update()
    {
        dataTabOn = GameObject.Find("DataPanel");
        if (updateOn)
        {
            for (int i = 0; i < playerInventory.inventorySize; i++)
            {
                if (!dataTabOn)
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
                } else {
                    if (playerInventory.getNote(i) != null) {
                        noteImgs[i].color = UnityEngine.Color.red;
                    } else {
                        noteImgs[i].color = UnityEngine.Color.gray;
                    }
                    GameObject.Find("Note" + (i + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.black;
                }
            }

            if (!dataTabOn)
            {
                if (selectedIndex < 0 || selectedIndex > playerInventory.inventorySize)
                {
                    selectedIndex = 0;
                }
                GameObject.Find("Slot" + (selectedIndex + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.white;
            } else {
                if (noteSelectedIndex < 0 || noteSelectedIndex > playerInventory.inventorySize)
                {
                    noteSelectedIndex = 0;
                }
                GameObject.Find("Note" + (noteSelectedIndex + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.white;
            }

            // Set selected slot with keys
            if (!dataTabOn)
            {
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
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    noteSelectedIndex = 0;
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    noteSelectedIndex = 1;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    noteSelectedIndex = 2;
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                    noteSelectedIndex = 3;
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                    noteSelectedIndex = 4;
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                    noteSelectedIndex = 5;
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                    noteSelectedIndex = 6;
                else if (Input.GetKeyDown(KeyCode.Alpha8))
                    noteSelectedIndex = 7;
            }
            playerInventory.setSelectedNoteIndex(noteSelectedIndex);
            playerInventory.setSelectedItemIndex(selectedIndex);
        }
    }

    public void clickChargePlayer()
    {
            this.dischargeSelectedBattery();
    }

    public void clickDischargePlayer()
    {
            this.chargeSelectedBattery();
    }

    private void chargeSelectedBattery()
    {
        Debug.Log("playerBattery " + playerBattery != null);
        GameObject selectedItem = this.playerInventory.peekSelectedItem();
        Debug.Log("playerBattery " + playerBattery != null);
        if (selectedItem != null
             && selectedItem.CompareTag("battery")
             && (PowerSource.transferPower(playerBattery, selectedItem.GetComponent<Battery>().getPowerSource(), 20f)
                 || PowerSource.transferPower(playerBattery, selectedItem.GetComponent<Battery>().getPowerSource(), selectedItem.GetComponent<Battery>().getPowerSource().getMaxPower() - selectedItem.GetComponent<Battery>().getPowerSource().getPowerLevel())))
        {
            Invoke("chargeSelectedBattery", 1f);
        }
    }

    private void dischargeSelectedBattery()
    {
            GameObject selectedItem = this.playerInventory.peekSelectedItem();
        if (selectedItem != null
            && selectedItem.CompareTag("battery")
            && (PowerSource.transferPower(selectedItem.GetComponent<Battery>().getPowerSource(), playerBattery, 20f)
                || PowerSource.transferPower(selectedItem.GetComponent<Battery>().getPowerSource(), playerBattery, playerBattery.getMaxPower() - playerBattery.getPowerLevel())))
        {
            Invoke("dischargeSelectedBattery", 1f);
        }
    }

    public void openNote(){
        playerInventory.getSelectedNote().GetComponent<NoteController>().openNote();
        GameObject.Find("DataPanel").SetActive(false);
    }
}