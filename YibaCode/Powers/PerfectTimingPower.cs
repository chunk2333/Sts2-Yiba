using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Yiba.YibaCode.Powers;

public class PerfectTimingPower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.ForEnergy(this), HoverTipFactory.Static(StaticHoverTip.Block)];
    
    private class Data
    {
        public int blockGainCount;
    }
    
    protected override object InitInternalData()
    {
        return new Data();
    }

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        
        if (cardSource == null)
            return;

        if (creature != Owner)
            return;

        if (amount <= 0)
            return;

        Data data = GetInternalData<Data>();

        data.blockGainCount++;

        if (data.blockGainCount >= Amount)
        {
            data.blockGainCount = 0;

            Flash();

            // 获得1点能力
            await PlayerCmd.GainEnergy(1, Owner.Player);
        }

        InvokeDisplayAmountChanged();
    }
    public override int DisplayAmount =>
        Amount - GetInternalData<Data>().blockGainCount;
}