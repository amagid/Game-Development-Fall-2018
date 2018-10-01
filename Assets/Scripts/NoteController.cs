using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    [SerializeField] string content;

    public string GetContent()
    {
        return this.content;
    }
}
