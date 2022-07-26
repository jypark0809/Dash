using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Teacher : UI_Popup
{
    float _timer;
    public float _reduce; // n�� ���� ������ ����
    public int _maxPos;
    public int _minPos;
    int _curPos; // ���� Position
    PlayerController _playerController;

    public Sprite[] _sprites;

    enum Buttons
    {
        RightTabButton,
        LeftTabButton,
    }

    enum Images
    {
        GaugeBar,
        PinImage,
        CutScene,
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

        if (Managers.Data.UserData.user.gender == "male")
            GetImage((int)Images.CutScene).sprite = _sprites[0];
        if (Managers.Data.UserData.user.gender == "female")
            GetImage((int)Images.CutScene).sprite = _sprites[1];
    }

    void Update()
    {
        if (_curPos == _maxPos)
        {
            _playerController.recovereDamage();
            ClosePopupUI();
        }

        if (_curPos == _minPos)
        {
            _playerController._state = Define.PlayerState.Die;
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
        if (PlayerPrefs.GetInt("vibrate") == 1)
            Vibration.Vibrate((long)50);

        _curPos++;
    }

    public void LeftTabButtonClicked(PointerEventData data)
    {
        if (PlayerPrefs.GetInt("vibrate") == 1)
            Vibration.Vibrate((long)50);

        _curPos++;
    }
}
