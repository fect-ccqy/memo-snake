using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondMinus : MonoBehaviour
{
    private int minusNum;
    [SerializeField] private Text numText;
    private Vector3 tdPos;
    private float xyOffset=4.5f;

    private void Awake()
    {
        minusNum = Random.Range(1, 14 + MessageSender.GetTheInstance().GetDifficultyNum() * 6);
        numText.text = minusNum.ToString();
    }

    
    // Update is called once per frame
    void Update()
    {
        tdPos = transform.position - DiamondSnake.GetTheInstance().transform.position;
        if ((-xyOffset < tdPos.x) && (tdPos.x < xyOffset) && (-xyOffset < tdPos.y) && (tdPos.y < xyOffset) && (tdPos.sqrMagnitude < 29.7f))
        {

            DiamondSnake.GetTheInstance().MinusNBody(minusNum);
            Destroy(this.gameObject);
        }


    }

}
