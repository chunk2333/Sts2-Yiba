using HarmonyLib;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Yiba.YibaCode.Patchs;

using HarmonyLib;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;

[HarmonyPatch(typeof(PhantasmalGardener), "GenerateAnimator")]
public static class Patch_GetPower_SkittishSafe
{
    static bool Prefix(PhantasmalGardener __instance, MegaSprite controller, ref CreatureAnimator __result)
    {
        // ⭐ 完全自己构建 Animator（安全版本）

        AnimState animState = new AnimState("idle_loop", isLooping: true);
        AnimState animState2 = new AnimState("buff");
        AnimState animState3 = new AnimState("attack");
        AnimState animState4 = new AnimState("attack_multi");
        AnimState animState5 = new AnimState("hurt_extended");
        AnimState animState6 = new AnimState("hurt");
        AnimState state = new AnimState("die");
        AnimState nextState = new AnimState("block_loop", isLooping: true);
        AnimState animState7 = new AnimState("block_start");
        AnimState animState8 = new AnimState("block_end");
        animState2.NextState = animState;
        animState3.NextState = animState;
        animState4.NextState = animState;
        animState5.NextState = animState;
        animState6.NextState = nextState;
        animState7.NextState = nextState;
        animState8.NextState = animState;
        CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
        creatureAnimator.AddAnyState("Idle", animState);
        creatureAnimator.AddAnyState("Cast", animState2);
        creatureAnimator.AddAnyState("Attack", animState3);
        creatureAnimator.AddAnyState("AttackMulti", animState4);
        creatureAnimator.AddAnyState("Dead", state);
        // ⭐ 安全写法（重点）
        creatureAnimator.AddAnyState(
            "Hit",
            animState6,
            () =>
            {
                var p = __instance.Creature?.GetPower<SkittishPower>();
                return p != null && p.HasGainedBlockThisTurn;
            });

        creatureAnimator.AddAnyState(
            "Hit",
            animState5,
            () => true // fallback，永不崩
        );
        creatureAnimator.AddAnyState("BlockStart", animState7);
        creatureAnimator.AddAnyState("BlockEnd", animState8);

        __result = 	creatureAnimator;

        return false; // ❗ 阻止原方法执行
    }
}
