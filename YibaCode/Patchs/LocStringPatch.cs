using HarmonyLib;
using System.Text;
using MegaCrit.Sts2.Core.Localization;

namespace Yiba.YibaCode.Patchs;

[HarmonyPatch(typeof(LocString), nameof(LocString.GetFormattedText))]
public class LocStringPatch
{
    // 用 HashSet 提升查找效率
    private static readonly HashSet<char> targets = new()
    {
        '觉','绝','决','爵','倔','掘','诀','珏','嚼'
    };

    static void Postfix(ref string __result)
    {
        if (string.IsNullOrEmpty(__result))
            return;

        __result = ReplaceText(__result);
    }

    private static string ReplaceText(string input)
    {
        StringBuilder sb = new StringBuilder(input.Length);

        foreach (char c in input)
        {
            // 核心逻辑：命中就替换
            sb.Append(targets.Contains(c) ? '撅' : c);
            //MainFile.Logger.Info($"把{c}替换为 撅 .");
        }

        return sb.ToString();

    }
}