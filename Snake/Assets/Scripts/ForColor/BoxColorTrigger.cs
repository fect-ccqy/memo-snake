using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColorTrigger : MonoBehaviour
{
    [SerializeField] private Color thisColor;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.transform.tag == "SnakeHead")&& (thisColor != ColorSnake.GetTheInstance().GetSnakeColor()))
        {
            ColorSnake.GetTheInstance().TheSnakeDie();
        }
    }
}
