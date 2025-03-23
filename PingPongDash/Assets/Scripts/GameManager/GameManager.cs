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
        /// �������ԃ~���b�P��
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
            // �V�[���ύX����
            SceneChange();
        }

        
        void Init()
        {
            timerObserver = new TimerObserver(TimeLimit, this);
            state = GameState.START;

            // �`���[�g���A���p�l���擾
            slideAnimation = GameObject.Find("TutorialPanel").GetComponent<SlideAnimation>();
            isSlide = false;

            // ���U���g�p�l���擾
            zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

            // �t�F�[�h�v���t�@�u�擾
            fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();

            ShowTutorialPanel();
        }

        /// <summary>
        /// �{�҂̊J�n����
        /// </summary>
        void GameStart()
        {

        }

        void Play()
        {
            Debug.Log("PLay��");
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

        // �}�E�X�C���[�W�E�A�j���[�^�[�擾�p
        //[SerializeField] private GameObject mouseImage;
        private Animator animator;
        //private bool isAnimationChange;

        // �`���[�g���A���p�l���̃X���C�h�A�j���[�V�����p
        SlideAnimation slideAnimation;
        private bool isSlide;

        // ���U���g�p�l���̃Y�[���A�j���[�V�����p
        ZoomAnimation zoomAnimation;

        // �t�F�[�h�v���t�@�u�擾�p
        FadeAnimation fadeAnimation;

        // �J�ڂ���V�[���i�[�p
        private string sceneName;


        /// <summary>
        /// Debug�p
        /// </summary>
        public void PushStartButton()
        {
            state = GameState.PLAY;
            timerObserver.SetTimer();
        }

        /// <summary>
        /// �^�C�g���{�^��������
        /// </summary>
        public void PushTitleButton()
        {
            sceneName = Common.SCENE_TITLE;

            Debug.Log("Push");
        }

        /// <summary>
        /// ���g���C�{�^��������
        /// </summary>
        public void PushRetryButton()
        {
            sceneName = "mainGame";
        }

        /// <summary>
        /// �`���[�g���A���p�l�����X���C�h�C��
        /// </summary>
        private void ShowTutorialPanel()
        {
            // �t�F�[�h�C�����I����Ă���X���C�h�C��
            if (fadeAnimation.GetAlpha() <= 0 && !isSlide)
            {
                slideAnimation.SlideIn();
                isSlide = true;
            }
        }

        

        /// <summary>
        /// �V�[����؂�ւ��鏈��
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