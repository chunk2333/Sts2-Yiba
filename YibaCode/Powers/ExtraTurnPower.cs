using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Yiba.YibaCode.Powers;


public class ExtraTurnPower :YibaPower
{
    internal static bool SkipPaelsEyeConsumption;

    protected override bool IsVisibleInternal => false;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.None;

    public override bool ShouldTakeExtraTurn(Player player)
    {
        return player == Owner.Player;
    }

    public override async Task AfterTakingExtraTurn(Player player)
    {
        if (player == Owner.Player)
        {
            SkipPaelsEyeConsumption = true;
            await PowerCmd.Remove(this);
        }
    }
}