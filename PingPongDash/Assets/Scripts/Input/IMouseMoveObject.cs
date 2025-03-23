using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMouseMoveObject : IMouseMove_
{
    /// <summary>
    /// マウスの上下移動の方向が切り替わったときに呼び出されます
    /// </summary>
    public virtual void OnMouseMoveChangeEvent()
    {

    }

    /// <summary>
    /// マウスの上下移動の方向が切り替わったときに上方向なら呼び出されます
    /// </summary>
    public virtual void OnMouseMoveChangeUP()
    {

    }

    /// <summary>
    /// マウスの上下移動の方向が切り替わったときに下方向なら呼び出されます
    /// </summary>
    public virtual void OnMouseMoveChangeDown()
    {

    }
}

public abstract class IMouseMove
{
    /// <summary>
    /// マウスの上下移動の方向が切り替わったときに呼び出されます
    /// </summary>
    public virtual void OnMouseMoveChangeEvent()
    {

    }

    /// <summary>
    /// マウスの上下移動の方向が切り替わったときに上方向なら呼び出されます
    /// </summary>
    public virtual void OnMouseMoveChangeUP()
    {

    }

    /// <summary>
    /// マウスの上下移動の方向が切り替わったときに下方向なら呼び出されます
    /// </summary>
    public virtual void OnMouseMoveChangeDown()
    {

    }
}

public abstract class IMouseMove_ : MonoBehaviour 
{

}
