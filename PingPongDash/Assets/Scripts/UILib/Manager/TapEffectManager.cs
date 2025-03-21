/*
    TapEffectManager.cs

    タップエフェクト管理クラス
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapEffectManager : MonoBehaviour
{

    #region - タップエフェクトを表示するキャンバス
    [Header("タップエフェクトを表示するキャンバス")]
    #endregion
    [SerializeField] Canvas canvas;

    [Header("-タップエフェクト-------------------------")]

    #region - タップエフェクトに使用する画像を格納する配列
    [Header("使用する画像(アニメーション順)")]
    [Tooltip("2～5枚？")]
    #endregion
    [SerializeField] public Sprite[] tapEffectSprites = new Sprite[5];

    #region - タップエフェクトのアニメーションの速度
    [Header("アニメーションの速度")]
    [Tooltip("0.1～4.0の値。4が遅い、0.1が早い")]
    [Range( uiCommon.MAX_SAMPLES, uiCommon.MIN_SAMPLES)]
    #endregion
    [SerializeField] public float tapEffectSamples = 0.4f;


    [Header("-ロングタップエフェクト-------------------")]
    
    #region - ロングタップエフェクトに使用する画像を格納する配列
    [Header("使用する画像(アニメーション順)")]
    [Tooltip("2～5枚？")]
    #endregion
    [SerializeField] public Sprite[] longTapEffectSprites = new Sprite[5];

    #region - ロングタップエフェクトのアニメーションの速度
    [Header("アニメーションの速度")]
    [Tooltip("0.1～4.0の値。4が遅い、0.1が早い")]
    [Range(uiCommon.MAX_SAMPLES, uiCommon.MIN_SAMPLES)]
    #endregion
    [SerializeField] public float longTapEffectSamples = 1.0f;

    #region - ロングタップエフェクトの生成速度
    [Header("エフェクト生成の速度")]
    [Tooltip("0.1～4.0の値。4が遅い、0.1が早い")]
    [Range(uiCommon.MAX_SAMPLES, uiCommon.MIN_SAMPLES)]
    #endregion
    [SerializeField] public float generateSamples = 0.1f;

    // パッケージ内のプレファブ取得用
    private GameObject tapEffectPrefab;

    // キャンバス内のレクトトランスフォーム取得用
    private RectTransform canvasRect;
    // ポインター(マウス)の位置
    private Vector2 mPos;

    // あらかじめオブジェクトを生成するためのリスト
    private List<GameObject> tapObjList = new List<GameObject>();
    private List<GameObject> longTapObjList = new List<GameObject>();
    // リストの添え字を指定するためのカウント
    private int tapIndex;
    private int longTapIndex;

    // ロングタップエフェクト表示までのタイマー
    private float showTimer;

    /// <summary>
    /// 初期化処理 
    /// </summary>
    private void Init()
    {
        // パッケージ内のタップエフェクトプレファブを取得
        tapEffectPrefab =Resources.Load<GameObject>
            ("UILib/Prefabs/TapEffectPrefab");

        // キャンバスのレクトトランスフォーム取得
        canvasRect = canvas.GetComponent<RectTransform>();

        // タップエフェクトに使用するオブジェクトを生成し、リストに登録
        for (int i = 0; i < uiCommon.GENERATE_COUNT; i++)
        {
            // ロングタップエフェクト用
            var tmpLong = Instantiate(tapEffectPrefab);
            tmpLong.gameObject.transform.SetParent(canvas.transform, false);
            tmpLong.GetComponent<Image>().sprite = longTapEffectSprites[0];
            longTapObjList.Add(tmpLong);

            // タップエフェクト用
            var tmp = Instantiate(tapEffectPrefab);
            tmp.gameObject.transform.SetParent(canvas.transform, false);
            tmp.GetComponent<Image>().sprite = tapEffectSprites[0];
            tapObjList.Add(tmp);
        }
    }

    /// <summary>
    /// タップ・ロングタップエフェクトの表示を行う関数
    /// </summary>
    /// <param name="_list">タップ・ロングタップ用のオブジェクトが格納されているリスト</param>
    /// <param name="_index">リスト内の添え字を指定する用</param>
    /// <param name="_sprites">タップ・ロングタップに使用するスプライトが格納されている配列</param>
    /// <param name="_samples">アニメーションの速度</param>
    private void ShowEffect(List<GameObject> _list , int _index , Sprite[] _sprites, float _samples)
    {
        // タップした場所を取得
        // キャンバスのレクトトランスフォーム内にあるマウスの位置をローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (canvasRect,
            Input.mousePosition,
            canvas.worldCamera,
            out mPos);

        // リストの中のオブジェクトを表示
        _list[_index].gameObject.transform.localPosition = new Vector2(mPos.x, mPos.y);
        _list[_index].gameObject.SetActive(true);

        // EffectAnimationコルーチンを開始
        StartCoroutine(_list[_index].GetComponent<TapEffectAnimation>().EffectAnimation(
            _sprites,
            _samples * uiCommon.MIN_SAMPLES));
    }

    /// <summary>
    // タップエフェクトの表示を行う関数
    /// </summary>
    private void TapEffect()
    {
        // タップエフェクトを表示
        ShowEffect(tapObjList, tapIndex, tapEffectSprites, tapEffectSamples);

        // 添え字をカウント
        if (tapIndex < uiCommon.GENERATE_COUNT - 1)
        {
            tapIndex++;
        }
        else
        {
            tapIndex = 0;
        }
    }
    
    /// <summary>
    // ロングタップエフェクトの表示を行う関数
    /// </summary>
    private void LongTapEffect()
    {
        showTimer += Time.deltaTime;

        // 次の生成までのタイマーが経過したら
        if (showTimer >= generateSamples * 0.1f)
        {
            // タップした座標にロングタップエフェクトを表示
            ShowEffect(longTapObjList, longTapIndex, longTapEffectSprites, longTapEffectSamples);

            // 添え字をカウント
            if (longTapIndex < uiCommon.GENERATE_COUNT - 1)
            {
                longTapIndex++;
            }
            else
            {
                longTapIndex = 0;
            }

            showTimer = 0;

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
        if (Input.GetMouseButtonDown(0))
        {
            // タップした座標にタップエフェクトを表示
            TapEffect();
        }
        if (Input.GetMouseButton(0))
        {
            // ロングタップエフェクトを表示
            LongTapEffect();
        }
    }
}

