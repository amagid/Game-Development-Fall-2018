using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource {

	private float maxPower;
	private float startingPower;
	private float currentPower;
	public Battery theBattery; // temporary fix bad code


	public PowerSource(float maxPower, float startingPower)
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
	public bool takePower(float amount)
	{
		if (currentPower >= amount)
		{
			currentPower -= amount;
			return true;
		} else
		{
			// if the power source doesn't have enough power for the power consumer to continue running set power to 0
			if(currentPower != 0){
				currentPower = 0;
			}
			return false;
		}
	}

	/// <summary>
	/// Instance method for giving a specified amount of Power to this PowerSource.
	/// Returns true if succeeded, false if failed.
	/// </summary>
	/// <param name="amount">The amount of Power to transfer</param>
	/// <returns>True if succeeded, False if failed</returns>
	public bool givePower(float amount)
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
	public float getPowerLevel()
	{
		return currentPower;
	}

	/// <summary>
	/// Gets this PowerSource's Power capacity
	/// </summary>
	/// <returns>The PowerSource's Power capacity</returns>
	public float getMaxPower()
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
	public static bool transferPower(PowerSource sender, PowerSource receiver, float amount)
	{
		if (sender.getPowerLevel() >= amount && receiver.getPowerLevel() + amount <= receiver.getMaxPower())
		{
			sender.takePower(amount);
			receiver.givePower(amount);
			return true;
		}
		return false;
	}
}
