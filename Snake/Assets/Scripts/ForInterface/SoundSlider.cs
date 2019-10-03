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
        slider = GetComponent<Slider>();//加载初始的状态
        slider.value = MessageSender.GetTheInstance().GetSoundVolume();
    }
    public void SetSoundVolume()
    {
        MessageSender.GetTheInstance().SetSoundVolume(slider.value);
    }

}
