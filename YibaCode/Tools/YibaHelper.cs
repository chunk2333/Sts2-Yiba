using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Yiba.YibaCode.Powers;

namespace Yiba.YibaCode.Tools;

public class YibaHelper
{
    public static async Task TakeExtraTurn(Player owner)
    {
        await PowerCmd.Apply<ExtraTurnPower>(new ThrowingPlayerChoiceContext(), owner.Creature, 1m, owner.Creature, null);
        PlayerCmd.EndTurn(owner, canBackOut: false);
    }

    public static bool IsSlayCard(CardModel card)
    {
        foreach (var c in card.Keywords)
        {
            if (c == CardKeyword.Sly)
            {
                return true;
            }
        }
        return false;
    }
}