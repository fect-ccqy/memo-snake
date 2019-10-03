using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    //(跨场景)负责播放按下按钮的音效(有的按钮按下后会切换场景，独立出来音效播放器能避免声音播放出问题)

    private static bool whetherExist = false;
    private static GameObject theExistentButtonSoundController;
    private static ButtonSoundPlayer theInstance;

    private AudioSource audioSourceOfButtonSound;



    //***************************************************************************
    //get set

    public static bool GetWhetherExist()
    {
        return ButtonSoundPlayer.whetherExist;
    }
    public static ButtonSoundPlayer GetTheInstance()
    {
        return ButtonSoundPlayer.theInstance;
    }


    //***************************************************************************
    //播放按钮音效（被SceneButtonManager调用）

    public void PlayButtonSound()
    {
        audioSourceOfButtonSound.volume = MessageSender.GetTheInstance().GetSoundVolume();
        audioSourceOfButtonSound.Play();
    }


    //***************************************************************************
    //生命周期
    private void Awake()
    {

        ButtonSoundPlayer.whetherExist = true;
        ButtonSoundPlayer.theExistentButtonSoundController = this.gameObject;
        audioSourceOfButtonSound = ButtonSoundPlayer.theExistentButtonSoundController.GetComponent<AudioSource>();
        ButtonSoundPlayer.theInstance = this;
        DontDestroyOnLoad(this.gameObject);

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
