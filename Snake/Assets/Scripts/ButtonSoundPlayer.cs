using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    //(跨场景)负责播放按下按钮的音效(有的按钮按下后会切换场景，独立出来音效播放器能避免声音播放出问题)

    public static bool whetherExist = false;
    public static GameObject theExistentButtonSoundController;
    public static AudioSource audioSourceOfButtonSound;

    private void Awake()
    {

        ButtonSoundPlayer.whetherExist = true;
        ButtonSoundPlayer.theExistentButtonSoundController = this.gameObject;
        ButtonSoundPlayer.audioSourceOfButtonSound = ButtonSoundPlayer.theExistentButtonSoundController.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

    }

    public static void PlayButtonSound()
    {
        ButtonSoundPlayer.audioSourceOfButtonSound.Play();
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
