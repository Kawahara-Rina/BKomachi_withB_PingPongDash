using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NakanoScroll
{
    public class MouseScrollHide
    {
        public float deadZone = 0.05f;  // 値以下のスクロールは無視
        public bool isHide = false;    // フラグ

        public void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // スクロールがデスゾーンを超えていない場合無視
            if (Mathf.Abs(scroll) < deadZone)
            {
                return;
            }

            if (isHide && scroll > 0f)
            {
                OnScrollUp();
                return;
            }
            if (!isHide && scroll < 0f)
            {
                OnScrollDown();
                return;
            }

        }

        void OnScrollUp()   // 上スクロールで隠れていない
        {
            isHide = false;
        }
        
        void OnScrollDown() // 下スクロールで隠れている
        {
            isHide = true;
        }
    }

}