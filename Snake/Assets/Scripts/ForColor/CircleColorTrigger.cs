﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColorTrigger : MonoBehaviour
{
    [SerializeField] private Color thisColor;

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
        if ((collision.transform.tag == "SnakeHead") && (thisColor != ColorSnake.GetTheInstance().GetSnakeColor()))
        {
            ColorSnake.GetTheInstance().TheSnakeDie();
        }

    }
}
