using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // エネミーが待機する時間
    [SerializeField] private float waitTime;

    private Animator animator;

    // デバッグ用
    // TODO　ここの取得するステータスを変更
    public enum EnemyState
    {
        NONE,
        SHOW,
        IDLE,
        OUT
    }

    public EnemyState state = EnemyState.NONE;

    private void Init()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        Init();
    }

    private void Debug()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // TODO　ステートを変える場所を変更する
            state = EnemyState.SHOW;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            state = EnemyState.OUT;
        }
    }

    private void ChangeAnimation()
    {
        switch (state)
        {
            case EnemyState.SHOW:
                animator.SetBool("Show", true);
                animator.SetBool("Out", false);
                break;

            case EnemyState.OUT:
                animator.SetBool("Out", true);
                animator.SetBool("Show", false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug();

        ChangeAnimation();
    }
}
