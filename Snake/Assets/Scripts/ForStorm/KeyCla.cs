using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCla : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "SnakeHead")
        {
            Destroy(this.gameObject);
        }
    }
}
