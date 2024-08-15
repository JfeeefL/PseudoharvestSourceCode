using System;
using System.Collections;
using System.Collections.Generic;
using Core.Core;
using UnityEngine;

public class IsKeyOut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Furniture"))
        {
            InputManager.Instance.keyOut = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Furniture"))
        {
            InputManager.Instance.keyOut = true;
        }
    }
}
