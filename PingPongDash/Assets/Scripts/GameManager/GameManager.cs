using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        PlayerAnimation player;

        [SerializeField]
        BGMManager bgmManager;

        [SerializeField]
        Enemy enemy;

        MainGameManager mainGameManager;

        [SerializeField]
        string stageName = "stage01";

        [SerializeField]
        Text scoreText;

        [SerializeField]
        Text timeText;

        /// <summary>
        /// 制限時間ミリ秒単位
        /// </summary>
        [SerializeField]
        int TimeLimit = 60 * 1000;

        void Start()
        {
            Init();
        }


        void Update()
        {
            GameLoop();
            timeText.text = ((float)timerObserver.RemainingTime() / 1000f).ToString();
            scoreText.text = mainGameManager.score.ToString();
        }

        
        void Init()
        {
            timerObserver = new TimerObserver(TimeLimit, this);
            state = GameState.START;

            uiManager = new MainGameUI();

            uiManager.ShowTutorialPanel();

            uiManager.ChangeMouseGUI(Common.STATE_NORMAL);

            mainGameManager = new MainGameManager(player,bgmManager,uiManager, enemy);
        }

        /// <summary>
        /// 本編の開始処理
        /// </summary>
        void GameStart()
        {
            state = GameState.PLAY;
            timerObserver.SetTimer();
            uiManager.ChangeMouseGUI(Common.STATE_PUSH);

            mainGameManager.isActive = true;
            enemy.gameObject.SetActive(true);
        }

        void Play()
        {
            mainGameManager.Loop();
            if(mainGameManager.isGameOver)
            {
                state = GameState.GAME_OVER;
            }
        }

        void TimeUp()
        {
            state = GameState.RESULT;

            mainGameManager.isActive = false;
        }

        void GameOver()
        {
            state = GameState.RESULT;

            mainGameManager.isActive = false;
        }

        void Result()
        {
            ScoreRanking.Save(new ScoreRankingConfig(stageName, 1), mainGameManager.score);
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
            state = GameState.START;
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