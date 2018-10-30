using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Update this class to handle requiring and using multiple PowerSources (like the Elevator Control Panel).
//TODO: Consider updating this class to handle gradient Power consumption (increase/decrease rate over time).
public class PowerConsumer : MonoBehaviour {

<<<<<<< HEAD:Assets/Scripts/PowerConsumer.cs
	[SerializeField] private float consumptionRate;
	[SerializeField] private PowerSource currentPowerSource;
	[SerializeField] private float activationThreshold = 0; // Minimum Power to activate device
	[SerializeField] private bool oneTimeActivation = false;
	private bool powerSourceExtractable = true;

	/// <summary>
	/// Constructor 1. Sets consumption rate and initial power source
	/// </summary>
	/// <param name="consumptionRate">The amount of Power per game tick that this PowerConsumer consumes</param>
	/// <param name="initialPowerSource">The initial PowerSource that will be used by this PowerConsumer</param>
	public PowerConsumer(float consumptionRate, PowerSource initialPowerSource)
	{
		this.consumptionRate = consumptionRate;
		this.currentPowerSource = initialPowerSource;
	}

	/// <summary>
	/// Constructor 2. Sets consumption rate only
	/// </summary>
	/// <param name="consumptionRate">The amount of Power per game tick that this PowerConsumer consumes</param>
	public PowerConsumer(float consumptionRate)
	{
		this.consumptionRate = consumptionRate;
		this.currentPowerSource = null;
	}

	/// <summary>
	/// Constructor 3. Takes no arguments and sets default data only
	/// </summary>
	public PowerConsumer()
	{
		this.consumptionRate = 0.1f;
		this.currentPowerSource = null;
	}

	/// <summary>
	/// Call this function from any device that needs Power to activate. Run the device's activation function if this function returns TRUE.
	/// NOTE: This function will actually subtract Power from the PowerSource, so don't check more than you have to.
	/// </summary>
	/// <returns>True if the PowerConsumer has enough Power to activate, False if not.</returns>
	public bool powerDevice()
	{
		if (this.oneTimeActivation)
		{
			return this.currentPowerSource != null && this.currentPowerSource.getPowerLevel() >= this.activationThreshold && this.currentPowerSource.takePower(this.activationThreshold) && this.removePowerSource() != null;
		} else {
			return this.currentPowerSource != null && this.currentPowerSource.getPowerLevel() >= this.activationThreshold && this.currentPowerSource.takePower(this.consumptionRate);
		}
	}

	/// <summary>
	/// Just like powerDevice(), except that this method only checks - it does not actually subtract Power from the PowerSource.
	/// </summary>
	/// <returns>True if the PowerConsumer COULD THEORETICALLY activate, False if not.</returns>
	public bool canPowerDevice()
	{
		return this.currentPowerSource != null && this.currentPowerSource.getPowerLevel() >= this.activationThreshold && this.currentPowerSource.getPowerLevel() >= this.consumptionRate;
	}

	/// <summary>
	/// Gets the current consumption rate of this PowerConsumer
	/// </summary>
	/// <returns>The current consumption rate of this PowerConsumer</returns>
	public float getConsumptionRate()
	{
		return this.consumptionRate;
	}

