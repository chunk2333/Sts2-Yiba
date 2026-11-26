using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
//王不惑
namespace Yiba.YibaCode.Cards;

[Pool(typeof(RegentCardPool))]
public class UnshakenKing() : YibaCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override int CanonicalStarCost => 2;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, CardKeyword.Retain];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        foreach (PowerModel power in Owner.Creature.Powers.ToList())
        {
            if (power.Type == PowerType.Debuff)
            {
                
                power.RemoveInternal();
            }
        }
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}