using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Yiba.YibaCode.Powers;


public class CognitiveImpairmentPower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Single;

    // public override Task BeforeCardPlayed(CardPlay cardPlay)
    // {
    //
    //     if (cardPlay.Card.Type == CardType.Attack)
    //     {
    //
    //         cardPlay.Card.Tags.Append(CardTag.Strike);
    //     }
    //     
    //     return base.BeforeCardPlayed(cardPlay);
    // }
    //
    // public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    // {
    //     if (applier == Owner)
    //     {
    //         foreach (var p in Owner.Player.Piles)
    //         {
    //             foreach (var card in p.Cards)
    //             {
    //                 if (!card.Tags.Contains(CardTag.Strike) && card.Type == CardType.Attack)
    //                 {
    //                     card.tags
    //                     
    //                     card.Tags = card.Tags.Append(CardTag.Strike);
    //                 }
    //             }
    //         }
    //     }
    // }
    //
    // public override Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    // {
    //     if (creator != null && creator.Creature == Owner)
    //     {
    //         if (!card.Tags.Contains(CardTag.Strike) && card.Type == CardType.Attack)
    //         {
    //             card.Tags.Append(CardTag.Strike);
    //         }
    //         Flash();
    //         
    //     }
    //     return base.AfterCardGeneratedForCombat(card, creator);
    // }
    
    
}