using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMouseMoveObject : IMouseMove_
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
}

public abstract class IMouseMove
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
}

public abstract class IMouseMove_ : MonoBehaviour 
{

}
