using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ConfirmAdReward : UI_Popup
{
    enum Buttons
    {
        OkayButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButtonClicked);
    }

    public void OnOkayButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.Get<UI_Goal>().disableAdReward();
        ClosePopupUI();
    }
}
