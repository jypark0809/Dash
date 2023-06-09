using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Define.ObjectType ObjectType { get; protected set; } = Define.ObjectType.Unknown;

    void Awake()
    {
        Init();
    }

    public virtual void Init() { }
}
