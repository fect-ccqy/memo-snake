using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMapCreater : MonoBehaviour
{


    private static DiamondMapCreater theInstance;

    private Vector3 tPos;

    [SerializeField] private GameObject partMap;

    [SerializeField] private GameObject lastMap, nowMap, nextMap;

    public static DiamondMapCreater GetTheInstance()
    {
        return theInstance;
    }

    public void CreateNewMap()
    {
        Destroy(lastMap);
        lastMap = nowMap;
        nowMap = nextMap;
        nextMap = Instantiate(partMap, tPos, transform.rotation);
        tPos.x += 90f;
    }

    private void Awake()
    {
        tPos = new Vector3(225, 0, 0);
        theInstance = this;
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
