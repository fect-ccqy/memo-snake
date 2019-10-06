using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSnake : MonoBehaviour
{

    private static StormSnake theInstance;
    private static int oneStepNum = 3;
    private static int arrayLength = 6;


    //********************************************************************
    //一些会用到的变量

    private bool whetherAlive = true;
    

    private float snakeSpeed = 20f;
   

    private int snakeLength;//不包括蛇头
    private Rigidbody2D thisRigidbody2d;
    private Vector2 dHeadTowards;
    //private Vector3 midScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
    //会频繁使用的临时变量
    private GameObject tSnakeBodyObj;
    private StormSnakeBody tSnakeBody;


    //循环队列  记录蛇头的历史位置，

    private Vector2[] historyPosArray = new Vector2[StormSnake.arrayLength];
    private Quaternion[] historyRotArray = new Quaternion[StormSnake.arrayLength];
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
    private StormSnakeBody nextSnakeBody;
    
    private StormSnakeBody snakeTail;






    public static StormSnake GetTheInstance()
    {
        return StormSnake.theInstance;
    }
    public static int GetArrayLength()
    {
        return StormSnake.arrayLength;
    }
    public static int GetOneStepNum()
    {
        return oneStepNum;
    }


    public Vector3 GetHistoryPos()
    {
        return historyPosArray[(qhead + StormSnake.oneStepNum - 1) % StormSnake.arrayLength];
    }

    public Quaternion GetHistoryRot()
    {
        return historyRotArray[(qhead + StormSnake.oneStepNum - 1) % StormSnake.arrayLength];
    }


    //***************************************************************************
    //初始化方法



    private void SetStartHistoryArray()
    {
        for (int i = 0; i < StormSnake.arrayLength; i++)
        {

            historyPosArray[i] = transform.position;
            historyRotArray[i] = transform.rotation;
        }
        qhead = 0;
        qtail = StormSnake.arrayLength - 1;
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

        snakeBodyObj = Resources.Load<GameObject>("Prefabs/StormPre/StormSnakeBody");
        snakeBodySpriteRenderer = snakeBodyObj.GetComponent<SpriteRenderer>();
        snakeBodySpriteRenderer.sprite = snakeBodySpr;

        //if (snakeBodyObj == null) print("null");
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteRenderer.sprite = snakeHeadSpr;
        //print("get the skin");
    }


    private void InstantiateFirstBody()
    {
        nextSnakeBodyObj = Instantiate(snakeBodyObj, transform.position, transform.rotation) as GameObject;
        nextSnakeBody = nextSnakeBodyObj.GetComponent<StormSnakeBody>();
        snakeLength = 1;
        nextSnakeBody.SetAllMemember(null, snakeLength, null);

        snakeTail = nextSnakeBody;

        snakeBodySpriteRenderer.sortingOrder--;
    }

    


    //***************************************************************************
    //对蛇的相关操作


    private void AddOneBody()
    {

        StormGameManager.GetTheInstance().SetScore(0);

        snakeLength++;
        tSnakeBodyObj = Instantiate(snakeBodyObj, snakeTail.transform.position, snakeTail.transform.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<StormSnakeBody>();

        tSnakeBody.SetAllMemember(snakeTail, snakeLength, null);
        snakeTail.SetNextBody(tSnakeBodyObj);
        snakeTail = tSnakeBody;
        
        snakeBodySpriteRenderer.sortingOrder--;
        StormGameManager.GetTheInstance().SetLenText(snakeLength);
    }

    public void MinusOneBody()
    {
        StormGameManager.GetTheInstance().SetScore(1);
        if (snakeLength == 1)
        {
            whetherAlive = false;
            return;
        }

        snakeLength--;

        snakeTail = snakeTail.GetLastSnakeBodyStormBody();
        Destroy(snakeTail.GetNextSnakeBodyObj());
        snakeTail.SetNextBody(null);
        snakeBodySpriteRenderer.sortingOrder++;

        StormGameManager.GetTheInstance().SetLenText(snakeLength);

    }

    private void SetHistoryArray()
    {
        historyPosArray[qtail] = transform.position;
        historyRotArray[qtail] = transform.rotation;

        qhead--;
        qtail--;
        if (qhead < 0)
        {
            qhead += StormSnake.arrayLength;
        }
        if (qtail < 0)
        {
            qtail += StormSnake.arrayLength;
        }
    }



    private void Awake()
    {
        whetherAlive = true;

        theInstance = this;
        thisRigidbody2d = GetComponent<Rigidbody2D>();

        SetStartHistoryArray();
        LoadSkinSprite();
        SetPrefabSnakeHeadAndBody();
        snakeBodySpriteRenderer.sortingOrder = 0;

        InstantiateFirstBody();

        StormGameManager.GetTheInstance().SetLenText(snakeLength);
        StormGameManager.GetTheInstance().SetSpeedText((int)(snakeSpeed));


    }
    void Update()
    {
        if (!whetherAlive)
        {
            StormGameManager.GetTheInstance().TheSnakDie();
        }


    }

    public void GetOneFood()
    {
        StormGameManager.GetTheInstance().SetScore(4);
        AddOneBody();
        SoundPlayer.PlayItemsSound(0);
       
    }

    private void FixedUpdate()
    {




        //鼠标相对蛇头的位移矢量(世界坐标)
        dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


        //鼠标相对蛇头的位移矢量的模长平方小于1.6则不进行方向和位置的变化避免出现抖动
        if (dHeadTowards.sqrMagnitude > 1.6f)
        {

            transform.up = dHeadTowards;
            thisRigidbody2d.velocity = transform.up * snakeSpeed;

        }

        SetHistoryArray();
        
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        //避免出现在离开物体时，鼠标相对蛇头的位移矢量的模长平方小于1.6，速度依然保持与物体接触时相同的bug
        dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = dHeadTowards;
        thisRigidbody2d.velocity = transform.up * snakeSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Hurtful")
        {

            MinusOneBody();

        }
        if (collision.transform.tag == "monster")
        {
            whetherAlive = false;
        }
    }
}
