using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static bool whetherExist = false;
    public static GameObject theExistentMusicController;

    public static float BkMusicVolume=1f;
    public static float SoundVolume=1f;
    // Start is called before the first frame update
    private void Awake()
    {
        MusicController.BkMusicVolume = 1f;
        MusicController.SoundVolume =1f;
        MusicController.whetherExist = true;
        MusicController.theExistentMusicController = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
