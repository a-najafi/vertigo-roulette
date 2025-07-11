using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{

    public enum EZoneState
    {
        None,
        Active,
        Win,
        Lose
    }
    
    public class ZoneInstance
    {
        private int zoneIndex;
        private ZoneConfiguration zoneConfiguration;
        private EZoneState zoneState;
    }
}