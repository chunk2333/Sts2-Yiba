using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace Yiba.YibaCode.Powers;

//同步者·奥斯提 - Power
public class SynchronizerOstyPower: YibaPower
{
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
        // if (cardPlay.Card.Owner != Owner.Player || cardPlay.Card.Type != CardType.Attack)
        // {
        //     return;
        // }
        
        if (!Osty.CheckMissingWithAnim(Owner.Player) && cardPlay.Card.Type == CardType.Attack)
        {

            for (int i = 0; i < Amount; i++)
            {
                
                if (cardPlay.Target == null || cardPlay.Target.IsDead || cardPlay.Target.CurrentHp <=0)
                {
                    Creature target = Owner.Player.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
                    await DamageCmd.Attack(Owner.Player.Osty.CurrentHp).FromOsty(Owner.Player.Osty, null, cardPlay).Targeting(target)
                        .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                        .Execute(choiceContext);
                }
                else
                {
                    await DamageCmd.Attack(Owner.Player.Osty.CurrentHp).FromOsty(Owner.Player.Osty, null, cardPlay).Targeting(cardPlay.Target)
                        .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                        .Execute(choiceContext);
                }
                
            }
            
            
        }
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == Owner.Side)
        {
            await PowerCmd.Remove(this);
        }
    }
}