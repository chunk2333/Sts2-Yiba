using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;

using Yiba.YibaCode.Cards;
//不放弃！
namespace Yiba.YibaCode.Cards;

[Pool(typeof(RegentCardPool))]
public class NeverGiveUp() : YibaCard(3, CardType.Skill,
    CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ForgeVar(50)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromForge();
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await ForgeCmd.Forge(DynamicVars.Forge.IntValue, Owner, this);
        foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards)
        {
            //MainFile.Logger.Info("遍历卡牌：" + card.ToString());

            if (card is SovereignBlade)
            {
                card.SetToFreeThisTurn();
            }

        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Forge.UpgradeValueBy(10);
    }
}