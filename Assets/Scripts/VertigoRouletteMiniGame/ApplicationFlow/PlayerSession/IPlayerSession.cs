using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    /// <summary>
    /// Defines the essential data and functionality for a player session.
    /// </summary>
    public interface IPlayerSession
    {
        #region Properties

        /// <summary>
        /// The player's display name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The player's inventory (items, currencies, etc.).
        /// </summary>
        PlayerInventory Inventory { get; }

        #endregion
    }
}