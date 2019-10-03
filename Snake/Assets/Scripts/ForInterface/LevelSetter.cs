using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetter : MonoBehaviour
{
    //在关卡选择界面为button onclick 提供方法以设定第几关

    public void ChooseOneLevel(int levelnum)
    {
        MessageSender.GetTheInstance().SetLevelNum(levelnum);
    }
}
