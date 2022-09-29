using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ConfirmPurchase : UI_Popup
{
    [SerializeField]
    Sprite[] _iconImages;

    Item _itemData;

    enum Texts
    {
        ItemNameText,
        PriceText,
    }

    enum Images
    {
        GoodsIcon,
    }

    enum Buttons
    {
        OkayButton,
        CancleButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Managers.Data.ItemDict.TryGetValue(PlayerPrefs.GetInt("itemId"), out _itemData);

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        // Set Price Icon Image
        if (_itemData.type == "Amber")
            GetImage((int)Images.GoodsIcon).sprite = _iconImages[0];
        else if (_itemData.type == "Ruby")
            GetImage((int)Images.GoodsIcon).sprite = _iconImages[1];

        // Set Item Name
        GetText((int)Texts.ItemNameText).text = _itemData.itemName;

        // Set Item Price
        GetText((int)Texts.PriceText).text = _itemData.price.ToString();

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(OnCancleButtonClicked);
    }

    public void OnOkayButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();

        if (_itemData.type == "Ruby")
        {
            if(_itemData.price > Managers.Data.UserData.user.ruby)
            {
                Managers.UI.ShowPopupUI<UI_NotEnoughGoods>();
            }
            else
            {
                Managers.Data.UserData.user.ruby -= _itemData.price;

                if (_itemData.itemId == 2)
                {
                    Managers.UI.Get<UI_Shop>().PurchaseLetterItem2();
                }
                if (_itemData.itemId == 3)
                {
                    Managers.Data.UserData.user.amber += 100;
                }

                if (_itemData.itemId == 4)
                {
                    Managers.Data.UserData.user.maleCostume[1] = true;
                }

                else if(_itemData.itemId == 5)
                {
                    Managers.Data.UserData.user.femaleCostume[1] = true;
                }
            }
        }
        else if (_itemData.type == "Amber")
        {
            if (_itemData.price > Managers.Data.UserData.user.amber)
            {
                Managers.UI.ShowPopupUI<UI_NotEnoughGoods>();
            }
            else
            {
                Managers.Data.UserData.user.amber -= _itemData.price;

                if (_itemData.itemId == 1)
                {
                    Managers.UI.Get<UI_Shop>().PurchaseLetterItem1();
                }
            }
        }
        Managers.UI.Get<UI_Shop>().UpdateData();
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
    }

    public void OnCancleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }
}