using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObj : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StormGameManager.GetTheInstance().OpenWinInterface();
    }
}
