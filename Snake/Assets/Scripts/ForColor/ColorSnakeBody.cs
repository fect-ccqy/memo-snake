using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnakeBody : MonoBehaviour
{
    private ColorSnakeBody lastsnakebody;

    private SpriteRenderer thisSpriteRenderer;
    private Vector2[] historyPosArray = new Vector2[ColorSnake.GetArrayLength()];
    private Color[] historyColorArray = new Color[ColorSnake.GetArrayLength()];
    private int qhead, qtail;

    private int theNum;
    private int arrayLen;
    private int oneStepNum;
    //
    private int tTransArrayNum;


    //***************************************************************************
    //get  set


    public void SetAllMemember(ColorSnakeBody _lastsnakebody, int thenum)
    {
        theNum = thenum;
        lastsnakebody = _lastsnakebody;
    }
    



    //***************************************************************************


    //初始化方法
    private void SetStartHistoryArray()
    {
        for (int i = 0; i < arrayLen; i++)
        {

            historyPosArray[i] = transform.position;
            historyColorArray[i] = thisSpriteRenderer.color;
        }
        qhead = 0;
        qtail = arrayLen - 1;
    }
    
    private void Awake()
    {

        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        oneStepNum = ColorSnake.GetOneStepNum();
        arrayLen = ColorSnake.GetArrayLength();

        SetStartHistoryArray();
    }




    //***************************************************************************
    //与蛇相关的操作


    private void MoveThisBody()
    {
        if (theNum != 1)
        {
            tTransArrayNum = (lastsnakebody.qhead + oneStepNum) % arrayLen;
            transform.position = lastsnakebody.historyPosArray[tTransArrayNum];
            thisSpriteRenderer.color = lastsnakebody.historyColorArray[tTransArrayNum];
        }
        else
        {

            transform.position = ColorSnake.GetTheInstance().GetHistoryPos();
            thisSpriteRenderer.color = ColorSnake.GetTheInstance().GetHistoryColor();
        }
    }
    
    private void SetHistoryArray()
    {

        historyPosArray[qtail] = transform.position;
        historyColorArray[qtail] = thisSpriteRenderer.color;
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
