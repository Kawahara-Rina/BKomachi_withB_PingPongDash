/*
    TimerManager.cs

    タイマーの管理クラス
*/
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    
    #region - タイマーを表示するText
    [Header("タイマーを表示するText")]
    #endregion
    [SerializeField] private Text text;

    #region - タイマーの初期値
    [Header("タイマーの初期値")]
    #endregion
    [SerializeField] private float timer = 0.0f;

    #region - タイマーの停止値
    [Header("タイマーの停止値")]
    #endregion
    [SerializeField] private float stopTime = 10.0f;

    #region - 1秒ごとのタイマーへの加算量
    [Header("1秒ごとのタイマーへの加算量\n例 ) 1.0で1秒に1回加算、20.0で1秒に20回加算")]
    #endregion
    [SerializeField] private float addTime = 1.0f;

    #region - カウントの種類の指定
    [Header("カウントの種類の指定\ntrue : カウントアップ\nfalse : カウントダウン")]
    #endregion
    [SerializeField] private bool isCountUp = true;

    #region - タイマーを停止するかどうか
    [Header("タイマーを停止するかどうか\ntrue : 停止\nfalse : 再開")]
    #endregion
    [SerializeField] private bool isStop = true;

    #region - 小数点位置の指定
    [Header("小数点位置の指定\nNone : 小数点以下表示なし\nFirst : 小数第1位まで表示\nSecond : 少数第2位まで表示")]
    #endregion
    [SerializeField] private uiCommon.DecimalPlace decimalPlace = uiCommon.DecimalPlace.None;

    // 小数点位置指定用
    private string format;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        // 小数点位置指定
        format = uiCommon.SetDecimalPlace(decimalPlace);

        // 初期値を表示
        text.text = timer.ToString(format);
    }

    /// <summary>
    /// タイマーをカウントアップする処理
    /// </summary>
    private void CountUpTimer()
    {
        if (timer < stopTime)
        {
            timer += Mathf.Abs(addTime) * Time.deltaTime;
        }
        else
        {
            timer = stopTime;
        }
    }

    /// <summary>
    /// タイマーをカウントダウンする処理
    /// </summary>
    private void CountDownTimer()
    {
        if (timer > stopTime)
        {
            timer -= Mathf.Abs(addTime) * Time.deltaTime;
        }
        else
        {
            timer = stopTime;
        }
    }

    /// <summary>
    /// タイマーのカウントを停止する関数
    /// タイマーの停止・再開制御をする場合は、この関数を任意のタイミングで呼び出してください。
    /// </summary>
    public void CountStop()
    {
        isStop = true;
    }

    /// <summary>
    /// タイマーのカウントを再開する関数
    /// タイマーの停止・再開制御をする場合は、この関数を任意のタイミングで呼び出してください。
    /// </summary>
    public void CountStart()
    {
        isStop = false;
    }

    /// <summary>
    /// timerを渡す関数
    /// timerの値に応じた処理をしたい場合は、この関数を利用してください。
    /// </summary>
    /// <returns></returns>
    public float GetTimer()
    {
        return timer;
    }

    private void Awake()
    {
        // 初期化処理
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            // カウントダウン・アップ処理
            if (isCountUp)
            {
                CountUpTimer();
            }
            else
            {
                CountDownTimer();
            }

            // テキスト表示
            text.text = timer.ToString(format);
        }
    }
}
