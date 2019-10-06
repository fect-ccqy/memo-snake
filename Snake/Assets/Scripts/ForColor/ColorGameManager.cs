using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGameManager : MonoBehaviour
{
    private static ColorGameManager theInstance;


    //*********************************************************************************************************************************************
    //关于UI
    
    [SerializeField] private Text dieScoreText;
    [SerializeField] private GameObject dieUIObj;
    [SerializeField] private GameObject settingUiObj;


    public static ColorGameManager GetTheInstance()
    {
        return theInstance;

    }
    //*********************************************************************************************************************************************
    //关于UI
    
    
    public void TheSnakDie()
    {
        Time.timeScale = 0;
        dieScoreText.text = ((int)ColorSnake.GetTheInstance().transform.position.x).ToString();
        dieUIObj.SetActive(true);
    }

    public void CloseSettingInterface()
    {
        Time.timeScale = 1f;
        settingUiObj.SetActive(false);
    }

    public void OpenSettingInterface()
    {
        if (!dieUIObj.activeSelf)
        {
            Time.timeScale = 0f;
            settingUiObj.SetActive(true);
        }
    }

    private void Awake()
    {
        ColorGameManager.theInstance = this;
        Time.timeScale = 1f;
        
    }
}
