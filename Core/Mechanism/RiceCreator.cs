using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCreator : MonoBehaviour
{
    /// <summary>
    /// 当前米粒数
    /// </summary>
    public int riceCount;
    /// <summary>
    /// 完成任务获得大米数
    /// </summary>
    public int addRiceCount;
    public GameObject rice;

    private void OnEnable()
    {
        EventHandler.OnGameBegin += OnGameBegin;
    }

    private void OnDisable()
    {
        EventHandler.OnGameBegin -= OnGameBegin;
    }

    private void OnGameBegin(float obj)
    {
        CreateRice(riceCount);
    }
    /// <summary>
    /// 方法生成大米
    /// </summary>
    /// <param name="_riceCount"></param>
    public void CreateRice(int _riceCount)
    {
        StartCoroutine(CreateRiceIenum(_riceCount));
    }
/// <summary>
/// 协程生成大米
/// </summary>
/// <param name="_riceCount"></param>
/// <returns></returns>
    public IEnumerator CreateRiceIenum(int _riceCount)
    {
        while (_riceCount>0)
        {
            Instantiate(rice, transform.position, Quaternion.identity);
            _riceCount--;
            yield return null;
        }
    }
}
