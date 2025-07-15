using System.IO;
using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    /// <summary>
    /// Provides static paths for saving/loading various session data.
    /// </summary>
    public static class SavePaths
    {
        #region Properties

        /// <summary>
        /// Path for storing the player session save file.
        /// </summary>
        public static string PlayerSession =>
            Path.Combine(Application.persistentDataPath, "playerSession_save.json");

        /// <summary>
        /// Path for storing the player session record save file.
        /// </summary>
        public static string Record =>
            Path.Combine(Application.persistentDataPath, "playerSession_record_save.json");

        /// <summary>
        /// Path for storing the game session save file.
        /// </summary>
        public static string GameSession =>
            Path.Combine(Application.persistentDataPath, "gameSession_save.json");

        #endregion
    }
}