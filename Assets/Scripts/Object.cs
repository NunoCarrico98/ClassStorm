using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public bool IsBroken { get; private set; }

    private Animator anim;

    private Coroutine coroutine;

    private GameLoop gameloop;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameloop = FindObjectOfType<GameLoop>();
        if (anim == null) Debug.Log("No anim on " + gameObject);
    }

    // Method that calls the Break animation and sets 
    // isBroken to true when the animation is over
    public void Break()
    {
        anim.SetTrigger("Break");
        coroutine = StartCoroutine(SetBrokenBoolean(true));
    }

    // Method that calls the Fix animation and sets 
    // isBroken to false when the animation is over
    public void Fix()
    {
        anim.SetTrigger("Fix");
        gameloop.AddObjectToList(gameObject.GetComponent<Object>());
        coroutine = StartCoroutine(SetBrokenBoolean(false));
    }

    // Set isBroken value after the current animation finishes
    private IEnumerator SetBrokenBoolean(bool value)
    {
        yield return new WaitForSeconds(0.2f);
        while (!IsAnimationFinished()) { yield return null; }

        IsBroken = value;
        Debug.Log("Set to: " + value);

        if(coroutine != null) StopCoroutine(coroutine);

        yield return null;
    }

    public bool IsAnimationFinished() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f;

    public bool FinishedAnimation(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f) return true;

        else return false;

    }
}
