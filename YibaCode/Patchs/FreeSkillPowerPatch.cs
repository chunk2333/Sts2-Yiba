using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Yiba.YibaCode.Patchs;


[HarmonyPatch(typeof(FreeSkillPower), nameof(FreeSkillPower.BeforeCardPlayed))]
public class FreeSkillPowerPatch
{
    static bool Prefix(CardPlay cardPlay, ref Task __result)
    {
        
        int baseCost = cardPlay.Card.EnergyCost.Canonical;
        int currentCost = cardPlay.Card.EnergyCost.GetWithModifiers(CostModifiers.All);

        bool isModified = currentCost != baseCost;

        if (cardPlay.Card.EnergyCost.GetAmountToSpend() == 0 && !isModified)
        {
            __result = Task.CompletedTask;
            return false;
        }
        
        foreach (var c in cardPlay.Card.Keywords)
        {
            if (c is CardKeyword.Sly)
            {
                // 直接返回
                __result = Task.CompletedTask;
                return false;
            }
        }
        return true;
    }
}