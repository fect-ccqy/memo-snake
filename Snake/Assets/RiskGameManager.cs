using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskGameManager : MonoBehaviour
{
    private int[,] gameMap=new int[100,100];//默认为0 即没有东西在里面
    private int sideLen = 43;


    private int[] wallNum=new int[5];

    private int foodNum;
    private int poisonNum;
    private int mushroomNum;
    private int mineNum;
    private int SpeederNum;
    private int shieldNum;//场景护盾数目
    private int wisdomNum;


    private GameObject wall1, wall3, wall5, wall7, wall9;


    private void Awake()
    {
        SetStartMember();
    }
    void SetStartMember()
    {
        for(int i = 0; i <= sideLen; i++)
        {
            for (int j = 0; j <= sideLen; j++)
            {
                gameMap[i, j] = 0;
                gameMap[-i, j] = 0;
                gameMap[i, -j] = 0;
                gameMap[-i, -j] = 0;
            }
        }

        if (MessageSender.GetDifficultyNum() == 0)
        {
            wallNum[0] = 3;
            wallNum[1] = 3;
            wallNum[2] = 2;
            wallNum[3] = 1;
            wallNum[3] = 0;

            foodNum=10;
            poisonNum=3;
            mushroomNum=4;
            mineNum=2;
            SpeederNum=2;
            shieldNum=4;//场景护盾数目
            wisdomNum=1;

        }
        else if (MessageSender.GetDifficultyNum() == 1)
        {
            wallNum[0] = 3;
            wallNum[1] = 6;
            wallNum[2] = 3;
            wallNum[3] = 1;
            wallNum[3] = 1;

            foodNum = 8;
            poisonNum = 5;
            mushroomNum = 3;
            mineNum = 3;
            SpeederNum = 4;
            shieldNum = 2;//场景护盾数目
            wisdomNum = 1;

        }
        else if (MessageSender.GetDifficultyNum() == 2)
        {
            wallNum[0] = 3;
            wallNum[1] = 6;
            wallNum[2] = 4;
            wallNum[3] = 2;
            wallNum[3] = 1;

            foodNum = 5;
            poisonNum = 8;
            mushroomNum = 1;
            mineNum = 4;
            SpeederNum = 4;
            shieldNum = 2;//场景护盾数目
            wisdomNum = 1;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
