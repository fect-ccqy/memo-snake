using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BKMusicSlider : MonoBehaviour
{
    //调节背景音量的slider

    
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = BKMusicPlayer.audioSourceOfBKMusic.volume;
    }
    public void SetBKMusicVolume()
    {
        BKMusicPlayer.audioSourceOfBKMusic.volume = slider.value;
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
