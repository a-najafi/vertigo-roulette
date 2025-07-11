using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Couroutine
{
    public static class CoroutineUtils
    {
        public static IEnumerator WaitForAll(this MonoBehaviour runner, IEnumerable<IEnumerator> routines)
        {
            var routineList = routines.ToList();
            bool[] completed = new bool[routineList.Count];

            for (int i = 0; i < routineList.Count; i++)
            {
                int idx = i;
                runner.StartCoroutine(RunAndMarkComplete(routineList[i], () => completed[idx] = true));
            }

            while (completed.Any(x => !x))
                yield return null;
        }

        private static IEnumerator RunAndMarkComplete(IEnumerator routine, Action onComplete)
        {
            yield return routine;
            onComplete?.Invoke();
        }
    }
    
}