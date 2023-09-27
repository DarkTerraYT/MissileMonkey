using MelonLoader;
using BTD_Mod_Helper;
using MissileMonkeyMod;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Objects;

[assembly: MelonInfo(typeof(MissileMonkeyMod.MissileMonkeyMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MissileMonkeyMod;

public class MissileMonkeyMod : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<MissileMonkeyMod>("MissileMonkeyMod loaded!");
    }
    public override void OnGameModelLoaded(GameModel model)
    {
        const string TowerID = "MissileMonkeyMod-MissileMonkey";

        // Making it so that the Missile Launcher is Affected by Striker Jones
        for(var i = 4; i <= 20; i++)
        {
            var striker = model.GetHeroWithNameAndLevel(TowerType.StrikerJones, i);
            var filterModel = striker.GetDescendant<RateSupportExplosiveModel>().filters[0].Cast<FilterInBaseTowerIdModel>();
            striker.GetDescendant<RateSupportExplosiveModel>().filters[0].Cast<FilterInBaseTowerIdModel>().baseIds = filterModel.baseIds.AddTo(TowerID);
        }

        var filterList = ArtilleryCommand.towerFilterList;
        if (!filterList.Contains(TowerID))
        {
            ArtilleryCommand.towerFilterList = ArtilleryCommand.towerFilterList.AddTo(TowerID);
        }
    }
}