using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Test : UI_Popup
{
    enum Buttons
    {
        SaveButton,
        InitButton,
        CancleButton,
    }

    enum Texts
    {
        StageText,
        GenderText,
        StatText1,
        StatText2,
        StatText3,
        AmberText,
        RubyText,
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

        GetButton((int)Buttons.SaveButton).gameObject.BindEvent(SaveButtonClicked);
        GetButton((int)Buttons.InitButton).gameObject.BindEvent(InitButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
    }

    public void SaveButtonClicked(PointerEventData data)
    {
        Managers.Game.SaveData.stage = int.Parse(GetText((int)Texts.StageText).text);
        Managers.Game.SaveData.gender = GetText((int)Texts.GenderText).text;
        Managers.Game.SaveData.stat1 = int.Parse(GetText((int)Texts.StatText1).text);
        Managers.Game.SaveData.stat2 = int.Parse(GetText((int)Texts.StatText2).text);
        Managers.Game.SaveData.stat3 = int.Parse(GetText((int)Texts.StatText3).text);
        Managers.Game.SaveData.amber = int.Parse(GetText((int)Texts.AmberText).text);
        Managers.Game.SaveData.ruby = int.Parse(GetText((int)Texts.RubyText).text);
        Managers.Game.SaveGame();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    public void InitButtonClicked(PointerEventData data)
    {
        Managers.Game.InitData();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    public void CancleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }
}
