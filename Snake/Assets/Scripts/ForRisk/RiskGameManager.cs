using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RiskGameManager : MonoBehaviour
{



    private static RiskGameManager theInstance;




    //*********************************************************************************************************************************************
    //关于生成地图相关的数据

    private int[,] gameMap = new int[120, 120];//默认为0 即没有东西在里面
    private int sideLen = 43;
    private static Transform snakeHeadTrans;//用于判断生成位置蛇头的距离
    private static GameObject theSnake;
    private Quaternion rotHorizontal;//水平(在awake中初始化)
    private Quaternion rorVertical;//垂直


    // checktype/settype分别为1,3,5,7,9   order: 0,1,2,3,4   itemtype: 1,2,3,4,5  在map上的存储数字
    private int[] wallNum = new int[5];
    private GameObject[] wall = new GameObject[5];
    
    //道具的checktype/settype均为1   表示尺寸
    //itemtype 6,7,8,9,10,,,,,在map上的存储
    //clipsNum 0,1,2,3,4,5,6  播放音频的下标
    private int foodNum;
    private int poisonNum;
    private int mushroomNum;
    private int mineNum;
    private int SpeederNum;
    private int shieldNum;//场景护盾数目
    private int wisdomNum;

    private GameObject foodPrefab;//6
    private GameObject poisonGrassPrefab;//7
    private GameObject mushroomPrefab;//8
    private GameObject minePrefab;//9
    private GameObject energyPrefab;//10speeder
    private GameObject sheildPrefab;//11

    
    //*********************************************************************************************************************************************
    //关于UI

    private int score = 0;
    [SerializeField] private Text lenText, speedText, scoreText;

    [SerializeField] private Text dieScoreText;
    [SerializeField] private GameObject dieUIObj;

    [SerializeField] private GameObject settingUiObj;
    private int tDifficulty;//记录打开设置界面时的难度，如果改变了，就重新加载场景

    
    //*********************************************************************************************************************************************
    //用于测试
    public GameObject testObj;
    private GameObject[,] gameobjMap = new GameObject[120, 120];








    //*********************************************************************************************************************************************
    //get set

    public static RiskGameManager GetTheInstance()
    {
        return theInstance;

    }

    //*********************************************************************************************************************************************
    //一些辅助运算方法

    private Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(-43, 44), Random.Range(-43, 44), 0);
        return randomPos;
    }

    private bool CheckPos(Vector3 checkpos, int checkType, int layWay)
    {
        int checkHeadLen = 2 * checkType + (7 - checkType) / 2;
        if ((checkpos - snakeHeadTrans.position).sqrMagnitude < checkHeadLen * checkHeadLen)
            return false;



        int dmidx = 60 + (int)checkpos.x;
        int dmidy = 60 + (int)checkpos.y;
        int checkLen = checkHeadLen - 3;
        //横着放
        if (layWay == 0)
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= checkLen; j++)
                {
                    if (!((gameMap[dmidx + j, dmidy + i] == 0) && (gameMap[dmidx + j, dmidy - i] == 0) && (gameMap[dmidx - j, dmidy + i] == 0) && (gameMap[dmidx - j, dmidy - i] == 0)))
                    {
                        return false;
                    }
                }
            }
        }

        //竖着放
        else
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= checkLen; j++)
                {
                    if (!((gameMap[dmidx + i, dmidy + j] == 0) && (gameMap[dmidx + i, dmidy - j] == 0) && (gameMap[dmidx - i, dmidy + j] == 0) && (gameMap[dmidx - i, dmidy - j] == 0)))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //*********************************************************************************************************************************************
    //初始化数据
    private void SetStartData()
    {


        RiskGameManager.theSnake = GameObject.Find("Snake");
        RiskGameManager.snakeHeadTrans = RiskGameManager.theSnake.transform;


        Time.timeScale = 1f;
        rotHorizontal = Quaternion.identity;
        rorVertical = Quaternion.identity;
        rorVertical.eulerAngles = new Vector3(0, 0, 90);



        for (int i = 0; i <= sideLen; i++)
        {
            for (int j = 0; j <= sideLen; j++)
            {
                gameMap[60 + i, 60 + j] = 0;
                gameMap[60 - i, 60 + j] = 0;
                gameMap[60 + i, 60 - j] = 0;
                gameMap[60 - i, 60 - j] = 0;
            }
        }

        if (MessageSender.GetTheInstance().GetDifficultyNum() == 0)
        {
            wallNum[0] = 3;
            wallNum[1] = 3;
            wallNum[2] = 2;
            wallNum[3] = 1;
            wallNum[4] = 0;

            foodNum = 10;
            poisonNum = 3;
            mushroomNum = 4;
            mineNum = 2;
            SpeederNum = 2;
            shieldNum = 4;//场景护盾数目
            wisdomNum = 1;

        }
        else if (MessageSender.GetTheInstance().GetDifficultyNum() == 1)
        {
            wallNum[0] = 3;
            wallNum[1] = 6;
            wallNum[2] = 3;
            wallNum[3] = 1;
            wallNum[4] = 1;

            foodNum = 8;
            poisonNum = 5;
            mushroomNum = 3;
            mineNum = 3;
            SpeederNum = 4;
            shieldNum = 2;//场景护盾数目
            wisdomNum = 1;

        }
        else if (MessageSender.GetTheInstance().GetDifficultyNum() == 2)
        {
            wallNum[0] = 3;
            wallNum[1] = 6;
            wallNum[2] = 4;
            wallNum[3] = 2;
            wallNum[4] = 1;

            foodNum = 5;
            poisonNum = 8;
            mushroomNum = 1;
            mineNum = 4;
            SpeederNum = 4;
            shieldNum = 2;//场景护盾数目
            wisdomNum = 1;

        }
        else
        {
            wallNum[0] = 0;
            wallNum[1] = 0;
            wallNum[2] = 0;
            wallNum[3] = 0;
            wallNum[3] = 0;

            foodNum = 0;
            poisonNum = 0;
            mushroomNum = 0;
            mineNum = 0;
            SpeederNum = 0;
            shieldNum = 0;//场景护盾数目
            wisdomNum = 0;
        }



    }

    
    private void LoadItems()
    {

        wall[0] = Resources.Load<GameObject>("Prefabs/smallwall100");
        wall[1] = Resources.Load<GameObject>("Prefabs/smallwall300");
        wall[2] = Resources.Load<GameObject>("Prefabs/smallwall500");
        wall[3] = Resources.Load<GameObject>("Prefabs/smallwall700");
        wall[4] = Resources.Load<GameObject>("Prefabs/smallwall900");


        foodPrefab = Resources.Load<GameObject>("Prefabs/OneFood");
        poisonGrassPrefab = Resources.Load<GameObject>("Prefabs/PoisonousGrass");
        mushroomPrefab = Resources.Load<GameObject>("Prefabs/Mushroom");
        minePrefab = Resources.Load<GameObject>("Prefabs/Boom");
        energyPrefab = Resources.Load<GameObject>("Prefabs/Energy");
        sheildPrefab = Resources.Load<GameObject>("Prefabs/Sheild");
        /*
        ;
        minePrefab;
        energyPrefab;//speeder
        sheildPrefab;
        */
    }


    //*********************************************************************************************************************************************
    //关于生成物体

    private void CreateItems()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < wallNum[i]; j++)
            {
                CreateOneWall(i);
            }
        }

        for (int i = 0; i < foodNum; i++)
        {

            CreateOneFood();

        }
        for (int i = 0; i < poisonNum; i++)
        {

            CreateOneGrass();

        }
        for (int i = 0; i < mushroomNum; i++)
        {

            CreateOneMushroom();

        }
        for (int i = 0; i < mineNum; i++)
        {

            CreateOneMine();

        }
        for (int i = 0; i < SpeederNum; i++)
        {

            CreateOneEnergy();

        }
        for (int i = 0; i < shieldNum; i++)
        {

            CreateOneSheild();

        }
    }

    private void CreateOneWall(int wallorder)
    {
        int settype = wallorder * 2 + 1;
        int itemtype = wallorder + 1;
        Vector3 tPos = GetRandomPos();
        int layMod = Random.Range(0, 2);
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
                layMod = Random.Range(0, 2);
            }
        }

        if (layMod == 0)
        {
            Instantiate(wall[wallorder], tPos, rotHorizontal);
            SetMapPos(tPos, settype, layMod, itemtype);
        }
        else if (layMod == 1)
        {
            Instantiate(wall[wallorder], tPos, rorVertical);
            SetMapPos(tPos, settype, layMod, itemtype);
        }


    }

    public void CreateOneFood()
    {
        int itemtype = 6;
        int settype = 1;

        Vector3 tPos = GetRandomPos();
        int layMod = 0;
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
            }
        }
        Instantiate(foodPrefab, tPos, rotHorizontal);
        SetMapPos(tPos, settype, layMod, itemtype);
    }

    public void CreateOneGrass()
    {
        int itemtype = 7;
        int settype = 1;

        Vector3 tPos = GetRandomPos();
        int layMod = 0;
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
            }
        }
        Instantiate(poisonGrassPrefab, tPos, rotHorizontal);
        SetMapPos(tPos, settype, layMod, itemtype);
    }

    public void CreateOneMushroom()
    {
        int itemtype = 8;
        int settype = 1;

        Vector3 tPos = GetRandomPos();
        int layMod = 0;
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
            }
        }
        Instantiate(mushroomPrefab, tPos, rotHorizontal);
        SetMapPos(tPos, settype, layMod, itemtype);
    }

    public void CreateOneMine()
    {
        int itemtype = 9;
        int settype = 1;

        Vector3 tPos = GetRandomPos();
        int layMod = 0;
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
            }
        }
        Instantiate(minePrefab, tPos, rotHorizontal);
        SetMapPos(tPos, settype, layMod, itemtype);
    }

    public void CreateOneEnergy()
    {
        int itemtype = 10;
        int settype = 1;

        Vector3 tPos = GetRandomPos();
        int layMod = 0;
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
            }
        }
        Instantiate(energyPrefab, tPos, rotHorizontal);
        SetMapPos(tPos, settype, layMod, itemtype);
    }

    public void CreateOneSheild()
    {
        int itemtype = 11;
        int settype = 1;

        Vector3 tPos = GetRandomPos();
        int layMod = 0;
        while (true)
        {
            if (CheckPos(tPos, settype, layMod))
            {
                break;

            }
            else
            {
                tPos = GetRandomPos();
            }
        }
        Instantiate(sheildPrefab, tPos, rotHorizontal);
        SetMapPos(tPos, settype, layMod, itemtype);
    }

    //生成物体后对数据的操作
    private void SetMapPos(Vector3 setpos, int setType, int layWay, int itemtype)
    {
        int checkHeadLen = 2 * setType + (7 - setType) / 2;




        int dmidx = 60 + (int)setpos.x;
        int dmidy = 60 + (int)setpos.y;
        int setLen = checkHeadLen - 3;
        //横着放
        if (layWay == 0)
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= setLen; j++)
                {
                    gameMap[dmidx + j, dmidy + i] = itemtype;
                    gameMap[dmidx + j, dmidy - i] = itemtype;
                    gameMap[dmidx - j, dmidy + i] = itemtype;
                    gameMap[dmidx - j, dmidy - i] = itemtype;
                }
            }
        }

        //竖着放
        else
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= setLen; j++)
                {
                    gameMap[dmidx + i, dmidy + j] = itemtype;
                    gameMap[dmidx + i, dmidy - j] = itemtype;
                    gameMap[dmidx - i, dmidy + j] = itemtype;
                    gameMap[dmidx - i, dmidy - j] = itemtype;


                }
            }
        }


    }

    public void SetMapPosZero(Vector3 setpos)
    {
        SetMapPos(setpos, 1, 0, 0);
    }

    //*********************************************************************************************************************************************
    //关于UI

    public void SetScore(int settype)
    {
        switch (settype)
        {
            case 0:
                score += 10;
                break;
            case 1:
                score -= 10;
                break;
            case 2:
                score *= 2;
                break;
            case 3:
                score /= 2;
                break;
            default:
                break;
        }
        SetScoreText();
    }

    public void SetLenText(int len)
    {
        lenText.text = len.ToString();
    }
    public void SetSpeedText(int speed)
    {
        speedText.text = speed.ToString();
    }
    private void SetScoreText()
    {
        scoreText.text = score.ToString();
    }


    public void TheSnakDie()
    {
        Time.timeScale = 0;
        dieScoreText.text = score.ToString();
        dieUIObj.SetActive(true);
    }
    public void OpenSettingInterface()
    {
        if (!dieUIObj.activeSelf)
        {
            tDifficulty = MessageSender.GetTheInstance().GetDifficultyNum();
            Time.timeScale = 0f;
            settingUiObj.SetActive(true);
        }
    }
    public void CloseSettingface()
    {
        if (tDifficulty == MessageSender.GetTheInstance().GetDifficultyNum())
        {
            Time.timeScale = 1f;
            settingUiObj.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }




    //*********************************************************************************************************************************************
    //生命周期


    private void Awake()
    {
        //用于测试
        Vector3 tobjpos = new Vector3(0f, 0f, 0f);
        for (int i = -50; i <= 50; i++)
        {
            for (int j = -50; j <= 50; j++)
            {
                tobjpos.x = i;
                tobjpos.y = j;
                gameobjMap[60 + i, 60 + j] = Instantiate(testObj, tobjpos, Quaternion.identity) as GameObject;
            }
        }

    
        RiskGameManager.theInstance = this;

        SetStartData();
       
        LoadItems();

        CreateItems();
    }
   


    // Start is called before the first frame update
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {
        //用于测试
        Vector3 tobjpos = new Vector3(0f, 0f, 0f);
        for (int i = -50; i <= 50; i++)
        {
            for (int j = -50; j <= 50; j++)
            {
                tobjpos.x = i;
                tobjpos.y = j;
                gameobjMap[60 + i, 60 + j].SetActive(gameMap[60 + i, 60 + j] != 0);
            }
        }
    }

    
}

