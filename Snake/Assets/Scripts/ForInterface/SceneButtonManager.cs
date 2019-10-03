using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    //每个场景都存在，但不是跨场景物体
    //负责提供button的播放音效方法和场景跳转方法
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //PlayClickButtonSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClickButtonSound()
    {
        ButtonSoundPlayer.GetTheInstance().PlayButtonSound();
    }


    public void GoToStartScene()
    {
        SceneManager.LoadScene(0);

    }
    public void GoToSettingScene()
    {
        SceneManager.LoadScene(1);

    }
    public void GoToSettingSkinScene()
    {
        SceneManager.LoadScene(2);

    }
    public void GoToHelpScene()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToSettingGameModeScene()
    {
        SceneManager.LoadScene(4);
    }
    public void GoToSettingLevelScene()
    {
        SceneManager.LoadScene(5);
    }


    public void GoToRiskGameScene()
    {
        SceneManager.LoadScene(6);
    }
    public void GoToStormGameScene()
    {
        //7,8,9,10
        SceneManager.LoadScene(7+MessageSender.GetTheInstance().GetLevelNum());
    }
    public void GoToDiamondsGameScene()
    {
        SceneManager.LoadScene(11);
    }
    public void GoToColorGameScene()
    {
        SceneManager.LoadScene(12);
    }
    


    public void ExitTheGame()
    {
        Application.Quit();
    }
    
}
