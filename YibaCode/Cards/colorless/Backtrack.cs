using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using Yiba.YibaCode.Cards;
using Yiba.YibaCode.Patchs;

//回溯
namespace Yiba.YibaCode.Cards;

[Pool(typeof(ColorlessCardPool))]
public class Backtrack() : YibaCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override void AddExtraArgsToDescription(LocString description)
    {
        base.AddExtraArgsToDescription(description);
        //var last = PlayCard.PlayedCard;
        var lastName = PlayCard.PlayedCardName;

        description.Add("LastCard", lastName);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        
        
        CardModel c = PlayCard.PlayedCard;
        // if (c.Type == CardType.Power)
        //     CombatState.CreateCard(c.CanonicalInstance, Owner);
        Creature target = PlayCard.PlayedTarget;
        if (!CombatManager.Instance.IsOverOrEnding)
        {
            if (c.TargetType == TargetType.AnyEnemy)
            {
                if (target == null || target.IsDead || target.CurrentHp <=0)
                {
                    target = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
                }

                if (c.Type == CardType.Power)
                {
                    await CardCmd.AutoPlay(choiceContext, c.CreateClone(), target);
                    return;
                }
                    
                
                await CardCmd.AutoPlay(choiceContext, c, null);
                
                
                
            }
            else
            {
                if (c.Type == CardType.Power)
                {
                    await CardCmd.AutoPlay(choiceContext, c.CreateClone(), target);
                    return;
                }
                    
                
                await CardCmd.AutoPlay(choiceContext, c, null);
            }
        }
        
        
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}