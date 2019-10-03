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

    private float snakeSpeed = 20f;
    private int snakeLength;//不包括蛇头
    private Rigidbody2D thisRigidbody2d;
    public Vector2 dHeadTowards;
    private Vector3 midScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
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
    
    private GameObject snakeHeadObj;
    private GameObject snakeTailObj;
    private SnakeBody snakeTail;
    private Transform snakeTailTrans;



    

    
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
        return historyPosArray[(qhead + Snake.oneStepNum-1) % Snake.arrayLength];
    }

    public Quaternion GetHistoryRot()
    {
        return historyRotArray[(qhead + Snake.oneStepNum-1) % Snake.arrayLength];
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
        snakeBodyObj = Resources.Load<GameObject>("Prefabs/SnakeBody");
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


    public void AddOneBody()
    {
        snakeLength++;
        tSnakeBodyObj = Instantiate(snakeBodyObj, snakeTailTrans.position, snakeTailTrans.rotation) as GameObject;
        tSnakeBody = tSnakeBodyObj.GetComponent<SnakeBody>();

        snakeTail.SetNextBody(tSnakeBodyObj, tSnakeBody);

        tSnakeBody.SetAllMemember(snakeTailObj, snakeTailTrans,snakeTail,snakeLength,null,null);

        snakeTailObj = tSnakeBodyObj;
        snakeTail = tSnakeBody;
        snakeTailTrans = tSnakeBodyObj.transform;
        snakeBodySpriteRenderer.sortingOrder--;
    }


    public void MinusOneBody()
    {
        snakeLength--;
        snakeTailObj = snakeTail.GetLastSnakeBodyObj();
        snakeTail = snakeTailObj.GetComponent<SnakeBody>();
        snakeTailTrans = snakeTailObj.transform;

        Destroy(snakeTail.GetNextSnakeBodyObj());

        snakeTail.SetNextBody(null,null);
        snakeBodySpriteRenderer.sortingOrder++;
    }


    public void DoubleSnakeBody()
    {
        int addNum = snakeLength;
        for(int i = 0; i < addNum; i++)
        {
            AddOneBody();
        }


    }


    public void HalfSnakeBody()
    {
        int minusNum = snakeLength/2;
        for (int i = 0; i < minusNum; i++)
        {
            MinusOneBody();
        }
    }


    public void AddNBody(int addN)
    {
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
            qhead += Snake.arrayLength;
        }
        if (qtail < 0)
        {
            qtail += Snake.arrayLength;
        }
    }


    //***************************************************************************



    private void Awake()
    {
        theInstance = this;
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
            thisRigidbody2d.velocity = transform.up * snakeSpeed;
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
        thisRigidbody2d.velocity = transform.up * snakeSpeed;
    }
}
