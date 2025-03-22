using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // チュートリアルパネルのスライドアニメーション用
    SlideAnimation slideAnimation;
    private bool isSlide;

    // リザルトパネルのズームアニメーション用
    ZoomAnimation zoomAnimation;

    // フェードプレファブ取得用
    FadeAnimation fadeAnimation;

    // 遷移するシーン格納用
    private string sceneName;

    // 各マウスGUIのアニメーター取得用
    [SerializeField] private GameObject pushMouseImage;
    [SerializeField] private GameObject dashMouseImage;
    [SerializeField] private GameObject hideMouseImage;
    [SerializeField] private GameObject showMouseImage;
    private Animator pushAnimator;
    private Animator dashAnimator;
    private Animator hideAnimator;
    private Animator showAnimator;

    // プレイヤーアニメーション取得用
    //private PlayerAnimation pAnimation;

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Init()
    {
        // チュートリアルパネル取得
        slideAnimation = GameObject.Find("TutorialPanel").GetComponent<SlideAnimation>();
        isSlide = false;

        // リザルトパネル取得
        zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

        // フェードプレファブ取得
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();

        // プレイヤーアニメーション取得
        //pAnimation = GameObject.Find("PlayerImage").GetComponent<PlayerAnimation>();

        // 各マウスGUIのアニメーター取得
        pushAnimator = pushMouseImage.GetComponent<Animator>();
        dashAnimator = dashMouseImage.GetComponent<Animator>();
        hideAnimator = hideMouseImage.GetComponent<Animator>();
        showAnimator = showMouseImage.GetComponent<Animator>();

        ChangeMouseGUI(Common.STATE_PUSH);

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

    /// <summary>
    /// マウスGUIの表示を切り替える処理 表示用
    /// </summary>
    public void ChangeMouseGUI(int _state)
    {
        // 現在のステートによって切り替え
        switch (_state)
        {
            case Common.STATE_PUSH:
                // 表示するもの　「プッシュ」・「ダッシュ」・「ハイド」
                pushAnimator.SetBool("Idle", false);
                pushAnimator.SetBool("Move", true);
                dashAnimator.SetBool("Idle", false);
                dashAnimator.SetBool("Move", true);
                hideAnimator.SetBool("Idle", false);
                hideAnimator.SetBool("Move", true);
                
                // 非表示　「ショー」
                showAnimator.SetBool("Idle", true);
                showAnimator.SetBool("Move", false);
                break;

            case Common.STATE_DASH:
                // 表示するもの　「ダッシュ」・「ハイド」
                dashAnimator.SetBool("Idle", false);
                dashAnimator.SetBool("Move", true);
                hideAnimator.SetBool("Idle", false);
                hideAnimator.SetBool("Move", true);

                // 非表示　「プッシュ」・「ショー」
                pushAnimator.SetBool("Idle", true);
                pushAnimator.SetBool("Move", false);
                showAnimator.SetBool("Idle", true);
                showAnimator.SetBool("Move", false);
                break;

            case Common.STATE_HIDE:
                // 表示するもの　「ショー」
                showAnimator.SetBool("Idle", false);
                showAnimator.SetBool("Move", true);

                // 非表示　「プッシュ」・「ダッシュ」・「ハイド」
                pushAnimator.SetBool("Idle", true);
                pushAnimator.SetBool("Move", false);
                dashAnimator.SetBool("Idle", true);
                dashAnimator.SetBool("Move", false);
                hideAnimator.SetBool("Idle", true);
                hideAnimator.SetBool("Move", false);
                break;

            case Common.STATE_SHOW:
            case Common.STATE_NORMAL:
                // すべて非表示
                pushAnimator.SetBool("Idle", true);
                pushAnimator.SetBool("Move", false);
                dashAnimator.SetBool("Idle", true);
                dashAnimator.SetBool("Move", false);
                hideAnimator.SetBool("Idle", true);
                hideAnimator.SetBool("Move", false);
                showAnimator.SetBool("Idle", true);
                showAnimator.SetBool("Move", false);
                break;
        }
    }

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
    }

}
