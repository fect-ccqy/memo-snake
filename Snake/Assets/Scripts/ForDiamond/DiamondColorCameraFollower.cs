using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondColorCameraFollower : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float xoffset;
    private Vector3 tPos;

    


    private void Awake()
    {
        tPos = transform.position;
        tPos.x = playerTransform.position.x+xoffset;
        tPos.y = playerTransform.position.y;
        transform.position = tPos;
    }


    private void FixedUpdate()
    {

        tPos = transform.position;
        tPos.x = playerTransform.position.x+xoffset;

        transform.position = tPos;



    }
}
