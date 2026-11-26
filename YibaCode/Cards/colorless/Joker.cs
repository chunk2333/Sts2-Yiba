using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Yiba.YibaCode.Cards;
using Yiba.YibaCode.Patchs;

namespace Yiba.YibaCode.Cards;


[Pool(typeof(ColorlessCardPool))]
public class Joker() : YibaCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        
        
        
        var allCards = new List<CardModel>();

        var poolTypes = ModelDb.AllCardPools;

        foreach (var pool in poolTypes)
        {
            var cards = pool.GetUnlockedCards(
                Owner.UnlockState,
                Owner.RunState.CardMultiplayerConstraint
            );
            allCards.AddRange(cards);
        }

        var result = allCards
            .Where(c =>
                c.Rarity != CardRarity.Curse &&
                c.Rarity != CardRarity.Quest &&
                c != this &&
                c.Type != CardType.Status)
            .DistinctBy(c => c.Id) // ⭐建议加
            .ToList();
        
        

        for (int i = 0; i < DynamicVars.Cards.BaseValue; i++)
        {
            List<CardModel> cards = CardFactory.GetDistinctForCombat(Owner, result, 1, Owner.RunState.Rng.CombatCardGeneration).ToList();
            CardModel c = cards[0];
            c.AddKeyword(CardKeyword.Exhaust);
            Creature target = null;
        
            if (!CombatManager.Instance.IsOverOrEnding)
            {
                if (c.TargetType == TargetType.AnyEnemy)
                {
                    if (target == null || target.IsDead || target.CurrentHp <=0)
                    {
                        target = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
                    }
                
                    await CardCmd.AutoPlay(choiceContext, c, target);
                }
                else
                {
                    await CardCmd.AutoPlay(choiceContext, c, null);
                }
            }
        }
        
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1m);
        EnergyCost.UpgradeBy(-1);
    }
}