using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


    public class Item : MonoBehaviour,IPointerDownHandler
{

 
    public float idItem;
    public Image Img;
    public float cost;
   
    public virtual void ChangeItem(Sprite img)
    {
        this.Img.sprite = img;
    }

    public virtual void LoadItem(Sprite sprite,int cost,int id)
    {
        this.idItem = id;
        this.Img.sprite = sprite;
        this.cost = cost;
        Img.SetNativeSize();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        Click();
    }

    public virtual void Click()
    {

       // ShopCtrl.Ins.TargetGraphic.Attachment_Head(Img.sprite);
        ShopCtrl.Ins.TargetGraphic.Attachment_Hand(Img.sprite);
    }

    public void Unselect()
    {

    }
    
}

