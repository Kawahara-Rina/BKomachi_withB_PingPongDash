using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    // インターホン前(戻る場所)のx座標指定用
    [SerializeField] private float defPosX = 0.0f;
    // インターホン前に戻るまでのスピード
    [SerializeField] private float backSpeed=1500.0f;

    // プレイヤーの座標取得用
    private Vector2 pos;
 
    // アニメーター取得用
    private Animator animator;

    // 走るときの追加値
    [SerializeField] private float plusDist = 10.0f;
    // 走る速さ
    [SerializeField] private float dashSpeed = 150.0f;
    // 走る距離の最大値
    private float dashEndPos;

    // デバッグ用　走る用の変数
    public int dashCount;

    public float pushSpeed;

    // デバッグ用
    // TODO　ここの取得するステータスを変更
    public enum State
    {
        IDLE,
        PUSH,
        DASH,
        HIDE,
        SHOW,
        NORMAL,
        BACK,
    }

    public State state = State.PUSH;


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        pushSpeed = 1;
        animator = GetComponent<Animator>();

        //um = GameObject.Find("UIManager").GetComponent<UIManager>();


    }

    /// <summary>
    /// ステート変更の関数
    /// </summary>
    public void ChangeState(State newState)
    {
        if(state != newState)
        {
            state = newState;
            animator.SetTrigger(StateToAnimationName(newState));
        }
    }

    // Stateからアニメーションの名前に変換する
    private string StateToAnimationName(State state)
    {
        switch (state)
        {
            case State.IDLE:
                return "Idle";
            case State.PUSH:
                return "Push";
            case State.DASH:
                return "Dash";
            case State.HIDE:
                return "Hide";
            case State.SHOW:
                return "Show";
            case State.NORMAL:
                return "Normal";
            case State.BACK:
                return "Back";
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// Show、Normalアニメーション終了後に呼び出す関数
    /// </summary>
    private void SetStateBack()
    {
        state = State.BACK;

        // TODO 移動していない場合はBACKアニメーションは再生しない
        pos = transform.localPosition;

        if(pos.x == defPosX)
        {
            ChangeState(State.PUSH);
        }
        else
        {
            animator.SetTrigger("Back");
        }

    }

    /// <summary>
    /// 隠れた後 or 走った後→→インターホン前に戻る動き
    /// </summary>
    private void BackMove()
    {
        if (state == State.BACK)
        {
            // 座標取得
            pos = transform.localPosition;
            pos.x -= backSpeed * Time.deltaTime;


                // インターホン前まで動かす
                if (pos.x > defPosX)
                {
                    transform.localPosition = new Vector2(pos.x, pos.y);
                }
                else
                {
                    transform.localPosition = new Vector2(defPosX, pos.y);
                    ChangeState(State.PUSH);
                }
            

            // 走る距離初期化
            dashEndPos = defPosX;
        }
    }

    /// <summary>
    /// 走る動き
    /// </summary>
    private void DashMove()
    {
        if (state == State.DASH)
        {
            // カウントの変更を取得する用
            var tmpCount = dashCount;

            // 座標取得
            pos = transform.localPosition;
            pos.x += dashSpeed * Time.deltaTime;

            // デバッグ用
            // カウントが増えたら進む場所を遠くする
            if (Input.GetMouseButtonDown(0))
            {
                dashCount++;
            }

            // TODO dashCountを本番に使う用の変数に修正する
            if(tmpCount != dashCount)
            {
                dashEndPos += plusDist;
            }

            // 進む場所に(最大距離まで)進んでいく
            if (pos.x < dashEndPos)
            {
                transform.localPosition = new Vector2(pos.x, pos.y);
            }
            else
            {
                transform.localPosition = new Vector2(dashEndPos, pos.y);
            }
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
        if(state == State.PUSH)
        {
            animator.speed = pushSpeed;
        }
        else
        {
            animator.speed = 1;
        }

        // 走っている時の動き
        DashMove();

        // 隠れた後 or 走った後→→インターホン前に戻る動き
        BackMove();

        Debug.Log(state);
    }

}
