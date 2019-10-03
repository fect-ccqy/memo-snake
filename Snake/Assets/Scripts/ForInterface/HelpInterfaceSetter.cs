using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpInterfaceSetter : MonoBehaviour
{
    //负责控制Help界面的画面变化
    [SerializeField] private GameObject[] helpImage;
    

    public void ChooseOneInterface(int interNum)
    {
        helpImage[0].SetActive(false);
        helpImage[1].SetActive(false);
        helpImage[2].SetActive(false);
        helpImage[3].SetActive(false);


        helpImage[interNum].SetActive(true);

    }
}
