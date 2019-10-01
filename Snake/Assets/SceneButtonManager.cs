using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayClickButtonSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClickButtonSound()
    {
        audioSource.volume = MusicController.SoundVolume;
        audioSource.Play();
    }
    public void GoToStartScene()
    {
        SceneManager.LoadScene(0);

    }
}
