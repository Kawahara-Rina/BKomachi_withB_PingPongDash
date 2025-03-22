/*
    SlideAnimation.cs

    スライドイン・アウトアニメーションクラス
*/
using UnityEngine;
using System;

public class SlideAnimation : MonoBehaviour
{
    // 定数
    private const float ADD_SPEED = 5000.0f;

    // スライドの方向を指定するための列挙体
    public enum FlowType
    {
        UpToBottom,
        BottomToUp,
        RightToLeft,
        LeftToRight
    };

    // スライドインかアウトを指定するための列挙体
    public enum SlideType
    {
        SlideIn,
        SlideOut
    };

    #region - スライド方向の指定用
    [Header("スライド方向の指定")]
    #endregion
    [SerializeField] private FlowType flowType = FlowType.UpToBottom;

    #region - スライドアウト時の方向指定
    [Header("スライドアウト方向の指定\ntrue ：逆方向にスライドアウト(スライドインの開始位置に戻る動き)\nfalse：一方通行にスライドアウト")]
    #endregion
    [SerializeField] private bool isReverse = false;

    #region - スライド"イン"時の開始位置(ローカルポジション)
    [Header("スライド”イン”時の開始位置")]
    #endregion
    [SerializeField] private Vector2 inStartPos;

    #region - スライド"イン"時の停止位置(ローカルポジション)
    [Header("スライド”イン”時の停止位置")]
    #endregion
    [SerializeField] private Vector2 inStopPos;

    #region - アニメーションの速度
    [Header("アニメーションの速度")]
    [Tooltip("0.1～4.0の値。0が遅い")]
    [Range(uiCommon.MIN_SAMPLES, uiCommon.MAX_SAMPLES)]
    #endregion
    [SerializeField] private float samples = 2.0f;

    // 現在の位置取得用
    private Vector2 pos;

    // スライドイン・アウトどちらかの指定用
    private SlideType slideType;

    // アニメーションを再生するかどうか
    private bool isShow = false;

    // 初めのスライドインの方向を変えないためのフラグ
    private bool isFirst = true;

    private FlowType reverseFlowType;
    private FlowType beforeFlowType;


    /// <summary>
    /// スライドインしたいときに呼び出す関数
    /// </summary>
    public void SlideIn()
    {
        slideType = SlideType.SlideIn;
        transform.localPosition = new Vector2(inStartPos.x, inStartPos.y);

        // スライドイン時の流れを再設定
        if (isReverse)
        {
            flowType = beforeFlowType;
        }

        isShow = true;
    }

    /// <summary>
    /// スライドアウトしたいときに呼び出す関数
    /// </summary>
    public void SlideOut()
    {
        slideType = SlideType.SlideOut;
        transform.localPosition = new Vector2(inStopPos.x, inStopPos.y);

        // スライドアウトが逆方向の場合
        if (isReverse)
        {
            // 流れを再設定
            flowType = reverseFlowType;
        }

        isShow = true;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        // 初期位置設定
        transform.localPosition = new Vector2(inStartPos.x,inStartPos.y);

        // 逆方向にスライドアウトする場合
        if (isReverse) {
            switch (flowType)
            {
                case FlowType.BottomToUp:
                    reverseFlowType = FlowType.UpToBottom;
                    break;

                case FlowType.UpToBottom:
                    reverseFlowType = FlowType.BottomToUp;
                    break;

                case FlowType.RightToLeft:
                    reverseFlowType = FlowType.LeftToRight;
                    break;

                case FlowType.LeftToRight:
                    reverseFlowType = FlowType.RightToLeft;
                    break;
            }

            // スライドイン時の方向を保存しておく
            beforeFlowType = flowType;
        }
    }

    /// <summary>
    /// 移動処理(スライド)部分
    /// </summary>
    private void Slide()
    {
        // 現在の座標を取得
        pos = transform.localPosition;

        // スライドの方向に応じて移動処理を変更
        switch (flowType)
        {
            case FlowType.UpToBottom:
                #region 上から下
                // スライドイン時の移動処理
                if (slideType == SlideType.SlideIn)
                {
                    if (pos.y > inStopPos.y)
                    {
                        pos.y -= Time.deltaTime * samples * ADD_SPEED;
                    }
                    else
                    {
                        // 位置の補正処理
                        pos.y = inStopPos.y;
                    }
                }
                // スライドアウト時の移動処理
                else
                {
                    if (isReverse)
                    {
                        if (pos.y > inStartPos.y)
                        {
                            pos.y -= Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.y = inStartPos.y;
                        }
                    }
                    else
                    {
                        if (pos.y > -inStartPos.y)
                        {
                            pos.y -= Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.y = -inStartPos.y;
                        }
                    }
                    
                }
                break;
            #endregion

            case FlowType.BottomToUp:
                #region 下から上
                // スライドイン時の移動処理
                if (slideType == SlideType.SlideIn)
                {
                    if (pos.y < inStopPos.y)
                    {
                        pos.y += Time.deltaTime * samples * ADD_SPEED;
                    }
                    else
                    {
                        // 位置の補正処理
                        pos.y = inStopPos.y;
                    }
                }
                // スライドアウト時の移動処理
                else
                {
                    if (isReverse)
                    {
                        if (pos.y < inStartPos.y)
                        {
                            pos.y += Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.y = inStartPos.y;
                        }
                    }
                    else
                    {
                        if (pos.y < -inStartPos.y)
                        {
                            pos.y += Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.y = -inStartPos.y;
                        }
                    }
                }
                break;
            #endregion

            case FlowType.RightToLeft:
                #region 右から左
                // スライドイン時の移動処理
                if (slideType == SlideType.SlideIn)
                {
                    if (pos.x > inStopPos.x)
                    {
                        pos.x -= Time.deltaTime * samples * ADD_SPEED;
                    }
                    else
                    {
                        // 位置の補正処理
                        pos.x = inStopPos.x;
                    }
                }
                // スライドアウト時の移動処理
                else
                {
                    if (isReverse)
                    {
                        if (pos.x > inStartPos.x)
                        {
                            pos.x -= Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.x = inStartPos.x;
                        }
                    }
                    else
                    {
                        if (pos.x > -inStartPos.x)
                        {
                            pos.x -= Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.x = -inStartPos.x;
                        }
                    }
                }
                break;
            #endregion

            case FlowType.LeftToRight:
                #region 左から右
                // スライドイン時の移動処理
                if (slideType == SlideType.SlideIn)
                {
                    if (pos.x < inStopPos.x)
                    {
                        pos.x += Time.deltaTime * samples * ADD_SPEED;
                    }
                    else
                    {
                        // 位置の補正処理
                        pos.x = inStopPos.x;
                    }
                }
                // スライドアウト時の移動処理
                else
                {
                    if (isReverse)
                    {
                        if (pos.x < inStartPos.x)
                        {
                            pos.x += Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.x = inStartPos.x;
                        }
                    }
                    else
                    {
                        if (pos.x < -inStartPos.x)
                        {
                            pos.x += Time.deltaTime * samples * ADD_SPEED;
                        }
                        else
                        {
                            pos.x = -inStartPos.x;
                        }
                    }
                }
                break;
                #endregion
        }

        // 現在の座標を変更
        transform.localPosition = new Vector2(pos.x, pos.y);
    }

    private void Awake()
    {
        // 初期化処理
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // アニメーション再生時
        if (isShow)
        {
            // 移動処理実行
            Slide();
        }
    }
}
