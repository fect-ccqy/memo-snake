using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{


    private static Snake theInstance;
    private static int oneStepNum = 3;
    private static int arrayLength = 6;


    //********************************************************************
    //一些会用到的变量

    private bool whetherAlive = true;

    private bool whetherHighSpeed;
    private float highSpeedTimer;
    //private bool whetherWisdom;
    private int wisdomNum = 4;
    private bool whetherGetSheild;
    private float sheildTimer;
    [SerializeField] private GameObject theSheild;

    private Vector3 tarPosition;

    private float snakeSpeed = 20f;
    private float highSpeedK = 1.4f;
    private int snakeLength;//不包括蛇头
    private Rigidbody2D thisRigidbody2d;
    private Vector2 dHeadTowards;

    //会频繁使用的临时变量
    private GameObject tSnakeBodyObj;
    private SnakeBody tSnakeBody;


    //循环队列  记录蛇头的历史位置，为后面体节的移动提供pos和rot   避免出现范例中蛇的体节所走轨迹与实际蛇头轨迹不一致的现象(在范例里面，体节的轨迹会摇摆(～￣▽￣)～～ )
    //话说范例里的bug有点多啊，，，是担心被反编译吗 (;￢_￢)

    private Vector2[] historyPosArray = new Vector2[Snake.arrayLength];
    private Quaternion[] historyRotArray = new Quaternion[Snake.arrayLength];
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
    private SnakeBody nextSnakeBody;
    
    private SnakeBody snakeTail;





    public static Snake GetTheInstance()
    {
        return Snake.theInstance;
    }
    public static int GetArrayLength()
    {
        return Snake.arrayLength;
    }
    public static int GetOneStepNum()
    {
        return oneStepNum;
    }


    public Vector3 GetHistoryPos()
    {
        return historyPosArray[(qhead + Snake.oneStepNum - 2) % Snake.arrayLength];
    }

    public Quaternion GetHistoryRot()
    {
        return historyRotArray[(qhead + Snake.oneStepNum - 2) % Snake.arrayLength];
    }


    //***************************************************************************
    //初始化方法



    private void SetStartHistoryArray()
    {
        for (int i = 0; i < Snake.arrayLength; i++)
        {

            historyPosArray[i] = transform.position;
            historyRotArray[i] = transform.rotation;
        }
        qhead = 0;
        qtail = Snake.arrayLength - 1;
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
        snakeBodyObj = Resources.Load<GameObject>("Prefabs/RiskPre/SnakeBody");
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
        nextSnakeBody = nextSnakeBodyObj.GetComponent<SnakeBody>();
        snakeLength = 1;
        nextSnakeBody.SetAllMemember( null, snakeLength, null);

        snakeTail = nextSnakeBody;

        snakeBodySpriteRenderer.sortingOrder--;
    }
    



    //***************************************************************************
    //对蛇的相关操作


    private void AddOneBody()
    {
        snakeLength++;
        tSnakeBodyObj = Instantiate(snakeBodyObj, snakeTail.transform.position, snakeTail.transform.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<SnakeBody>();


        tSnakeBody.SetAllMemember(snakeTail, snakeLength, null);
        snakeTail.SetNextBody(tSnakeBodyObj);
        snakeTail = tSnakeBody;

        snakeBodySpriteRenderer.sortingOrder--;

    }


    private void MinusOneBody()
    {
        snakeLength--;

        snakeTail = snakeTail.GetLastSnakeBodyObj();
        Destroy(snakeTail.GetNextSnakeBodyObj());
        snakeTail.SetNextBody(null);
        snakeBodySpriteRenderer.sortingOrder++;

        
    }


    private void DoubleSnakeBody()
    {
        int addNum = snakeLength;
        for (int i = 0; i < addNum; i++)
        {
            AddOneBody();
        }


    }


    private void HalfSnakeBody()
    {

        int minusNum = snakeLength / 2;
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
        historyPosArray[qtail] = transform.position;
        historyRotArray[qtail] = transform.rotation;

        qhead--;
        qtail--;
        if (qhead < 0)
        {
            qhead += Snake.arrayLength;
        }
        if (qtail < 0)
        {
            qtail += Snake.arrayLength;
        }
    }


    private void SetVelocity()
    {

        if (!whetherHighSpeed)
        {
            thisRigidbody2d.velocity = transform.up * snakeSpeed;
        }
        else
        {
            thisRigidbody2d.velocity = transform.up * snakeSpeed * highSpeedK;
        }
    }


    //吃到物体后触发的方法
    public void GetOneFood()
    {

        RiskMapCreater.GetTheInstance().ClearTarPosList();
        RiskGameManager.GetTheInstance().SetScore(0);
        wisdomNum--;
        if (wisdomNum < 0)
        {
            wisdomNum = 0;

        }
        else if (wisdomNum == 0)
        {
            BackFromWisdom();
        }
        else
        {
            RiskMapCreater.GetTheInstance().SnakeGetTheWisdom();
        }
        AddOneBody();
        SoundPlayer.PlayItemsSound(0);
        RiskGameManager.GetTheInstance().SetLenText(snakeLength);
    }
    public void GetOnePoison()
    {

        if (whetherGetSheild)
        {
            SoundPlayer.PlayItemsSound(5);
            whetherGetSheild = false;
            theSheild.SetActive(false);
            sheildTimer = 0;
            return;
        }

        RiskGameManager.GetTheInstance().SetScore(1);

        SoundPlayer.PlayItemsSound(1);
        if (snakeLength <= 2)
        {
            whetherAlive = false;
            RiskGameManager.GetTheInstance().SetLenText(snakeLength - 2);
            return;
        }
        MinusNBody(2);
        RiskGameManager.GetTheInstance().SetLenText(snakeLength);
    }
    public void GetMushRoom()
    {
        RiskGameManager.GetTheInstance().SetScore(2);
        DoubleSnakeBody();
        SoundPlayer.PlayItemsSound(2);
        RiskGameManager.GetTheInstance().SetLenText(snakeLength);
    }
    public void GetMine()
    {
        if (whetherGetSheild)
        {
            SoundPlayer.PlayItemsSound(5);
            whetherGetSheild = false;
            theSheild.SetActive(false);
            sheildTimer = 0;
            return;
        }
        RiskGameManager.GetTheInstance().SetScore(3);
        SoundPlayer.PlayItemsSound(3);
        if (snakeLength == 1)
        {
            whetherAlive = false;
            RiskGameManager.GetTheInstance().SetLenText(0);
            return;
        }
        HalfSnakeBody();
        RiskGameManager.GetTheInstance().SetLenText(snakeLength);
    }
    public void GetEnergy()
    {
        SoundPlayer.PlayItemsSound(4);
        whetherHighSpeed = true;
        highSpeedTimer = 5f;
        SetVelocity();
        RiskGameManager.GetTheInstance().SetSpeedText((int)(snakeSpeed * highSpeedK));
    }
    public void GetSheild()
    {
        SoundPlayer.PlayItemsSound(5);
        whetherGetSheild = true;
        sheildTimer = 5f;
        theSheild.SetActive(true);
    }
    public void GetWisdom()
    {
        SoundPlayer.PlayItemsSound(6);
        wisdomNum = 4;
        thisSpriteRenderer.sprite = snakeDizzyHeadSpr;
        thisRigidbody2d.velocity = Vector3.zero;

        tarPosition=RiskMapCreater.GetTheInstance().GetNextTarPos();
        //Time.timeScale = 0;
    }
    public void BackFromWisdom()
    {
        
        thisSpriteRenderer.sprite = snakeHeadSpr;
        
    }

    //***************************************************************************



    private void Awake()
    {
        whetherAlive = true;
        //whetherWisdom = false;
        wisdomNum = 0;
        whetherGetSheild = false;
        sheildTimer = 0f;
        theSheild.SetActive(false);

        highSpeedTimer = 0f;
        whetherHighSpeed = false;
        theInstance = this;
        thisRigidbody2d = GetComponent<Rigidbody2D>();

        SetStartHistoryArray();
        LoadSkinSprite();
        SetPrefabSnakeHeadAndBody();
        snakeBodySpriteRenderer.sortingOrder = 0;

        InstantiateFirstBody();
        

        RiskGameManager.GetTheInstance().SetLenText(snakeLength);
        RiskGameManager.GetTheInstance().SetSpeedText((int)(snakeSpeed));

        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (whetherAlive)
        {
            if (whetherHighSpeed)
            {
                whetherHighSpeed = highSpeedTimer > 0;
                highSpeedTimer -= Time.deltaTime;
                if (!whetherHighSpeed)
                {
                    RiskGameManager.GetTheInstance().SetSpeedText((int)(snakeSpeed));
                }
            }
            if (whetherGetSheild)
            {
                whetherGetSheild = sheildTimer > 0;
                theSheild.SetActive(whetherGetSheild);
                sheildTimer -= Time.deltaTime;
            }
        }

        else
        {
            RiskGameManager.GetTheInstance().TheSnakDie();
        }
       

    }


    private void FixedUpdate()
    {

        if (wisdomNum==0)
        {

            //蛇头相对鼠标的位移矢量(世界坐标)
            dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


            //蛇头相对鼠标的位移矢量的模长平方小于1.6则不进行方向和位置的变化避免出现抖动
            if (dHeadTowards.sqrMagnitude > 1.6f)
            {
                transform.up = dHeadTowards;
                SetVelocity();

            }



            
        }
        else
        {
            if ((tarPosition - transform.position).sqrMagnitude < 0.1f)
            {
                tarPosition= RiskMapCreater.GetTheInstance().GetNextTarPos();
            }
            transform.up = tarPosition - transform.position;
            if (whetherHighSpeed)
            {
                transform.position += transform.up * snakeSpeed * highSpeedK * Time.fixedDeltaTime;
            }
            else
            {
                transform.position += transform.up * snakeSpeed * Time.fixedDeltaTime;
            }
            
        }

        SetHistoryArray();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (wisdomNum==0)
        {
            //避免出现在离开物体时，蛇头相对鼠标的位移矢量的模长平方小于1.6，速度依然保持与物体接触时相同的bug
            dHeadTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.up = dHeadTowards;
            SetVelocity();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "DeathWall")
        {
            SoundPlayer.PlayItemsSound(7);
            whetherAlive = false;
            return;
        }
    }





}
