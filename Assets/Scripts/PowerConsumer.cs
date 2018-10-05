using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Update this class to handle requiring and using multiple PowerSources (like the Elevator Control Panel).
//TODO: Consider updating this class to handle gradient Power consumption (increase/decrease rate over time).
public class PowerConsumer : MonoBehaviour {

    [SerializeField] private int consumptionRate;
    [SerializeField] private PowerSource currentPowerSource;

    /// <summary>
    /// Constructor 1. Sets consumption rate and initial power source
    /// </summary>
    /// <param name="consumptionRate">The amount of Power per game tick that this PowerConsumer consumes</param>
    /// <param name="initialPowerSource">The initial PowerSource that will be used by this PowerConsumer</param>
    public PowerConsumer(int consumptionRate, PowerSource initialPowerSource)
    {
        this.consumptionRate = consumptionRate;
        this.currentPowerSource = initialPowerSource;
    }

    /// <summary>
    /// Constructor 2. Sets consumption rate only
    /// </summary>
    /// <param name="consumptionRate">The amount of Power per game tick that this PowerConsumer consumes</param>
    public PowerConsumer(int consumptionRate)
    {
        this.consumptionRate = consumptionRate;
        this.currentPowerSource = null;
    }

    /// <summary>
    /// Constructor 3. Takes no arguments and sets default data only
    /// </summary>
    public PowerConsumer()
    {
        this.consumptionRate = 1;
        this.currentPowerSource = null;
    }

    /// <summary>
    /// Call this function from any device that needs Power to activate. Run the device's activation function if this function returns TRUE.
    /// NOTE: This function will actually subtract Power from the PowerSource, so don't check more than you have to.
    /// </summary>
    /// <returns>True if the PowerConsumer has enough Power to activate, False if not.</returns>
    public bool powerDevice()
    {
        return this.currentPowerSource != null && this.currentPowerSource.takePower(this.consumptionRate);
    }

    /// <summary>
    /// Just like powerDevice(), except that this method only checks - it does not actually subtract Power from the PowerSource.
    /// </summary>
    /// <returns>True if the PowerConsumer COULD THEORETICALLY activate, False if not.</returns>
    public bool canPowerDevice()
    {
        return this.currentPowerSource != null && this.currentPowerSource.getPowerLevel() >= this.consumptionRate;
    }

    /// <summary>
    /// Gets the current consumption rate of this PowerConsumer
    /// </summary>
    /// <returns>The current consumption rate of this PowerConsumer</returns>
    public int getConsumptionRate()
    {
        return this.consumptionRate;
    }

    /// <summary>
    /// Gets the current PowerSource, if any, attached to this PowerConsumer
    /// </summary>
    /// <returns>The current PowerSource, if any, attached to this PowerConsumer. Null if no PowerSource is attached.</returns>
    public PowerSource getPowerSource()
    {
        return this.currentPowerSource;
    }

    /// <summary>
    /// Check if this PowerConsumer has a PowerSource attached to it
    /// </summary>
    /// <returns>True if there is a PowerSource attached, False if not.</returns>
    public bool hasPowerSource()
    {
        return this.currentPowerSource != null;
    }

    /// <summary>
    /// Set the consumption rate of this PowerConsumer
    /// </summary>
    /// <param name="consumptionRate">The new consumption rate</param>
    public void setConsumptionRate(int consumptionRate)
    {
        this.consumptionRate = consumptionRate;
    }

    /// <summary>
    /// Remove and return the current PowerSource, if any.
    /// </summary>
    /// <returns>The current PowerSource formerly attached to this PowerConsumer. Null if no PowerSource was attached.</returns>
    public PowerSource removePowerSource()
    {
        PowerSource oldSource = this.currentPowerSource;
        this.currentPowerSource = null;
        return oldSource;
    }

    /// <summary>
    /// Attach a PowerSource to this PowerConsumer
    /// </summary>
    /// <param name="source">The PowerSource to attach</param>
    /// <returns>True if attachment succeeded, False if failed. Fails when there is already a PowerSource attached.</returns>
    public bool attachPowerSource(PowerSource source)
    {
        if (this.currentPowerSource == null)
        {
            this.currentPowerSource = source;
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Replace and return the current PowerSource.
    /// </summary>
    /// <param name="source">The new PowerSource to attach to this PowerConsumer.</param>
    /// <returns>The old PowerSource, if any. Null if no PowerSource was attached.</returns>
    public PowerSource replacePowerSource(PowerSource source)
    {
        PowerSource oldSource = this.currentPowerSource;
        this.currentPowerSource = source;
        return oldSource;
    }
}
