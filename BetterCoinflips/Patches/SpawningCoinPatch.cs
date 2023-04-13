using HarmonyLib;
using MapGeneration.Distributors;

namespace BetterCoinflips.Patches
{
    [HarmonyPatch(typeof(LockerChamber), nameof(LockerChamber.SpawnItem))]
    public static class LockerChamberItemSpawnPatch 
    {
        public static bool Prefix(ItemType id, int amount)
        {
            if (id == ItemType.Coin && !Plugin.Instance.Config.SpawnDefaultCoins)
            {
                return false;
            }
            return true;
        }
    }
}