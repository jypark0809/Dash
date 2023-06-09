using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Teacher : UI_Popup
{
    float _timer;
    public float _reduce;
    public int _maxPos;
    public int _minPos;
    int _curPos;
    PlayerController _player;
    Teacher _teacher;

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

        _player = Managers.Object.Player;
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.RightTabButton).gameObject.BindEvent(OnTouchButtonClicked);
        GetButton((int)Buttons.LeftTabButton).gameObject.BindEvent(OnTouchButtonClicked);

        if (Managers.Game.SaveData.gender == "male")
            GetImage((int)Images.CutScene).sprite = _sprites[0];
        if (Managers.Game.SaveData.gender == "female")
            GetImage((int)Images.CutScene).sprite = _sprites[1];

        Time.timeScale = 0;
    }

    public void SetInfo(Teacher teacher)
    {
        _teacher = teacher;
    }

    void Update()
    {
        if (_curPos == _maxPos)
        {
            _player.OnDamaged(_teacher, 0);
            ClosePopupUI();
            Time.timeScale = 1;
        }

        if (_curPos == _minPos)
        {
            Managers.Object.Player.State = Define.PlayerState.Die;
            ClosePopupUI();
            Time.timeScale = 1;
        }

        _timer += Time.unscaledDeltaTime;
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

    void OnTouchButtonClicked(PointerEventData evt)
    {
        if (PlayerPrefs.GetInt("vibrate") == 1)
            Vibration.Vibrate((long)50);

        _curPos++;
    }
}
