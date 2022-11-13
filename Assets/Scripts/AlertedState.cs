using UnityEngine;

public class AlertedState : NpcState
{
    private static readonly int Midair = Animator.StringToHash("IsJumping");
    private float _scaredTimer;

    public override void Update(ScaredNpc npc)
    {
        _scaredTimer += Time.fixedDeltaTime;
        if (_scaredTimer >= 3f)
            npc.ChangeState(npc.DistanceFromPlayer() < 3f ? new FleeState() : new IdleState());
    }

    public override void Enter(ScaredNpc npc)
    {
        _scaredTimer = 0;
        npc.animator.SetBool(Midair, true);
        npc.rigidBody.AddForce(npc.transform.up * 20f, ForceMode2D.Impulse);
    }

    public override void Exit(ScaredNpc npc)
    {
        npc.animator.SetBool(Midair, false);
    }
}