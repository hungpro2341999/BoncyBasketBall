using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


    public class Item : MonoBehaviour,IPointerClickHandler
{

 
    public float idItem;
    public Image Img;
    public Text TextCost;
    public float cost;
    public TypeItem type;

    public Image ImgSelect;
    public Image ImgBuyed;
    public virtual void ChangeItem(Sprite img)
    {
        this.Img.sprite = img;
    }

    public virtual void LoadItem(Sprite sprite,int cost,int id,TypeItem type)
    {

        this.TextCost.text = cost.ToString();
        this.idItem = id;
        this.Img.sprite = sprite;
        //this.cost = cost;
        Img.SetNativeSize();
        this.type = type;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Select();
        }
    }
    public virtual void Click()
    {

       // ShopCtrl.Ins.TargetGraphic.Attachment_Head(Img.sprite);
        ShopCtrl.Ins.TargetGraphic.Attachment_Head(Img.sprite);
       

        var a = ShopCtrl.Ins.Item_Heads;
        for(int i = 0; i < a.Count; i++)
        {
            
            if(a[i].idItem == idItem)
            {
               
                Select();
            }
            else
            {
                Unselect();
            }
        }
        ShopCtrl.Ins.TargetGraphic.Apply();
    }
    public virtual void Select()
    {
        Debug.Log("Check :: ");
        ImgSelect.gameObject.SetActive(true); 
    }
    public virtual void Unselect()
    {
        Debug.Log("UnCheck :: ");
        ImgSelect.gameObject.SetActive(false);
    }

    public virtual void Use()
    {
     
        ImgBuyed.enabled = true;
    }

    public virtual void UnUse()
    {
        ImgBuyed.enabled = false;
    }

    public  virtual void Default()
    {
        ImgSelect.gameObject.SetActive(false);
        ImgBuyed.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }
}

