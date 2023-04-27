using HarmonyLib;
using MapGeneration.Distributors;

namespace BetterCoinflips.Patches
{
    [HarmonyPatch(typeof(LockerChamber), nameof(LockerChamber.SpawnItem))]
    public static class LockerChamberItemSpawnPatch 
    {
        public static bool Prefix(ItemType id, int amount)
        {
            if (id == ItemType.Coin && Plugin.Instance.Config.DefaultCoinsAmount != 0)
            {
                amount -= Plugin.Instance.Config.DefaultCoinsAmount--;
                Plugin.Instance.Config.DefaultCoinsAmount -= amount;
                return false;
            }
            return true;
        }
    }
}