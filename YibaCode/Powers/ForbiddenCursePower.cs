using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Yiba.YibaCode.Cards;

namespace Yiba.YibaCode.Powers;

public class ForbiddenCursePower : YibaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private int upgradedStack = 0;
    private int noUpgradedStack = 0;

    // =========================
    // 获得层数
    // =========================

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (cardSource == null || !(power is ForbiddenCursePower))
        {
            return;
        }
        
        // ⚠️ 核心：cardSource 可能为 null（怪物/系统）
        bool isUpgraded = cardSource?.IsUpgraded == true;

        if (isUpgraded)
        {
            upgradedStack += (int)amount;
        }
        else
        {
            noUpgradedStack += (int)amount;
        }

        MainFile.Logger.Info(
            $"[禁制诅咒] 当前层数：升级={upgradedStack} 未升级={noUpgradedStack} 总={Amount}");
    }
    

    // =========================
    // 回合开始：清理未升级层
    // =========================

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        
       

        if (noUpgradedStack <= 0)
            return;

        MainFile.Logger.Info($"[禁制诅咒] 移除未升级层数：{noUpgradedStack}");
        

        await PowerCmd.ModifyAmount(choiceContext, this,  -noUpgradedStack, Owner, null);

        noUpgradedStack = 0;
        
    }
    

    // =========================
    // 打攻击牌：消耗层数
    // =========================
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
            return;

        if (cardPlay.Card is ForbiddenCurse)
            return;

        if (cardPlay.Card.Type != CardType.Attack)
            return;

        if (Amount <= 0)
            return;

        Flash();

        // 优先消耗 未升级层（你也可以改成优先升级层）
        if (noUpgradedStack > 0)
        {
            noUpgradedStack--;
        }
        else if (upgradedStack > 0)
        {
            upgradedStack--;
        }

        await PowerCmd.Decrement(this);

        MainFile.Logger.Info(
            $"[禁制诅咒] 打牌后剩余：升级={upgradedStack} 未升级={noUpgradedStack} 总={Amount}");
    }

    // =========================
    // 伤害倍率
    // =========================

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay)
    {
        if (!props.IsPoweredAttack_())
            return 1m;

        if (dealer != Owner && !Owner.Pets.Contains<Creature>(dealer))
            return 1m;

        if (target == null)
            return 1m;

        return 2m;
    }

    // public override decimal ModifyDamageMultiplicative(
    //     Creature? target,
    //     decimal amount,
    //     ValueProp props,
    //     Creature? dealer,
    //     CardModel? cardSource)
    // {
    //     
    // }
}