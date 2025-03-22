using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kabasawa
{
    public class MouseMoveInput 
    {
        [SerializeField,Range(0f,0.9f)]
        float deadZone = 0f;

        MouseMoveDirection preInputVec = MouseMoveDirection.NONE;

        [SerializeField, Range(0.5f, 2f)]
        float reseTime = 0.5f;

        float inputSpan = 0f;

        List<IMouseMove> objects = new List<IMouseMove>();


        public void Register(IMouseMove _immObj)
        {
            if (!objects.Contains(_immObj))
            {
                objects.Add(_immObj);
            }
        }

        public void Unregister(IMouseMove _immObj)
        {
            objects.Remove(_immObj);
        }

        // Update is called once per frame
        public void Update()
        {
            MouseMoveDirection _in;

            _in = GetInputVec();

            if(_in == MouseMoveDirection.NONE)
            {
                //���͂���Ă��Ȃ��ꍇ���Z�b�g�������s��
                Reset();
                return;
            }

            //���Z�b�g�܂ł̊Ԋu������������
            inputSpan = 0;

            //���͂ɕύX������ꍇ�̂�
            if (preInputVec != _in)
            {
                preInputVec = _in;

                foreach(IMouseMove obj in objects)
                {
                    obj.OnMouseMoveChangeEvent();
                }

                switch(preInputVec)
                {
                    case MouseMoveDirection.UP:
                        foreach (IMouseMove obj in objects)
                        {
                            obj.OnMouseMoveChangeUP();
                        }
                    break;


                    case MouseMoveDirection.DOWN:

                        foreach (IMouseMove obj in objects)
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
        /// Mouse�̈ړ��������擾���܂�
        /// Input.GetAxis("Mouse Y")�̐�Βl��deadZone�����������ꍇ��MouseMoveDirection.NONE��Ԃ��܂�
        /// </summary>
        /// <returns>Mouse�̈ړ�����</returns>
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