/*
    ZoomAnimation.cs

    ズームイン・アウトアニメーションクラス
*/
using UnityEngine;

public class ZoomAnimation : MonoBehaviour
{
    // 定数
    private const float ADD_SPEED = 10.0f;

    // フェードインかアウトかを指定するための列挙体
    public
        enum ZoomType
    {
        ZoomIn,
        ZoomOut
    };

    #region - ズームイン・アウトの指定
    [Header("ズームイン・アウトの指定")]
    [Tooltip("動的に制御したい場合は、任意のタイミングでこのフラグを切り替えてください。")]
    #endregion
    [SerializeField] public ZoomType zoomType = ZoomType.ZoomIn;

    #region - 最初からアニメーションをするかどうか
    [Header("最初からアニメーションをするかどうか(true:する,false:しない)")]
    [Tooltip("最初から表示しない場合は、任意のタイミングでこのフラグをtrueにしてください。")]
    #endregion
    [SerializeField] public bool isShow = true;

    #region - スケールの最大値
    [Header("スケールの最大値(拡大時)")]
    [Tooltip("0.0～1.0の値。0が小さい")]
    [Range(uiCommon.MIN_SCALE, uiCommon.MAX_SCALE)]
    #endregion
    [SerializeField] private float maxScale = 1.0f;

    #region - スケールの最小値
    [Header("スケールの最小値(縮小時)")]
    [Tooltip("0.0～1.0の値。0が小さい")]
    [Range(uiCommon.MIN_SCALE, uiCommon.MAX_SCALE)]
    #endregion
    [SerializeField] private float minScale = 0;

    #region - アニメーションの速度
    [Header("アニメーションの速度")]
    [Tooltip("0.1～4.0の値。0が遅い")]
    [Range(uiCommon.MIN_SAMPLES, uiCommon.MAX_SAMPLES)]
    #endregion
    [SerializeField] private float samples = 1.0f;

    // 初期スケールの設定を一度だけ行うためのフラグ
    private bool isFirst;

    // スケール取得用
    private float scale;

    /// <summary>
    /// ズームインを行うタイミングで呼び出す関数
    /// </summary>
    public void ShowZoomIn()
    {
        zoomType = ZoomType.ZoomIn;
        isShow = true;
    }

    /// <summary>
    /// ズームアウトを行うタイミングで呼び出す関数
    /// </summary>
    public void ShowZoomOut()
    {
        zoomType = ZoomType.ZoomOut;
        isShow = true;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        isFirst = true;
    }

    private void Awake()
    {
        // 初期化処理
        Init();
    }

    /// <summary>
    /// 使用用途に応じて初期のスケールを設定する関数
    /// </summary>
    private void SetScale()
    {
        if (isFirst)
        {
            if (zoomType == ZoomType.ZoomIn)
            {
                // ズームイン→小から大に変化するため、スケールはminScale
                scale = minScale;
            }
            else
            {
                // ズームアウト→大から小に変化するため、スケールはmaxScale
                scale = maxScale;
            }

            isFirst = false;
        }
    }

    /// <summary>
    /// ズームイン・アウトの処理
    /// </summary>
    private void Zoom()
    {
        // ズームイン
        if (zoomType == ZoomType.ZoomIn)
        {
            if (scale < maxScale)
            {
                scale+= Time.deltaTime * samples * ADD_SPEED;
            }
            else
            {
                scale = maxScale;
            }

        }
        // ズームアウト
        else
        {
            if (scale > minScale)
            {
                scale -= Time.deltaTime * samples * ADD_SPEED;
            }
            else
            {
                scale = minScale;
            }

        }

        transform.localScale = new Vector3(scale,scale,scale);
    }

    // Update is called once per frame
    void Update()
    {
        // 演出開始でズームイン・アウト
        if (isShow)
        {
            // 初期のスケールを設定
            SetScale();

            // ズーム処理
            Zoom();
        }
    }
}
