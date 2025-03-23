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

        string sceneName;

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


        [SerializeField]
        Text resultText;

        /// <summary>
        /// 制限時間ミリ秒単位
        /// </summary>
        [SerializeField]
        int TimeLimit = 60 * 1000;

        AudioSource audioSource;

        [SerializeField]
        AudioClip audioClip;

        void Start()
        {
            Init();
        }


        void Update()
        {
            GameLoop();
            timeText.text = ((float)timerObserver.RemainingTime() / 1000f).ToString();
            scoreText.text = mainGameManager.score.ToString();

            SceneChange();
        }

        
        void Init()
        {
            audioSource = GetComponent<AudioSource>();

            timerObserver = new TimerObserver(TimeLimit, this);
            state = GameState.START;

            uiManager = new MainGameUI();

            uiManager.ChangeMouseGUI(Common.STATE_NORMAL);

            mainGameManager = new MainGameManager(player,bgmManager,uiManager, enemy, audioSource,audioClip);

            state = GameState.START;
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

            resultText.text = "Time UP!!\n";
            if(mainGameManager.score > ScoreRanking.LoadRanking(new ScoreRankingConfig(stageName, 1))[0])
            {
                resultText.text += "ハイスコア更新!!\n";
            }
            resultText.text += "Score : " + mainGameManager.score;
        }

        void GameOver()
        {
            state = GameState.RESULT;

            mainGameManager.isActive = false;

            resultText.text = "Gane Over...\n"; 
            if (mainGameManager.score > ScoreRanking.LoadRanking(new ScoreRankingConfig(stageName, 1))[0])
            {
                resultText.text += "ハイスコア更新!!\n";
            }
            resultText.text += "Score : " + mainGameManager.score;
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
        /// タイトルボタン押下時
        /// </summary>
        public void PushTitleButton()
        {
            sceneName = Common.SCENE_TITLE;
        }

        /// <summary>
        /// リトライボタン押下時
        /// </summary>
        public void PushRetryStage1Button()
        {
            sceneName = Common.SCENE_STAGE1;
        }

        /// <summary>
        /// リトライボタン押下時
        /// </summary>
        public void PushRetryStage2Button()
        {
            sceneName = Common.SCENE_STAGE2;
        }

        /// <summary>
        /// シーンを切り替える処理
        /// </summary>
        private void SceneChange()
        {
            if (uiManager.fadeAnimation.GetAlpha() >= 1.0f)
            {
                switch (sceneName)
                {
                    case Common.SCENE_STAGE1:
                        Common.LoadScene(Common.SCENE_STAGE1);
                        break;

                    case Common.SCENE_STAGE2:
                        Common.LoadScene(Common.SCENE_STAGE2);
                        break;

                    case Common.SCENE_TITLE:
                        Common.LoadScene(Common.SCENE_TITLE);
                        break;
                }
            }
        }


    }
}