using HarmonyLib;
using System.Text;
using MegaCrit.Sts2.Core.Localization;

namespace Yiba.YibaCode.Patchs;

[HarmonyPatch(typeof(LocString), nameof(LocString.GetRawText))]
public class LocStringRawPatch
{
    private static readonly HashSet<char> targets = new()
    {
        '觉','绝','决','爵','倔','掘','诀','珏','嚼', '抉'
    };

    static void Postfix(ref string __result)
    {
        if (string.IsNullOrEmpty(__result))
            return;

        __result = ReplaceText(__result);
    }

    private static string ReplaceText(string input)
    {
        var sb = new StringBuilder(input.Length);

        foreach (char c in input)
        {
            sb.Append(targets.Contains(c) ? '撅' : c);
        }

        return sb.ToString();
    }
}