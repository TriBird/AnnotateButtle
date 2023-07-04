using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjCtrl : MonoBehaviour
{
    Vector3 mousePos, worldPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x+200, mousePos.y,10f));
        transform.position = worldPos;
    }
}
