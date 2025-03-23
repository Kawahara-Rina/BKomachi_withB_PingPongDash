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

    private PlayerAnimation pa;

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

        pa = GameObject.Find("PlayerImage").GetComponent<PlayerAnimation>();
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
    /// TODO ステートの変更
    /// </summary>
    /// <param name="_state"></param>
    public void SetBGM(PlayerAnimation.State _state)
    {
        switch (_state)
        {
            case PlayerAnimation.State.PUSH:
            case PlayerAnimation.State.SHOW:
            case PlayerAnimation.State.BACK:
            case PlayerAnimation.State.NORMAL:
                BGMFadeIn(pushAudioSource);
                BGMFadeOut(dashAudioSource);
                BGMFadeOut(hideAudioSource);
                break;

            case PlayerAnimation.State.DASH:
                BGMFadeIn(dashAudioSource);
                BGMFadeOut(pushAudioSource);
                BGMFadeOut(hideAudioSource);
                break;

            case PlayerAnimation.State.HIDE:
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

        SetBGM(pa.state);
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
        //DebugBGMChange();
        SetBGM(pa.state);

    }
}