	/// <summary>
	/// Gets the current activation threshold of this PowerConsumer
	/// </summary>
	/// <returns>The current activation threshold of this PowerConsumer</returns>
	public float getActivationThreshold()
	{
		return this.activationThreshold;
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
	/// Checks whether or not this PowerConsumer is set up for one time activation, or if it consumes continuously.
	/// </summary>
	/// <returns>True if this PowerConsumer is set up for one time activation, False if it consumes continuously.</returns>
	public bool isOneTimeActivation()
	{
		return this.oneTimeActivation;
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
		if (this.powerSourceExtractable)
		{
			PowerSource oldSource = this.currentPowerSource;
			this.currentPowerSource = null;
			return oldSource;
		} else
		{
			return null;
		}
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
		if (this.powerSourceExtractable)
		{
			PowerSource oldSource = this.currentPowerSource;
			this.currentPowerSource = source;
			return oldSource;
		} else
		{
			return null;
		}
	}

	/// <summary>
	/// Sets whether or not the PowerSource used by this device can be extracted by the player
	/// </summary>
	/// <param name="extractable">Whether or not the PowerSource can be extracted</param>
	public void setPowerSourceExtractable(bool extractable)
	{
		this.powerSourceExtractable = extractable;
	}

	/// <summary>
	/// Sets whether or not the PowerSource used by this device can be extracted by the player
	/// </summary>
	/// <param name="extractable">Whether or not the PowerSource can be extracted</param>
	public bool isPowerSourceExtractable()
	{
		return this.powerSourceExtractable;	
	}


=======
    [SerializeField] private float consumptionRate;
    [SerializeField] private PowerSource currentPowerSource;
    [SerializeField] private float activationThreshold = 0; // Minimum Power to activate device
    private bool powerSourceExtractable = true;

    /// <summary>
    /// Constructor 1. Sets consumption rate and initial power source
    /// </summary>
    /// <param name="consumptionRate">The amount of Power per game tick that this PowerConsumer consumes</param>
    /// <param name="initialPowerSource">The initial PowerSource that will be used by this PowerConsumer</param>
    public PowerConsumer(float consumptionRate, PowerSource initialPowerSource)
    {
        this.consumptionRate = consumptionRate;
        this.currentPowerSource = initialPowerSource;
    }

    /// <summary>
    /// Constructor 2. Sets consumption rate only
    /// </summary>
    /// <param name="consumptionRate">The amount of Power per game tick that this PowerConsumer consumes</param>
    public PowerConsumer(float consumptionRate)
    {
        this.consumptionRate = consumptionRate;
        this.currentPowerSource = null;
    }

    /// <summary>
    /// Constructor 3. Takes no arguments and sets default data only
    /// </summary>
    public PowerConsumer()
    {
        this.consumptionRate = 0.1f;
        this.currentPowerSource = null;
    }

    /// <summary>
    /// Call this function from any device that needs Power to activate. Run the device's activation function if this function returns TRUE.
    /// NOTE: This function will actually subtract Power from the PowerSource, so don't check more than you have to.
    /// </summary>
    /// <returns>True if the PowerConsumer has enough Power to activate, False if not.</returns>
    public bool powerDevice()
    {
        return this.currentPowerSource != null && this.currentPowerSource.getPowerLevel() >= this.activationThreshold && this.currentPowerSource.takePower(this.consumptionRate);
    }

    /// <summary>
    /// Just like powerDevice(), except that this method only checks - it does not actually subtract Power from the PowerSource.
    /// </summary>
    /// <returns>True if the PowerConsumer COULD THEORETICALLY activate, False if not.</returns>
    public bool canPowerDevice()
    {
        return this.currentPowerSource != null && this.currentPowerSource.getPowerLevel() >= this.activationThreshold && this.currentPowerSource.getPowerLevel() >= this.consumptionRate;
    }

    /// <summary>
    /// Gets the current consumption rate of this PowerConsumer
    /// </summary>
    /// <returns>The current consumption rate of this PowerConsumer</returns>
    public float getConsumptionRate()
    {
        return this.consumptionRate;
    }

    /// <summary>
    /// Gets the current activation threshold of this PowerConsumer
    /// </summary>
    /// <returns>The current activation threshold of this PowerConsumer</returns>
    public float getActivationThreshold()
    {
        return this.activationThreshold;
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
        if (this.powerSourceExtractable)
        {
            PowerSource oldSource = this.currentPowerSource;
            this.currentPowerSource = null;
            return oldSource;
        } else
        {
            return null;
        }
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
        if (this.powerSourceExtractable)
        {
            PowerSource oldSource = this.currentPowerSource;
            this.currentPowerSource = source;
            return oldSource;
        } else
        {
            return null;
        }
    }

    /// <summary>
    /// Sets whether or not the PowerSource used by this device can be extracted by the player
    /// </summary>
    /// <param name="extractable">Whether or not the PowerSource can be extracted</param>
    public void setPowerSourceExtractable(bool extractable)
    {
        this.powerSourceExtractable = extractable;
    }

    /// <summary>
    /// Gets whether or not the PowerSource used by this device can be extracted by the player
    /// </summary>
    public bool isPowerSourceExtractable()
    {
        return this.powerSourceExtractable;
    }
>>>>>>> ede83ecdeb9a6a452e419e3821774de75aa84cff:Assets/Scripts/PowerSystem/PowerConsumer.cs
}



