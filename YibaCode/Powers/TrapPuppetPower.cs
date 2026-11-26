using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Yiba.YibaCode.Powers;


public class TrapPuppetPower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<PoisonPower>()];

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == Owner.Side)
        {
            Flash();
            
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                NCreature nCreature = NCombatRoom.Instance?.GetCreatureNode(hittableEnemy);
                if (nCreature != null)
                {
                    NGaseousImpactVfx child = NGaseousImpactVfx.Create(nCreature.VfxSpawnPosition, new Color("83eb85"));
                    NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(child);
                }
            }

            int poisonCount = Owner.Block * Amount;
            

            if (Owner.HasPower<ReboundMyPower>())
            {
                poisonCount -= 999999999;
            }
            
            await PowerCmd.Apply<PoisonPower>(choiceContext, CombatState.HittableEnemies, poisonCount, Owner, null);
            await PowerCmd.Remove(this);
        }
    }
    
}