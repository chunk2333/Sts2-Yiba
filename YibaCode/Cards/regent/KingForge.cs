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
using Yiba.YibaCode.Powers;

// 王铸剑
namespace Yiba.YibaCode.Cards;

[Pool(typeof(RegentCardPool))]
public class KingForge() : YibaCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ForgeVar(15)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromForge();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        //await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.TriggerAnim(Owner.Creature, "PowerUp", Owner.Character.PowerUpAnimDelay);
        await ForgeCmd.Forge(DynamicVars.Forge.IntValue, Owner, this);
        await PowerCmd.Apply<KingForgePower>(choiceContext, Owner.Creature, 1m, base.Owner.Creature, this);
        
        var piles = new[]
        {
            PileType.Hand,
            PileType.Draw,
            PileType.Discard,
            PileType.Exhaust
        };

        foreach (var pileType in piles)
        {
            foreach (var c in pileType.GetPile(Owner).Cards)
            {
                if (c is SovereignBlade)
                {
                    c.UpgradeInternal();
                }
            }
        }
        
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}