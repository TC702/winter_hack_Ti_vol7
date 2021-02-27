using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditContesnts : MonoBehaviour
{
    public GameObject Title_obj;
    public GameObject Detail_obj;


    public void SetContents(string val1, string val2)
    {
        Text title = Title_obj.GetComponent<Text>();
        Text detail = Detail_obj.GetComponent<Text>();

        title.text = val1;
        detail.text = val2;
    }

}
