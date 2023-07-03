using System.Collections;
using UnityEngine;

public class Teacher : ObstacleController
{
    public float _range = 15f;
    public int _speed = 5;
    Coroutine _coTeacherEvent;

    public override void Init()
    {
        
    }

    void Update()
    {
        if (transform.position.x - Managers.Object.Player.transform.position.x < _range)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();

        if (target == null)
            return;

        if (_coTeacherEvent != null)
            StopCoroutine(CoTriggerTeacherEvent());

        StartCoroutine(CoTriggerTeacherEvent());
    }

    IEnumerator CoTriggerTeacherEvent()
    {
        UI_Teacher ui = Managers.UI.ShowPopupUI<UI_Teacher>();
        ui.SetInfo(this);

        while (ui != null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.5f);

        gameObject.layer = 7;
        Managers.Object.Player.Sprite.color = new Color(1, 1, 1, 1);
        _coTeacherEvent = null;
    }
}
