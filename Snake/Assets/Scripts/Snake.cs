﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{


    
    private GameObject nextSnakeBodyObj;
    private SnakeBody nextSnakeBody;


   


    public static float snakeSpeed = 20f;
    private static GameObject snakeHeadObj;
    private static GameObject snakeTailObj;
    private static SnakeBody snakeTail;
    private static Transform snakeTailTrans;





    //private static float stepLen = 1.5f;
    //private static float fPerSec=50f;
    //public static int oneStepNum = (int)(fPerSec * stepLen / snakeSpeed);


    private static int snakeLength;//不包括蛇头

    //循环队列  记录蛇头的历史位置，为后面体节的移动提供pos和rot   避免出现范例中蛇的体节所走轨迹与实际蛇头轨迹不一致的现象(在范例里面，体节的轨迹会摇摆(～￣▽￣)～～ )
    //话说范例里的bug有点多啊，，，是担心被反编译吗 (;￢_￢)
    public static int oneStepNum = 2;
    public static int arrayLength = 6;
    public static Vector2[] historyPosArray = new Vector2[Snake.arrayLength];
    public static Quaternion[] historyRotArray = new Quaternion[Snake.arrayLength];
    public static int qhead, qtail;//qhead为保存的最新的，qtail为最旧的    从head加入新元素，从tail删除


    private SpriteRenderer thisSpriteRenderer;

    private Sprite snakeHeadSpr;
    private Sprite snakeDizzyHeadSpr;
    private Sprite snakeBodySpr;

    private Rigidbody2D thisRigidbody2d;


    private static GameObject snakeBodyObj;
    private static SpriteRenderer snakeBodySpriteRenderer;

    private string spritePathFa = "Sprites/Snake/";
    private string spritePathHead = "snakeHead_";
    private string spritePathDizzy = "snakeDizzy_";
    private string spritePathBody = "snake_";
    private string[] spritePathColor = {"green","red","purple","darkgreen" };


    //***
    public Vector2 dHeadTowards;
    private Vector3 midScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
    private GameObject tSnakeBodyObj;
    private SnakeBody tSnakeBody;





    //***************************************************************************
    //初始化方法



    private void SetStartHistoryArray()
    {
        for (int i = 0; i < Snake.arrayLength; i++)
        {

            Snake.historyPosArray[i] = transform.position;
            Snake.historyRotArray[i] = transform.rotation;
        }
        Snake.qhead = 0;
        Snake.qtail = Snake.arrayLength - 1;
    }

    //加载皮肤的sprite
    private void LoadSkinSprite()
    {
        print("StartloadSkinSprite SkinNum=" + MessageSender.GetSkinNum());
        snakeHeadSpr = Resources.Load<Sprite>(spritePathFa + spritePathHead + spritePathColor[MessageSender.GetSkinNum()]);
        snakeDizzyHeadSpr = Resources.Load<Sprite>(spritePathFa + spritePathDizzy + spritePathColor[MessageSender.GetSkinNum()]);
        snakeBodySpr = Resources.Load<Sprite>(spritePathFa + spritePathBody + spritePathColor[MessageSender.GetSkinNum()]);
        print("FinishLoadSkinSprite SkinNum=" + MessageSender.GetSkinNum());

    }


    private void SetPrefabSnakeHeadAndBody()
    {
        Snake.snakeBodyObj = Resources.Load<GameObject>("Prefabs/SnakeBody");
        Snake.snakeBodySpriteRenderer = Snake.snakeBodyObj.GetComponent<SpriteRenderer>();
        Snake.snakeBodySpriteRenderer.sprite = snakeBodySpr;

        if (Snake.snakeBodyObj == null) print("null");
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteRenderer.sprite = snakeHeadSpr;
        print("get the skin");
    }


    private void InstantiateFirstBody()
    {
        nextSnakeBodyObj = Instantiate(Snake.snakeBodyObj, transform.position, transform.rotation) as GameObject;
        nextSnakeBody = nextSnakeBodyObj.GetComponent<SnakeBody>();
        Snake.snakeLength = 1;
        nextSnakeBody.SetAllMemember(gameObject, transform, null, Snake.snakeLength, null, null);
    }


    private void SetStartHeadAndTail()
    {

        Snake.snakeHeadObj = gameObject;

        Snake.snakeTailObj = nextSnakeBodyObj;
        Snake.snakeTail = nextSnakeBody;
        Snake.snakeTailTrans = nextSnakeBodyObj.transform;

        Snake.snakeBodySpriteRenderer.sortingOrder--;

    }



    //***************************************************************************
    //对蛇的相关操作


    private void AddOneBody()
    {
        Snake.snakeLength++;
        tSnakeBodyObj = Instantiate(Snake.snakeBodyObj, Snake.snakeTailTrans.position, Snake.snakeTailTrans.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<SnakeBody>();

        Snake.snakeTail.SetNextBody(tSnakeBodyObj, tSnakeBody);

        tSnakeBody.SetAllMemember(Snake.snakeTailObj, Snake.snakeTailTrans,Snake.snakeTail,Snake.snakeLength,null,null);

        Snake.snakeTailObj = tSnakeBodyObj;
        Snake.snakeTail = tSnakeBody;
        Snake.snakeTailTrans = tSnakeBodyObj.transform;
        Snake.snakeBodySpriteRenderer.sortingOrder--;
    }


    private void MinusOneBody()
    {
        Snake.snakeLength--;
        Snake.snakeTailObj = Snake.snakeTail.GetLastSnakeBodyObj();
        Snake.snakeTail = Snake.snakeTailObj.GetComponent<SnakeBody>();
        Snake.snakeTailTrans = Snake.snakeTailObj.transform;

        Destroy(Snake.snakeTail.GetNextSnakeBodyObj());

        Snake.snakeTail.SetNextBody(null,null);
        Snake.snakeBodySpriteRenderer.sortingOrder++;
    }


    private void DoubleSnakeBody()
    {
        int addNum = Snake.snakeLength;
        for(int i = 0; i < addNum; i++)
        {
            AddOneBody();
        }


    }


    private void HalfSnakeBody()
    {
        int minusNum = Snake.snakeLength/2;
        for (int i = 0; i < minusNum; i++)
        {
            MinusOneBody();
        }
    }


    private void AddNBody(int addN)
    {
        for (int i = 0; i < addN; i++)
        {
            AddOneBody();
        }
    }


    private void MinusNBody(int minusN)
    {
        for (int i = 0; i < minusN; i++)
        {
            MinusOneBody();
        }
    }


    private void SetHistoryArray()
    {
        Snake.historyPosArray[Snake.qtail] = transform.position;
        Snake.historyRotArray[Snake.qtail] = transform.rotation;

        Snake.qhead--;
        Snake.qtail--;
        if (Snake.qhead < 0)
        {
            Snake.qhead += Snake.arrayLength;
        }
        if (Snake.qtail < 0)
        {
            Snake.qtail += Snake.arrayLength;
        }
    }


    //***************************************************************************



    private void Awake()
    {
        thisRigidbody2d = GetComponent<Rigidbody2D>();

        SetStartHistoryArray();
        LoadSkinSprite();
        SetPrefabSnakeHeadAndBody();


        InstantiateFirstBody();

        SetStartHeadAndTail();


    }

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

      
       

        //蛇头相对鼠标的位移矢量(世界坐标)
        dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


        //蛇头相对鼠标的位移矢量的模长平方小于1.6则不进行方向和位置的变化避免出现抖动
        if (dHeadTowards.sqrMagnitude > 1.6f)
        {
            //dHeadTowards = Input.mousePosition - midScreenPos;//获得鼠标相对屏幕中心的位移矢量  即方法2的处理方式
            transform.up = dHeadTowards;
            thisRigidbody2d.velocity = transform.up * Snake.snakeSpeed;
        }

       

        //********
        //if(!Input.anyKey)

        //movement

        //transform.position += transform.up * snakeSpeed * Time.fixedDeltaTime;


        //test
        if (Input.GetKeyDown(KeyCode.Space)) DoubleSnakeBody();
        if (Input.GetKeyDown(KeyCode.D)) HalfSnakeBody();

        SetHistoryArray();
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        //避免出现在离开物体时，蛇头相对鼠标的位移矢量的模长平方小于1.6，速度依然保持与物体接触时相同的bug
        dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = dHeadTowards;
        thisRigidbody2d.velocity = transform.up * Snake.snakeSpeed;
    }
}
