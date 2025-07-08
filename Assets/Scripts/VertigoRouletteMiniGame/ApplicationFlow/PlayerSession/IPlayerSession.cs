using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    public interface IPlayerSession
    {
        public string Name { get; }
        public PlayerInventory Inventory { get; }

    }
}