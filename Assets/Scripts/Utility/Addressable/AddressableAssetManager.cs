using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utility.Addressable
{
    
    public class AssetLoadResult<T>
    {
        public T Asset;
    }
    
    public static class AddressableAssetManager
    {
        private static readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public static IEnumerator LoadAsset<T>(AssetReference reference, AssetLoadResult<T> resultHolder)
        {
            string key = reference.RuntimeKey.ToString();

            if (_handles.TryGetValue(key, out var existingHandle))
            {
                if (existingHandle.IsDone && existingHandle.Result is T existingResult)
                {
                    resultHolder.Asset = existingResult;
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

            if (finalHandle.Status == AsyncOperationStatus.Succeeded)
                resultHolder.Asset = (T)finalHandle.Result;
            else
                Debug.LogError($"Failed to load addressable asset: {key}");
        }


        public static void Release(AssetReference reference)
        {
            string key = reference.RuntimeKey.ToString();

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