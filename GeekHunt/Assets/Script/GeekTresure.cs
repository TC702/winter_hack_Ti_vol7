using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeekTresure : MonoBehaviour
{
    private TreasureList tlist = new TreasureList();

    // Start is called before the first frame update
    void Start()
    {
        string[] title = tlist.title;
        //Debug.Log(title[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

