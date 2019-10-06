using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMapTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DiamondMapCreater.GetTheInstance().CreateNewMap();
    }

}
