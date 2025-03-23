using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kabasawa
{
    public class AlternateMouseClickObserver
    {
        private enum ClickType {NONE, LEFT, RIGHT}      // �}�E�X�N���b�N���X�g
        private ClickType preClick = ClickType.NONE;    // �O�̃N���b�N�{�^��

        float span = 10f;                         // �Ԋu
        private float elapsedTime = 0f;                 // �o�ߎ���

        List<IAlternateMouseClickListener> listeners = new List<IAlternateMouseClickListener>();


        public void Update()
        {
            Check(); // �N���b�N�`�F�b�N

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= span)    // �o�߂����烊�Z�b�g����
            {
                Reset();
            }

        }

        private void Check() // �N���b�N�`�F�b�N
        {
            // ���N���b�N
            if (preClick != ClickType.RIGHT && Input.GetMouseButtonDown(0))
            {
                preClick = ClickType.RIGHT;

                foreach (IAlternateMouseClickListener lis in listeners)
                {
                    lis.OnAlternateMouseClick();
                }
            }

            // �E�N���b�N
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

            //�ȈՃ��Z�b�g
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
