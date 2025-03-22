/*
    uiCommon.cs

    複数のスクリプトから使用する定数や汎用関数などをまとめたクラス
*/
using UnityEngine;
using UnityEngine.UI;

public class uiCommon
{
    // 共通定数
    // スケールの最大値・最小値
    public const float MAX_SCALE = 1.0f;
    public const float MIN_SCALE = 0.0f;

    // 透明度の最大値・最小値
    public const float MAX_ALPHA = 1.0f;
    public const float MIN_ALPHA = 0.0f;

    // 線の太さの最大値・最小値
    public const float MAX_THICKNESS = 10.0f;
    public const float MIN_THICKNESS =  0.1f;

    // 再生速度の最大値・最小値
    public const float MAX_SAMPLES = 4.0f;
    public const float MIN_SAMPLES = 0.1f;

    // タップエフェクト関連
    public const int GENERATE_COUNT = 120;   // プレファブ初期生成時の個数
    public const float GENERATE_POS = 100;   // プレファブ初期生成時の座標

    // 小数点以下の桁数指定用
    public enum DecimalPlace
    {
        None,
        First,
        Second
    };

    /// <summary>
    /// 小数点位置を返す関数
    /// </summary>
    /// <returns></returns>
    public static string SetDecimalPlace(DecimalPlace _place)
    {
        var format = "";

        // 小数点位置指定
        switch (_place)
        {
            // 小数点なし
            case DecimalPlace.None:
                format = "F0";
                break;

            // 小数点第1位まで表示
            case DecimalPlace.First:
                format = "F1";
                break;

            // 小数点第2位まで表示
            case DecimalPlace.Second:
                format = "F2";
                break;
        }

        return format;
    }

    /// <summary>
    /// オブジェクトをアクティブ・非アクティブにする関数
    /// </summary>
    /// <param name="_gameObject">アクティブ・非アクティブにするオブジェクト</param>
    /// <param name="_bool">アクティブか非アクティブかのブール値</param>
    public static void SetActive(GameObject _gameObject,bool _bool)
    {
        _gameObject.SetActive(_bool);
    }

    /// <summary>
    /// テキストを表示する関数
    /// </summary>
    /// <param name="_msg"> 表示したい内容</param>
    /// <param name="_text">使用するテキスト</param>
    private void ShowText(string _msg, Text _text)
    {
        // 指定したテキストを表示
        _text.text = _msg;
    }
}
