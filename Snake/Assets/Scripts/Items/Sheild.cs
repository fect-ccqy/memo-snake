﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
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
        Snake.GetTheInstance().GetSheild();
        RiskGameManager.GetTheInstance().SetMapPosZero(transform.position);
        RiskGameManager.GetTheInstance().CreateOneSheild();
        Destroy(this.gameObject);
    }
}
