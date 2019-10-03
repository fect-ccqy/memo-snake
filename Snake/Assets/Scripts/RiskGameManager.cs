using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskGameManager : MonoBehaviour
{


    private int[,] gameMap = new int[120, 120];//默认为0 即没有东西在里面
    private int sideLen = 43;


    private int[] wallNum=new int[5];
    private GameObject[] wall = new GameObject[5];// checktype/settype分别为1,3,5,7,9
                                                  // order                 0,1,2,3,4
                                                  //itemtype               1,2,3,4,5
    private int score = 0;

    //checktype均为1
    //itemtype 6,7,8,9,10,,,,,
    //clipsNum 0,1,2,3,4,5,6
    private int foodNum;
    private int poisonNum;
    private int mushroomNum;
    private int mineNum;
    private int SpeederNum;
    private int shieldNum;//场景护盾数目
    private int wisdomNum;



    private static GameObject theSnake;
    private static Snake thesnake;
    private static Transform snakeHeadTrans;

    //
    private Quaternion rotHorizontal;//水平
    private Quaternion rorVertical;//垂直


    private void Awake()
    {
        RiskGameManager.theSnake = GameObject.Find("Snake");
        RiskGameManager.snakeHeadTrans = RiskGameManager.theSnake.transform;
        RiskGameManager.thesnake = RiskGameManager.theSnake.GetComponent<Snake>();
        print(RiskGameManager.snakeHeadTrans.position);
        SetStartMemberNum();
        LoadItems();


        CreatItems();
    }
    private void SetStartMemberNum()
    {
        rotHorizontal = Quaternion.identity;
        rorVertical = Quaternion.identity;
        rorVertical.eulerAngles = new Vector3(0, 0, 90);

        for (int i = 0; i <= sideLen; i++)
        {
            for (int j = 0; j <= sideLen; j++)
            {
                gameMap[60+i, 60+j] = 0;
                gameMap[60-i, 60+j] = 0;
                gameMap[60+i, 60-j] = 0;
                gameMap[60-i, 60-j] = 0;
            }
        }

        if (MessageSender.GetTheInstance().GetDifficultyNum() == 0)
        {
            wallNum[0] = 3;
            wallNum[1] = 3;
            wallNum[2] = 2;
            wallNum[3] = 1;
            wallNum[4] = 0;

            foodNum=10;
            poisonNum=3;
            mushroomNum=4;
            mineNum=2;
            SpeederNum=2;
            shieldNum=4;//场景护盾数目
            wisdomNum=1;

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

    }

   



    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void CreatItems()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < wallNum[i]; j++)
            {
                CreatOneWall(i);
            }
        }
    }
    private void CreatOneWall(int wallorder)
    {
        int settype = wallorder * 2 + 1;
        int itemtype = wallorder + 1;
        Vector3 tPos = GetRandomPos();
        int layMod = Random.Range(0, 2);
        while(true)
        {
            if (CheckPos(tPos,wallorder*2+1,layMod))
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
            Instantiate(wall[wallorder], tPos,rotHorizontal);
            SetMapPos(tPos, settype, layMod, itemtype);
        }
        else if (layMod == 1)
        {
            Instantiate(wall[wallorder], tPos, rorVertical);
            SetMapPos(tPos, settype, layMod, itemtype);
        }


    }
    private Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(-43, 44), Random.Range(-43, 44), 0);
        return randomPos;
    }

    private bool CheckPos(Vector3 checkpos,int checkType,int layWay)
    {
        int checkHeadLen = 2 * checkType + (7 - checkType) / 2;
        if((checkpos-snakeHeadTrans.position).sqrMagnitude<checkHeadLen*checkHeadLen)
            return false;



        int dmidx = 60 + (int)checkpos.x;
        int dmidy = 60 + (int)checkpos.y;
        int checkLen = checkHeadLen - 3;
        //横着放
        if (layWay == 0)
        {
            for (int i = 0; i <=2; i++)
            {
                for(int j = 0; j <= checkLen; j++)
                {
                    if(!((gameMap[dmidx + j, dmidy + i] == 0)&&(gameMap[dmidx + j, dmidy - i] == 0)&&(gameMap[dmidx - j, dmidy + i] == 0) &&(gameMap[dmidx - j, dmidy - i] == 0)))
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

    private void SetMapPos(Vector3 setpos, int setType, int layWay,int itemtype)
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


    public static void TriggerOneBodyFood()
    {
        thesnake.AddOneBody();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
