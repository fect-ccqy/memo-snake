using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    [SerializeField] private Transform monster;

    [SerializeField] private float timer;//可设置初相位

    [SerializeField] private float halfMoveRan;
    private Vector3 dPos;
    private void Awake()
    {

        dPos = transform.position;
    }

    

    private void FixedUpdate()
    {
        monster.position = dPos + transform.up * Mathf.Sin(timer) * halfMoveRan;
        timer += Time.fixedDeltaTime * 1.5f;
        if (timer > Mathf.PI * 2) timer -= Mathf.PI * 2;
    }
}

