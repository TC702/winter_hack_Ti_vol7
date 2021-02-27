using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batsu_button : MonoBehaviour
{

    public void onClick()
    {
        GameObject parent = transform.parent.gameObject;
        parent.SetActive(false);
    }
}
