using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NakanoScroll
{
    public class MouseScrollHide
    {
        public float deadZone = 0.05f;  // �l�ȉ��̃X�N���[���͖���
        public bool isHide = false;    // �t���O

        public void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // �X�N���[�����f�X�]�[���𒴂��Ă��Ȃ��ꍇ����
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

        void OnScrollUp()   // ��X�N���[���ŉB��Ă��Ȃ�
        {
            isHide = false;
        }
        
        void OnScrollDown() // ���X�N���[���ŉB��Ă���
        {
            isHide = true;
        }
    }

}