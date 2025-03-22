using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    // インターホン前(戻る場所)のx座標指定用
    [SerializeField] private float defPosX = 0.0f;
    // インターホン前に戻るまでのスピード
    [SerializeField] private float speed=1.0f;

    // プレイヤーの座標取得用
    private Vector2 pos;
 

    // アニメーター取得用
    private Animator animator;

    // ステートが変わったかどうか
    private bool isStateChange;

    // デバッグ用
    // TODO　ここの取得するステータスを変更
    public enum State
    {
        PUSH,
        DASH,
        HIDE,
        SHOW,
        NORMAL,
        BACK,
    }

    // TODO
    // backの時の動き
    // 

    private State state = State.DASH;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        animator = GetComponent<Animator>();
        isStateChange = false;
    }

    /// <summary>
    /// アニメーション変更処理
    /// </summary>
    private void AnimationChange()
    {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.Q))
        {
            state = State.PUSH;
            isStateChange = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            state = State.DASH;
            isStateChange = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            state = State.HIDE;
            isStateChange = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            state = State.SHOW;
            isStateChange = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            state = State.NORMAL;
            isStateChange = true;
        }

        if (isStateChange)
        {
            switch (state)
            {
                case State.PUSH:
                    animator.SetTrigger("Idle");
                    break;

                case State.DASH:
                    animator.SetTrigger("Dash");
                    break;

                case State.HIDE:
                    animator.SetTrigger("Hide");
                    break;

                case State.SHOW:
                    animator.SetTrigger("Show");
                    break;

                case State.NORMAL:
                    animator.SetTrigger("Normal");
                    break;
            }
        }

        isStateChange = false;
    }

    private void SetStateBack()
    {
        state = State.BACK;
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
            pos.x -= speed * Time.deltaTime;

            // インターホン前まで動かす
            if (pos.x > defPosX)
            {
                transform.localPosition = new Vector2(pos.x, pos.y);
            }
            else
            {
                transform.localPosition = new Vector2(defPosX, pos.y);
                state = State.PUSH;
                isStateChange = true;
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
        // アニメーション変更処理
        AnimationChange();

        BackMove();
    }
}
