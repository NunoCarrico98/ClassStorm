using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    [SerializeField] private RepairSprite sprite;
    private bool canMove;
    private Camera cam;
    private NavMeshAgent agent;
    private Object objectToFix;
    private Coroutine coroutine;
    private bool flag;
    private Animator animator;
    private bool isWalking;

    private void Awake()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        canMove = true;
        isWalking = false;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForInput();
        if (CheckForDestinationReached())
            animator.SetBool("isWalking", false);
    }

    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            isWalking = true;
            animator.SetBool("isWalking", isWalking);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                objectToFix = hit.collider.GetComponent<Object>();
            }
        }
    }

    private bool CheckForDestinationReached()
    {
        if (isWalking)
            if (Vector3.Distance(transform.position, agent.destination) <= 1.5f)
                return true;
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Object" && !flag)
        {
            Object obj = other.GetComponent<Object>();
            if (objectToFix == obj && obj.IsBroken)
            {
                flag = true;
                canMove = false;
                agent.isStopped = true;
                agent.destination = transform.position;
                obj.Fix();
                coroutine = StartCoroutine(SetActiveMovement(obj, true));
            }
        }
    }

    private IEnumerator SetActiveMovement(Object obj, bool value)
    {
        yield return new WaitForSeconds(0.2f);
        while (!obj.IsAnimationFinished()) { yield return null; }

        canMove = value;
        agent.isStopped = false;
        flag = false;


        if (coroutine != null) StopCoroutine(coroutine);

        yield return null;
    }
}
