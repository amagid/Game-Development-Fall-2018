using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralLightController : MonoBehaviour, PoweredOperation {

    private Inventory inventory;
    [SerializeField] private GameObject central_lights;
    public double xOffset;
    public double yOffset;
    public double zOffset;

    private bool deviceActive = false;
    private PowerConsumer powerConsumer;
    private GameObject battery;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        this.deviceActive = false;
        this.powerConsumer = this.getPowerConsumer();
    }

    // Update is called once per frame
    void Update()
    {

        // Activate/Operate/Deactivate device based on powerConsumer state
        bool deviceIsPowered = this.powerConsumer.powerDevice();
        if (deviceIsPowered && !this.isActive())
        {
            this.activate();
        }
        else if (deviceIsPowered && this.isActive())
        {
            this.operate();
        }
        else if (!deviceIsPowered && this.isActive())
        {
            this.deactivate();
        }
    }

    public void activate()
    {
        this.deviceActive = true;
        this.battery.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
        this.battery.SetActive(true);

        central_lights.SetActive(true);
    }

    public void operate() {}

    public void deactivate()
    {
        this.battery = null;
        central_lights.SetActive(false);
        deviceActive = false;
    }

    public bool isActive()
    {
        return deviceActive;
    }

    public PowerConsumer getPowerConsumer()
    {
        PowerConsumer pc = this.gameObject.GetComponent<PowerConsumer>();
        if (pc == null)
        {
            throw new NoPowerConsumerException("CentralLightControllers must always have PowerConsumers! Please attach a PowerConsumer component in the Unity editor.");
        }
        return pc;
    }

    public void setBattery(GameObject bat)
    {
        this.battery = bat;
    }
}
