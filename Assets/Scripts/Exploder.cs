using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float force;
    [SerializeField] private bool applyOnEnable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ApplyForce();
    }

    private void OnEnable()
    {
        ApplyForce();
    }

    private void ApplyForce()
    {
        Rigidbody[] rbs = target.GetComponentsInChildren<Rigidbody>();

        for(int i = 0; i < rbs.Length; i++)
        {
            rbs[i].AddExplosionForce(force, transform.position, 100);
        }
    }
}
