using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using Yiba.YibaCode.Cards;
using Yiba.YibaCode.MyDynamicVar;

namespace Yiba.YibaCode.Cards;

//穿刺木桩
[Pool(typeof(ColorlessCardPool))]
public class PunctureStake() : YibaCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(20m, ValueProp.Move), new DynamicVar("PunctureStakeDynamicVar", 5)];
    
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    
    // private int killPercentage = 5;
    //
    // protected override void AddExtraArgsToDescription(LocString description)
    // {
    //     base.AddExtraArgsToDescription(description);
    //     //var last = PlayCard.PlayedCard;
    //
    //     description.Add("killPercentage", killPercentage);
    // }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");

        // ===== 当前血量（攻击前）=====
        int currentHp = play.Target.CurrentHp;
        int maxHp = play.Target.MaxHp;

        // 已损失血量百分比
        decimal lostHpPercent = (maxHp - currentHp) * 100m / maxHp;

        // 每1%血对应概率（5 / 7）
        decimal baseChance = DynamicVars["PunctureStakeDynamicVar"].BaseValue;

        // 最终斩杀概率
        decimal killChance = lostHpPercent * baseChance / 10 + baseChance;
        
        MainFile.Logger.Info($"[穿刺木桩] 最终斩杀概率斩杀概率: {killChance:F2}%");
        
        if (killChance > 100)
            killChance = 100;

        // RNG 0~99
        int rng = Owner.RunState.Rng.CombatCardGeneration.NextInt(100);

        // ===== 日志 =====
        MainFile.Logger.Info($"[穿刺木桩] 当前血量: {currentHp}/{maxHp}");
        MainFile.Logger.Info($"[穿刺木桩] 已损失血量: {lostHpPercent:F2}%");
        MainFile.Logger.Info($"[穿刺木桩] 斩杀概率: {killChance:F2}%");
        MainFile.Logger.Info($"[穿刺木桩] 随机数: {rng}");

        // ===== 是否斩杀 =====
        bool isKill = rng < killChance;

        if (isKill)
        {
            MainFile.Logger.Info($"[穿刺木桩] >>> 触发斩杀（使用超大伤害）");

            DamageVar bigDamage = new DamageVar(9999999999m, ValueProp.Move);

            await DamageCmd.Attack(bigDamage.BaseValue)
                .FromCard(this, play)
                .Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }
        else
        {
            MainFile.Logger.Info($"[穿刺木桩] 未触发斩杀（普通伤害）");

            decimal damage = DynamicVars.Damage.BaseValue;

            await DamageCmd.Attack(damage)
                .FromCard(this, play)
                .Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        
        DynamicVars.Damage.UpgradeValueBy(5m);
        DynamicVars["PunctureStakeDynamicVar"].UpgradeValueBy(2);
        
    }
}