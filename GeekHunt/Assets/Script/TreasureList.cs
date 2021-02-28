using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureList : MonoBehaviour
{
    private int size = 9;
    public string[] title = new string[9]
    {
        "ひろゆき",
        "2ch",
        "掲示板",
        "SE",
        "Programer",
        "Program",
        "OpenSource",
        "フルスタックエンジニア",
        "Unity"

    };
    public string[] detail = new string[9]
    {
        "2chを作った人。写像については知らない。",
        "ネットの匿名掲示板。テレビの２チャンネルで流していいものではない。",
        "情報を伝えるための板。いろんな人がそれぞれの目的で使う。",
        "情報システム関連の業務に従事する者",
        "コンピュータのプログラムを作成する人",
        "テキストファイル。コンピュータに対する命令が書かれたもの。人とコンピュータの橋。",
        "誰でも使えるソースコード/ソフトウェア。先人の遺産。",
        "強い人。",
        "3Dゲームを作る際によく使われるフレームワークの１つ。2Dゲームを作る際にも活躍。"
    };
    public bool[] isHave = new bool[9]
    {
        true,
        true,
        false,
        false,
        true,
        false,
        false,
        true,
        false
    };

    public int Size { get => size; set => size = value; }

    private GameObject Contents;
    private GameObject List_con;

    void Start()
    {
        Contents = this.transform.Find("Contents").gameObject;
        List_con = this.transform.Find("Scroll View").gameObject;
    }


    public void GetTreasure(int index)
    {
        if (isHave[index] == false)
        {
            isHave[index] = true;
            //Debug.Log(string.Format("{0}: is discover = {1}", title[index], isHave[index]));
            List_con.GetComponent<Chisiki_list_controller>().UpdateButton(index);
            //Contents.GetComponent<EditContesnts>().SetContents(title[index], detail[index]);
            //Contents.SetActive(true);
            //return true;
        }
        else
        {
            Debug.Log("This Treasure is already gotton.");
            //return false;
        }
    }


    //public string[] Title { get => title; set => title = value; }
    //public string[] Content { get => content; set => content = value; }
}
