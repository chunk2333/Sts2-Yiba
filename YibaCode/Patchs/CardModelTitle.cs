using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Yiba.YibaCode.Cards;

namespace Yiba.YibaCode.Patchs;


[HarmonyPatch(typeof(CardModel), nameof(CardModel.Title), MethodType.Getter)]
public class CardModelTitle
{
    /// <summary>
    /// Prefix：在原函数执行前运行
    /// </summary>
    /// <param name="__instance">当前实例（也就是 this）</param>
    /// <param name="__result">返回值（用于提前返回）</param>
    /// <returns>false = 跳过原函数，true = 继续执行原函数</returns>
    public static bool Prefix(CardModel __instance, ref string __result)
    {
        // 👉 这里的 __instance 就是 this
        var card = __instance;

        // ===== 示例2：自定义拦截逻辑 =====
        // 比如：只拦截 “王不惑”
        if (card is SovereignBlade) // 你的卡牌类
        {
            
            if (card.Keywords.Contains(CardKeyword.Ethereal) &&
                card.Keywords.Contains(CardKeyword.Exhaust))
            {
                
                string baseTitle = card.TitleLocString.GetFormattedText();

                if (card.IsUpgraded)
                {
                    baseTitle += '+';
                }

                if (card.MaxUpgradeLevel > 1)
                {
                    baseTitle += $"+{card.MaxUpgradeLevel}";
                }

                if (!baseTitle.StartsWith("赝品-"))
                {
                    __result = $"赝品-{baseTitle}";
                }
                else
                {
                    __result = baseTitle;
                }

                return false;
                
            }
            
        }
        
        return true;
    }
}