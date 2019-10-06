using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StormGameManager : MonoBehaviour
{
    private static StormGameManager theInstance;


    //*********************************************************************************************************************************************
    //关于UI

    private int score = 0;
    [SerializeField] private Text lenText, speedText, scoreText;

    [SerializeField] private Text dieScoreText;
    [SerializeField] private GameObject dieUIObj;

    [SerializeField] private GameObject settingUiObj;

    [SerializeField] private GameObject winUiObj;
    private int tDifficulty;//记录打开设置界面时的难度，如果改变了，就重新加载场景


    public static StormGameManager GetTheInstance()
    {
        return theInstance;

    }
    //*********************************************************************************************************************************************
    //关于UI

    public void SetScore(int settype)
    {
        switch (settype)
        {
            case 0:
                score += 10;
                break;
            case 1:
                score -= 10;
                break;
            case 2:
                score *= 2;
                break;
            case 3:
                score /= 2;
                break;
            case 4:
                score += 5;
                break;
            default:
                break;
        }
        SetScoreText();
    }

    public void SetLenText(int len)
    {
        lenText.text = len.ToString();
    }
    public void SetSpeedText(int speed)
    {
        speedText.text = speed.ToString();
    }
    private void SetScoreText()
    {
        scoreText.text = score.ToString();
    }


    public void TheSnakDie()
    {
        Time.timeScale = 0;
        dieScoreText.text = score.ToString();
        dieUIObj.SetActive(true);
    }

    public void OpenWinInterface()
    {
        Time.timeScale = 0;
        winUiObj.SetActive(true);
    }

    public void OpenSettingInterface()
    {
        if ((!dieUIObj.activeSelf)&&(!winUiObj.activeSelf))
        {
            Time.timeScale = 0f;
            settingUiObj.SetActive(true);
        }
    }
    public void CloseSettingface()
    {
        Time.timeScale = 1f;
        settingUiObj.SetActive(false);
    }


    private void Awake()
    {
        StormGameManager.theInstance = this;


        Time.timeScale = 1f;

        SetScoreText();
    }

}
