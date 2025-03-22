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

        MainGameUI uiManager;

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
        }

        
        void Init()
        {
            timerObserver = new TimerObserver(TimeLimit, this);
            state = GameState.START;

            uiManager = new MainGameUI();

            uiManager.ShowTutorialPanel();

            uiManager.ChangeMouseGUI(Common.STATE_NORMAL);
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
            uiManager.ShowResultPanel();
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
            uiManager.PushTitleButton();
        }

        /// <summary>
        /// リトライボタン押下時
        /// </summary>
        public void PushRetryButton()
        {
            uiManager.PushRetryButton();
        }


    }
}