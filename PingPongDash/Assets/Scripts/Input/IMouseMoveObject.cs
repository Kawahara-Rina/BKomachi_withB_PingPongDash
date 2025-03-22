using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMouseMoveObject : MonoBehaviour
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

    /// <summary>
    /// オブザーバーに登録.
    /// このクラスを使う場合最初に起動してください
    /// </summary>
    protected void RegisterMouseMoveObserver()
    {
        MouseMoveInput.CreateInstance().Register(this);
    }

    /// <summary>
    /// オブザーバー登録を解除
    /// eventを使わなくなった場合この関数を呼び出してください
    /// </summary>
    protected void UnregisterMouseMoveObserver()
    {
        MouseMoveInput.CreateInstance().Unregister(this);
    }


}
