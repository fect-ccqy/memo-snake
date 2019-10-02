using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusicPlayer : MonoBehaviour
{
    //跨场景背景音乐播放


    public static bool whetherExist = false;
    public static GameObject theExistentBKMusicController;
    public static AudioSource audioSourceOfBKMusic;

    private void Awake()
    {

        BKMusicPlayer.whetherExist = true;
        BKMusicPlayer.theExistentBKMusicController = this.gameObject;
        BKMusicPlayer.audioSourceOfBKMusic = BKMusicPlayer.theExistentBKMusicController.GetComponent<AudioSource>();
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
