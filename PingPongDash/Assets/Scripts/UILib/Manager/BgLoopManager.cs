/*
    BgLoopManager.cs

    背景のループ表示管理クラス
*/
using UnityEngine;

public class BgLoop : MonoBehaviour
{
    // 定数
    private const float ADD_SPEED = 500.0f;

    // スクロールの方向
    private enum Direction {
        Up,
        Down,
        Right,
        Left
    }

    #region - スクロールの方向
    [Header("スクロールの方向")]
    #endregion
    [SerializeField] private Direction direction = Direction.Left;

    #region - 使用するImage①
    [Header("使用するImage①\n※画像1,2はスクロール方向に合わせて配置してください")]
    [Tooltip("Canvasに設置してあるImageを格納してください")]
    #endregion
    [SerializeField] private GameObject image1;

    #region - 使用するImage②
    [Header("使用するImage②\n※画像1,2はスクロール方向に合わせて配置してください")]
    [Tooltip("Canvasに設置してあるImageを格納してください")]
    #endregion
    [SerializeField] private GameObject image2;

    #region - アニメーションの速度
    [Header("アニメーションの速度")]
    [Tooltip("0.1～4.0の値。0が遅い")]
    [Range(uiCommon.MIN_SAMPLES, uiCommon.MAX_SAMPLES)]
    #endregion
    [SerializeField] private float samples = 1.0f;

    #region - スクロールを停止するか
    [Header("スクロールを停止するか\ntrue : 停止する、false : 停止しない")]
    [Tooltip("動的に制御したい場合は、ScrollStart(),ScrollStop()を呼び出してください。")]
    #endregion
    [SerializeField] private bool isStop = false;

    // 背景画像の横幅・縦幅
    private float width;
    private float height;

    // 初期状態の座標取得用
    private Vector3 startPosition1;
    private Vector3 startPosition2;

    // 座標更新用
    private Vector3 pos1;
    private Vector3 pos2;

    /// <summary>
    /// スクロールを停止する関数
    /// </summary>
    public void ScrollStop()
    {
        isStop = true;
    }

    /// <summary>
    /// スクロールを開始する関数
    /// </summary>
    public void ScrollStart()
    {
        isStop = false;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        // 初期位置取得
        startPosition1 = image1.transform.localPosition;
        startPosition2 = image2.transform.localPosition;

        // 画像の幅・高さ取得
        var rect = image1.GetComponent<RectTransform>().rect;
        width = rect.width;
        height = rect.height;
    }

    /// <summary>
    /// 指定した方向に向けてスクロールする関数
    /// </summary>
    private void Scroll()
    {
        // 各方向にスクロール
        switch (direction)
        {
            case Direction.Up:
                #region 上にスクロール
                pos1.y = image1.transform.localPosition.y + Time.deltaTime * samples  * ADD_SPEED;
                pos2.y = image2.transform.localPosition.y + Time.deltaTime * samples  * ADD_SPEED;

                image1.transform.localPosition = pos1;
                image2.transform.localPosition = pos2;

                // 背景が画面外に出たら再配置
                if (image1.transform.localPosition.y >= height)
                {
                    image1.transform.localPosition = new Vector3(startPosition1.x, image2.transform.localPosition.y - height, startPosition1.z);
                }
                if (image2.transform.localPosition.y >= height)
                {
                    image2.transform.localPosition = new Vector3(startPosition2.x, image1.transform.localPosition.y - height, startPosition2.z);
                }
                break;
            #endregion

            case Direction.Down:
                #region 下にスクロール
                pos1.y = image1.transform.localPosition.y - Time.deltaTime * samples  * ADD_SPEED;
                pos2.y = image2.transform.localPosition.y - Time.deltaTime * samples  * ADD_SPEED;

                image1.transform.localPosition = pos1;
                image2.transform.localPosition = pos2;

                // 背景が画面外に出たら再配置
                if (image1.transform.localPosition.y <= -height)
                {
                    image1.transform.localPosition = new Vector3(startPosition1.x, image2.transform.localPosition.x + height, startPosition1.z);
                }
                if (image2.transform.localPosition.y <= -height)
                {
                    image2.transform.localPosition = new Vector3(startPosition2.x, image1.transform.localPosition.x + height, startPosition2.z);
                }
                break;
            #endregion

            case Direction.Right:
                #region 右にスクロール
                pos1.x = image1.transform.localPosition.x + Time.deltaTime * samples  * ADD_SPEED;
                pos2.x = image2.transform.localPosition.x + Time.deltaTime * samples  * ADD_SPEED;

                image1.transform.localPosition = pos1;
                image2.transform.localPosition = pos2;

                // 背景が画面外に出たら再配置
                if (image1.transform.localPosition.x >= width)
                {
                    image1.transform.localPosition = new Vector3(image2.transform.localPosition.x - width, startPosition1.y, startPosition1.z);
                }
                if (image2.transform.localPosition.x >= width)
                {
                    image2.transform.localPosition = new Vector3(image1.transform.localPosition.x - width, startPosition2.y, startPosition2.z);
                }
                break;
            #endregion

            case Direction.Left:
                #region 左にスクロール
                // 背景を左にスクロール
                pos1.x = image1.transform.localPosition.x - Time.deltaTime * samples  * ADD_SPEED;
                pos2.x = image2.transform.localPosition.x - Time.deltaTime * samples  * ADD_SPEED;

                image1.transform.localPosition = pos1;
                image2.transform.localPosition = pos2;

                // 背景が画面外に出たら再配置
                if (image1.transform.localPosition.x <= -width)
                {
                    image1.transform.localPosition = new Vector3(image2.transform.localPosition.x + width, startPosition1.y, startPosition1.z);
                }
                if (image2.transform.localPosition.x <= -width)
                {
                    image2.transform.localPosition = new Vector3(image1.transform.localPosition.x + width, startPosition2.y, startPosition2.z);
                }
                break;
                #endregion
        }

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

            // 各方向にスクロール
            Scroll();
        }
    }
}
