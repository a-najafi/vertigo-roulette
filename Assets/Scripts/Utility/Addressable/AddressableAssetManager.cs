using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utility.Addressable
{
    /// <summary>
    /// Generic result holder for asynchronous addressable asset loading.
    /// </summary>
    /// <typeparam name="T">The type of asset being loaded.</typeparam>
    public class AssetLoadResult<T>
    {
        /// <summary>
        /// The loaded asset (null if loading failed).
        /// </summary>
        public T Asset;
    }

    /// <summary>
    /// Utility manager for loading and releasing addressable assets with caching.
    /// </summary>
    public static class AddressableAssetManager
    {
        #region Non Serialized Parameters

        /// <summary>
        /// Internal cache of AsyncOperationHandles keyed by the asset's runtime key.
        /// </summary>
        private static readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads an addressable asset asynchronously and assigns it to the provided result holder.
        /// </summary>
        /// <typeparam name="T">Type of asset to load.</typeparam>
        /// <param name="reference">The AssetReference to load.</param>
        /// <param name="resultHolder">Result holder that will store the loaded asset.</param>
        /// <returns>Coroutine enumerator.</returns>
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

        /// <summary>
        /// Releases the handle for a specific addressable asset, if loaded.
        /// </summary>
        /// <param name="reference">The AssetReference whose handle will be released.</param>
        public static void Release(AssetReference reference)
        {
            string key = reference.RuntimeKey.ToString();

            if (_handles.TryGetValue(key, out var handle) && handle.IsValid())
            {
                Addressables.Release(handle);
                _handles.Remove(key);
            }
        }

        /// <summary>
        /// Releases all loaded addressable assets and clears the handle cache.
        /// </summary>
        public static void ReleaseAll()
        {
            foreach (var handle in _handles.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }
            _handles.Clear();
        }

        #endregion
    }
}
