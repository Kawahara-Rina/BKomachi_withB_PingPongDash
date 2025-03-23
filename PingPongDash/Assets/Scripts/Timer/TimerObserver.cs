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
            remainingTime = waitTime; // 残り時間

            while (remainingTime > 0)
            {
                if (exit)
                {
                    exit = false;
                    return;
                }


                if (isStop)
                {
                    // カウントダウン終了後にリスナーに通知
                    await Task.Delay(_delayTime);
                    continue;
                }


                // 1秒待機
                await Task.Delay(_delayTime);

                // 残り時間を1秒減らす
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
        /// カウントダウンを終了します
        /// </summary>
        public void End()
        {
            exit = true;
        }

        public static async Task SetTimer(int _miliSeconds, ITimerListeners _timerListener)
        {
            await Task.Delay(_miliSeconds);  // ミリ秒単位で待機

            if (_timerListener != null)
            {
                _timerListener.OnTimeUp();
            }
        }

        public static async Task SetTimer(int _miliSeconds, List<ITimerListeners> _timerListeners)
        {
            await Task.Delay(_miliSeconds);  // ミリ秒単位で待機

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
