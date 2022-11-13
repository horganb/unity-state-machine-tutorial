using System;
using UnityEngine;

public class FleeState : NpcState
{
    private static readonly int IsMoving = Animator.StringToHash("IsRunning");

    public override void Update(ScaredNpc npc)
    {
        if (npc.DistanceFromPlayer() > 6f)
        {
            npc.ChangeState(new IdleState());
            return;
        }
        
        npc.MoveAwayFromPlayer(40f);
        npc.FaceAwayFromPlayer();
    }

    public override void Enter(ScaredNpc npc)
    {
        npc.animator.SetBool(IsMoving, true);
    }

    public override void Exit(ScaredNpc npc)
    {
        npc.animator.SetBool(IsMoving, false);
    }
}