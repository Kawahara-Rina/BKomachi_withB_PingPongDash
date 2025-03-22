using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface IMouseScroll
{
    public virtual void OnScrollUp()   // 上スクロールで隠れていない
    {

    }

    public virtual void OnScrollDown() // 下スクロールで隠れている
    {

    }
}
