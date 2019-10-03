using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform playerTransform;

    private float smoothK = 0.24f;

    //
    private Vector3 dPos;
    private float dLen;

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
        
        dPos = playerTransform.position - transform.position;
        dLen = dPos.sqrMagnitude;
        dPos.z = 0;
        dPos = dPos *dLen* smoothK * Time.fixedDeltaTime;
        
        transform.position += dPos;
        


       

    }
}
