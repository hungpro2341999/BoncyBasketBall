using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


    public class Item : MonoBehaviour,IPointerClickHandler
{

 
    public int idItem;
    public Image Img;
    public Text TextCost;
    public int cost;
    public TypeItem type;

    public Image ImgSelect;
    public Image ImgSelect2;

    public bool isBuy;
    public bool isUsing;
    public bool isFree;

    public Transform Transcost;

    private void Start()
    {
     
    }
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
        ShopCtrl.Ins.CurrSelectHead = this;
        ShopCtrl.Ins.SelectItemHead(ShopCtrl.Ins.CurrSelectHead.idItem);
        
      
        ShopCtrl.Ins.TargetGraphic.Apply();
    }

    public void Select_2()
    {
        ImgSelect.gameObject.SetActive(true);
        ImgSelect2.gameObject.SetActive(true);
    }


    public virtual void Select()
    {
        Debug.Log("Check :: ");
        ImgSelect.gameObject.SetActive(true); 
    }
    public virtual void Unselect()
    {
        ImgSelect2.gameObject.SetActive(false);
        // Debug.Log("UnCheck :: ");
        ImgSelect.gameObject.SetActive(false);
    }

    public virtual void Use()
    {

        ImgSelect2.gameObject.SetActive(true);
    }

    public virtual void UnUse()
    {
        ImgSelect2.gameObject.SetActive(false);
    }

    public  virtual void Default()
    {
        ImgSelect.gameObject.SetActive(false);
        ImgSelect2.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public void LoadSaveData(bool isFree , bool isBuy, bool isUse)
    {
        this.isFree = isFree;
        this.isBuy = isBuy;
        this.isUsing = isUse;
        LoadStatusItem();
    }


    public void LoadStatusItem()
    {
        if (isFree)
        {
            if (!isUsing)
            {
                FreeItem();
            }
            else
            {
                 ItemUsing();
            }
            
        }
        else
        {
            if (isUsing)
            {
                ItemUsing();
            }
            else if (isBuy)
            {
                isSelling();
            }
            else
            {
                ItemOwn();
            }
        }
    }

    public virtual void FreeItem()
    {
      
        ImgSelect.gameObject.SetActive(false); 
        Transcost.gameObject.SetActive(false);
       
    }

    public virtual void ItemUsing()
    {
        ImgSelect2.gameObject.SetActive(true);
        ImgSelect.gameObject.SetActive(true);
        Transcost.gameObject.SetActive(false);
    }

    public virtual void isSelling()
    {
        ImgSelect.gameObject.SetActive(false);
        Transcost.gameObject.SetActive(true);
    }
    public virtual void ItemOwn()
    {
        Transcost.gameObject.SetActive(false);
    }

    public virtual void Buy()
    {
        if (isBuy)
        {
            if (CtrlDataGame.Ins.GetCoin() >= cost)
            {
                int coin = CtrlDataGame.Ins.GetCoin() - cost;
                CtrlDataGame.Ins.SaveCoin(coin);
                isBuy = false;
                isUsing = true;
                LoadStatusItem();
                ShopCtrl.Ins.SaveShopHead();
                ShopCtrl.Ins.SelectItemHead(idItem);
                CtrlDataGame.Ins.SetHead(this.idItem);
            }
        }
        
    }

    

}

