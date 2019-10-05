using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondFood : MonoBehaviour
{
    private int addNum;
    [SerializeField] private Text numText;


    private void Awake()
    {
        if (Random.Range(0, 4) == 0)
        {
            gameObject.SetActive(false);
        }

        addNum = Random.Range(1, 10 - MessageSender.GetTheInstance().GetDifficultyNum() * 3);
        numText.text = addNum.ToString();
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

            DiamondSnake.GetTheInstance().AddNBody(addNum);
            Destroy(this.gameObject);
        }
    }
}
