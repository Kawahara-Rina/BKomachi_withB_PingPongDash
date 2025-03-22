using UnityEngine;
using UnityEngine.SceneManagement;

public class Common : MonoBehaviour
{
    // 定数

    // シーン名
    public const string SCENE_TITLE = "Title";
    public const string SCENE_GAME = "UIScene";
    public const string SCENE_STAGE1 = "Stage1";
    public const string SCENE_STAGE2 = "Stage2";

    // ステート
    public const int STATE_PUSH = 1;
    public const int STATE_DASH = 2;
    public const int STATE_HIDE = 3;
    public const int STATE_SHOW = 4;
    public const int STATE_NORMAL = 5;

    /// <summary>
    /// シーンを呼び出す汎用関数
    /// </summary>
    static public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
