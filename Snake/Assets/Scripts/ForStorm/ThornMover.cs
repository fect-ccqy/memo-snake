using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMover : MonoBehaviour
{

    [SerializeField] private Transform thron;
    
    [SerializeField] private float timer;//可设置初相位

    private float halfMoveRan=1f;
    private Vector3 dPos;
    private void Awake()
    {
        
        dPos = transform.position+transform.up*1.3f;
        //thron.position = new Vector3(5, 5, 0);
    }
    

    private void FixedUpdate()
    {
        thron.position = dPos+transform.up * Mathf.Sin(timer) * halfMoveRan;
        timer += Time.fixedDeltaTime*1.5f;
        if (timer > Mathf.PI*2) timer -= Mathf.PI*2;
    }
}
