using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSnakeBody : MonoBehaviour
{
    private GameObject lastSnakeBodyObj;
    private Transform lastSnakeBodyTrans;
    private DiamondSnakeBody lastsnakebody;
    private int theNum;//第几个体节  从1开始  不包括蛇头
    private GameObject nextSnakeBodyObj;
    private DiamondSnakeBody nextsnakebody;


    private Vector2[] historyPosArray = new Vector2[DiamondSnake.GetArrayLength()];
    private Quaternion[] historyRotArray = new Quaternion[DiamondSnake.GetArrayLength()];
    private int qhead, qtail;

    private int arrayLen;
    private int oneStepNum;
    //
    private int tTransArrayNum;


    //***************************************************************************
    //get  set


    public void SetAllMemember(GameObject _lastSnakeBodyObj, Transform _lastSnakeBodyTrans, DiamondSnakeBody _lastsnakebody, int thenum, GameObject _nextSnakeBodyObj, DiamondSnakeBody _nextsnakebody)
    {
        lastSnakeBodyObj = _lastSnakeBodyObj;
        lastSnakeBodyTrans = _lastSnakeBodyTrans;
        theNum = thenum;
        nextSnakeBodyObj = _nextSnakeBodyObj;
        nextsnakebody = _nextsnakebody;
        lastsnakebody = _lastsnakebody;
    }


    public void SetNextBody(GameObject _nextSnakeBodyObj, DiamondSnakeBody _nextsnakebody)
    {
        nextSnakeBodyObj = _nextSnakeBodyObj;
        nextsnakebody = _nextsnakebody;
    }


    public GameObject GetLastSnakeBodyObj()
    {
        return lastSnakeBodyObj;
    }


    public GameObject GetNextSnakeBodyObj()
    {
        return nextSnakeBodyObj;
    }


    //***************************************************************************




    private void Awake()
    {
        oneStepNum = DiamondSnake.GetOneStepNum();
        arrayLen = DiamondSnake.GetArrayLength();
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

            transform.position = DiamondSnake.GetTheInstance().GetHistoryPos();
            transform.rotation = DiamondSnake.GetTheInstance().GetHistoryRot();
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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        MoveThisBody();

        SetHistoryArray();

    }

}
