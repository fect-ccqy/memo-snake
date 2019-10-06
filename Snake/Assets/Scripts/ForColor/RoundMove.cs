using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundMove : MonoBehaviour
{

    private Vector3 tPos;
    [SerializeField] private Transform[] blockTransforms;

    [SerializeField] private float rotSpeed;//角度制   每秒转动多少角度

    // Start is called before the first frame update
    void Start()
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
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotSpeed * Time.fixedDeltaTime);
    }
}
