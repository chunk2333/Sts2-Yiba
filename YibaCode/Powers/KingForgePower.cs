using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Yiba.YibaCode.Powers;


public class KingForgePower : YibaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromForge();

    public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        //MainFile.Logger.Info($"抽完卡牌：{card.Title}");
        
        if (card.Owner != Owner.Player)
        {
            return Task.CompletedTask;
        }

        

        if (card is SovereignBlade && card.CurrentUpgradeLevel < card.MaxUpgradeLevel)
        {
            Flash();
            card.UpgradeInternal();
        }

        return Task.CompletedTask;
    }
}