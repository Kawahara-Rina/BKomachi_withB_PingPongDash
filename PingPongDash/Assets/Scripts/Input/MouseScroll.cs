using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NakanoScroll
{
    public class MouseScroll
    {
        public float deadZone = 0.05f;  // �l�ȉ��̃X�N���[���͖���

        public void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // �X�N���[�����f�X�]�[���𒴂��Ă��Ȃ��ꍇ����
            if (Mathf.Abs(scroll) < deadZone)
            {
                return;
            }

            if (scroll > 0f)
            {
                foreach (IMouseScroll obj in objects)
                {
                    obj.OnScrollUp();
                }
                return;
            }
            if (scroll < 0f)
            {
                foreach (IMouseScroll obj in objects)
                {
                    obj.OnScrollDown();
                }
                return;
            }

        }

        List<IMouseScroll> objects = new List<IMouseScroll>();


        public void Register(IMouseScroll _immObj)
        {
            if (!objects.Contains(_immObj))
            {
                objects.Add(_immObj);
            }
        }

        public void Unregister(IMouseScroll _immObj)
        {
            objects.Remove(_immObj);
        }
    }
}