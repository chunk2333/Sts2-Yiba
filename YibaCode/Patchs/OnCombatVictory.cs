using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace Yiba.YibaCode.Patchs;

[HarmonyPatch(typeof(Hook), nameof(Hook.AfterCombatVictory))]
public class OnCombatVictory
{
    static void Postfix(IRunState runState, CombatState? combatState, CombatRoom room)
    {
        MainFile.Logger.Info("战斗胜利了...");
        PlayCard.Set_Start();
    }
}