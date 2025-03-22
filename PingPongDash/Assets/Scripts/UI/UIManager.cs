using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �}�E�X�C���[�W�E�A�j���[�^�[�擾�p
    //[SerializeField] private GameObject mouseImage;
    //private Animator animator;
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
    
    // �f�o�b�O�p
    private enum STATE
    {
        IDLE,
        PUSH,
        DASH,
        HIDE
    }

    private STATE state;

    /// <summary>
    /// �������֐�
    /// </summary>
    private void Init()
    {
        // �}�E�X�C���[�W�̃A�j���[�^�[�擾
        //animator = mouseImage.GetComponent<Animator>();
        //isAnimationChange = false;

        // �`���[�g���A���p�l���擾
        slideAnimation = GameObject.Find("TutorialPanel").GetComponent<SlideAnimation>();
        isSlide = false;

        // ���U���g�p�l���擾
        zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

        // �t�F�[�h�v���t�@�u�擾
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();


        // �f�o�b�O�p
        state = STATE.IDLE;
    }

    /// <summary>
    /// �^�C�g���{�^��������
    /// </summary>
    public void PushTitleButton()
    {
        sceneName = Common.SCENE_TITLE;
    }

    /// <summary>
    /// ���g���C�{�^��������
    /// </summary>
    public void PushRetryButton()
    {
        sceneName = Common.SCENE_GAME;
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
    /// ���U���g�p�l�����Y�[���C��
    /// </summary>
    private void ShowResultPanel()
    {
        // TODO ���U���g���o�������̕ύX
        // �^�C���A�b�v�H��Ƃ���Ɍ��������Ƃ��H
        if (Input.GetKey(KeyCode.A))
        {
            zoomAnimation.ShowZoomIn();
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

                case Common.SCENE_GAME:
                    Common.LoadScene(Common.SCENE_GAME);
                    break;
            }
        }
    }

    /*
    /// <summary>
    /// ����(�}�E�X)GUI�̃A�j���[�V�����؂�ւ�����
    /// </summary>
    private void MouseAnimationChange()
    {
        // �f�o�b�O�p
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
        // ����������
        Init();
    }

    private void Update()
    {
        // �`���[�g���A���p�l���X���C�h�C��
        ShowTutorialPanel();

        // ���U���g�p�l���Y�[���C��
        ShowResultPanel();

        // �V�[���ύX����
        SceneChange();

        // �}�E�X�A�j���[�V�����ύX����
        //MouseAnimationChange();
    }

}
