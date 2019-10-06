using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSnake : MonoBehaviour
{
    //该脚本启动顺序应在diamongamemanager后面


    private static DiamondSnake theInstance;
    private static int oneStepNum = 1;
    private static int arrayLength = 6;


    //********************************************************************
    //一些会用到的变量

    private bool whetherAlive = true;
    private float moveRange = 14.5f;//蛇可以上下移动的范围

    private float snakeSpeed = 25f;


    private int snakeLength;//不包括蛇头
    private Vector2 dHeadTowards;
    private Vector3 midScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);

    //会频繁使用的临时变量
    private GameObject tSnakeBodyObj;
    private DiamondSnakeBody tSnakeBody;
    private Vector3 tVel;
    private Vector3 tdPos;

    //循环队列  记录蛇头的历史位置，

    private Vector2[] historyPosArray = new Vector2[DiamondSnake.arrayLength];
    private Quaternion[] historyRotArray = new Quaternion[DiamondSnake.arrayLength];
    private int qhead, qtail;//qhead为保存的最新的，qtail为最旧的    从head加入新元素，从tail删除



    //********************************************************************
    //资源相关
    private string spritePathFa = "Sprites/Snake/";
    private string spritePathHead = "snakeHead_";
    private string spritePathDizzy = "snakeDizzy_";
    private string spritePathBody = "snake_";
    private string[] spritePathColor = { "green", "red", "purple", "darkgreen" };
    private Sprite snakeHeadSpr;
    private Sprite snakeDizzyHeadSpr;
    private Sprite snakeBodySpr;
    private SpriteRenderer thisSpriteRenderer;


    private GameObject snakeBodyObj;
    private SpriteRenderer snakeBodySpriteRenderer;


    //********************************************************************
    //头尾以及下一个体节
    private GameObject nextSnakeBodyObj;
    private DiamondSnakeBody nextSnakeBody;
    private DiamondSnakeBody snakeTail;





    public static DiamondSnake GetTheInstance()
    {
        return DiamondSnake.theInstance;
    }
    public static int GetArrayLength()
    {
        return DiamondSnake.arrayLength;
    }
    public static int GetOneStepNum()
    {
        return oneStepNum;
    }


    public Vector3 GetHistoryPos()
    {
        return historyPosArray[(qhead + DiamondSnake.oneStepNum - 1) % DiamondSnake.arrayLength];
    }

    public Quaternion GetHistoryRot()
    {
        return historyRotArray[(qhead + DiamondSnake.oneStepNum - 1) % DiamondSnake.arrayLength];
    }


    //***************************************************************************
    //初始化方法



    private void SetStartHistoryArray()
    {
        for (int i = 0; i < DiamondSnake.arrayLength; i++)
        {

            historyPosArray[i] = transform.position;
            historyRotArray[i] = transform.rotation;
        }
        qhead = 0;
        qtail = DiamondSnake.arrayLength - 1;
    }

    //加载皮肤的sprite
    private void LoadSkinSprite()
    {
        //print("StartloadSkinSprite SkinNum=" + MessageSender.GetTheInstance().GetSkinNum());
        snakeHeadSpr = Resources.Load<Sprite>(spritePathFa + spritePathHead + spritePathColor[MessageSender.GetTheInstance().GetSkinNum()]);
        snakeDizzyHeadSpr = Resources.Load<Sprite>(spritePathFa + spritePathDizzy + spritePathColor[MessageSender.GetTheInstance().GetSkinNum()]);
        snakeBodySpr = Resources.Load<Sprite>(spritePathFa + spritePathBody + spritePathColor[MessageSender.GetTheInstance().GetSkinNum()]);
        //print("FinishLoadSkinSprite SkinNum=" + MessageSender.GetTheInstance().GetSkinNum());

    }


    private void SetPrefabSnakeHeadAndBody()
    {

        snakeBodyObj = Resources.Load<GameObject>("Prefabs/DiamondPre/DiamondSnakeBody");
        //if (snakeBodyObj == null) print("null");


        snakeBodySpriteRenderer = snakeBodyObj.GetComponent<SpriteRenderer>();
        snakeBodySpriteRenderer.sprite = snakeBodySpr;
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteRenderer.sprite = snakeHeadSpr;

        //print("get the skin");
    }


    private void InstantiateFirstBody()
    {
        nextSnakeBodyObj = Instantiate(snakeBodyObj, transform.position, transform.rotation) as GameObject;
        nextSnakeBody = nextSnakeBodyObj.GetComponent<DiamondSnakeBody>();
        snakeLength = 1;
        nextSnakeBody.SetAllMemember(null, snakeLength, null);

        snakeTail = nextSnakeBody;

        snakeBodySpriteRenderer.sortingOrder--;

    }

    

    //***************************************************************************
    //对蛇的相关操作


    private void AddOneBody()
    {
        snakeLength++;

        DiamondGameManager.GetTheInstance().SetScore(0);
        tSnakeBodyObj = Instantiate(snakeBodyObj, snakeTail.transform.position, snakeTail.transform.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<DiamondSnakeBody>();

        tSnakeBody.SetAllMemember(snakeTail, snakeLength, null);
        snakeTail.SetNextBody(tSnakeBodyObj);
        snakeTail = tSnakeBody;
        snakeBodySpriteRenderer.sortingOrder--;//确定体节的前后覆盖关系
        DiamondGameManager.GetTheInstance().SetLenText(snakeLength);
    }

    private void MinusOneBody()
    {
        DiamondGameManager.GetTheInstance().SetScore(1);

        if (snakeLength == 1)
        {
            whetherAlive = false;
            return;
        }
        snakeLength--;

        snakeTail = snakeTail.GetLastSnakeBodyDiamondBody();
        Destroy(snakeTail.GetNextSnakeBodyObj());
        snakeTail.SetNextBody(null);
        snakeBodySpriteRenderer.sortingOrder++;
        DiamondGameManager.GetTheInstance().SetLenText(snakeLength);
    }

    
    public void AddNBody(int addN)
    {
        SoundPlayer.PlayItemsSound(0);
        for (int i = 0; i < addN; i++)
        {
            AddOneBody();
        }
    }
    
    public void MinusNBody(int minusN)
    {
        for (int i = 0; i < minusN; i++)
        {
            MinusOneBody();
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
            qhead += DiamondSnake.arrayLength;
        }
        if (qtail < 0)
        {
            qtail += DiamondSnake.arrayLength;
        }
    }



    private void Awake()
    {
        tVel = new Vector3(0f, 0f, 0f);
        tdPos = new Vector3(snakeSpeed, 0f, 0f);
        whetherAlive = true;
        theInstance = this;
        

        SetStartHistoryArray();//纯数值处理，对前面的流程无依赖
        LoadSkinSprite();//加载皮肤资源，对前面的流程无依赖
        SetPrefabSnakeHeadAndBody();//加载pefab，并设定皮肤，要求前面已加载皮肤
        snakeBodySpriteRenderer.sortingOrder = 0;//要求prefab已加载

        InstantiateFirstBody();//生成第一个体节


        DiamondGameManager.GetTheInstance().SetLenText(snakeLength);
        DiamondGameManager.GetTheInstance().SetSpeedText((int)(snakeSpeed));
    }
    void Update()
    {
        if (!whetherAlive)
        {
            DiamondGameManager.GetTheInstance().TheSnakDie();
        }


    }
    


    private void FixedUpdate()
    {

        //不选择rigidbody2d.velocity的移动方式是因为会出现蛇头部分进入墙体

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
