using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSetter : MonoBehaviour
{
    //在游戏模式选择界面，用来提供button  onclick时的方法
    public void ChooseOneGameMode(int gamemodenum)
    {
        MessageSender.GetTheInstance().SetGameModeNum(gamemodenum);
    }
}
