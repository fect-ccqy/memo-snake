using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    /*******************
    *用于场景间信息传递
    * 
    ****************/

    private static bool whetherExist=false;
    private static MessageSender theInstance;
    private static GameObject theExistentMessageSender;


    private float soundVolume=0.6f;//存储音效的音量

    private int gameModeNum=0;//0 Risk   1 storm    2 diamonds  3 color
    private int skinNum=0;//0 浅蓝绿色   1  浅橙色   2  浅蓝色   3 暗绿色
    private int levelNum=0;//0  第一关   1 第二关  2 第三关  3 第四关
    private int difficultyNum=0;//0 简单    1 普通    2 困难



    //***************************************************************************
    //get set

    public static bool GetWhetherExist()
    {
        return MessageSender.whetherExist;
    }
    public static MessageSender GetTheInstance()
    {
        return MessageSender.theInstance;
    }




    public void SetSoundVolume(float vol)
    {
        soundVolume = vol;
        print("SetSoundVolume:" + soundVolume);
    }

    public float GetSoundVolume()
    {
        return soundVolume;
    }



    public void SetGameModeNum(int gameModenum)
    {
        gameModeNum = gameModenum;
        print("SetGameMode:" + gameModeNum);
    }

    public int GetGameModeNum()
    {
        return gameModeNum;
    }




    public void SetSkinNum(int skinnum)
    {
        skinNum = skinnum;
        print("SetSkin:" + skinNum);
    }

    public int GetSkinNum()
    {
        return skinNum;
    }





    public void SetLevelNum(int levelnum)
    {
        levelNum = levelnum;
        print("SetLevel:" + levelNum);
    }

    public int GetLevelNum()
    {
        return levelNum;
    }


    public void SetDifficultyNum(int difficultynum)
    {
        difficultyNum = difficultynum;
        print("SetDifficulty:" + difficultyNum);
    }
    public int GetDifficultyNum()
    {
        return difficultyNum;
    }


    //***************************************************************************
    //生命周期

    private void Awake()
    {
        MessageSender.whetherExist = true;
        MessageSender.theInstance = this;
        MessageSender.theExistentMessageSender = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }
}
