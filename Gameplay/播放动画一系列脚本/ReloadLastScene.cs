using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLastScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.6f);
        SceneController.Instance.canPress = true;
        SceneController.Instance.GotoScene("ReloadScene",GameManager.Instance.LastScene);
        
    }
}
