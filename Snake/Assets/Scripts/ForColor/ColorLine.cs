using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLine : MonoBehaviour
{
    private SpriteRenderer thisSpriteRender;
    private Color[] theColors = { Color.red, Color.yellow, Color.blue, Color.cyan };
    private float perT = 0.6f;
    private float timer=0;

    private int colorNum = 4;
    private void Awake()
    {
        thisSpriteRender = GetComponent<SpriteRenderer>();
        thisSpriteRender.color = theColors[Random.Range(0, colorNum)];
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > perT)
        {
            timer = 0;
            thisSpriteRender.color = theColors[Random.Range(0,colorNum)];
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "SnakeHead")
        {
            ColorSnake.GetTheInstance().SetSnakeColor(thisSpriteRender.color);
        }

        print(Color.yellow);
    }
}
