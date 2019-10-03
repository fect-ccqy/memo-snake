using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private GameObject lastSnakeBodyObj;
    private Transform lastSnakeBodyTrans;
    private SnakeBody lastsnakebody;
    private int theNum;//第几个体节  从1开始  不包括蛇头
    private GameObject nextSnakeBodyObj;
    private SnakeBody nextsnakebody;


    private Vector2[] historyPosArray = new Vector2[Snake.GetArrayLength()];
    private Quaternion[] historyRotArray = new Quaternion[Snake.GetArrayLength()];
    private int qhead, qtail;

    private int arrayLen;
    private int oneStepNum;
    //
    private int tTransArrayNum;


    //***************************************************************************
    //get  set


    public void SetAllMemember(GameObject _lastSnakeBodyObj, Transform _lastSnakeBodyTrans, SnakeBody _lastsnakebody, int thenum, GameObject _nextSnakeBodyObj, SnakeBody _nextsnakebody)
    {
        lastSnakeBodyObj = _lastSnakeBodyObj;
        lastSnakeBodyTrans = _lastSnakeBodyTrans;
        theNum = thenum;
        nextSnakeBodyObj = _nextSnakeBodyObj;
        nextsnakebody = _nextsnakebody;
        lastsnakebody = _lastsnakebody;
    }


    public void SetNextBody(GameObject _nextSnakeBodyObj, SnakeBody _nextsnakebody)
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
        oneStepNum = Snake.GetOneStepNum();
        arrayLen = Snake.GetArrayLength();
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

            transform.position = Snake.GetTheInstance().GetHistoryPos();
            transform.rotation = Snake.GetTheInstance().GetHistoryRot();
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
        /*
        Vector3 dPos = lastSnakeBodyTrans.position - transform.position;
        if (dPos.sqrMagnitude > sqrDistance)
        {
            transform.position += Snake.snakeSpeed * Time.fixedDeltaTime * dPos * 0.45f;
        }*/

        //transform.position += Snake.snakeSpeed * Time.fixedDeltaTime * dPos* 0.45f;
        //transform.position += Snake.snakeSpeed * Time.fixedDeltaTime * dPos * dPos.sqrMagnitude * 0.12f;
        //transform.position += Snake.snakeSpeed * Time.fixedDeltaTime * dPos * dPos.sqrMagnitude*dPos.sqrMagnitude * 0.04f;

        MoveThisBody();

        SetHistoryArray();

    }
}
