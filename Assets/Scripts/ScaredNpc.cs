using System;
using UnityEngine;

public class ScaredNpc : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rigidBody;
    public GameObject player;

    private NpcState _state;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerActor>().gameObject;
        _state = new IdleState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _state.Update(this);
    }

    public void ChangeState(NpcState newState)
    {
        _state.Exit(this);
        _state = newState;
        _state.Enter(this);
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    private bool PlayerToRight()
    {
        return player.transform.position.x > transform.position.x;
    }

    public void MoveAwayFromPlayer(float force)
    {
        var horizontalUnitForce = PlayerToRight() ? -transform.right : transform.right;
        rigidBody.AddForce(horizontalUnitForce * force);
    }

    public void FaceAwayFromPlayer()
    {
        var newScale = transform.localScale;
        newScale.x = Math.Abs(newScale.x) * (PlayerToRight() ? -1 : 1);
        transform.localScale = newScale;
    }
}
