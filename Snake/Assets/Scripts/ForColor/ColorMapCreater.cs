using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMapCreater : MonoBehaviour
{
    private static ColorMapCreater theInstance;

    private Vector3 tPos;

    [SerializeField] private GameObject[] partMaps;

    [SerializeField] private GameObject lastMap;
    private GameObject nowMap, nextMap;
    private int nmapNum=0;
    private int tnum;

    public static ColorMapCreater GetTheInstance()
    {
        return theInstance;
    }

    public void CreateNewMap()
    {
        GetRandomNum();
        Destroy(lastMap);
        lastMap = nowMap;
        nowMap = nextMap;
        nextMap = Instantiate(partMaps[tnum], tPos, transform.rotation);
        tPos.x += 200f;
    }
    private void GetRandomNum()
    {
        tnum = Random.Range(0, partMaps.Length);
        while (tnum==nmapNum)
        {

            tnum = Random.Range(0, partMaps.Length);

        }
        
    }
    private void Awake()
    {
        tPos = new Vector3(200, 0, 0);
        theInstance = this;

        GetRandomNum();
        nowMap = Instantiate(partMaps[tnum], tPos, transform.rotation);
        tPos.x += 200f;

        GetRandomNum();
        nextMap = Instantiate(partMaps[tnum], tPos, transform.rotation);
        tPos.x += 200f;

    }
    
}
