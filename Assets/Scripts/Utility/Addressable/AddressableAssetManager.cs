using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utility.Addressable
{
    public static class AddressableAssetManager
    {
        private static readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public static IEnumerator LoadAsset<T>(AssetReference reference, Action<T> onComplete)
        {
            string key = reference.AssetGUID;

            if (_handles.TryGetValue(key, out var existingHandle))
            {
                if (existingHandle.IsDone)
                {
                    onComplete?.Invoke((T)existingHandle.Result);
                    yield break;
                }
            }
            else
            {
                var handle = reference.LoadAssetAsync<T>();
                _handles[key] = handle;
            }

            var finalHandle = _handles[key];
            yield return finalHandle;
            onComplete?.Invoke((T)finalHandle.Result);
        }

        public static void Release(AssetReference reference)
        {
            string key = reference.AssetGUID;

            if (_handles.TryGetValue(key, out var handle) && handle.IsValid())
            {
                Addressables.Release(handle);
                _handles.Remove(key);
            }
        }

        public static void ReleaseAll()
        {
            foreach (var handle in _handles.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }
            _handles.Clear();
        }
    }
}