using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public bool isBroken { get; private set; }

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null) Debug.Log("No anim on " + gameObject);
    }

    // Method that calls the Break animation and sets 
    // isBroken to true when the animation is over
    public void Break()
    {
        anim.SetTrigger("Break");
        StartCoroutine(SetBrokenBoolean(true));
    }

    // Method that calls the Fix animation and sets 
    // isBroken to false when the animation is over
    public void Fix()
    {
        anim.SetTrigger("Fix");
        StartCoroutine(SetBrokenBoolean(false));
    }

    // Set isBroken value after the current animation finishes
    private IEnumerator SetBrokenBoolean(bool value)
    {
        while (!FinishedAnimation()) { }

        isBroken = value;

        StopCoroutine("SetBrokenBoolean");

        yield return null;
    }

    public bool FinishedAnimation() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f;

    public bool FinishedAnimation(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f) return true;

        else return false;

    }
}
