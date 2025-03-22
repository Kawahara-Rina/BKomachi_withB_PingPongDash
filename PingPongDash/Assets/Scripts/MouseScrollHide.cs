using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NakanoScroll
{
    public class MouseScrollHide : MonoBehaviour
    {
        public float deadZone = 0.05f;  // 値以下のスクロールは無視
        private bool isHide = false;    // フラグ

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            HandleScrollSwitch();   // 隠れると隠れていないを切り替える
        }

        void HandleScrollSwitch()
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