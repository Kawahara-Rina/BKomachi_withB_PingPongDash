/*
    ScoreManager.cs

    スコアの管理クラス
*/
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region - スコアを表示するText
    [Header("スコアを表示するText")]
    #endregion
    [SerializeField] private Text text;

    #region - スコアの初期値
    [Header("スコアの初期値")]
    #endregion
    [SerializeField] private float score = 0.0f;

    #region - スコアの上限値
    [Header("スコアの上限値")]
    #endregion
    [SerializeField] private float maxScore = 100.0f;

    #region - スコアの下限値
    [Header("スコアの下限値")]
    #endregion
    [SerializeField] private float minScore = 0.0f;

    #region - 1回ごとのスコアへの加算量
    [Header("1回ごとのスコアへの加算量")]
    #endregion
    [SerializeField] private float addScore = 10.0f;

    #region - 1回ごとのスコアへの減算量
    [Header("1回ごとのスコアへの減算量")]
    #endregion
    [SerializeField] private float subScore = 10.0f;

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
    }

    /// <summary>
    /// スコアを加算する処理
    /// 任意のタイミングでこの関数を呼び出して使用してください。
    /// </summary>
    public void CountUpScore()
    {
        if (score < maxScore)
        {
            score += addScore;
        }
        else
        {
            score = maxScore;
        }
    }

    /// <summary>
    /// スコアを減算する処理
    /// 任意のタイミングでこの関数を呼び出して使用してください。
    /// </summary>
    public void CountDownScore()
    {
        if (score > minScore)
        {
            score -=Mathf.Abs(subScore);
        }
        else
        {
            score = minScore;
        }
    }

    /// <summary>
    /// スコアを返す処理
    /// scoreの値に応じた処理をしたい場合は、この関数を利用してください。
    /// </summary>
    /// <returns></returns>
    public float GetScore()
    {
        return score;
    }

    private void Awake()
    {
        // 初期化処理
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // テキスト表示
        text.text = score.ToString(format);
    }
}
