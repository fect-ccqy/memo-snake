using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
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
        Snake.GetTheInstance().GetMushRoom();
        RiskMapCreater.GetTheInstance().SetMapPosZero(transform.position);
        RiskMapCreater.GetTheInstance().CreateOneMushroom();
        Destroy(this.gameObject);
    }
}
