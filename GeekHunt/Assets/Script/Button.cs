using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private GameObject Content;
    public int num;

    private TreasureList tlist = new TreasureList();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        //Debug.Log(num);

        Content.SetActive(true);
        string title = tlist.title[num];
        string detail = tlist.detail[num];
        Content.GetComponent<EditContesnts>().SetContents(title, detail);
    }

    public void SetNumber(int val1)
    {
        num = val1;
        Text button_name = this.GetComponentInChildren<Text>();
        string Sercret = new string('?', tlist.title[num].Length);
        if (tlist.isHave[num]) 
        {
            button_name.text = string.Format(" {0:D3} : {1}", num, Sercret);
        }
        else
        {
            button_name.text = string.Format(" {0:D3} : {1}", num, tlist.title[num]);
        }
        Content = GameObject.Find("Contents");
        //Debug.Log(string.Format("{0}: setbutton", val1));

    }
}
