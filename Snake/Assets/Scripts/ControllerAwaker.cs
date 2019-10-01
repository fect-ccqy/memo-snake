using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAwaker : MonoBehaviour
{
    public GameObject crossSceneMessageSender;
    public GameObject musicController;
    //public GameObject buttonController;

    private GameObject existentCrossSceneMessageSender;
    private GameObject existentCrossSceneMusicController;
    //private GameObject existentCrossSceneButtonController;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!MessageSender.whetherExist)
        {
            existentCrossSceneMessageSender=Instantiate(crossSceneMessageSender, transform.position,transform.rotation)as GameObject;
            existentCrossSceneMessageSender.name = crossSceneMessageSender.name;
        }
        if (!MusicController.whetherExist)
        {
            existentCrossSceneMusicController=Instantiate(musicController, transform.position,transform.rotation)as GameObject;
            existentCrossSceneMusicController.name = musicController.name;
        }
        /*
        if (!ButtonController.whetherExist)
        {
            existentCrossSceneButtonController= Instantiate(buttonController, transform.position, transform.rotation) as GameObject;
            existentCrossSceneButtonController.name = buttonController.name;
        }
        */
        
    }
    void Start()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
