using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy enemyDetails { get; set; }
    public Vector2 velocity;
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }
    private void Start()
    {
        //init start state for enemy();
    }
}
