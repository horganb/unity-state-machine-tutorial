public class IdleState : NpcState
{
    public override void Update(ScaredNpc npc)
    {
        if (npc.DistanceFromPlayer() < 2f)
            npc.ChangeState(new AlertedState());
    }
}