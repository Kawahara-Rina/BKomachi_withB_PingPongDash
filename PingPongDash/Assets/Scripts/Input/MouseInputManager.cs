using UnityEngine;
using NakanoScroll;

namespace Kabasawa
{
    public class MouseInputManager : MonoBehaviour
    {
        public AlternateMouseClickObserver alternateMouseClickObserver ;
        public MouseMoveInput mouseMoveInput ;
        public MouseAvg mouseAvg ;
        public MouseScroll mouseScroll ;

        public static MouseInputManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                alternateMouseClickObserver = new AlternateMouseClickObserver();
                mouseMoveInput = new MouseMoveInput();
                mouseAvg = new MouseAvg();
                mouseScroll = new MouseScroll();
            }
            else
            {
                Destroy(gameObject); // 重複したインスタンスを削除
            }
        }


        private void Update()
        {
            alternateMouseClickObserver.Update();
            mouseMoveInput.Update();
            mouseAvg.Update();
            mouseScroll.Update();
        }


    }
}