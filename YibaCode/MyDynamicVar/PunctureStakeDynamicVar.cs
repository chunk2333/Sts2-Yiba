using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Yiba.YibaCode.MyDynamicVar;

public class PunctureStakeDynamicVar : DynamicVar
{
    // 在描述中用作占位符的键，推荐添加前缀避免撞车
    public const string Key = "PunctureStake";
    // 本地化键，这里设置为大写的Key，也就是"TEST-LEECH"
    public static readonly string LocKey = Key.ToUpperInvariant();

    public PunctureStakeDynamicVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip(LocKey);
    }
}