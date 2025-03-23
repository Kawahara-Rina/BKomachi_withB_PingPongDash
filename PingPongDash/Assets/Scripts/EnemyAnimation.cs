using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // �G�l�~�[���ҋ@���鎞��
    [SerializeField] private float waitTime;

    private Animator animator;

    // �f�o�b�O�p
    // TODO�@�����̎擾����X�e�[�^�X��ύX
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
            // TODO�@�X�e�[�g��ς���ꏊ��ύX����
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
