using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronShooter : MonoBehaviour
{

    private GameObject moveThron;
    private GameObject theMoveThron;
    private AudioSource soundPlayer;
    //private MoveThorn themovethron;

    [SerializeField] private float speed;
    [SerializeField] private float timer;
    private float tPer;

    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.clip = Resources.Load<AudioClip>("Sounds/arrow");

        tPer = 2.2f - 0.6f * MessageSender.GetTheInstance().GetDifficultyNum();
        moveThron = Resources.Load<GameObject>("Prefabs/StormPre/MoveThorn");
    }
    private void FixedUpdate()
    {
        if (timer < 0)
        {
            theMoveThron=Instantiate(moveThron,transform.position, transform.rotation) as GameObject;

            soundPlayer.volume = MessageSender.GetTheInstance().GetSoundVolume();            
            soundPlayer.Play();

            theMoveThron.GetComponent<MoveThorn>().SetSpeed(speed);
            timer = tPer;
        }
        timer -= Time.fixedDeltaTime;

    }
}
