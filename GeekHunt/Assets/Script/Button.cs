using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private GameObject Content;
    private GameObject rootParent;
    private GameObject chisiki_p;
    public int num;
    private bool isNew = false;

    private TreasureList tlist = new TreasureList();

    //private TreasureList tlist;

    // Start is called before the first frame update
    void Start()
    {
        rootParent = transform.root.gameObject;
        Content = rootParent.transform.Find("Chisiki_Panel/Contents").gameObject;
        //chisiki_p = rootParent.transform.Find("Chisiki_Panel").gameObject;
        //tlist = chisiki_p.GetComponent<TreasureList>();
        //Debug.Log("button find contents");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        //Debug.Log(num);
        Content.SetActive(true);
        if (isNew)
        {
            SetNumber(num);
        }
        if (tlist.isHave[num])
        {
            string title = tlist.title[num];
            string detail = tlist.detail[num];
            Content.GetComponent<EditContesnts>().SetContents(title, detail);
        }
        else
        {
            string title = new string('?', tlist.title[num].Length);
            string detail = new string('?', tlist.detail[num].Length);
            Content.GetComponent<EditContesnts>().SetContents(title, detail);
        }
    }

    public void SetNumber(int val1)
    {
        //tlist = chisiki_p.GetComponent<TreasureList>();
        //Debug.Log(tlist.isHave[val1]);
        //Debug.Log(tlist.title[val1]);
        num = val1;
        Text button_name = this.GetComponentInChildren<Text>();
        int title_length = tlist.title[num].Length;
        string Sercret = new string('?', title_length);
        if (tlist.isHave[num])
        {
            button_name.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            if(title_length < 10)
            {
                button_name.text = string.Format(" {0:D3} : {1}", num, tlist.title[num]);
            }
            else
            {
                //Debug.Log(tlist.title[0].ToCharArray());
                string title = tlist.title[num].Substring(0, 9);
                button_name.text = string.Format(" {0:D3} : {1:}...", num, title);
            }
        }
        else
        {
            button_name.text = string.Format(" {0:D3} : {1}", num, Sercret);
        }
        //Content = GameObject.Find("Contents");
        //Debug.Log(string.Format("{0}: setbutton", val1));
    }

    public void GetTreasure_received()
    {
        tlist.isHave[num] = true;
        isNew = true;
        Text button_name = this.GetComponentInChildren<Text>();
        int title_length = tlist.title[num].Length;
        if (title_length < 10)
        {
            button_name.text = string.Format(" new : {1}", num, tlist.title[num]);
            button_name.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        else
        {
            //Debug.Log(tlist.title[0].ToCharArray());
            string over_title = tlist.title[num].Substring(0, 9);
            button_name.text = string.Format(" new : {1:}...", num, over_title);
            button_name.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        Content.SetActive(true);
        string title = tlist.title[num];
        string detail = tlist.detail[num];
        Content.GetComponent<EditContesnts>().SetContents(title, detail);
    }
}
