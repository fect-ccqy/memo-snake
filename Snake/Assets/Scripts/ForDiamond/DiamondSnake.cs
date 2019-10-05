using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSnake : MonoBehaviour
{
    private static DiamondSnake theInstance;
    private static int oneStepNum = 1;
    private static int arrayLength = 6;


    //********************************************************************
    //一些会用到的变量

    private bool whetherAlive = true;
    private float moveRange = 14.5f;

    private float snakeSpeed = 25f;


    private int snakeLength;//不包括蛇头
    private Rigidbody2D thisRigidbody2d;
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

    private GameObject snakeHeadObj;
    private GameObject snakeTailObj;
    private DiamondSnakeBody snakeTail;
    private Transform snakeTailTrans;





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
        print("StartloadSkinSprite SkinNum=" + MessageSender.GetTheInstance().GetSkinNum());
        snakeHeadSpr = Resources.Load<Sprite>(spritePathFa + spritePathHead + spritePathColor[MessageSender.GetTheInstance().GetSkinNum()]);
        snakeDizzyHeadSpr = Resources.Load<Sprite>(spritePathFa + spritePathDizzy + spritePathColor[MessageSender.GetTheInstance().GetSkinNum()]);
        snakeBodySpr = Resources.Load<Sprite>(spritePathFa + spritePathBody + spritePathColor[MessageSender.GetTheInstance().GetSkinNum()]);
        print("FinishLoadSkinSprite SkinNum=" + MessageSender.GetTheInstance().GetSkinNum());

    }


    private void SetPrefabSnakeHeadAndBody()
    {

        snakeBodyObj = Resources.Load<GameObject>("Prefabs/DiamondPre/DiamondSnakeBody");
        snakeBodySpriteRenderer = snakeBodyObj.GetComponent<SpriteRenderer>();
        snakeBodySpriteRenderer.sprite = snakeBodySpr;

        if (snakeBodyObj == null) print("null");
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteRenderer.sprite = snakeHeadSpr;
        print("get the skin");
    }


    private void InstantiateFirstBody()
    {
        nextSnakeBodyObj = Instantiate(snakeBodyObj, transform.position, transform.rotation) as GameObject;
        nextSnakeBody = nextSnakeBodyObj.GetComponent<DiamondSnakeBody>();
        snakeLength = 1;
        nextSnakeBody.SetAllMemember(gameObject, transform, null, snakeLength, null, null);
    }


    private void SetStartHeadAndTail()
    {

        snakeHeadObj = gameObject;

        snakeTailObj = nextSnakeBodyObj;
        snakeTail = nextSnakeBody;
        snakeTailTrans = nextSnakeBodyObj.transform;

        snakeBodySpriteRenderer.sortingOrder--;

    }




    //***************************************************************************
    //对蛇的相关操作


    private void AddOneBody()
    {

        DiamondGameManager.GetTheInstance().SetScore(0);

        snakeLength++;
        tSnakeBodyObj = Instantiate(snakeBodyObj, snakeTailTrans.position, snakeTailTrans.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<DiamondSnakeBody>();

        snakeTail.SetNextBody(tSnakeBodyObj, tSnakeBody);

        tSnakeBody.SetAllMemember(snakeTailObj, snakeTailTrans, snakeTail, snakeLength, null, null);

        snakeTailObj = tSnakeBodyObj;
        snakeTail = tSnakeBody;
        snakeTailTrans = tSnakeBodyObj.transform;
        snakeBodySpriteRenderer.sortingOrder--;
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
        snakeTailObj = snakeTail.GetLastSnakeBodyObj();
        snakeTail = snakeTailObj.GetComponent<DiamondSnakeBody>();
        snakeTailTrans = snakeTailObj.transform;

        Destroy(snakeTail.GetNextSnakeBodyObj());

        snakeTail.SetNextBody(null, null);
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
        thisRigidbody2d = GetComponent<Rigidbody2D>();

        SetStartHistoryArray();
        LoadSkinSprite();
        SetPrefabSnakeHeadAndBody();


        InstantiateFirstBody();

        SetStartHeadAndTail();


    }
    void Update()
    {
        if (!whetherAlive)
        {
            DiamondGameManager.GetTheInstance().TheSnakDie();
        }


    }
    
    // Start is called before the first frame update
    void Start()
    {

        DiamondGameManager.GetTheInstance().SetLenText(snakeLength);
        DiamondGameManager.GetTheInstance().SetSpeedText((int)(snakeSpeed));
    }



    private void FixedUpdate()
    {


        /*
        tdPos.y = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y) * snakeSpeed / 7f;
        transform.up =tVel;
        transform.position += tVel * Time.fixedDeltaTime;

        if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f,transform.position.z);
        }*/

        //蛇头相对鼠标的位移矢量(世界坐标)
        transform.position += tdPos * Time.fixedDeltaTime;

        dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        
        //蛇头相对鼠标的位移矢量的模长平方小于1.6则不进行方向和位置的变化避免出现抖动
        if (dHeadTowards.sqrMagnitude > 1.6f)
        {
            

            //dHeadTowards = Input.mousePosition - midScreenPos;//获得鼠标相对屏幕中心的位移矢量  
            transform.up = dHeadTowards;



            
            if (transform.position.y >= moveRange)
            {
                transform.position = new Vector3(transform.position.x, moveRange, transform.position.z);
                if (dHeadTowards.y > 0)
                {

                    tVel.y = 0;
                }
                else
                {
                    tVel.y = dHeadTowards.y * snakeSpeed / 8f;
                }
            }
            else if (transform.position.y <= -moveRange)
            {
                transform.position = new Vector3(transform.position.x, -moveRange, transform.position.z);
                if (dHeadTowards.y < 0)
                {

                    tVel.y = 0;
                }
                else
                {
                    tVel.y = dHeadTowards.y * snakeSpeed / 8f;
                }

            }
            else
            {
                tVel.y = dHeadTowards.y * snakeSpeed / 8f;
            }

            
            
        //    if(v)
            thisRigidbody2d.velocity = tVel;

            //这里使用transform和rigibody混合的移动方式
            //若只用transform，在墙边时会抖动
            //若只使用rigidbody，在墙边x方向速度也会减慢*/
            
            /*
            transform.up = dHeadTowards;

            tVel.x = snakeSpeed;
            tVel.y = dHeadTowards.y * snakeSpeed / 7f;
            thisRigidbody2d.velocity = tVel;
            //*/
            /*
            transform.up = dHeadTowards;

            tVel.x = snakeSpeed;
            tVel.y = dHeadTowards.y * snakeSpeed / 7f;

            transform.position += tVel * Time.fixedDeltaTime;
            //*/
            
        }
        

        SetHistoryArray();

        if (Input.GetKeyDown(KeyCode.Space)) AddOneBody();

        //********
        //if(!Input.anyKey)

        //movement

        //transform.position += transform.up * snakeSpeed * Time.fixedDeltaTime;


        //fortest

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        //避免出现在离开物体时，蛇头相对鼠标的位移矢量的模长平方小于1.6，速度依然保持与物体接触时相同的bug
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
