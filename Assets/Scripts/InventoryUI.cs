using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private Inventory playerInventory;
    List<GameObject> imgs;
    List<Image> powerLevels;
    List<GameObject> noteImgs;
    static int selectedIndex = 0;
    int noteSelectedIndex = 0;
    private bool updateOn;
    private bool charging = false;
    private bool discharging = false;
    private bool dataTabOn = false;
    private PowerSource playerBattery;
    private TabletUI tablet;


    public static void highLight(int index)
    {
        selectedIndex = index;
    }

    public void highNoteLight(int index)
    {
        noteSelectedIndex = index;
    }

    // Use this for initialization
    void Start()
    {
        tablet = GameObject.Find("Tablet").GetComponent<TabletUI>();
        dataTabOn = GameObject.Find("DataPanel") != null;
        updateOn = true;
        imgs = new List<GameObject>();
        powerLevels = new List<Image>();
        noteImgs = new List<GameObject>();
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
                imgs.Add(GameObject.Find(tag));
                imgs[i].GetComponent<Image>().color = UnityEngine.Color.gray;
                powerLevels.Add(GameObject.Find(powerLevelTag).GetComponent<Image>());
            } else {
                noteImgs.Add(GameObject.Find(noteTag));
                noteImgs[i].GetComponent<Image>().color = UnityEngine.Color.gray;
            }

        }


    }

    // Update is called once per frame
    // TODO: Refactor so that updates are only done on events rather than every frame to improve performance
    void Update()
    {
        dataTabOn = GameObject.Find("DataPanel") != null;
        if (updateOn)
        {
            for (int i = 0; i < playerInventory.inventorySize; i++)
            {
                if (!dataTabOn)
                {
                    if (playerInventory.getItem(i) != null)
                    {
                        imgs[i].transform.Find("Battery").gameObject.SetActive(true);
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
                        imgs[i].transform.Find("Battery").gameObject.SetActive(false); // TODO: Revisit later to potentially improve look
                        RectTransform rt = (RectTransform)powerLevels[i].gameObject.transform;
                        rt.sizeDelta = new Vector2(0, 0);
                    }

                    GameObject.Find("Slot" + (i + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.black;
                } else {
                    if (playerInventory.getNote(i) != null) {
                        Debug.Log(i);
                        Debug.Log(noteImgs);
                        noteImgs[i].transform.Find("Image").gameObject.SetActive(true);
                        noteImgs[i].transform.Find("Image").GetComponent<Image>().sprite = playerInventory.getNote(i).GetComponent<NoteController>().image;
                    } else {
                        noteImgs[i].transform.Find("Image").gameObject.SetActive(false);
                    }
                    GameObject.Find("Note" + (i + 1)).GetComponent<Outline>().effectColor = UnityEngine.Color.black;
                }
            }

            if (!dataTabOn)
            {
				selectedIndex = playerInventory.getSelectedItemIndex ();
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
        Debug.Log("playerBattery " + (playerBattery != null));
        GameObject selectedItem = this.playerInventory.peekSelectedItem();
        Debug.Log("playerBattery " + (playerBattery != null));
        if (selectedItem != null
             && selectedItem.CompareTag("battery")
             && (PowerSource.transferPower(playerBattery, selectedItem.GetComponent<Battery>().getPowerSource(), 20f)
                 || PowerSource.transferPower(playerBattery, selectedItem.GetComponent<Battery>().getPowerSource(), selectedItem.GetComponent<Battery>().getPowerSource().getMaxPower() - selectedItem.GetComponent<Battery>().getPowerSource().getPowerLevel())))
        {
            ;
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
            ;
        }
    }

    public void openNote(){
        if (playerInventory.getSelectedNote() != null)
        {
            dataTabOn = false;
            tablet.closeTablet();
            tablet.openedNoteFromInventory = true;
            playerInventory.getSelectedNote().GetComponent<NoteController>().openNote();
        }
    }
}