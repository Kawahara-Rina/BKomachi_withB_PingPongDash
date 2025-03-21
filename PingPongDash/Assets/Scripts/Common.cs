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

    /// <summary>
    /// シーンを呼び出す汎用関数
    /// </summary>
    static public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
