using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class RiceDetector : MonoBehaviour
{
    public float length;
    private bool isTouchingRice;
    public Transform leftPoint, rightPoint,downPoint;
    private bool isWorking;
    public float detectTime;
    public DectectState nowDectectState=DectectState.Nothing;

    public LayerMask detectLayer;
    public GameObject RedLine;
    private void OnEnable()
    {
        GameManager.Instance.riceDetector = this;
    }

    private void Start()
    {
        transform.DetachChildren();
        RedLine.transform.position = downPoint.transform.position;
    }

    /// <summary>
    /// 执行该脚本返回给GameManager当前判断，只有Perfect情况才能通关
    /// </summary>
    /// <returns></returns>
    public IEnumerator DetectAll() 
    {
        isWorking = true;
        isTouchingRice = true;
        nowDectectState = DectectState.Nothing;
        transform.position = leftPoint.position;
        transform.DOMove(rightPoint.position, detectTime).SetEase(Ease.Linear).OnComplete(()=>isWorking=false);
        //总扫描次数
        int totalScan = 0;
        //米的数量
        int riceCount = 0;
        //正确米的数量
        int rightRiceCount=0;
        //其他点数量
        int otherCount = 0;
        int nullCount = 0;
        //totalScan=riceCount+otherCount(理想状态)  如果小于，则有空
        while (isWorking)
        {
            totalScan++;
            var position = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down,length,detectLayer);
            
            if (hit.collider == null)
            {
                Debug.DrawLine(position,hit.point,Color.red,10);
                nullCount++;
                yield return null;
                continue;
            }
                
            if (hit.collider.CompareTag("Rice"))
            {
                riceCount++;
                //状态机切换
                if (hit.point.y > downPoint.position.y)
                {
                    Debug.DrawLine(position,hit.point,Color.green,10);
                    rightRiceCount++;
                }
                else
                {
                    Debug.DrawLine(position,hit.point,Color.yellow,10);
                }
            }
            else if (hit.collider.CompareTag("Furniture") || hit.collider.CompareTag("Draggable"))
            {
                Debug.DrawLine(position,hit.point,Color.red,10);
                otherCount++;
            }
            yield return null;
        }

        if (totalScan == nullCount)
            nowDectectState = DectectState.Nothing;
        else if(otherCount>0)
            nowDectectState = DectectState.HaveForniture;
        else if (totalScan>rightRiceCount)
            nowDectectState = DectectState.NotEnoughRice;
        else
            nowDectectState = DectectState.Perfect;
        GameManager.Instance.NowDectectState = nowDectectState;
        /*Debug.Log("TotalScan:"+totalScan);
        Debug.Log("RiceCount:"+riceCount);
        Debug.Log("RightRiceCount:"+rightRiceCount);
        Debug.Log("OtherCount:"+otherCount);
        Debug.Log("nullCount:"+nullCount);
        Debug.Log(nowDectectState);*/
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = !isTouchingRice ? Color.red : Color.green;
        //Gizmos.DrawLine(transform.position,transform.position+ Vector3.down*length);
    }
}
