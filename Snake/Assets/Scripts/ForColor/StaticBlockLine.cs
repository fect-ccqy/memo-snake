using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockLine : MonoBehaviour
{
    private Vector3 tPos;
    [SerializeField] private Transform[] blockTransforms;
    private void Awake()
    {

        SwapPos(Random.Range(0, 4), Random.Range(0, 4));
        SwapPos(Random.Range(0, 4), Random.Range(0, 4));
        SwapPos(Random.Range(0, 4), Random.Range(0, 4));
        SwapPos(Random.Range(0, 4), Random.Range(0, 4));

    }

    private void SwapPos(int num1, int num2)
    {

        tPos = blockTransforms[num1].position;
        blockTransforms[num1].position = blockTransforms[num2].position;
        blockTransforms[num2].position = tPos;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
