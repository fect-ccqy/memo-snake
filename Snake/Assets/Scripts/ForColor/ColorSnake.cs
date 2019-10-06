using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake : MonoBehaviour
{
    private static ColorSnake theInstance;
    private static int oneStepNum = 2;
    private static int arrayLength = 6;



    //循环队列  记录蛇头的历史位置，
    private Vector2[] historyPosArray = new Vector2[ColorSnake.arrayLength];
    private Color[] historyColorArray = new Color[ColorSnake.arrayLength];
    private int qhead, qtail;//qhead为保存的最新的，qtail为最旧的    从head加入新元素，从tail删除

    

    public static ColorSnake GetTheInstance()
    {
        return ColorSnake.theInstance;
    }
    public static int GetArrayLength()
    {
        return ColorSnake.arrayLength;
    }
    public static int GetOneStepNum()
    {
        return oneStepNum;
    }
    
    public Vector3 GetHistoryPos()
    {
        return historyPosArray[(qhead + ColorSnake.oneStepNum - 1) % ColorSnake.arrayLength];
    }
    public Color GetHistoryColor()
    {
        return historyColorArray[(qhead + ColorSnake.oneStepNum - 1) % ColorSnake.arrayLength];
    }



    //********************************************************************
    //一些会用到的变量

    private bool whetherAlive;
    private int snakeLength;//不包括蛇头
    private SpriteRenderer thisSpriteRenderer;
    


    //用蛇头的移动
    private float moveRange = 17f;//蛇可以上下移动的范围
    private float snakeSpeed = 25f;    
    private Rigidbody2D thisRigidbody2d;
    private Vector2 dHeadTowards;
    private Vector3 midScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);


   


    //会频繁使用的临时变量
    private GameObject tSnakeBodyObj;
    private ColorSnakeBody tSnakeBody;
    private Vector3 tVel;
    private Vector3 tdPos;




    //body Prefab
    private GameObject snakeBodyObj;
    private SpriteRenderer snakeBodySpriteRenderer;

    //********************************************************************
    //头尾以及下一个体节
    private GameObject nextSnakeBodyObj;
    private ColorSnakeBody nextSnakeBody;

    private GameObject snakeHeadObj;
    private GameObject snakeTailObj;
    private ColorSnakeBody snakeTail;
    private Transform snakeTailTrans;






    
    //初始化方法

    private void AddOneBody()
    {


        snakeLength++;
        tSnakeBodyObj = Instantiate(snakeBodyObj, snakeTailTrans.position, snakeTailTrans.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<ColorSnakeBody>();
        

        tSnakeBody.SetAllMemember(snakeTail, snakeLength);

        snakeTailObj = tSnakeBodyObj;
        snakeTail = tSnakeBody;
        snakeTailTrans = tSnakeBodyObj.transform;
        snakeBodySpriteRenderer.sortingOrder--;
    }

    private void AddNBody(int addN)
    {
        //SoundPlayer.PlayItemsSound(0);
        for (int i = 0; i < addN; i++)
        {
            AddOneBody();
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
            qhead += ColorSnake.arrayLength;
        }
        if (qtail < 0)
        {
            qtail += ColorSnake.arrayLength;
        }
    }

    private void SetStartHistoryArray()
    {
        for (int i = 0; i < ColorSnake.arrayLength; i++)
        {

            historyPosArray[i] = transform.position;
            historyColorArray[i] = thisSpriteRenderer.color;
        }
        qhead = 0;
        qtail = ColorSnake.arrayLength - 1;
    }
    
    private void InstantiateFirstBody()
    {
        nextSnakeBodyObj = Instantiate(snakeBodyObj, transform.position, transform.rotation) as GameObject;
        nextSnakeBody = nextSnakeBodyObj.GetComponent<ColorSnakeBody>();
        snakeLength = 1;
        nextSnakeBody.SetAllMemember(null, snakeLength);
    }

    private void SetStartHeadAndTail()
    {

        snakeHeadObj = gameObject;

        snakeTailObj = nextSnakeBodyObj;
        snakeTail = nextSnakeBody;
        snakeTailTrans = nextSnakeBodyObj.transform;

        snakeBodySpriteRenderer.sortingOrder--;

    }

    private void Awake()
    {
        whetherAlive = true;
        theInstance = this;
        tVel = new Vector3(0f, 0f, 0f);
        tdPos = new Vector3(snakeSpeed, 0f, 0f);



        thisRigidbody2d = GetComponent<Rigidbody2D>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteRenderer.color = Color.white;

        snakeBodyObj = Resources.Load<GameObject>("Prefabs/ColorPre/ColorSnakeBody");
        snakeBodySpriteRenderer = snakeBodyObj.GetComponent<SpriteRenderer>();
        snakeBodySpriteRenderer.color = thisSpriteRenderer.color;
        snakeBodySpriteRenderer.sortingOrder = 0;


        SetStartHistoryArray();//需要设置color后再运行这个函数

        InstantiateFirstBody();//加载snakeBodyObj，设置snakelength之后使用

        SetStartHeadAndTail();//生成第一个体节之后使用

        AddNBody(10);//只有在初始时生成一定长度，之后就用不上了
    }


    //***************************************************************************
    //对蛇的相关操作

    public void SetSnakeColor(Color tcolor)
    {
        thisSpriteRenderer.color = tcolor;
    }

    public Color GetSnakeColor()
    {
        print(thisSpriteRenderer.color);
        return thisSpriteRenderer.color;
    }

    public void TheSnakeDie()
    {
        whetherAlive = false;
    }



    void Update()
    {
        if (!whetherAlive)
        {
            ColorGameManager.GetTheInstance().TheSnakDie();
        }
    }


    private void FixedUpdate()
    {


        //tranform移动方式

        dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = dHeadTowards;

        tdPos.y = dHeadTowards.y * snakeSpeed / 7f;

        transform.position += tdPos * Time.fixedDeltaTime;

        if (transform.position.y >= moveRange)
        {
            transform.position = new Vector3(transform.position.x, moveRange, transform.position.z);

        }
        else if (transform.position.y <= -moveRange)
        {
            transform.position = new Vector3(transform.position.x, -moveRange, transform.position.z);
        }

        SetHistoryArray();
        
    }



}
