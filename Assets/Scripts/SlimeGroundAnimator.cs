using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundAnimator : MonoBehaviour
{
    private SlimeGround slimeGround;

    private Animator animator;

    private const string ANIMATOR_DESTROY_PARAMETER = "isDestroy";
    private void Awake()
    {
        animator = GetComponent<Animator>();
        slimeGround = GetComponent<SlimeGround>();
    }
    private void Start()
    {
        slimeGround.OnDestroyed += SlimeGround_OnDestroyed;
    }

    private void SlimeGround_OnDestroyed(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ANIMATOR_DESTROY_PARAMETER);
    }
}
