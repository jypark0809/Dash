using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Prologue : UI_Popup
{
    GameScene _gameScene;

    enum Buttons
    {
        CloseButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);

        _gameScene = (GameScene)Managers.Scene.CurrentScene;
        _gameScene.StopGame();
    }

    public void OnCloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
        _gameScene.RestartGame();
        PlayerPrefs.SetInt("Tutorial", 0);
        Managers.UI.ShowPopupUI<UI_Hint>();
    }
}
