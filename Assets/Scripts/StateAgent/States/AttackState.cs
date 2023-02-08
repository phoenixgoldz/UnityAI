using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackState : State
{
    private float timer;

    public AttackState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.movement.Stop();
        owner.animator.SetTrigger("Attack");

        AnimationClip[] clips = owner.animator.runtimeAnimatorController.animationClips;

        AnimationClip clip = clips.FirstOrDefault<AnimationClip>(clip => clip.name == "Mutant Swiping");
        timer = (clip != null) ? clip.length : 1;
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            owner.stateMachine.StartState(nameof(ChaseState));
        }
    }
} 















