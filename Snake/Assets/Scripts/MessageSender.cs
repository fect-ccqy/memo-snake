using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    public static bool whetherExist=false;
    public static GameObject theExistentMessageSender;

    private static int gameModeNum;
    private static int skinNum=0;
    private static int levelNum;
    private static int difficultyNum=0;

    private void Awake()
    {
        MessageSender.whetherExist = true;
        MessageSender.theExistentMessageSender = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }



    public void SetGameModeNum(int gameModenum)
    {
        MessageSender.gameModeNum = gameModenum;
    }
    
    public int GetGameModeNum()
    {
        return MessageSender.gameModeNum;
    }




    public void SetSkinNum(int skinnum)
    {
        MessageSender.skinNum = skinnum;
    }

    public int GetSkinNum()
    {
        return MessageSender.skinNum;
    }





    public void SetLevelNum(int levelnum)
    {
        MessageSender.levelNum = levelnum;
    }

    public int GetLevelNum()
    {
        return MessageSender.levelNum;
    }


    public void SetDifficultyNum(int difficultynum)
    {
        MessageSender.difficultyNum = difficultynum;
    }
    public int GetDifficultyNum()
    {
        return MessageSender.difficultyNum;
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
