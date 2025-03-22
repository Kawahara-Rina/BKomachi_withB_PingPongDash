using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMouseMoveObject : MonoBehaviour
{
    /// <summary>
    /// �}�E�X�̏㉺�ړ��̕������؂�ւ�����Ƃ��ɌĂяo����܂�
    /// </summary>
    public virtual void OnMouseMoveChangeEvent()
    {

    }

    /// <summary>
    /// �}�E�X�̏㉺�ړ��̕������؂�ւ�����Ƃ��ɏ�����Ȃ�Ăяo����܂�
    /// </summary>
    public virtual void OnMouseMoveChangeUP()
    {

    }

    /// <summary>
    /// �}�E�X�̏㉺�ړ��̕������؂�ւ�����Ƃ��ɉ������Ȃ�Ăяo����܂�
    /// </summary>
    public virtual void OnMouseMoveChangeDown()
    {

    }

    /// <summary>
    /// �I�u�U�[�o�[�ɓo�^.
    /// ���̃N���X���g���ꍇ�ŏ��ɋN�����Ă�������
    /// </summary>
    protected void RegisterMouseMoveObserver()
    {
        MouseMoveInput.CreateInstance().Register(this);
    }

    /// <summary>
    /// �I�u�U�[�o�[�o�^������
    /// event���g��Ȃ��Ȃ����ꍇ���̊֐����Ăяo���Ă�������
    /// </summary>
    protected void UnregisterMouseMoveObserver()
    {
        MouseMoveInput.CreateInstance().Unregister(this);
    }


}
