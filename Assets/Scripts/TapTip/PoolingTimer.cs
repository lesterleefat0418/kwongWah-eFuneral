using System;
using UnityEngine;

namespace WPFTabTip
{
    internal static class PoolingTimer
    {
        private static bool Pooling;
        private static MonoBehaviour coroutineHandler;

        internal static void SetCoroutineHandler(MonoBehaviour handler)
        {
            coroutineHandler = handler;
        }

        internal static void PoolUntilTrue(Func<bool> PoolingFunc, Action Callback, TimeSpan dueTime, TimeSpan period)
        {
            if (Pooling) return;

            Pooling = true;
            coroutineHandler.StartCoroutine(TimerCoroutine(PoolingFunc, Callback, dueTime, period));
        }

        private static System.Collections.IEnumerator TimerCoroutine(Func<bool> PoolingFunc, Action Callback, TimeSpan dueTime, TimeSpan period)
        {
            yield return new WaitForSeconds((float)dueTime.TotalSeconds);

            while (!PoolingFunc())
            {
                yield return new WaitForSeconds((float)period.TotalSeconds);
            }

            Callback?.Invoke();
            Pooling = false;
        }
    }
}
