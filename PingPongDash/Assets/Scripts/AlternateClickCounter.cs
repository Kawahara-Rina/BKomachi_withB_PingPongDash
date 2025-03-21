using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nakano
{
    public class AlternateClickCounter : MonoBehaviour
    {
        private enum ClickType {NONE, LEFT, RIGHT}      // �}�E�X�N���b�N���X�g
        private ClickType preClick = ClickType.NONE;    // �O�̃N���b�N�{�^��
        private int buttonCnt = 0;                      // �N���b�N��
        private int savedCnt = 0;                       // �ۑ���
        public float span = 1f;                         // �Ԋu
        private float elapsedTime = 0f;                 // �o�ߎ���

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckAlternateMouseClick(); // �N���b�N�`�F�b�N

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= span)    // �o�߂����烊�Z�b�g����
            {
                CountReset();
            }
        }

        void CheckAlternateMouseClick() // �N���b�N�`�F�b�N
        {
            // ���N���b�N
            if (preClick != ClickType.RIGHT && Input.GetMouseButtonDown(0))
            {
                buttonCnt++;
                preClick = ClickType.RIGHT;
            }
            // �E�N���b�N
            else if (preClick != ClickType.LEFT && Input.GetMouseButtonDown(1))
            {
                buttonCnt++;
                preClick = ClickType.LEFT;
            }
            
        }

        void CountReset()
        {
            Debug.Log(buttonCnt);
            savedCnt = buttonCnt;
            buttonCnt = 0;
            elapsedTime = 0f;
        }
    }


}
