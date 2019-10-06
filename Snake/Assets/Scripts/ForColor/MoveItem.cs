using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItem : MonoBehaviour
{
    
    [SerializeField] private float timer;//可设置初相位

    [SerializeField] private float halfMoveRan ;

    private GameObject itemPre;
    private GameObject theItemObj;
    private Transform theItem;

    //private Vector3 dPos;
    private void Awake()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                itemPre = Resources.Load<GameObject>("Prefabs/ColorPre/CyanCircleTrigger");
                break;
            case 1:
                itemPre = Resources.Load<GameObject>("Prefabs/ColorPre/BlueCircleTrigger");
                break;
            case 2:
                itemPre = Resources.Load<GameObject>("Prefabs/ColorPre/RedCircleTrigger");
                break;
            case 3:
                itemPre = Resources.Load<GameObject>("Prefabs/ColorPre/YellowCircleTrigger");
                break;
            default:
                break;

        }
        theItemObj = Instantiate(itemPre, transform)as GameObject;
        theItem = theItemObj.transform;
        //dPos = transform.position + transform.up * 1.3f;
        //thron.position = new Vector3(5, 5, 0);
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
        theItem.position = transform.position+transform.up * Mathf.Sin(timer) * halfMoveRan;
        timer += Time.fixedDeltaTime * 1.5f;
        if (timer > Mathf.PI * 2) timer -= Mathf.PI * 2;
    }
}
