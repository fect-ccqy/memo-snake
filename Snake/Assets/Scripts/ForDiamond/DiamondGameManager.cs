using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondGameManager : MonoBehaviour
{
    private static DiamondGameManager theInstance;


    //*********************************************************************************************************************************************
    //关于UI

    private int score = 0;
    [SerializeField] private Text lenText, speedText, scoreText;

    [SerializeField] private Text dieScoreText;
    [SerializeField] private GameObject dieUIObj;

    [SerializeField] private GameObject settingUiObj;
    

    public static DiamondGameManager GetTheInstance()
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
        DiamondGameManager.theInstance = this;


        Time.timeScale = 1f;

        SetScoreText();
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
