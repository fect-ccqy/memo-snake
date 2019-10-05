using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondMinus : MonoBehaviour
{
    private int minusNum;
    [SerializeField] private Text numText;


    private void Awake()
    {
        minusNum = Random.Range(1, 14 + MessageSender.GetTheInstance().GetDifficultyNum() * 6);
        numText.text = minusNum.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "SnakeHead")
        {

            DiamondSnake.GetTheInstance().MinusNBody(minusNum);
            Destroy(this.gameObject);
        }
    }
}
