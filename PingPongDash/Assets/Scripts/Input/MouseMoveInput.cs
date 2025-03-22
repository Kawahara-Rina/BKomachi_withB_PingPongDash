using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kabasawa
{
    public class MouseMoveInput : MonoBehaviour 
    {
        public static MouseMoveInput Instance { get; private set; }

        [SerializeField,Range(0f,0.9f)]
        float deadZone = 0f;

        MouseMoveDirection preInputVec = MouseMoveDirection.NONE;

        [SerializeField, Range(0.5f, 2f)]
        float reseTime = 0.5f;

        float inputSpan = 0f;

        List<IMouseMoveObject> objects = new List<IMouseMoveObject>();


        public void Register(IMouseMoveObject _immObj)
        {
            if (!objects.Contains(_immObj))
            {
                objects.Add(_immObj);
            }
        }

        public void Unregister(IMouseMoveObject _immObj)
        {
            objects.Remove(_immObj);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            MouseMoveDirection _in;

            _in = GetInputVec();

            if(_in == MouseMoveDirection.NONE)
            {
                //入力されていない場合リセット処理を行う
                Reset();
                return;
            }

            //リセットまでの間隔を初期化する
            inputSpan = 0;

            //入力に変更がある場合のみ
            if (preInputVec != _in)
            {
                preInputVec = _in;

                foreach(IMouseMoveObject obj in objects)
                {
                    obj.OnMouseMoveChangeEvent();
                }

                switch(preInputVec)
                {
                    case MouseMoveDirection.UP:
                        foreach (IMouseMoveObject obj in objects)
                        {
                            obj.OnMouseMoveChangeUP();
                        }
                    break;


                    case MouseMoveDirection.DOWN:

                        foreach (IMouseMoveObject obj in objects)
                        {
                            obj.OnMouseMoveChangeDown();
                        }

                    break;

                    default:
                    break;
                }
            }

        }

        /// <summary>
        /// 外部から生成できるようにする
        /// </summary>
        public static MouseMoveInput CreateInstance()
        {
            if (Instance == null)
            {
                GameObject obj = new GameObject("MouseMoveInput");
                Instance = obj.AddComponent<MouseMoveInput>();
            }
            return Instance;
        }

        /// <summary>
        /// Mouseの移動方向を取得します
        /// Input.GetAxis("Mouse Y")の絶対値がdeadZoneよりも小さい場合はMouseMoveDirection.NONEを返します
        /// </summary>
        /// <returns>Mouseの移動方向</returns>
        MouseMoveDirection GetInputVec()
        {
            float v = Input.GetAxis("Mouse Y");

            if(Mathf.Abs(v) <= deadZone)
            {
                return MouseMoveDirection.NONE;
            }

            if(v > 0)
            {
                return MouseMoveDirection.UP;
            }
            else
            {
                return MouseMoveDirection.DOWN;
            }
            
        }

        private void Reset()
        {
            inputSpan += Time.deltaTime;

            if(inputSpan >= reseTime)
            {
                inputSpan = 0;
                preInputVec = MouseMoveDirection.NONE;
            }
        }
    }
}


            