using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Target : UI_Popup
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
        FemaleNpc,
        MaleNpc,
        TipImage,
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
        GetImage((int)Images.MaleNpc).gameObject.SetActive(false);
        GetImage((int)Images.TipImage).gameObject.SetActive(false);
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
            GetText((int)Texts.PageText).text = _curPage.ToString() + " / 3";
            GetButton((int)Buttons.LeftButton).gameObject.SetActive(true);
            GetImage((int)Images.FemaleNpc).gameObject.SetActive(false);
            GetImage((int)Images.MaleNpc).gameObject.SetActive(true);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
        else if (_curPage == 2)
        {
            _curPage++;
            GetText((int)Texts.PageText).text = _curPage.ToString() + " / 3";
            GetButton((int)Buttons.RightButton).gameObject.SetActive(false);
            GetImage((int)Images.MaleNpc).gameObject.SetActive(false);
            GetImage((int)Images.TipImage).gameObject.SetActive(true);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
    }

    public void LeftButtonClicked(PointerEventData data)
    {
        if (_curPage == 3)
        {
            _curPage--;
            GetText((int)Texts.PageText).text = _curPage.ToString() + " / 3";
            GetButton((int)Buttons.RightButton).gameObject.SetActive(true);
            GetImage((int)Images.FemaleNpc).gameObject.SetActive(false);
            GetImage((int)Images.MaleNpc).gameObject.SetActive(true);
            GetImage((int)Images.TipImage).gameObject.SetActive(false);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
        else if (_curPage == 2)
        {
            _curPage--;
            GetText((int)Texts.PageText).text = _curPage.ToString() + " / 3";
            GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
            GetImage((int)Images.FemaleNpc).gameObject.SetActive(true);
            GetImage((int)Images.MaleNpc).gameObject.SetActive(false);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
    }
}
