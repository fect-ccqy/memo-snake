using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficlultySetter : MonoBehaviour
{
    //在选择难度界面，用来控制难度选择和相关UI
    


    [SerializeField] private Button[] button;
    // public Button but1;


    
    private Sprite[] buttonUnSelectedImage = new Sprite[3];
    private Sprite[] buttonSelectedImage = new Sprite[3];



    private SpriteState[] buttonUnSelectedSpriteState= new SpriteState[3];
    private SpriteState[] buttonSelectedSpriteState= new SpriteState[3];

    private void Awake()
    {


        SetButtonSpriteState();//加载初始的状态

        ChooseOneDifficulty(MessageSender.GetTheInstance().GetDifficultyNum());

    }

    private void SetButtonSpriteState()
    {
        buttonUnSelectedImage[0] = Resources.Load<Sprite>("Sprites/Interface/levelLow1");
        buttonUnSelectedImage[1] = Resources.Load<Sprite>("Sprites/Interface/levelNormal1");
        buttonUnSelectedImage[2] = Resources.Load<Sprite>("Sprites/Interface/levelHigh1");

        buttonSelectedImage[0] = Resources.Load<Sprite>("Sprites/Interface/levelLow2");
        buttonSelectedImage[1] = Resources.Load<Sprite>("Sprites/Interface/levelNormal2");
        buttonSelectedImage[2] = Resources.Load<Sprite>("Sprites/Interface/levelHigh2");


        for (int i = 0; i < 3; i++)
        {

            buttonUnSelectedSpriteState[i].pressedSprite = buttonUnSelectedImage[i];
            buttonUnSelectedSpriteState[i].highlightedSprite = buttonUnSelectedImage[i];
            buttonUnSelectedSpriteState[i].selectedSprite = buttonUnSelectedImage[i];


            buttonSelectedSpriteState[i].pressedSprite = buttonSelectedImage[i];
            buttonSelectedSpriteState[i].highlightedSprite = buttonSelectedImage[i];
            buttonSelectedSpriteState[i].selectedSprite = buttonSelectedImage[i];

            

        }
    }

    public void ChooseOneDifficulty(int difficulty)
    {

        button[0].spriteState = buttonUnSelectedSpriteState[0];
        button[1].spriteState = buttonUnSelectedSpriteState[1];
        button[2].spriteState = buttonUnSelectedSpriteState[2];
        button[0].image.sprite = buttonUnSelectedImage[0];
        button[1].image.sprite = buttonUnSelectedImage[1];
        button[2].image.sprite = buttonUnSelectedImage[2];
        MessageSender.GetTheInstance().SetDifficultyNum(difficulty);
        button[difficulty].image.sprite = buttonSelectedImage[difficulty];
        button[difficulty].spriteState = buttonSelectedSpriteState[difficulty];
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

