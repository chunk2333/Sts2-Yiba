using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Characters;

namespace Yiba.YibaCode.Powers;

//为你而战 - Power
public class FightForYouPower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new SummonVar(Amount))];

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == Owner.Side)
        {
            int block = Owner.Block;
            Flash();
            await CreatureCmd.TriggerAnim(Owner, Necrobinder.GetSummonAnimIfApplicable(Owner.Player.Character), Necrobinder.GetSummonDelayIfApplicable(Owner.Player.Character));
            await OstyCmd.Summon(choiceContext, Owner.Player, Amount * block, this);
        }
    }
}