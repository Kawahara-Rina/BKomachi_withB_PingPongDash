using UnityEngine;
using NakanoScroll;

namespace Kabasawa
{
    public class MouseInputManager : MonoBehaviour
    {
        public AlternateMouseClickObserver alternateMouseClickObserver ;
        public MouseMoveInput mouseMoveInput ;
        public MouseAvg mouseAvg ;
        public MouseScrollHide mouseScrollHide ;

        public static MouseInputManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // 重複したインスタンスを削除
            }
        }

        private void Start()
        {
             alternateMouseClickObserver = new AlternateMouseClickObserver();
             mouseMoveInput = new MouseMoveInput();
             mouseAvg = new MouseAvg();
             mouseScrollHide = new MouseScrollHide();
        }

        private void Update()
        {
            alternateMouseClickObserver.Update();
            mouseMoveInput.Update();
            mouseAvg.Update();
            mouseScrollHide.Update();
        }


    }
}