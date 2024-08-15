using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


[CreateAssetMenu(fileName = "New DialogContent", menuName = "Game/New Dialog", order = 0)]
public class DialogContent : ScriptableObject
{
    public List<Dialog> dialogList; 
}
[System.Serializable]
public class Dialog
{
    public string dialogName;
    [Multiline]
    public string dialogText;
    public Sprite dialogImage;
}
