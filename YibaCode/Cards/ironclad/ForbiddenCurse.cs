using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Yiba.YibaCode.Cards;
using Yiba.YibaCode.Patchs;
using Yiba.YibaCode.Powers;

namespace Yiba.YibaCode.Cards;

[Pool(typeof(IroncladCardPool))]
public class ForbiddenCurse() : YibaCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(2), new PowerVar<ForbiddenCursePower>(2)];
    
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.SingleplayerOnly;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [EnergyHoverTip];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public int Amount;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, base.Owner);
        await PowerCmd.Apply<ForbiddenCursePower>(choiceContext, Owner.Creature, DynamicVars["ForbiddenCursePower"].BaseValue, Owner.Creature, this);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        
        if (cardPlay.Card.Owner != Owner)
        {
            return;
        }
        
        bool hasCard =  false;

        foreach (var c in PileType.Exhaust.GetPile(Owner).Cards)
        {
            if (c is ForbiddenCurse)
            {
                hasCard =  true;
                break;
            }
        }

        // int cardNum = 0;
        //
        // foreach (var c in PileType.Deck.GetPile(Owner).Cards)
        // {
        //     if (c is ForbiddenCurse)
        //     {
        //         cardNum += 1;
        //     }
        // }
        


        // bool hasPower = false;
        //
        // foreach (var p in Owner.Creature.Powers)
        // {
        //     if (p is ForbiddenCursePower)
        //     {
        //         hasPower = true;
        //     }
        // }
        
        
        
        
        
        
        
        
        if (cardPlay.Card.Type == CardType.Attack && hasCard)
        {
            
            if (PlayCard.PlayedAttackCardAmountForbiddenCurse >= 2)
            {
                CardModel card = CreateClone();
                
                
                
                
                var cards = PileType.Exhaust.GetPile(Owner).Cards
                    .Where(c => c is ForbiddenCurse)
                    .ToList();

                if (cards.Count <= 0)
                {
                    PlayCard.PlayedAttackCardAmountForbiddenCurse = 0;
                    return;
                }
                //await Task.Yield();
                
                
                await CardPileCmd.Add(cards, PileType.Hand);
                
                
                
                
                
                //CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true), 1.2f);
                PlayCard.PlayedAttackCardAmountForbiddenCurse = 0;
            }
        }
    }

    protected override void OnUpgrade()
    {

    }
}