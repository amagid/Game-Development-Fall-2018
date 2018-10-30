using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoweredOperation {

    /// <summary>
    /// Perform initial activation work for this device upon recieving sufficient Power
    /// </summary>
    void activate();

    /// <summary>
    /// Tick-by-tick operation of this device. Operate() should finish in one Update() call.
    /// </summary>
    void operate();

    /// <summary>
    /// Perform deactivation work for this device upon losing sufficient Power
    /// </summary>
    void deactivate();

    /// <summary>
    /// Check whether or not this device was active LAST TICK. Used for deciding whether to run activate() vs operate() or deactivate() vs doing nothing.
    /// You should consider just storing an *active* boolean variable for this. Set it to true when activate() runs, set it to false when deactivate() runs.
    /// Then, in your device's Update() function, run activate() if isActive returns false and there is Power, run operate() if isActive returns true and
    /// there is Power, run deactivate() if isActive returns true and there is NOT Power, and do nothing if isActive returns false and there is NOT Power.
    /// </summary>
    /// <returns>True if device was active LAST TICK, False if not.</returns>
    bool isActive();

    /// <summary>
    /// Gets the PowerConsumer attached to this device. PowerConsumers must be added as Components through the Unity editor and configured there.
    /// </summary>
    /// <returns>The PowerConsumer attached to this device.</returns>
    PowerConsumer getPowerConsumer();

}
