using System.IO;
using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    public static class SavePaths
    {
        public static string PlayerSession => Path.Combine(Application.persistentDataPath, "playerSession_save.json");
        public static string Record => Path.Combine(Application.persistentDataPath, "playerSession_record_save.json");
        public static string GameSession => Path.Combine(Application.persistentDataPath, "gameSession_save.json");
        
    }
}