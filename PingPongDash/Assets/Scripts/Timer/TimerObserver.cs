using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kabasawa
{
    public class TimerObserver
    {
        public bool isStop = false;

        float waitTime = 0f;

        bool exit = false;

        float remainingTime = 0f;

        public float RemainingTime() { return remainingTime; }

        List<ITimerListeners> timerListeners = new List<ITimerListeners>();


        public TimerObserver(float _waitTime, List<ITimerListeners> _timerListeners)
        {
            this.waitTime = _waitTime;
            this.timerListeners = _timerListeners;
        }

        public TimerObserver(float _waitTime, ITimerListeners _timerListener)
        {
            this.waitTime = _waitTime;
            this.timerListeners.Add(_timerListener);
        }

        public void Register(ITimerListeners _timerListener)
        {
            if (!timerListeners.Contains(_timerListener))
            {
                timerListeners.Add(_timerListener);
            }
        }

        public void Unregister(ITimerListeners _timerListener)
        {
            timerListeners.Remove(_timerListener);
        }

        public async Task SetTimer(int _delayTime = 100)
        {
            remainingTime = waitTime; // �c�莞��

            while (remainingTime > 0)
            {
                if (exit)
                {
                    exit = false;
                    return;
                }


                if (isStop)
                {
                    // �J�E���g�_�E���I����Ƀ��X�i�[�ɒʒm
                    await Task.Delay(_delayTime);
                    continue;
                }


                // 1�b�ҋ@
                await Task.Delay(_delayTime);

                // �c�莞�Ԃ�1�b���炷
                remainingTime += -_delayTime;
            }

            foreach (ITimerListeners listener in timerListeners)
            {
                if (listener != null)
                {
                    listener.OnTimeUp();
                }
            }
        }

        /// <summary>
        /// �J�E���g�_�E�����I�����܂�
        /// </summary>
        public void End()
        {
            exit = true;
        }

        public static async Task SetTimer(int _miliSeconds, ITimerListeners _timerListener)
        {
            await Task.Delay(_miliSeconds);  // �~���b�P�ʂőҋ@

            if (_timerListener != null)
            {
                _timerListener.OnTimeUp();
            }
        }

        public static async Task SetTimer(int _miliSeconds, List<ITimerListeners> _timerListeners)
        {
            await Task.Delay(_miliSeconds);  // �~���b�P�ʂőҋ@

            foreach(ITimerListeners listener in _timerListeners)
            {
                if (listener != null)
                {
                    listener.OnTimeUp();
                }
            }
        }
    }
}
