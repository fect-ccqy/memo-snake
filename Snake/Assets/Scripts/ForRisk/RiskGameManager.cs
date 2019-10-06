using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RiskGameManager : MonoBehaviour
{



    private static RiskGameManager theInstance;



    
    //*********************************************************************************************************************************************
    //关于UI

    private int score = 0;
    [SerializeField] private Text lenText, speedText, scoreText;

    [SerializeField] private Text dieScoreText;
    [SerializeField] private GameObject dieUIObj;

    [SerializeField] private GameObject settingUiObj;
    private int tDifficulty;//记录打开设置界面时的难度，如果改变了，就重新加载场景

    





    //*********************************************************************************************************************************************
    //get set

    public static RiskGameManager GetTheInstance()
    {
        return theInstance;

    }


    //*********************************************************************************************************************************************
    private void Awake()
    {

        Time.timeScale = 1f;

        RiskGameManager.theInstance = this;

        SetScoreText();
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
    public void OpenSettingInterface()
    {
        if (!dieUIObj.activeSelf)
        {
            tDifficulty = MessageSender.GetTheInstance().GetDifficultyNum();
            Time.timeScale = 0f;
            settingUiObj.SetActive(true);
        }
    }
    public void CloseSettingface()
    {
        if (tDifficulty == MessageSender.GetTheInstance().GetDifficultyNum())
        {
            Time.timeScale = 1f;
            settingUiObj.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }

    
}

