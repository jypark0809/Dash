using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTest : MonoBehaviour
{
    [SerializeField]
    Toggle toggle1;

    private void Awake()
    {
        toggle1.onValueChanged.AddListener(OnToggleValueChangedEvent);
    }

    public void OnToggleValueChangedEvent(bool boolean)
    {
        transform.gameObject.SetActive(boolean);
    }
}
