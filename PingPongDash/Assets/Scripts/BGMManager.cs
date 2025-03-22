using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // 押す・逃げる・隠れるフェーズのBGM取得
    [SerializeField] private GameObject push;
    [SerializeField] private GameObject dash;
    [SerializeField] private GameObject hide;
    private AudioSource pushAudioSource;
    private AudioSource dashAudioSource;
    private AudioSource hideAudioSource;

    // フェードイン・アウト時の速度
    [SerializeField] private float speed = 1.0f;

    // デバッグ用
    // TODO　ここの取得するステータスを変更
    public enum BGMState
    {
        PUSH,
        DASH,
        HIDE
    }

    private BGMState bgmState;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        // 各AudioSource取得
        pushAudioSource = push.GetComponent<AudioSource>();
        dashAudioSource = dash.GetComponent<AudioSource>();
        hideAudioSource = hide.GetComponent<AudioSource>();

        bgmState = BGMState.PUSH;
    }

    /// <summary>
    /// BGMをフェードインさせる処理
    /// </summary>
    private void BGMFadeIn(AudioSource _audio)
    {
        _audio.volume += speed * Time.deltaTime ;
    }

    /// <summary>
    /// BGMをフェードアウトさせる処理
    /// </summary>
    private void BGMFadeOut(AudioSource _audio)
    {
        _audio.volume -= speed * Time.deltaTime;
    }

    /// <summary>
    /// BGMを切り替える関数
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
    /// デバッグ用
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
        // 初期化処理
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO デバッグ用　本番はこの関数を呼び出さない
        DebugBGMChange();
    }
}
