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

    // ����Ƃ��̒ǉ��l
    [SerializeField] private float plusDist = 10.0f;
    // ���鑬��
    [SerializeField] private float dashSpeed = 150.0f;
    // ���鋗���̍ő�l
    private float dashEndPos;

    // �f�o�b�O�p�@����p�̕ϐ�
    public int dashCount;

    public float pushSpeed;

    // �f�o�b�O�p
    // TODO�@�����̎擾����X�e�[�^�X��ύX
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
    /// ����������
    /// </summary>
    private void Init()
    {
        pushSpeed = 1;
        animator = GetComponent<Animator>();

        //um = GameObject.Find("UIManager").GetComponent<UIManager>();


    }

    /// <summary>
    /// �X�e�[�g�ύX�̊֐�
    /// </summary>
    public void ChangeState(State newState)
    {
        if(state != newState)
        {
            state = newState;
            animator.SetTrigger(StateToAnimationName(newState));
        }
    }

    // State����A�j���[�V�����̖��O�ɕϊ�����
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
    /// Show�ANormal�A�j���[�V�����I����ɌĂяo���֐�
    /// </summary>
    private void SetStateBack()
    {
        state = State.BACK;

        // TODO �ړ����Ă��Ȃ��ꍇ��BACK�A�j���[�V�����͍Đ����Ȃ�
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
                    ChangeState(State.PUSH);
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
        if(state == State.PUSH)
        {
            animator.speed = pushSpeed;
        }
        else
        {
            animator.speed = 1;
        }

        // �����Ă��鎞�̓���
        DashMove();

        // �B�ꂽ�� or �������と���C���^�[�z���O�ɖ߂铮��
        BackMove();

        Debug.Log(state);
    }

}
