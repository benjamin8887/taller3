using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoolCall : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string animationB;

    public void ChangeBoolTrigger(bool value)
    {
        anim.SetBool(animationB, value);
    }

}
