using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Yiba.YibaCode.MyDynamicVar;

public class LastPlayCard: DynamicVar
{
    // 在描述中用作占位符的键，推荐添加前缀避免撞车
    public const string Key = "Yiba-LastPlayCard";
    // 本地化键，这里设置为大写的Key，也就是"TEST-LEECH"
    public static readonly string LocKey = Key.ToUpperInvariant();
    
    

    public LastPlayCard() : base(Key, 0)
    {
        this.WithTooltip(LocKey);
    }
}