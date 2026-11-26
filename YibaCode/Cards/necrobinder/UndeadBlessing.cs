using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Yiba.YibaCode.Cards;
using Yiba.YibaCode.Patchs;
//亡灵赐福
namespace Yiba.YibaCode.Cards;

[Pool(typeof(NecrobinderCardPool))]
public class UndeadBlessing() : YibaCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    private const string _calculatedEnergyKey = "CalculatedEnergy";


    protected override IEnumerable<IHoverTip> ExtraHoverTips => [EnergyHoverTip];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => (new DynamicVar[4]
    {
        new EnergyVar(0),
        new CalculationBaseVar(0m),
        new CalculationExtraVar(1m),
        new CalculatedVar("CalculatedEnergy").WithMultiplier((CardModel card, Creature? _) => OstyDieCount.OstyDieCount_Now)
    });

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PlayerCmd.GainEnergy(((CalculatedVar)base.DynamicVars["CalculatedEnergy"]).Calculate(play.Target), Owner);
        
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}