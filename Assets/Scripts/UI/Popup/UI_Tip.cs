using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Tip : UI_Popup
{
    int _curPage = 1;

    enum Buttons
    {
        CloseButton,
        RightButton,
        LeftButton,
    }

    enum Texts
    {
        PageText,
    }

    enum Images
    {
        ItemPage,
        ObstaclePage,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);
        GetButton((int)Buttons.RightButton).gameObject.BindEvent(RightButtonClicked);
        GetButton((int)Buttons.LeftButton).gameObject.BindEvent(LeftButtonClicked);
        GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
        GetImage((int)Images.ObstaclePage).gameObject.SetActive(false);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    public void RightButtonClicked(PointerEventData data)
    {
        if (_curPage == 1)
        {
            _curPage++;
            GetText((int)Texts.PageText).text = _curPage.ToString() + " / 2";
            GetButton((int)Buttons.RightButton).gameObject.SetActive(false);
            GetButton((int)Buttons.LeftButton).gameObject.SetActive(true);
            GetImage((int)Images.ItemPage).gameObject.SetActive(false);
            GetImage((int)Images.ObstaclePage).gameObject.SetActive(true);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
    }

    public void LeftButtonClicked(PointerEventData data)
    {
        if (_curPage == 2)
        {
            _curPage--;
            GetText((int)Texts.PageText).text = _curPage.ToString() + " / 2";
            GetButton((int)Buttons.RightButton).gameObject.SetActive(true);
            GetImage((int)Images.ItemPage).gameObject.SetActive(true);
            GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
            GetImage((int)Images.ObstaclePage).gameObject.SetActive(false);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
    }
}
