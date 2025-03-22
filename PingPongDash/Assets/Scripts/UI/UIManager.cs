using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �`���[�g���A���p�l���̃X���C�h�A�j���[�V�����p
    SlideAnimation slideAnimation;
    private bool isSlide;

    // ���U���g�p�l���̃Y�[���A�j���[�V�����p
    ZoomAnimation zoomAnimation;

    // �t�F�[�h�v���t�@�u�擾�p
    FadeAnimation fadeAnimation;

    // �J�ڂ���V�[���i�[�p
    private string sceneName;

    // �e�}�E�XGUI�̃A�j���[�^�[�擾�p
    [SerializeField] private GameObject pushMouseImage;
    [SerializeField] private GameObject dashMouseImage;
    [SerializeField] private GameObject hideMouseImage;
    [SerializeField] private GameObject showMouseImage;
    private Animator pushAnimator;
    private Animator dashAnimator;
    private Animator hideAnimator;
    private Animator showAnimator;

    // �v���C���[�A�j���[�V�����擾�p
    //private PlayerAnimation pAnimation;

    /// <summary>
    /// �������֐�
    /// </summary>
    private void Init()
    {
        // �`���[�g���A���p�l���擾
        slideAnimation = GameObject.Find("TutorialPanel").GetComponent<SlideAnimation>();
        isSlide = false;

        // ���U���g�p�l���擾
        zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

        // �t�F�[�h�v���t�@�u�擾
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();

        // �v���C���[�A�j���[�V�����擾
        //pAnimation = GameObject.Find("PlayerImage").GetComponent<PlayerAnimation>();

        // �e�}�E�XGUI�̃A�j���[�^�[�擾
        pushAnimator = pushMouseImage.GetComponent<Animator>();
        dashAnimator = dashMouseImage.GetComponent<Animator>();
        hideAnimator = hideMouseImage.GetComponent<Animator>();
        showAnimator = showMouseImage.GetComponent<Animator>();

        ChangeMouseGUI(Common.STATE_PUSH);

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

    /// <summary>
    /// �}�E�XGUI�̕\����؂�ւ��鏈�� �\���p
    /// </summary>
    public void ChangeMouseGUI(int _state)
    {
        // ���݂̃X�e�[�g�ɂ���Đ؂�ւ�
        switch (_state)
        {
            case Common.STATE_PUSH:
                // �\��������́@�u�v�b�V���v�E�u�_�b�V���v�E�u�n�C�h�v
                pushAnimator.SetBool("Idle", false);
                pushAnimator.SetBool("Move", true);
                dashAnimator.SetBool("Idle", false);
                dashAnimator.SetBool("Move", true);
                hideAnimator.SetBool("Idle", false);
                hideAnimator.SetBool("Move", true);
                
                // ��\���@�u�V���[�v
                showAnimator.SetBool("Idle", true);
                showAnimator.SetBool("Move", false);
                break;

            case Common.STATE_DASH:
                // �\��������́@�u�_�b�V���v�E�u�n�C�h�v
                dashAnimator.SetBool("Idle", false);
                dashAnimator.SetBool("Move", true);
                hideAnimator.SetBool("Idle", false);
                hideAnimator.SetBool("Move", true);

                // ��\���@�u�v�b�V���v�E�u�V���[�v
                pushAnimator.SetBool("Idle", true);
                pushAnimator.SetBool("Move", false);
                showAnimator.SetBool("Idle", true);
                showAnimator.SetBool("Move", false);
                break;

            case Common.STATE_HIDE:
                // �\��������́@�u�V���[�v
                showAnimator.SetBool("Idle", false);
                showAnimator.SetBool("Move", true);

                // ��\���@�u�v�b�V���v�E�u�_�b�V���v�E�u�n�C�h�v
                pushAnimator.SetBool("Idle", true);
                pushAnimator.SetBool("Move", false);
                dashAnimator.SetBool("Idle", true);
                dashAnimator.SetBool("Move", false);
                hideAnimator.SetBool("Idle", true);
                hideAnimator.SetBool("Move", false);
                break;

            case Common.STATE_SHOW:
            case Common.STATE_NORMAL:
                // ���ׂĔ�\��
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
    }

}
