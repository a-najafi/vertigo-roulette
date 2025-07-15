using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utility.Addressable;
using Utility.PropertyAttributes;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    /// <summary>
    /// Serializable player session data, including player name and inventory,
    /// with static helpers for saving/loading from disk.
    /// </summary>
    [Serializable]
    public class PlayerSession : IPlayerSession
    {
        #region Serialized Parameters

        [SerializeField, ReadOnly] private string name = "DefaultPlayerSession";
        [SerializeField, ReadOnly] private PlayerInventory inventory = new PlayerInventory();

        #endregion

        #region Properties

        /// <summary>
        /// The name of the player/session.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The player's inventory.
        /// </summary>
        public PlayerInventory Inventory => inventory;

        #endregion

        #region Public Methods

        /// <summary>
        /// Asynchronously saves the player session to a JSON file at the specified path.
        /// </summary>
        /// <param name="playerSession">The player session instance to save.</param>
        /// <param name="savePath">Path to the save file.</param>
        public static IEnumerator SavePlayerSession(PlayerSession playerSession, string savePath)
        {
            // Serialize to JSON.
            string json = JsonUtility.ToJson(playerSession, true);

            // Write to file asynchronously.
            Task writeAsyncOperationHandle = File.WriteAllTextAsync(savePath, json);
            yield return writeAsyncOperationHandle;
        }

        /// <summary>
        /// Asynchronously loads a player session from a JSON file.
        /// </summary>
        /// <param name="loadedPlayerSession">Result holder for loaded PlayerSession.</param>
        /// <param name="savePath">Path to the save file.</param>
        public static IEnumerator LoadPlayerSession(AssetLoadResult<PlayerSession> loadedPlayerSession, string savePath)
        {
            var readAllTextAsync = File.ReadAllTextAsync(savePath);
            yield return readAllTextAsync;

            // Parse and assign the loaded PlayerSession if successful.
            if (readAllTextAsync.IsCompletedSuccessfully)
            {
                PlayerSession playerSession = JsonUtility.FromJson<PlayerSession>(readAllTextAsync.Result);
                if (playerSession != null)
                    loadedPlayerSession.Asset = playerSession;
            }
        }

        #endregion
    }
}
