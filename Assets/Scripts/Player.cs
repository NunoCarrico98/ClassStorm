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
			if (objectToFix == obj && obj.isBroken)
			{
				canMove = false;
				agent.isStopped = true;
				obj.Fix();
				StartCoroutine(SetActiveMovement(obj, true));
			}
		}
	}

	private IEnumerator SetActiveMovement(Object obj, bool value)
	{
		while (!obj.IsAnimationFinished()) { }

		canMove = value;
		agent.isStopped = false;

		StopCoroutine("SetActiveMovement");

		yield return null;
	}
}
