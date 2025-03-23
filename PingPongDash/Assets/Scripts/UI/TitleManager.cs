using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // �t�F�[�h�v���t�@�u�擾�p
    FadeAnimation fadeAnimation;

    // �V�[������J�ڊi�[�p
    private string sceneName;

    // SE�擾�p
    private AudioSource audioSource;
    [SerializeField] private AudioClip doorSE;
    [SerializeField] private AudioClip templeSE;

    /// <summary>
    /// �������֐�
    /// </summary>
    private void Init()
    {
        // �t�F�[�h�v���t�@�u�擾
        fadeAnimation = GameObject.Find("FadePrefab").GetComponent<FadeAnimation>();

        // �I�[�f�B�I�\�[�X�擾
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// �X�e�[�W1�{�^��������
    /// </summary>
    public void PushStage1Button()
    {
        // SE�炷
        Common.PlaySE(audioSource,doorSE);
        sceneName = Common.SCENE_STAGE1;
    }

    /// <summary>
    /// �X�e�[�W2�{�^��������
    /// </summary>
    public void PushStage2Button()
    {
        // SE�炷
        Common.PlaySE(audioSource,templeSE);
        sceneName = Common.SCENE_STAGE2;
    }

    /// <summary>
    /// �V�[����؂�ւ��鏈��
    /// </summary>
    private void SceneChange()
    {
        if (fadeAnimation.GetAlpha() >= 1.0f)
        {
            // TODO �J�ڂ���V�[���̕ύX
            switch (sceneName)
            {
                case Common.SCENE_STAGE1:
                    Common.LoadScene(Common.SCENE_GAME);
                    break;

                case Common.SCENE_STAGE2:
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


    // Update is called once per frame
    void Update()
    {
        // �V�[���؂�ւ�����
        SceneChange();
    }
}
