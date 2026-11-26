using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Yiba.YibaCode.Cards;

namespace Yiba.YibaCode.Cards.defect;
//临阵磨枪
[Pool(typeof(ColorlessCardPool))]
public class Preparation() : YibaCard(1, CardType.Skill,
    CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards.Where((CardModel c) => c.IsUpgradable))
        {
            if (card.Type == CardType.Attack)
                CardCmd.Upgrade(card);
            
        }
        foreach (CardModel card in PileType.Draw.GetPile(Owner).Cards.Where((CardModel c) => c.IsUpgradable))
        {
            if (card.Type == CardType.Attack)
                CardCmd.Upgrade(card);
            
        }
        foreach (CardModel card in PileType.Discard.GetPile(Owner).Cards.Where((CardModel c) => c.IsUpgradable))
        {
            if (card.Type == CardType.Attack)
                CardCmd.Upgrade(card);
            
        }
        foreach (CardModel card in PileType.Exhaust.GetPile(Owner).Cards.Where((CardModel c) => c.IsUpgradable))
        {
            if (card.Type == CardType.Attack)
                CardCmd.Upgrade(card);
            
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}