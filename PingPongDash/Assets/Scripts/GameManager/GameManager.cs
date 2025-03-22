using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kabasawa
{
    public class GameManager : MonoBehaviour, ITimerListeners
    {
        enum GameState
        {
            INIT,
            START,
            PLAY,
            TIME_UP,
            GAME_OVER,
            RESULT,
        }

        GameState state = GameState.START;

        TimerObserver timerObserver;

        /// <summary>
        /// 制限時間ミリ秒単位
        /// </summary>
        [SerializeField]
        int TimeLimit = 60 * 1000;

        void Awake()
        {
            Init();
        }


        void Update()
        {
            GameLoop();
            // シーン変更処理
            SceneChange();
        }

        
        void Init()
        {
            timerObserver = new TimerObserver(TimeLimit, this);
            state = GameState.START;

            // チュートリアルパネル取得
            slideAnimation = GameObject.Find("TutorialPanel").GetComponent<SlideAnimation>();
            isSlide = false;

            // リザルトパネル取得
            zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

            // フェードプレファブ取得
            fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();

            ShowTutorialPanel();
        }

        /// <summary>
        /// 本編の開始処理
        /// </summary>
        void GameStart()
        {

        }

        void Play()
        {
            Debug.Log("PLay中");
        }

        void TimeUp()
        {
            state = GameState.RESULT;
        }

        void GameOver()
        {

        }

        void Result()
        {
            zoomAnimation.ShowZoomIn();
        }

        void GameLoop()
        {
            switch(state)
            {
                case GameState.INIT:
                    Init();
                break;

                case GameState.START:
                    GameStart();
                break;

                case GameState.PLAY:
                    Play();
                break;

                case GameState.TIME_UP:
                    TimeUp();
                break;

                case GameState.GAME_OVER:
                    GameOver();
                break;

                case GameState.RESULT:
                    Result();
                break;
            }
        }

        public void OnTimeUp()
        {
            state = GameState.TIME_UP;
        }

        // マウスイメージ・アニメーター取得用
        //[SerializeField] private GameObject mouseImage;
        private Animator animator;
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


        /// <summary>
        /// Debug用
        /// </summary>
        public void PushStartButton()
        {
            state = GameState.PLAY;
            timerObserver.SetTimer();
        }

        /// <summary>
        /// タイトルボタン押下時
        /// </summary>
        public void PushTitleButton()
        {
            sceneName = Common.SCENE_TITLE;

            Debug.Log("Push");
        }

        /// <summary>
        /// リトライボタン押下時
        /// </summary>
        public void PushRetryButton()
        {
            sceneName = "mainGame";
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

                    case "mainGame":
                        Common.LoadScene("mainGame");
                        break;
                }
            }
        }
    }
}