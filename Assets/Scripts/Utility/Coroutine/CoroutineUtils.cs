using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Coroutine
{
    /// <summary>
    /// Provides utility methods for running and synchronizing multiple coroutines in parallel.
    /// </summary>
    public static class CoroutineUtils
    {
        #region Public Methods

        /// <summary>
        /// Runs all the provided coroutine routines in parallel and waits until all are complete.
        /// Usage: yield return this.WaitForAll(listOfCoroutines);
        /// </summary>
        /// <param name="runner">The MonoBehaviour that will start the coroutines.</param>
        /// <param name="routines">The set of IEnumerators (coroutines) to run in parallel.</param>
        /// <returns>Coroutine enumerator that completes when all routines finish.</returns>
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper coroutine that runs the given routine and invokes a callback upon completion.
        /// </summary>
        /// <param name="routine">The coroutine to run.</param>
        /// <param name="onComplete">Action to invoke when done.</param>
        private static IEnumerator RunAndMarkComplete(IEnumerator routine, Action onComplete)
        {
            yield return routine;
            onComplete?.Invoke();
        }

        #endregion
    }
}