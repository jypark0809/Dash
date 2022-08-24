using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    enum Images
    {
        MainImage,
        TextImage,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        GetImage((int)Images.MainImage).gameObject.BindEvent(OnImageClicked);
    }

    public void OnImageClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
        else
        {
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        }
    }
}
