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
