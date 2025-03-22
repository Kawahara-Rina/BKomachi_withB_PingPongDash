using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    // �C���^�[�z���O(�߂�ꏊ)��x���W�w��p
    [SerializeField] private float defPosX = 0.0f;
    // �C���^�[�z���O�ɖ߂�܂ł̃X�s�[�h
    [SerializeField] private float speed=1.0f;

    // �v���C���[�̍��W�擾�p
    private Vector2 pos;
 

    // �A�j���[�^�[�擾�p
    private Animator animator;

    // �X�e�[�g���ς�������ǂ���
    private bool isStateChange;

    // �f�o�b�O�p
    // TODO�@�����̎擾����X�e�[�^�X��ύX
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
    // back�̎��̓���
    // 

    private State state = State.DASH;

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        animator = GetComponent<Animator>();
        isStateChange = false;
    }

    /// <summary>
    /// �A�j���[�V�����ύX����
    /// </summary>
    private void AnimationChange()
    {
        // �f�o�b�O�p
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
    /// �B�ꂽ�� or �������と���C���^�[�z���O�ɖ߂铮��
    /// </summary>
    private void BackMove()
    {
        if (state == State.BACK)
        {
            // ���W�擾
            pos = transform.localPosition;
            pos.x -= speed * Time.deltaTime;

            // �C���^�[�z���O�܂œ�����
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
        // ����������
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // �A�j���[�V�����ύX����
        AnimationChange();

        BackMove();
    }
}
