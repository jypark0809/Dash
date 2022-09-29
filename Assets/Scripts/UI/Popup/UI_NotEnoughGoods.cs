using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NotEnoughGoods : UI_Popup
{
    [SerializeField]
    Sprite[] _iconImages;

    Item _itemData;

    enum Images
    {
        GoodsIcon,
    }

    enum Buttons
    {
        OkayButton,
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
        Bind<Image>(typeof(Images));

        // Set Price Icon Image
        if (_itemData.type == "Amber")
            GetImage((int)Images.GoodsIcon).sprite = _iconImages[0];
        else if (_itemData.type == "Ruby")
            GetImage((int)Images.GoodsIcon).sprite = _iconImages[1];


        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButtonClicked);
    }

    public void OnOkayButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }
}
