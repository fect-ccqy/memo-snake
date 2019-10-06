﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    private Vector3 startPos;
   
    [SerializeField] private float timer;
    [SerializeField] private Transform[] blockTransforms;
    [SerializeField] private float halfMoveRan;

    private Vector3 tPos;
    private void Awake()
    {
        startPos = transform.position;
        
         SwapPos(Random.Range(0, 4), Random.Range(0, 4));
         SwapPos(Random.Range(0, 4), Random.Range(0, 4));
         SwapPos(Random.Range(0, 4), Random.Range(0, 4));
         SwapPos(Random.Range(0, 4), Random.Range(0, 4));
       
    }

    private void SwapPos(int num1,int num2)
    {
        
        tPos = blockTransforms[num1].position;
        blockTransforms[num1].position = blockTransforms[num2].position;
        blockTransforms[num2].position = tPos;

    }
    
    private void FixedUpdate()
    {
        float tsin = Mathf.Sin(timer);
        transform.position = startPos + tsin * tsin*tsin * transform.up * halfMoveRan;
        timer += Time.fixedDeltaTime * 1.5f;
        if (timer > Mathf.PI * 2) timer -= Mathf.PI * 2;
    }
}
