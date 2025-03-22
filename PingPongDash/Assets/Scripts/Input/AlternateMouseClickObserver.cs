using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kabasawa
{
    public class AlternateMouseClickObserver
    {
        private enum ClickType {NONE, LEFT, RIGHT}      // マウスクリックリスト
        private ClickType preClick = ClickType.NONE;    // 前のクリックボタン

        float span = 10f;                         // 間隔
        private float elapsedTime = 0f;                 // 経過時間

        List<IAlternateMouseClickListener> listeners = new List<IAlternateMouseClickListener>();


        public void Update()
        {
            Check(); // クリックチェック

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= span)    // 経過したらリセットする
            {
                Reset();
            }

        }

        private void Check() // クリックチェック
        {
            // 左クリック
            if (preClick != ClickType.RIGHT && Input.GetMouseButtonDown(0))
            {
                preClick = ClickType.RIGHT;

                foreach (IAlternateMouseClickListener lis in listeners)
                {
                    lis.OnAlternateMouseClick();
                }
            }

            // 右クリック
            else if (preClick != ClickType.LEFT && Input.GetMouseButtonDown(1))
            {
                preClick = ClickType.LEFT;

                foreach (IAlternateMouseClickListener lis in listeners)
                {
                    lis.OnAlternateMouseClick();
                }
            }


        }

        void Reset()
        {
            elapsedTime = 0f;

            //簡易リセット
            preClick = ClickType.NONE;
        }

        public void Register(IAlternateMouseClickListener _iamcl)
        {
            if (!listeners.Contains(_iamcl))
            {
                listeners.Add(_iamcl);
            }
        }

        public void Unregister(IAlternateMouseClickListener _iamcl)
        {
            listeners.Remove(_iamcl);
        }
    }


}
