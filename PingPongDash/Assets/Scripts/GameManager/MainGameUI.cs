using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUI
{
    // ���U���g�p�l���̃Y�[���A�j���[�V�����p
    ZoomAnimation zoomAnimation;

    // �t�F�[�h�v���t�@�u�擾�p
    FadeAnimation fadeAnimation;


    // �e�}�E�XGUI�̃A�j���[�^�[�擾�p
    GameObject pushMouseImage;
    GameObject dashMouseImage;
    GameObject hideMouseImage;
    GameObject showMouseImage;

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
        // ���U���g�p�l���擾
        zoomAnimation = GameObject.Find("ResultPanel").GetComponent<ZoomAnimation>();

        // �t�F�[�h�v���t�@�u�擾
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();


        pushMouseImage = GameObject.Find("PushMouseImage");
        dashMouseImage = GameObject.Find("DashMouseImage");
        hideMouseImage = GameObject.Find("HideMouseImage");
        showMouseImage = GameObject.Find("ShowMouseImage");

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
        SceneChange(Common.SCENE_TITLE);
    }

    /// <summary>
    /// ���g���C�{�^��������
    /// </summary>
    public void PushRetryButton()
    {
        SceneChange(Common.SCENE_GAME);
    }

    /// <summary>
    /// ���U���g�p�l�����Y�[���C��
    /// </summary>
    public void ShowResultPanel()
    {
        zoomAnimation.ShowZoomIn();
    }

    /// <summary>
    /// �V�[����؂�ւ��鏈��
    /// </summary>
    private void SceneChange(string _sceneName)
    {
        if (fadeAnimation.GetAlpha() >= 1.0f)
        {
            Common.LoadScene(_sceneName);
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

    public MainGameUI()
    {
        // ����������
        Init();
    }

}
