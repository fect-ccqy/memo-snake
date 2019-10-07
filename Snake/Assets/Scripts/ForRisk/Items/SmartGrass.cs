using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartGrass : MonoBehaviour
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

        RiskMapCreater.GetTheInstance().CreateOneWisdom();
        RiskMapCreater.GetTheInstance().SnakeEatTheWisdom(transform.position);
        Snake.GetTheInstance().GetWisdom();
        Destroy(this.gameObject);
    }
}
