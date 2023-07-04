using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardCtrl : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ドラッグ開始時に実行されるメソッド
    public void OnBeginDrag(PointerEventData eventData)
    {
        print($"OnBeginDrag : {eventData}");
    }

    // ドラッグ終了時に実行されるメソッド
    public void OnEndDrag(PointerEventData eventData)
    {
        print($"OnEndDrag : {eventData}");
    }
}
