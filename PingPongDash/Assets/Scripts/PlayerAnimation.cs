using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    // �C���^�[�z���O(�߂�ꏊ)��x���W�w��p
    [SerializeField] private float defPosX = 0.0f;
    // �C���^�[�z���O�ɖ߂�܂ł̃X�s�[�h
    [SerializeField] private float backSpeed=1500.0f;

    // �v���C���[�̍��W�擾�p
    private Vector2 pos;
 
    // �A�j���[�^�[�擾�p
    private Animator animator;

    // �X�e�[�g���ς�������ǂ���
    private bool isStateChange;

    // ����Ƃ��̒ǉ��l
    [SerializeField] private float plusDist = 10.0f;
    // ���鑬��
    [SerializeField] private float dashSpeed = 150.0f;
    // ���鋗���̍ő�l
    private float dashEndPos;

    // �f�o�b�O�p�@����p�̕ϐ�
    private int dashCount;

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

    public State state = State.PUSH;

    // UIManager�擾�p
    private UIManager um;

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        animator = GetComponent<Animator>();
        isStateChange = false;

        um = GameObject.Find("UIManager").GetComponent<UIManager>();


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
                    um.ChangeMouseGUI(Common.STATE_PUSH);
                    animator.SetTrigger("Idle");
                    break;

                case State.DASH:
                    um.ChangeMouseGUI(Common.STATE_DASH);
                    animator.SetTrigger("Dash");
                    break;

                case State.HIDE:
                    um.ChangeMouseGUI(Common.STATE_HIDE);
                    animator.SetTrigger("Hide");
                    break;

                case State.SHOW:
                    um.ChangeMouseGUI(Common.STATE_SHOW);
                    animator.SetTrigger("Show");

                    break;

                case State.NORMAL:
                    um.ChangeMouseGUI(Common.STATE_NORMAL);
                    animator.SetTrigger("Normal");
                    break;
            }
        }

        isStateChange = false;
    }

    /// <summary>
    /// Show�ANormal�A�j���[�V�����I����ɌĂяo���֐�
    /// </summary>
    private void SetStateBack()
    {
        state = State.BACK;

        // TODO �ړ����Ă��Ȃ��ꍇ��BACK�A�j���[�V�����͍Đ����Ȃ�
        pos = transform.localPosition;

        if(pos.x == defPosX)
        {
            isStateChange = true;
            state = State.PUSH;

        }
        else
        {
            animator.SetTrigger("Back");
        }

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
            pos.x -= backSpeed * Time.deltaTime;

           

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
            

            // ���鋗��������
            dashEndPos = defPosX;
        }
    }

    /// <summary>
    /// ���铮��
    /// </summary>
    private void DashMove()
    {
        if (state == State.DASH)
        {
            // �J�E���g�̕ύX���擾����p
            var tmpCount = dashCount;

            // ���W�擾
            pos = transform.localPosition;
            pos.x += dashSpeed * Time.deltaTime;

            // �f�o�b�O�p
            // �J�E���g����������i�ޏꏊ����������
            if (Input.GetMouseButtonDown(0))
            {
                dashCount++;
            }

            // TODO dashCount��{�ԂɎg���p�̕ϐ��ɏC������
            if(tmpCount != dashCount)
            {
                dashEndPos += plusDist;
            }

            // �i�ޏꏊ��(�ő勗���܂�)�i��ł���
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
        // ����������
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // �A�j���[�V�����ύX����
        AnimationChange();

        // �����Ă��鎞�̓���
        DashMove();

        // �B�ꂽ�� or �������と���C���^�[�z���O�ɖ߂铮��
        BackMove();
    }
}
