using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardCtrl : MonoBehaviour
{

    public GameMaster gm;
    public int CardNumber = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDrag()
    {
        gm.DragObj_Trans.gameObject.SetActive(true);
    }

    public void OnMouseUp()
    {
        gm.DragObj_Trans.gameObject.SetActive(false);

        if(gm.annofield_ctrl.is_touch){
            transform.GetComponent<Image>().color = new Color32(0xac, 0x53, 0x53, 0xff);
            gm.Annotate(CardNumber);
        }
    }


}
