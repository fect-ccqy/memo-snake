using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    

    public Transform playerTransform;

    private float smoothK = 0.24f;


    
    //private Vector3 leftDownPoint = new Vector3(0, 0, 0);


    //
    private Vector3 tarPos;
    private Vector3 dPos;
    private float dLen;

    
    private Vector3 leftDownWorldPoint;
    private Vector3 rightUpWorldPoint;

    private void Awake()
    {
        SetWorldPoint();
    }


    private void SetWorldPoint()
    {

        Vector3 leftDownScreenPoint = new Vector3(0, 0, 0);
        Vector3 rightUpScreenPoint = new Vector3(Screen.width, Screen.height, 0);
        Vector3 midScreenPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Vector3 leftdownworlpoint = Camera.main.ScreenToWorldPoint(leftDownScreenPoint);
        Vector3 rightupworldpoint = Camera.main.ScreenToWorldPoint(rightUpScreenPoint);
        Vector3 midscreenworldpoint = Camera.main.ScreenToWorldPoint(midScreenPoint);


        leftDownWorldPoint = new Vector3(-47.5f, -47.5f, 0) + rightupworldpoint - midscreenworldpoint;
        rightUpWorldPoint = new Vector3(47.5f, 47.5f, 0) + leftdownworlpoint - midscreenworldpoint;



        print("leftdownworlpoint" + leftdownworlpoint);
        print("rightupworldpoint" + rightupworldpoint);

        print("leftDownWorldPoint" + leftDownWorldPoint);
        print("rightUpWorldPoint" + rightUpWorldPoint);
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 dPos = (transform.position - playerTransform.position) * Time.deltaTime * 0.9f;
        dPos.z = 0;
        transform.position +=dPos;
        */

        
        /*
        Vector3 tPos = playerTransform.position;
        tPos.z = transform.position.z;
        transform.position = tPos;
        */
        
        
        
    }
    private void FixedUpdate()
    {
        tarPos = playerTransform.position;

        if (tarPos.x<leftDownWorldPoint.x)
        {
            tarPos.x = leftDownWorldPoint.x;
        }
        else if (tarPos.x>rightUpWorldPoint.x)
        {
            tarPos.x = rightUpWorldPoint.x;
        }

        if (tarPos.y < leftDownWorldPoint.y)
        {
            tarPos.y = leftDownWorldPoint.y;
        }
        else if (tarPos.y>rightUpWorldPoint.y)
        {
            tarPos.y = rightUpWorldPoint.y;
        }

        


        dPos = tarPos - transform.position;
        dLen = dPos.sqrMagnitude;
        dPos.z = 0;
        dPos = dPos *dLen* smoothK * Time.fixedDeltaTime;
        
        transform.position += dPos;
        


       

    }
}
