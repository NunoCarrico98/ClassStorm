using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	private bool canMove;
	private Camera cam;
	private NavMeshAgent agent;
	private Object objectToFix;
    private Coroutine coroutine;

	private void Awake()
	{
		cam = Camera.main;
		agent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		canMove = true;

    }

	// Update is called once per frame
	private void Update()
    {
		CheckForInput();
    }

	private void CheckForInput()
	{
		if (Input.GetMouseButtonDown(0) && canMove)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
				objectToFix = hit.collider.GetComponent<Object>();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Object")
		{
			Object obj = other.GetComponent<Object>();
			if (objectToFix == obj && obj.IsBroken)
			{
                Debug.Log("Should Stop");
				canMove = false;
				agent.isStopped = true;
				obj.Fix();
                coroutine = StartCoroutine(SetActiveMovement(obj, true));
                Debug.Log(coroutine);
			}
		}
	}

	private IEnumerator SetActiveMovement(Object obj, bool value)
	{
        yield return new WaitForSeconds(0.2f);
        while (!obj.IsAnimationFinished()) { yield return null; }

		canMove = value;
		agent.isStopped = false;

		if(coroutine != null) StopCoroutine(coroutine);

		yield return null;
	}
}
