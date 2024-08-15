using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CreateList : MonoBehaviour
{
    public GameObject obj;

    private IEnumerator Start()
    {
        for (int j = 0; j < 1000; j++)
        {
            Instantiate(obj, transform.position, quaternion.identity);
            yield return null;
        }
    }
    
}
