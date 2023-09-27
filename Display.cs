﻿using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissileMonkeyMod
{
    internal class MissileDisplay : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTowerFromId("BombShooter-020").GetAttackModel().weapons[0].projectile.display.GUID;
    }
    internal class Missile2Display : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTowerFromId("BombShooter-030").GetAttackModel().weapons[0].projectile.display.GUID;
    }
    internal class Missile3Display : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTowerFromId("BombShooter-050").GetAttackModel().weapons[0].projectile.display.GUID;
    }
    internal class MissileLauncherDisplay : ModTowerDisplay<MissileMonkey>
    {
        public override string BaseDisplay => Game.instance.model.GetTowerFromId("SniperMonkey-004").display.GUID;
        public override bool UseForTower(int[] tiers)
        {
            return tiers.Max() >= 0;
        }
    }
}
