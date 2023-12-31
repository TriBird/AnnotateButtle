using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardCtrl : MonoBehaviour, IPointerClickHandler
{

    public GameMaster gm;
    public int CardNumber = 0;

    public bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        if(isSelected){
            gm.Annotate_del(CardNumber);
            transform.GetComponent<Image>().color = new Color32(0xF5, 0xEF, 0xE7, 0xff);
            isSelected = false;
        } else {
            if(gm.Annotate(CardNumber)){
                transform.GetComponent<Image>().color = new Color32(0xE0, 0x93, 0x91, 0xff);
                isSelected = true;
            }
        }

    }
}
