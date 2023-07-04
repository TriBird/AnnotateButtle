using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoFieldCtrl : MonoBehaviour
{

    public bool is_touch = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D partner)
    {
        // print("enter");
        is_touch = true;
    }
    private void OnTriggerExit2D(Collider2D partner)
    {
        Invoke("touch_release", 0.1f);
    }
    private void touch_release(){
        is_touch = false;
    }
}