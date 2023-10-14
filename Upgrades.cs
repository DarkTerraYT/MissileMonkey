using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNewtonsoft.Json.Utilities;
using System;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Utils;
using System.Collections.Generic;
using Il2CppAssets.Scripts.Simulation.Towers;

namespace MissileMonkeyMod
{
    internal class CamoSensors : ModUpgrade<MissileMonkey>
    {
        public override int Path => TOP;

        public override int Tier => 1;

        public override int Cost => 500;

        public override string Description => "Missile Monkey can now see CAMO Bloons";


        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(i => i.isActive = false);
        }
    }
    internal class BetterEngineering : ModUpgrade<MissileMonkey>
    {
        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 1200;
        public override string Description => "Attacks 33% Faster, Does 2 More Damage, and can now hit Black Bloons.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate *= 0.77f;
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.GetDamageModel().damage += 2;
            projectileModel.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;
        }
    }
    internal class TripleLaunchers : ModUpgrade<MissileMonkey>
    {
        public override int Path => TOP;
        public override int Tier => 3;
        public override int Cost => 4000;
        public override string Description => "Does 3 More Damage and now Shoots Three Missiles (5 if Double Attack is Bought)";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.GetDamageModel().damage += 3;

            if(towerModel.GetWeapon().emission.name == "ArcEmissionModel_1")
            {
                towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_0", 5, 0, 30, null, false, false);
            }
            else
            {
                towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_0", 3, 0, 30, null, false, false);
            }
        }
    }
    internal class MOABMaulers : ModUpgrade<MissileMonkey>
    {
        public override int Path => TOP;
        public override int Tier => 4;
        public override int Cost => 5000;
        public override string Description => "Missile Monkey now Shoots MOAB Maulers, Along With it's Increased Damage to MOAB Class Bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.AddBehavior(Game.instance.model.GetTower("BombShooter", pathTwoTier: 3).GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModifierForTagModel>().Duplicate());
            towerModel.GetWeapon().projectile.ApplyDisplay<Missile2Display>();
        }
    }
    internal class MoabAssassination : ModUpgrade<MissileMonkey>
    {
        public override int Path => TOP;
        public override int Tier => 5;
        public override int Cost => 38000;
        public override string Description => "Now Uses the MOAB Eliminator Missiles, Also Does More Damage to Fortified Bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.AddBehavior(Game.instance.model.GetTower("BombShooter", pathTwoTier: 4).GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModifierForTagModel>().Duplicate());
            projectileModel.GetDamageModel().damage += Game.instance.model.GetTowerFromId("BombShooter-050").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage;
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Fortified", "Fortified", 1f, 10f, false, false));
            towerModel.GetWeapon().projectile.ApplyDisplay<Missile3Display>();
        }
    }
    internal class BiggerMissiles : ModUpgrade<MissileMonkey>
    {
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 300;
        public override string Description => "Missile Explosions are Bigger";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.radius *= 1.5f;
            projectileModel.scale *= 1.5f;
        }
    }
    internal class EvenBiggerMissiles : ModUpgrade<MissileMonkey>
    {
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 500;
        public override string Description => "Missile Explosions are Even Bigger";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.radius *= 1.5f;
            projectileModel.scale *= 1.5f;
        }
    }
    internal class ViolentMissiles : ModUpgrade<MissileMonkey>
    {
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 2500;
        public override string Description => "Does More Damage to Cereamic, Fortified, and MOAB Class Bloons. As Well 3 More Base Damage";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Ceramic", "Ceramic", 1, 5, false, false));
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Fortified", "Fortified", 1, 5, false, false));
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moab", "Moab", 1, 15, false, false));
            projectileModel.GetDamageModel().damage += 3;
        }
    }
    internal class SuperStrip : ModUpgrade<MissileMonkey>
    {
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 8500;
        public override string Description => "Does More Damage and Explosions now Strip all Bloons Properties Under a BFB. This Includes: Fortification, Camo, Lead, and Regrow";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            Il2CppSystem.Collections.Generic.List<string> IgnoredTags = new Il2CppSystem.Collections.Generic.List<string>(4);
            IgnoredTags.Add("Bfb");
            IgnoredTags.Add("Ddt");
            IgnoredTags.Add("Zomg");
            IgnoredTags.Add("Bad");

            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            towerModel.GetWeapon().projectile.AddBehavior(new RemoveBloonModifiersModel("superStripRemoveBloonModifiersModel", true, true, false, true, true, IgnoredTags));
            projectileModel.AddBehavior(new RemoveBloonModifiersModel("superStripRemoveBloonModifiersModel_", true, true, false, true, true, IgnoredTags));
            projectileModel.GetDamageModel().damage += 6;
            projectileModel.UpdateCollisionPassList();
            System.Collections.Generic.List<DamageModifierForTagModel> behaviors = new System.Collections.Generic.List<DamageModifierForTagModel>(3);
            behaviors = projectileModel.GetBehaviors<DamageModifierForTagModel>();

            foreach (var behavior in behaviors)
            {
                behavior.damageAddative += 10;
            }
        }
    }
    internal class UltraStrip : ModUpgrade<MissileMonkey>
    {
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 40000;
        public override string Description => "Does Even More Damage and Also can now Strip all Bloon Properties on Bloons Under a ZOMG";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            Il2CppSystem.Collections.Generic.List<string> IgnoredTags = new Il2CppSystem.Collections.Generic.List<string>(2);
            IgnoredTags.Add("Zomg");
            IgnoredTags.Add("Bad");

            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;

            foreach (var model in projectileModel.GetBehaviors<RemoveBloonModifiersModel>())
            {
                projectileModel.RemoveBehavior(model);
            }

            foreach (var model in towerModel.GetWeapon().projectile.GetBehaviors<RemoveBloonModifiersModel>())
            {
                towerModel.GetWeapon().projectile.RemoveBehavior(model);   
            }

            projectileModel.AddBehavior(new RemoveBloonModifiersModel("RemoveBloonModifiers_", true, true, false, true, true, IgnoredTags));
            towerModel.GetWeapon().projectile.AddBehavior(new RemoveBloonModifiersModel("RemoveBloonModifiers_", true, true, false, true, true, IgnoredTags));
            projectileModel.UpdateCollisionPassList();
            projectileModel.GetDamageModel().damage += 15;

            System.Collections.Generic.List<DamageModifierForTagModel> behaviors = new System.Collections.Generic.List<DamageModifierForTagModel>(3);
            behaviors = projectileModel.GetBehaviors<DamageModifierForTagModel>();

            foreach (var behavior in behaviors)
            {
                behavior.damageAddative += 25;
            }
        }
    }
    internal class FasterMissiles : ModUpgrade<MissileMonkey>
    {
        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override int Cost => 400;
        public override string Description => "Shoots Twice as Fast";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate *= 0.5f;
        }
    }
    internal class DoubleAttack : ModUpgrade<MissileMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 2;

        public override int Cost => 1250;

        public override string DisplayName => "Double Rockets";

        public override string Description => "Now Shoots two Missiles (Five if Triple Launchers is Bought)";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;

            if (towerModel.GetWeapon().emission.name == "ArcEmissionModel_0")
            {
                towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_1", 5, 0, 15, null, false, false);
            }
            else
            {
                towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_1", 2, 0, 15, null, false, false);
            }
        }
    }
    internal class EHV : ModUpgrade<MissileMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 3;

        public override int Cost => 2500;
        public override string Description => "Improved Helmet & Missiles Allow Them to be Shot Through Walls";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile;

            projectileModel.AddBehavior(Game.instance.model.GetTowerFromId("WizardMonkey-200").GetWeapon().projectile.GetBehavior<TrackTargetModel>().Duplicate());

            towerModel.ignoreBlockers = true;
            towerModel.GetAttackModel().attackThroughWalls = true;
        }
    }
    internal class DoubleBomb : ModUpgrade<MissileMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 4;

        public override int Cost => 3000;
        public override string Description => "Missiles now Contain 2 Bombs (2x damage)";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectileModel = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            projectileModel.GetDamageModel().damage *= 2;
        }
    }
    internal class UltraKnockback : ModUpgrade<MissileMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 5;

        public override int Cost => 45000;
        public override string Description => "Shoots way Faster, Does More Damage to Ceramic Bloons, Fortified Bloons, and MOAB Class Bloons. Also now Knocks Bloons Back.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate = Game.instance.model.GetTowerFromId("SniperMonkey-003").GetWeapon().rate;
            var projectileModel_ = towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
            var projectileModel = towerModel.GetWeapon().projectile;
            var knockbackModel = Game.instance.model.GetTowerFromId("SuperMonkey-002").GetWeapon().projectile.GetBehavior<KnockbackModel>().Duplicate();
            knockbackModel.lifespan *= 2;
            knockbackModel.heavyMultiplier *= 2;
            knockbackModel.lightMultiplier *= 3;
            knockbackModel.moabMultiplier *= 2;
            projectileModel.AddBehavior(knockbackModel);
            projectileModel_.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Ceramic", "Ceramic", 1, 5, false, false));
            projectileModel_.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Fortified", "Fortified", 1, 5, false, false));
            projectileModel_.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moab", "Moab", 1, 15, false, false));
            projectileModel.UpdateCollisionPassList();
        }
    }
}
