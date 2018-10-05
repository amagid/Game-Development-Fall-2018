using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource {

    [SerializeField] private int maxPower;
    [SerializeField] private int startingPower;
    private int currentPower;

    public PowerSource(int maxPower, int startingPower)
    {
        this.maxPower = maxPower;
        this.startingPower = startingPower;
        this.currentPower = this.startingPower;
    }

	// Use this for initialization
	void Start () {
        currentPower = startingPower;
	}
    
    /// <summary>
    /// Instance method for taking a specified amount of Power from this PowerSource.
    /// Returns true if succeeded, false if failed.
    /// </summary>
    /// <param name="amount">The amount of Power to transfer</param>
    /// <returns>True if succeeded, False if failed</returns>
    public bool takePower(int amount)
    {
        if (currentPower >= amount)
        {
            currentPower -= amount;
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Instance method for giving a specified amount of Power to this PowerSource.
    /// Returns true if succeeded, false if failed.
    /// </summary>
    /// <param name="amount">The amount of Power to transfer</param>
    /// <returns>True if succeeded, False if failed</returns>
    public bool givePower(int amount)
    {
        if (currentPower + amount > maxPower)
        {
            return false;
        } else
        {
            currentPower += amount;
            return true;
        }
    }

    /// <summary>
    /// Gets this PowerSource's current Power level
    /// </summary>
    /// <returns>The PowerSource's current Power level</returns>
    public int getPowerLevel()
    {
        return currentPower;
    }

    /// <summary>
    /// Gets this PowerSource's Power capacity
    /// </summary>
    /// <returns>The PowerSource's Power capacity</returns>
    public int getMaxPower()
    {
        return maxPower;
    }

    /// <summary>
    /// Checks if this PowerSource is empty
    /// </summary>
    /// <returns>True if empty, False if not</returns>
    public bool isEmpty()
    {
        return currentPower == 0;
    }

    /// <summary>
    /// Checks if this PowerSource is full
    /// </summary>
    /// <returns>True if full, False if not</returns>
    public bool isFull()
    {
        return currentPower == maxPower;
    }

    /// <summary>
    /// Static class method for transferring Power between two arbitrary PowerSources.
    /// Returns true if succeeded, false if failed.
    /// </summary>
    /// <param name="sender">The PowerSource to take Power from</param>
    /// <param name="receiver">The PowerSource to give Power to</param>
    /// <param name="amount">The amount of Power to attempt to transfer</param>
    /// <returns>True if succeeded, False if failed.</returns>
    public static bool transferPower(PowerSource sender, PowerSource receiver, int amount)
    {
        if (sender.getPowerLevel() >= amount && receiver.getPowerLevel() + amount <= receiver.getPowerLevel())
        {
            sender.takePower(amount);
            receiver.givePower(amount);
            return true;
        }
        return false;
    }
}
