using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;

namespace MissileMonkeyMod
{
    internal class The_United_States_of_America : ModParagonUpgrade<MissileMonkey>
    {
        public override int Cost => 1000000;

        public override string Icon => "Paragon-Icon";
        public override string Portrait => "Paragon-Icon";

        public override string DisplayName => "The United States of America.";

        public override string Description => "The Pledge of Allegiance \n Ability: The Cold War But Warm, Adds 100 Damage and Doubles Attack Speed for 5 Seconds, and Something Secret ;)";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.ApplyDisplay<Missile4Display>();

            var projectileModel = towerModel.GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;

            projectileModel.GetDamageModel().damage = 300;
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moab", "Moab", 5, 0, false, false));
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moab", "Fortified", 2, 0, false, false));
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moab", "Ceramic", 2, 0, false, false));

            var knockbackModel = Game.instance.model.GetTowerFromId("SuperMonkey-002").GetWeapon().projectile.GetBehavior<KnockbackModel>().Duplicate();
            knockbackModel.lifespan *= 12;
            knockbackModel.heavyMultiplier *= 25;
            knockbackModel.lightMultiplier *= 35;
            knockbackModel.moabMultiplier *= 20;
            towerModel.GetWeapon().projectile.AddBehavior(knockbackModel);

            Il2CppReferenceArray<Model> abilityBehaviors = new(0);

            abilityBehaviors.AddTo(new TurboModel("TurboModel_", 5f, 0.5f, null, 100, 1f, true));

            var abilityModel = new AbilityModel("coldWarButWarm", "The Cold War But Warm", "Adds 100 Damage, and Doubles Attack Speed for 5 Seconds and Something Secret ;)", 0, 0, GetSpriteReference<MissileMonkeyMod>("Paragon-Icon"), 20, abilityBehaviors, false, false, null, 1, 0, -1, true, false);

            towerModel.AddBehavior(abilityModel);

            towerModel.GetAttackModel().weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("WizardMonkey-200").GetWeapon().projectile.GetBehavior<TrackTargetModel>().Duplicate());

            towerModel.ignoreBlockers = true;
            towerModel.GetAttackModel().attackThroughWalls = true;

            towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_1", 8, 0, 15, null, false, false);

            List<string> IgnoredTags = new(0);

            projectileModel.AddBehavior(new RemoveBloonModifiersModel("RemoveBloonModifiers_", true, true, false, true, true, IgnoredTags));
            towerModel.GetWeapon().projectile.AddBehavior(new RemoveBloonModifiersModel("RemoveBloonModifiers_", true, true, false, true, true, IgnoredTags));
            projectileModel.UpdateCollisionPassList();

            projectileModel.radius *= 3f;
            projectileModel.scale *= 3f;

            projectileModel.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;
            
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(i => i.isActive = false);
        }
    }
}
