using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // �����E������E�B���t�F�[�Y��BGM�擾
    [SerializeField] private GameObject push;
    [SerializeField] private GameObject dash;
    [SerializeField] private GameObject hide;
    private AudioSource pushAudioSource;
    private AudioSource dashAudioSource;
    private AudioSource hideAudioSource;

    // �t�F�[�h�C���E�A�E�g���̑��x
    [SerializeField] private float speed = 1.0f;

    // �f�o�b�O�p
    // TODO�@�����̎擾����X�e�[�^�X��ύX
    public enum BGMState
    {
        PUSH,
        DASH,
        HIDE
    }

    private BGMState bgmState;

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        // �eAudioSource�擾
        pushAudioSource = push.GetComponent<AudioSource>();
        dashAudioSource = dash.GetComponent<AudioSource>();
        hideAudioSource = hide.GetComponent<AudioSource>();

        bgmState = BGMState.PUSH;
    }

    /// <summary>
    /// BGM���t�F�[�h�C�������鏈��
    /// </summary>
    private void BGMFadeIn(AudioSource _audio)
    {
        _audio.volume += speed * Time.deltaTime ;
    }

    /// <summary>
    /// BGM���t�F�[�h�A�E�g�����鏈��
    /// </summary>
    private void BGMFadeOut(AudioSource _audio)
    {
        _audio.volume -= speed * Time.deltaTime;
    }

    /// <summary>
    /// BGM��؂�ւ���֐�
    /// </summary>
    /// <param name="_state"></param>
    public void SetBGM(BGMState _state)
    {
        switch (_state)
        {
            case BGMState.PUSH:
                BGMFadeIn(pushAudioSource);
                BGMFadeOut(dashAudioSource);
                BGMFadeOut(hideAudioSource);
                break;

            case BGMState.DASH:
                BGMFadeIn(dashAudioSource);
                BGMFadeOut(pushAudioSource);
                BGMFadeOut(hideAudioSource);
                break;

            case BGMState.HIDE:
                BGMFadeIn(hideAudioSource);
                BGMFadeOut(pushAudioSource);
                BGMFadeOut(dashAudioSource);
                break;
        }
    }

    /// <summary>
    /// �f�o�b�O�p
    /// </summary>
    private void DebugBGMChange()
    {
        if (Input.GetKey(KeyCode.B))
        {
            bgmState = BGMState.PUSH;
        }
        if (Input.GetKey(KeyCode.N))
        {
            bgmState = BGMState.DASH;
        }
        if (Input.GetKey(KeyCode.M))
        {
            bgmState = BGMState.HIDE;
        }

        SetBGM(bgmState);
    }

    private void Awake()
    {
        // ����������
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO �f�o�b�O�p�@�{�Ԃ͂��̊֐����Ăяo���Ȃ�
        DebugBGMChange();
    }
}
