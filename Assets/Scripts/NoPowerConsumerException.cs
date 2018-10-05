using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerConsumerException : MissingComponentException
{
    public NoPowerConsumerException(string message)
        : base(message)
    {
        Debug.LogError(message);
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
