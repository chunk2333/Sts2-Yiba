using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Runs;

namespace Yiba.YibaCode.Patchs;



[HarmonyPatch(typeof(Hook), nameof(Hook.BeforeCombatStart))]
public class ComBatStart
{
    static void Postfix(IRunState runState, ICombatState? combatState)
    {
        MainFile.Logger.Info("开始准备战斗");
        OstyDieCount.OstyDieCount_Now = 0;
    }
}