using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // フェードプレファブ取得用
    FadeAnimation fadeAnimation;

    // シーンする遷移格納用
    private string sceneName;

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Init()
    {
        // フェードプレファブ取得
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();

    }

    /// <summary>
    /// ステージ1ボタン押下時
    /// </summary>
    public void PushStage1Button()
    {
        sceneName = Common.SCENE_STAGE1;
    }

    /// <summary>
    /// ステージ2ボタン押下時
    /// </summary>
    public void PushStage2Button()
    {
        sceneName = Common.SCENE_STAGE2;
    }

    /// <summary>
    /// シーンを切り替える処理
    /// </summary>
    private void SceneChange()
    {
        if (fadeAnimation.GetAlpha() >= 1.0f)
        {
            // TODO 遷移するシーンの変更
            switch (sceneName)
            {
                case Common.SCENE_STAGE1:
                    Common.LoadScene(Common.SCENE_GAME);
                    break;

                case Common.SCENE_STAGE2:
                    Common.LoadScene(Common.SCENE_GAME);
                    break;
            }
        }
    }

    private void Awake()
    {
        // 初期化処理
        Init();
    }


    // Update is called once per frame
    void Update()
    {
        // シーン切り替え処理
        SceneChange();
    }
}
