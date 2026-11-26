// using HarmonyLib;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Entities.Creatures;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Models;
// using MegaCrit.Sts2.Core.Models.Powers;
// using Yiba.YibaCode.Powers;
//
// namespace Yiba.YibaCode.Patchs;
//
// [HarmonyPatch(
//     typeof(PowerCmd),
//     "Apply",
//     new Type[] {
//         typeof(PlayerChoiceContext),
//         typeof(PowerModel),
//         typeof(Creature),
//         typeof(decimal),
//         typeof(Creature),
//         typeof(CardModel),
//         typeof(bool)
//     }
// )]
// public class PerfectTimingPowerPatch
// {
//     static void Postfix(
//         PlayerChoiceContext choiceContext,
//         PowerModel power,
//         Creature target,
//         decimal amount,
//         Creature applier,
//         CardModel cardSource,
//         bool silent)
//     {
//         
//         if (power == null || target == null || applier == null)
//             return;
//         
//         if (!applier.IsPlayer)
//             return;
//         
//         
//         
//         if (power is not WeakPower)
//             return;
//         if (!applier.HasPower<PerfectTimingPower>())
//         {
//             return;
//         }
//         _ = PowerCmd.Apply<VulnerablePower>(
//             choiceContext,
//             target,
//             applier.GetPowerAmount<PerfectTimingPower>(),
//             applier,
//             cardSource,
//             silent
//         );
//     }
// }