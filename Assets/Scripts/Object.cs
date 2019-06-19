using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public bool IsBroken { get; private set; }

    private RepairSprite sprite;
    private Animator anim;
	private Collider fixCollider;
    private Coroutine coroutine;

    private GameLoop gameloop;

    private void Awake()
    {
        sprite = FindObjectOfType<RepairSprite>();
        anim = GetComponent<Animator>();
        gameloop = FindObjectOfType<GameLoop>();
        if (anim == null) Debug.Log("No anim on " + gameObject);
		fixCollider = GetComponent<Collider>();
		fixCollider.enabled = false;
	}

    // Method that calls the Break animation and sets 
    // isBroken to true when the animation is over
    public void Break()
    {
		anim.SetTrigger("Break");
        coroutine = StartCoroutine(SetBrokenBoolean(true));
		fixCollider.enabled = true;
    }

    // Method that calls the Fix animation and sets 
    // isBroken to false when the animation is over
    public void Fix()
    {
        anim.SetTrigger("Fix");
        sprite.Activate(true, transform);
        gameloop.AddObjectToList(gameObject.GetComponent<Object>());
        coroutine = StartCoroutine(SetBrokenBoolean(false));
    }

    // Set isBroken value after the current animation finishes
    private IEnumerator SetBrokenBoolean(bool value)
    {
        yield return new WaitForSeconds(0.2f);
        while (!IsAnimationFinished()) { yield return null; }

        IsBroken = value;
		if(!value) sprite.Activate(false);

        if (coroutine != null) StopCoroutine(coroutine);

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
