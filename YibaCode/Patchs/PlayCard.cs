using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Yiba.YibaCode.Cards;
using Yiba.YibaCode.Tools;

namespace Yiba.YibaCode.Patchs;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.OnPlayWrapper))]
public static class PlayCard
{

    public static CardModel PlayedCard = ModelDb.Card<Shiv>();

    public static string PlayedCardName = "无";

    public static PlayerChoiceContext LastChoiceContext = null;
    
    public static Creature PlayedTarget = null;

    public static int PlayedAttackCardAmount = 0;
    
    public static int PlayedAttackCardAmountForbiddenCurse = 0;
    
    private static bool hasCardForbiddenCurse = false;

    public static void Set_Start()
    {
        PlayedCard  = ModelDb.Card<Shiv>();
        PlayedCardName = "无";
        PlayedTarget = null;
        PlayedAttackCardAmount = 0;
        PlayedAttackCardAmountForbiddenCurse = 0;
        hasCardForbiddenCurse = false;
    }
    
    
    // Postfix 在原方法执行完成后调用
    static async void Postfix(
        CardModel __instance,                    // 这里就是方法的实例
        PlayerChoiceContext choiceContext,
        Creature? target,
        bool isAutoPlay,
        ResourceInfo resources,
        bool skipCardPileVisuals = false
    )


    {

        // if (!LocalContext.IsMe(__instance.Owner))
        //     return;
        
        
        
        if (__instance is Backtrack)
        {
            MainFile.Logger.Info($"当前已经打完卡牌: {__instance.Title}，不进行记录");
            //await Task.Yield();
            return;
        }

        PlayedCard = __instance;
        PlayedCardName = __instance.Title;
        PlayedTarget = target;
        LastChoiceContext =  choiceContext;

        MainFile.Logger.Info($"当前已经打完卡牌: {__instance.Title}");
        
        
        
        
        if (!LocalContext.IsMe(__instance.Owner))
        {
            //await Task.Yield();
            return;
        }


        foreach (var c in PileType.Exhaust.GetPile(__instance.Owner).Cards)
        {
            if (c is ForbiddenCurse)
            {
                hasCardForbiddenCurse = true;
                break;
            }
        }



        if (__instance.Type == CardType.Attack)
        {
            if (hasCardForbiddenCurse)
            {
                PlayedAttackCardAmountForbiddenCurse += 1;
            }

            PlayedAttackCardAmount += 1;

        }



        

        // 如果你想异步处理，可以直接 await
        //await Task.Yield(); // 示例异步操作
    }
}
