using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Teacher : UI_Popup
{
    float _timer;
    public float _reduce; // n초 마다 게이지 감소
    public int _maxPos;
    public int _minPos;
    int _curPos; // 현재 Position
    PlayerController _playerController;

    enum Buttons
    {
        RightTabButton,
        LeftTabButton,
    }

    enum Images
    {
        GaugeBar,
        PinImage,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        _playerController = Managers.Game._player.GetComponent<PlayerController>();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.RightTabButton).gameObject.BindEvent(RightTabButtonClicked);
        GetButton((int)Buttons.LeftTabButton).gameObject.BindEvent(LeftTabButtonClicked);
    }

    void Update()
    {
        if (_curPos == _minPos || _curPos == _maxPos)
        {
            _playerController._isFight = false;
            ClosePopupUI();
        }
            
        _timer += Time.deltaTime;
        if (_timer > _reduce)
        {
            _curPos--;
            _timer = 0;
        }

        int distance = _maxPos - _minPos;
        int curDistance = _curPos - _minPos;
        float ratio = (float)curDistance / distance;
        GetImage((int)Images.GaugeBar).rectTransform.sizeDelta = new Vector2(566 * ratio, 73);
        GetImage((int)Images.PinImage).rectTransform.anchoredPosition = new Vector2((float)566/distance * _curPos, 0);
    }

    public void RightTabButtonClicked(PointerEventData data)
    {
        _curPos++;
    }

    public void LeftTabButtonClicked(PointerEventData data)
    {
        _curPos++;
    }
}
