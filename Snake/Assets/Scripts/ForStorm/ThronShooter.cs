using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronShooter : MonoBehaviour
{

    private GameObject moveThron;
    private GameObject theMoveThron;
    //private MoveThorn themovethron;
    
    [SerializeField] private float speed;
    [SerializeField] private float timer;
    [SerializeField] private float tPer;

    private void Awake()
    {
        moveThron = Resources.Load<GameObject>("Prefabs/StormPre/MoveThorn");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (timer < 0)
        {
            theMoveThron=Instantiate(moveThron,transform.position, transform.rotation) as GameObject;
            theMoveThron.GetComponent<MoveThorn>().SetSpeed(speed);
            timer = tPer;
        }
        timer -= Time.fixedDeltaTime;

    }
}
