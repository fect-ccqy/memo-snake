using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficlultySetter : MonoBehaviour
{
    //在选择难度界面，用来控制难度选择和相关UI


    public Button[] button;
    // public Button but1;
    public Sprite[] buttonUnSelectedImage;
    public Sprite[] buttonSelectedImage;

   
    private SpriteState[] buttonUnSelectedSpriteState= new SpriteState[3];
    private SpriteState[] buttonSelectedSpriteState= new SpriteState[3];

    private void Awake()
    {


        SetButtonSpriteState();

        ChooseOneDifficulty(MessageSender.GetDifficultyNum());

    }

    private void SetButtonSpriteState()
    {
        for(int i = 0; i < 3; i++)
        {

            buttonSelectedSpriteState[i].pressedSprite = buttonSelectedImage[i];
            buttonSelectedSpriteState[i].highlightedSprite = buttonSelectedImage[i];
            buttonSelectedSpriteState[i].selectedSprite = buttonSelectedImage[i];

            buttonUnSelectedSpriteState[i].pressedSprite = buttonUnSelectedImage[i];
            buttonUnSelectedSpriteState[i].highlightedSprite = buttonUnSelectedImage[i];
            buttonUnSelectedSpriteState[i].selectedSprite = buttonUnSelectedImage[i];

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
        MessageSender.SetDifficultyNum(difficulty);
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

