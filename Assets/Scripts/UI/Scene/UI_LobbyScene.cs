using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    enum Buttons
    {
        TipButton,
        TargetButton,
        ShopButton,
        CollectionButton,
        OptionButton,
        PlayButton,
        InitButton,
    }

    enum Texts
    {
        AmberText,
        RubyText,
    }

    enum GameObjects
    {
        GoodsGroup,
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
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.TipButton).gameObject.BindEvent(TipButtonClicked);
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OptionButtonClicked);
        GetButton((int)Buttons.TargetButton).gameObject.BindEvent(TargetButtonClicked);
        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(ShopButtonClicked);
        GetButton((int)Buttons.CollectionButton).gameObject.BindEvent(CollectionButtonClicked);
        GetButton((int)Buttons.PlayButton).gameObject.BindEvent(PlayButtonClicked);
        GetButton((int)Buttons.InitButton).gameObject.BindEvent(InitButtonClicked);

        GetText((int)Texts.AmberText).text = Managers.Game.SaveData.amber.ToString();
        GetText((int)Texts.RubyText).text = Managers.Game.SaveData.ruby.ToString();
    }

    public void TipButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Tip>();
    }

    public void OptionButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Option>();
    }

    public void TargetButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Target>();
    }

    public void ShopButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Shop>();
        SetActiveGoodsUI(false);
    }

    public void CollectionButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Collection>();
    }

    public void PlayButtonClicked(PointerEventData data)
    {
        if (Managers.Game.SaveData.stat1 + Managers.Game.SaveData.stat2 + Managers.Game.SaveData.stat3 < 9)
        {
            if (Managers.Game.SaveData.gender == "unselected")
                Managers.UI.ShowPopupUI<UI_SelectGender>();
            else
            {
                Managers.Scene.LoadScene(Define.Scene.Game);
            }

            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
        else
        {
            Managers.Scene.LoadScene(Define.Scene.Ending);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
    }

    public void InitButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Test>();
    }

    public void SetActiveGoodsUI(bool boolean)
    {
        GetObject((int)GameObjects.GoodsGroup).SetActive(boolean);
        if (boolean)
        {
            GetText((int)Texts.AmberText).text = Managers.Game.SaveData.amber.ToString();
            GetText((int)Texts.RubyText).text = Managers.Game.SaveData.ruby.ToString();
        }
    }
}
