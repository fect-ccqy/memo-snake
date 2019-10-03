using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusicPlayer : MonoBehaviour
{
    //跨场景播放背景音乐


    private static bool whetherExist = false;
    private static GameObject theExistentBKMusicController;
    private static BKMusicPlayer theInstance;


    private AudioSource audioSourceOfBKMusic;



    //***************************************************************************
    //get set


    public static bool GetWhetherExist()
    {
        return BKMusicPlayer.whetherExist;
    }


    public static BKMusicPlayer GetTheInstance()
    {
        return BKMusicPlayer.theInstance;
    }


    public float GetVolume()
    {
        return audioSourceOfBKMusic.volume;

    }


    public void SetVolume(float vol)
    {
        audioSourceOfBKMusic.volume = vol;
    }



    //***************************************************************************
    //生命周期

    private void Awake()
    {

        BKMusicPlayer.whetherExist = true;
        BKMusicPlayer.theExistentBKMusicController = this.gameObject;
        audioSourceOfBKMusic = BKMusicPlayer.theExistentBKMusicController.GetComponent<AudioSource>();
        BKMusicPlayer.theInstance = this;
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
