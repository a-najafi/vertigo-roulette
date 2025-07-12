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
        private RouletteInstance rouletteInstance = null;

        public int ZoneIndex => zoneIndex;

        public ZoneConfiguration ZoneConfiguration => zoneConfiguration;

        public EZoneState ZoneState => zoneState;

        public RouletteInstance RouletteInstance => rouletteInstance;


        public ZoneInstance(int zoneIndex, ZoneConfiguration zoneConfiguration)
        {
            this.zoneIndex = zoneIndex;
            this.zoneConfiguration = zoneConfiguration;
            this.zoneState = EZoneState.None;
        }
    }
}