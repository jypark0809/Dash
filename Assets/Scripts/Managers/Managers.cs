using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers Instance { get { Init(); return instance; } }

    GameManagerEx _game = new GameManagerEx();
    public static GameManagerEx Game { get { return Instance._game; } }

    #region
    // InputManager _input = new InputManager();
    DataManager _data = new DataManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    // public static InputManager Input { get { return Instance._input; } }
    public static DataManager Data { get { return Instance._data; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        // _input.OnUpdate();
    }

    static void Init()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            instance._data.Init();
            instance._sound.Init();
        }
    }

    public static void Clear()
    {
        // Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
    }
}
