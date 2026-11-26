using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Yiba.YibaCode.Cards;

namespace Yiba.YibaCode.Cards;
//取舍
[Pool(typeof(SilentCardPool))]
public class TradeOff() : YibaCard(0, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var choiceContext1 = choiceContext;
        var choiceContext2 = choiceContext;
        IEnumerable<CardModel> cards = await CardSelectCmd.FromHandForDiscard(choiceContext, Owner,
            new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, DynamicVars.Cards.IntValue), null, this);
        
        await CardCmd.Discard(choiceContext, cards);

        CardModel cardModel = await CardSelectCmd.FromHandForUpgrade(choiceContext1, Owner, this);
        if (cardModel != null)
        {
            CardCmd.Upgrade(cardModel);
        }

       
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}