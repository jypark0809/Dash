using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    float time;

    enum Images
    {
        MainImage,
        TextImage,
    }

    void Start()
    {
        Init();
        StartCoroutine(BlinkImage());
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

    IEnumerator BlinkImage()
    {
        while (true)
        {
            GetImage((int)Images.TextImage).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GetImage((int)Images.TextImage).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
