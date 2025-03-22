using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // マウスイメージ・アニメーター取得用
    //[SerializeField] private GameObject mouseImage;
    //private Animator animator;
    //private bool isAnimationChange;

    // チュートリアルパネルのスライドアニメーション用
    SlideAnimation slideAnimation;
    private bool isSlide;

    // リザルトパネルのズームアニメーション用
    ZoomAnimation zoomAnimation;

    // フェードプレファブ取得用
    FadeAnimation fadeAnimation;

    // 遷移するシーン格納用
    private string sceneName;
    
    // デバッグ用
    private enum STATE
    {
        IDLE,
        PUSH,
        DASH,
        HIDE
    }

    private STATE state;

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Init()
    {
        // マウスイメージのアニメーター取得
        //animator = mouseImage.GetComponent<Animator>();
        //isAnimationChange = false;

        // チュートリアルパネル取得
        slideAnimation = GameObject.Find("TutorialPanel").GetComponent<SlideAnimation>();
        isSlide = false;

        // リザルトパネル取得
        zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

        // フェードプレファブ取得
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();


        // デバッグ用
        state = STATE.IDLE;
    }

    /// <summary>
    /// タイトルボタン押下時
    /// </summary>
    public void PushTitleButton()
    {
        sceneName = Common.SCENE_TITLE;
    }

    /// <summary>
    /// リトライボタン押下時
    /// </summary>
    public void PushRetryButton()
    {
        sceneName = Common.SCENE_GAME;
    }

    /// <summary>
    /// チュートリアルパネルをスライドイン
    /// </summary>
    private void ShowTutorialPanel()
    {
        // フェードインが終わってからスライドイン
        if (fadeAnimation.GetAlpha() <= 0 && !isSlide)
        {
            slideAnimation.SlideIn();
            isSlide = true;
        }
    }

    /// <summary>
    /// リザルトパネルをズームイン
    /// </summary>
    private void ShowResultPanel()
    {
        // TODO リザルトを出す条件の変更
        // タイムアップ？大家さんに見つかったとき？
        if (Input.GetKey(KeyCode.A))
        {
            zoomAnimation.ShowZoomIn();
        }
    }

    /// <summary>
    /// シーンを切り替える処理
    /// </summary>
    private void SceneChange()
    {
        if (fadeAnimation.GetAlpha() >= 1.0f)
        {
            switch (sceneName)
            {
                case Common.SCENE_TITLE:
                    Common.LoadScene(Common.SCENE_TITLE);
                    break;

                case Common.SCENE_GAME:
                    Common.LoadScene(Common.SCENE_GAME);
                    break;
            }
        }
    }

    /*
    /// <summary>
    /// 操作(マウス)GUIのアニメーション切り替え処理
    /// </summary>
    private void MouseAnimationChange()
    {
        // デバッグ用
        if (Input.GetKey(KeyCode.Z))
        {
            state = STATE.PUSH;
            isAnimationChange = true;
        }
        if (Input.GetKey(KeyCode.X))
        {
            state = STATE.PUSH;
            isAnimationChange = true;
        }
        if (Input.GetKey(KeyCode.C))
        {
            state = STATE.PUSH;
            isAnimationChange = true;
        }

        if (isAnimationChange)
        {
            switch (state)
            {
                case STATE.IDLE:
                    break;

                case STATE.PUSH:
                    animator.SetBool("Push", true);
                    break;
            }
        }

        isAnimationChange = false;
    }
    */

    private void Awake()
    {
        // 初期化処理
        Init();
    }

    private void Update()
    {
        // チュートリアルパネルスライドイン
        ShowTutorialPanel();

        // リザルトパネルズームイン
        ShowResultPanel();

        // シーン変更処理
        SceneChange();

        // マウスアニメーション変更処理
        //MouseAnimationChange();
    }

}
