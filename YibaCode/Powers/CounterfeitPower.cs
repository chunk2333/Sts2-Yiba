using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Yiba.YibaCode.Powers;




public class CounterfeitPower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<SovereignBlade>(), HoverTipFactory.FromKeyword(CardKeyword.Ethereal), HoverTipFactory.FromKeyword(CardKeyword.Exhaust)];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {

        if (cardPlay.Card.Owner != Owner.Player)
        {
            return;
        }

        if (cardPlay.Card is SovereignBlade)
        {

            if (cardPlay.Card.Keywords.Contains(CardKeyword.Ethereal) &&
                cardPlay.Card.Keywords.Contains(CardKeyword.Exhaust))
            {
                MainFile.Logger.Info("赝品能力：禁止打出赝品版的君王之剑");
                return;
            }

            for (int i = 0; i < Amount; i++)
            {
                CardModel faker = cardPlay.Card.CreateClone();
                faker.SetToFreeThisTurn();
                CardCmd.ApplyKeyword(faker, CardKeyword.Exhaust, CardKeyword.Ethereal);
                Flash();
                await CardPileCmd.AddGeneratedCardToCombat(faker, PileType.Hand, Owner.Player);
            }
            
            
        }

        
    }
}