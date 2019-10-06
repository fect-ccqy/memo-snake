using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMapCreater : MonoBehaviour
{
    private static ColorMapCreater theInstance;

    private Vector3 tPos;

    [SerializeField] private GameObject[] partMaps;

    [SerializeField] private GameObject lastMap, nowMap, nextMap;

    public static ColorMapCreater GetTheInstance()
    {
        return theInstance;
    }

    public void CreateNewMap()
    {
        Destroy(lastMap);
        lastMap = nowMap;
        nowMap = nextMap;
        nextMap = Instantiate(partMaps[Random.Range(0,partMaps.Length)], tPos, transform.rotation);
        tPos.x += 200f;
    }

    private void Awake()
    {
        tPos = new Vector3(600, 0, 0);
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
