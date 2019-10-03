using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSetter : MonoBehaviour
{
    //在皮肤选择界面为button onclick 提供方法以设定皮肤

    public void ChooseOneSkin(int skinnum)
    {
        MessageSender.GetTheInstance().SetSkinNum(skinnum);
    }
}
