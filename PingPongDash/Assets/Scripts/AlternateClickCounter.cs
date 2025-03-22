using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nakano
{
    public class AlternateClickCounter : MonoBehaviour
    {
        private enum ClickType {NONE, LEFT, RIGHT}      // マウスクリックリスト
        private ClickType preClick = ClickType.NONE;    // 前のクリックボタン
        private int buttonCnt = 0;                      // クリック数
        private int savedCnt = 0;                       // 保存先
        public float span = 1f;                         // 間隔
        private float elapsedTime = 0f;                 // 経過時間

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckAlternateMouseClick(); // クリックチェック

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= span)    // 経過したらリセットする
            {
                CountReset();
            }
        }

        void CheckAlternateMouseClick() // クリックチェック
        {
            // 左クリック
            if (preClick != ClickType.RIGHT && Input.GetMouseButtonDown(0))
            {
                buttonCnt++;
                preClick = ClickType.RIGHT;
            }
            // 右クリック
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
