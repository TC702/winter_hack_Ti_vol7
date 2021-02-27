using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureList : MonoBehaviour
{
    private int size = 9;
    public string[] title = new string[9]
    {
        "Hiroyuki",
        "2ch",
        "keiziban",
        "a",
        "i",
        "u",
        "e",
        "o",
        "k"

    };
    public string[] detail = new string[9]
    {
        "2chを作った人",
        "ネットの匿名掲示板",
        "ゴミだめ",
        "a",
        "i",
        "u",
        "e",
        "o",
        "k"
    };
    public bool[] isHave = new bool[9]
    {
        false,
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

    


    //public string[] Title { get => title; set => title = value; }
    //public string[] Content { get => content; set => content = value; }
}
