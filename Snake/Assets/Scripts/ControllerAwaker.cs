using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAwaker : MonoBehaviour
{
    //负责在开始界面生成多个跨场景的对象


    //prefabs
    private GameObject messageSender;
    private GameObject bKMusicController;
    private GameObject buttonSoundController;

    //public GameObject buttonController;


    //记录生成的实例，用来改变生成实例的名字(主要是去除"clone",避免查找物体出现问题)
    private GameObject existentCrossSceneMessageSender;
    private GameObject existentCrossSceneBKMusicController;
    private GameObject existentCrossSceneButtonSoundController;


    //private GameObject existentCrossSceneButtonController;
    // Start is called before the first frame update
    private void Awake()
    {
        Screen.fullScreen = true;



        if (!MessageSender.GetWhetherExist())
        {
            messageSender = Resources.Load<GameObject>("Prefabs/CrossSceneMessageSender");
            existentCrossSceneMessageSender=Instantiate(messageSender, transform.position,transform.rotation)as GameObject;
            existentCrossSceneMessageSender.name = messageSender.name;
        }
        if (!BKMusicPlayer.GetWhetherExist())
        {
            bKMusicController = Resources.Load<GameObject>("Prefabs/CrossSceneBKMusicController");
            existentCrossSceneBKMusicController=Instantiate(bKMusicController, transform.position,transform.rotation)as GameObject;
            existentCrossSceneBKMusicController.name = bKMusicController.name;
        }
        if (!ButtonSoundPlayer.GetWhetherExist())
        {

            buttonSoundController = Resources.Load<GameObject>("Prefabs/CrossSceneButtonSoundController");
            existentCrossSceneButtonSoundController = Instantiate(buttonSoundController, transform.position, transform.rotation) as GameObject;
            existentCrossSceneButtonSoundController.name = buttonSoundController.name;
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
