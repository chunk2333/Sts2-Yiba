using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Yiba.YibaCode.Tools;

namespace Yiba.YibaCode.Powers;


public class AgilePosturePower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (YibaHelper.IsSlayCard(cardPlay.Card))
        {
            Flash();
            PlayerCmd.GainEnergy(Amount, Owner.Player);
        }
    }
}