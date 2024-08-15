using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPaper : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float startSize = 4;
    [SerializeField] float targetSize = 7;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Spin());
    }

    [SerializeField]
    float spinTime = 1.3f;

    private WaitForFixedUpdate _wait = new WaitForFixedUpdate();
    IEnumerator Spin()
    {
        float startTime = Time.time;
        while (Time.time - spinTime < startTime)
        {
            transform.localScale = Mathf.Lerp(startSize, targetSize,(Time.time-startTime)/spinTime) * Vector2.one;
            _rb.angularVelocity = 700;
            yield return _wait;
        }
        
    }
}
