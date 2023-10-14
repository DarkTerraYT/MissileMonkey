using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;

namespace MissileMonkeyMod
{
    internal class MissileMonkey : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Military;

        public override string BaseTower => TowerType.BombShooter;

        public override int Cost => 650;

        public override int TopPathUpgrades => 5;

        public override int MiddlePathUpgrades => 5;

        public override int BottomPathUpgrades => 5;

        public override string Portrait => Name + "-Icon";

        public override string Description => "Just when the monkeys thought they had done it all, the engineer monkey made a new missile launcher. This gave the monkeys extra hope that they could defeat the bloon forces.";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetAttackModel().weapons[0].rate = 0.33f;
            towerModel.range = 500;
            towerModel.isGlobalRange = true;
            towerModel.GetAttackModel().range = 500;
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.GetDamageModel().damage = 2;
            projectileModel.pierce = 20;
            towerModel.GetWeapon().projectile.ApplyDisplay<MissileDisplay>();
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan += 25;
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().speed *= 2.35f;
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().Speed *= 2.35f;
        }
        public override bool IsValidCrosspath(int[] tiers) => ModHelper.HasMod("UltimateCrosspathing") ? true : base.IsValidCrosspath(tiers);
    }
}
