using BaseLib.Extensions;
using HarmonyLib;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using Yiba.YibaCode.Powers;

namespace Yiba.YibaCode.Patchs;
[HarmonyPatch(typeof(CardModel), "get_Tags")]
public class CardModelCtorPatch
{
    
    static readonly AccessTools.FieldRef<CardModel, HashSet<CardTag>> TagsRef =
        AccessTools.FieldRefAccess<CardModel, HashSet<CardTag>>("_tags");
    
    static void Postfix(CardModel __instance)
    {
        if (!LocalContext.IsMe(__instance.Owner))
            return;

        if (!__instance.Owner.HasPower<CognitiveImpairmentPower>())
            return;
        TagsRef(__instance)?.Add(CardTag.Strike);
        //MainFile.Logger.Info($"为卡牌：{__instance.Title} 添加 打击 tag。");
    }
}