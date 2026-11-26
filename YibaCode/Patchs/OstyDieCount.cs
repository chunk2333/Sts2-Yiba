using HarmonyLib;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace Yiba.YibaCode.Patchs;



[HarmonyPatch(typeof(Hook), nameof(Hook.ShouldDie))]
public static class OstyDieCount
{
    public static int OstyDieCount_Now = 0;
    static void Postfix(ICombatState combatState, Creature creature, ref bool __result)
    {


        if (creature.ModelId.ToString() == "MONSTER.OSTY")
        {
            
            OstyDieCount_Now += 1;
            MainFile.Logger.Info($"当前奥斯提死亡次数: {OstyDieCount_Now}");
        }
        else
        {
            MainFile.Logger.Info($"怪物死亡: {creature.ModelId.ToString()}");
        }
        
        
    }
}