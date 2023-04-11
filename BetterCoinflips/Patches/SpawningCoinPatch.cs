using GameCore;
using HarmonyLib;
using MapGeneration.Distributors;
using UnityEngine;
using Log = Exiled.API.Features.Log;

namespace BetterCoinflips.Patches
{
    public class SpawningCoinPatch
    {
        [HarmonyPatch(typeof(ItemDistributor), nameof(ItemDistributor.CreatePickup))]
        public static class CreatePickupPatch
        {
            public static bool Prefix(ItemType id, Transform t, string triggerDoor)
            {
                if (id == ItemType.Coin)
                {
                    Log.Info("sraka");
                    return false;
                }
                return true;
            }
        }
    }
}