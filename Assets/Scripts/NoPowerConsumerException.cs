using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NoPowerConsumerException : MissingComponentException
{
    public NoPowerConsumerException(string message)
        : base(message)
    {
        Debug.LogError(message);
		#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
		#endif
    }
}
