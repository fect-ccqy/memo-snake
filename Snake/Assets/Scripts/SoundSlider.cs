using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundSlider : MonoBehaviour
{
    //调节音效音量的slider

    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = MessageSender.soundVolume;
    }
    public void SetSoundVolume()
    {
        //音效分为游戏音效(比如吃到食物)，和按下button两部分
        MessageSender.soundVolume = slider.value;
        ButtonSoundPlayer.audioSourceOfButtonSound.volume = slider.value;
    }

}
