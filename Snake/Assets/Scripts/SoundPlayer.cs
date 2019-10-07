using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static AudioSource soundPlayer;
    private static AudioClip[] audioClips=new AudioClip[9];


    private void Awake()
    {
        SoundPlayer.soundPlayer = GetComponent<AudioSource>();
        audioClips[0] = Resources.Load<AudioClip>("Sounds/eat");
        audioClips[1] = Resources.Load<AudioClip>("Sounds/poison");
        audioClips[2] = Resources.Load<AudioClip>("Sounds/eat");
        audioClips[3] = Resources.Load<AudioClip>("Sounds/boom");
        audioClips[4] = Resources.Load<AudioClip>("Sounds/energy");
        audioClips[5] = Resources.Load<AudioClip>("Sounds/sheild");
        audioClips[6] = Resources.Load<AudioClip>("Sounds/wisdom");
        audioClips[7] = Resources.Load<AudioClip>("Sounds/wall");
        audioClips[8] = Resources.Load<AudioClip>("Sounds/arrow");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayItemsSound(int clipnum)
    {
        soundPlayer.volume = MessageSender.GetTheInstance().GetSoundVolume();
        soundPlayer.clip = audioClips[clipnum];
        soundPlayer.Play();
    }


}
