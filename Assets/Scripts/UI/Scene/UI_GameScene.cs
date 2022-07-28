using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    public PlayerController player;
    enum Buttons { JumpButton, PauseButton }
    enum Images { Letter1, Letter2, Letter3 }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        GetButton((int)Buttons.JumpButton).gameObject.BindEvent(JumpButtonClicked);
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(PauseButtonClicked);

        Managers.Game.healthUI[0] = GetImage((int)Images.Letter1);
        Image image2 = GetImage((int)Images.Letter2);
        Image image3 = GetImage((int)Images.Letter3);
        Managers.Game.healthUI[1] = image2;
        Managers.Game.healthUI[2] = image3;
    }

    public void JumpButtonClicked(PointerEventData data)
    {
        player._isJump = true;
        player._state = PlayerController.PlayerState.Jump;
        if (player._jumpCount > 0)
        {
            player._rigid.velocity = Vector2.up * player._jumpPower;
            player._jumpCount--;
        }
    }

    public void PauseButtonClicked(PointerEventData data)
    {
        Time.timeScale = 0;

        // TODO : UI
    }
}
