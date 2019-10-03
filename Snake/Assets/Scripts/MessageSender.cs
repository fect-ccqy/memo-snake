using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    /*******************
    *用于场景间信息传递
    * 
    ****************/

    public static bool whetherExist=false;
    public static GameObject theExistentMessageSender;
    public static float soundVolume=0.6f;//存储音效的音量

    private static int gameModeNum=0;//0 Risk   1 storm    2 diamonds  3 color
    private static int skinNum=0;//0 浅蓝绿色   1  浅橙色   2  浅蓝色   3 暗绿色
    private static int levelNum=0;//0  第一关   1 第二关  2 第三关  3 第四关
    private static int difficultyNum=0;//0 简单    1 普通    2 困难



    //for test
    //private float timers=0;
    //private int timess = 0;

    private void Awake()
    {
        MessageSender.whetherExist = true;
        MessageSender.theExistentMessageSender = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }



    public static void SetGameModeNum(int gameModenum)
    {
        MessageSender.gameModeNum = gameModenum;
        print("SetGameMode:" + MessageSender.gameModeNum);
    }

    public static int GetGameModeNum()
    {
        return MessageSender.gameModeNum;
    }




    public static void SetSkinNum(int skinnum)
    {
        MessageSender.skinNum = skinnum;
        print("SetSkin:" + MessageSender.skinNum);
    }

    public static int GetSkinNum()
    {
        return MessageSender.skinNum;
    }





    public static void SetLevelNum(int levelnum)
    {
        MessageSender.levelNum = levelnum;
        print("SetLevel:" + MessageSender.levelNum);
    }

    public static int GetLevelNum()
    {
        return MessageSender.levelNum;
    }


    public static void SetDifficultyNum(int difficultynum)
    {
        MessageSender.difficultyNum = difficultynum;
        print("SetDifficulty:" + MessageSender.difficultyNum);
    }
    public static int GetDifficultyNum()
    {
        return MessageSender.difficultyNum;
    }

    /*

    private void FixedUpdate()
    {
        timers += Time.fixedDeltaTime;
        timess++;
        if (timers >= 1f)
        {
            print(timess);
            timers = 0;
            timess = 0;
        }

    }
    */
}
