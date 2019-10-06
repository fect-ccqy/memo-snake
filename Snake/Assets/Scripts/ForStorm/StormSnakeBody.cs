using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSnakeBody : MonoBehaviour
{
    
    private StormSnakeBody lastsnakebody;
    private int theNum;//第几个体节  从1开始  不包括蛇头
    private GameObject nextSnakeBodyObj;


    private Vector2[] historyPosArray = new Vector2[StormSnake.GetArrayLength()];
    private Quaternion[] historyRotArray = new Quaternion[StormSnake.GetArrayLength()];
    private int qhead, qtail;

    private int arrayLen;
    private int oneStepNum;
    //
    private int tTransArrayNum;


    //***************************************************************************
    //get  set


    public void SetAllMemember(StormSnakeBody _lastsnakebody, int thenum, GameObject _nextSnakeBodyObj)
    {
        theNum = thenum;
        nextSnakeBodyObj = _nextSnakeBodyObj;
        lastsnakebody = _lastsnakebody;
    }


    public void SetNextBody(GameObject _nextSnakeBodyObj)
    {
        nextSnakeBodyObj = _nextSnakeBodyObj;
    }



    public StormSnakeBody GetLastSnakeBodyStormBody()
    {
        return lastsnakebody;
    }


    public GameObject GetNextSnakeBodyObj()
    {
        return nextSnakeBodyObj;
    }

    //***************************************************************************




    private void Awake()
    {
        oneStepNum = StormSnake.GetOneStepNum();
        arrayLen = StormSnake.GetArrayLength();
        SetStartHistoryArray();
    }



    //***************************************************************************


    //初始化方法
    private void SetStartHistoryArray()
    {
        for (int i = 0; i < arrayLen; i++)
        {

            historyPosArray[i] = transform.position;
            historyRotArray[i] = transform.rotation;
        }
        qhead = 0;
        qtail = arrayLen - 1;
    }


    //***************************************************************************
    //与蛇相关的操作


    private void MoveThisBody()
    {
        if (theNum != 1)
        {
            tTransArrayNum = (lastsnakebody.qhead + oneStepNum) % arrayLen;
            transform.position = lastsnakebody.historyPosArray[tTransArrayNum];
            transform.rotation = lastsnakebody.historyRotArray[tTransArrayNum];
        }
        else
        {

            transform.position = StormSnake.GetTheInstance().GetHistoryPos();
            transform.rotation = StormSnake.GetTheInstance().GetHistoryRot();
        }
    }


    private void SetHistoryArray()
    {

        historyPosArray[qtail] = transform.position;
        historyRotArray[qtail] = transform.rotation;
        qhead--;
        qtail--;

        if (qhead < 0)
        {
            qhead += arrayLen;
        }
        if (qtail < 0)
        {
            qtail += arrayLen;
        }
    }

    //***************************************************************************

        
    private void FixedUpdate()
    {
        MoveThisBody();

        SetHistoryArray();

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Hurtful")
        {

            StormSnake.GetTheInstance().MinusOneBody();
        }
    }
}
