using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool questAtEndOfDialogue;
    [TextArea(4, 10)]
    public string[] sentences;
}
