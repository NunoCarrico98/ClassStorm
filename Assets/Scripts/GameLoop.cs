using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    [SerializeField] private float[] breakIntervals;

    [SerializeField] private int breaksNeededToChangeInterval;

    private List<Object> objects;

    private Object currentObject;

    private float currentInterval;

    private int currentIntervalFlag = 0;

    private int breaksCounter = 0;

    private int breaksFlag = 0;

    private void Awake()
    {
        GetObjects();
        SetInterval();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BreakTimer());
    }

    // Update is called once per frame
    void Update()
    {
        ManageBreakInterval();
    }

    private void ManageBreakInterval()
    {
        if (breaksCounter % breaksNeededToChangeInterval == 0 && breaksCounter != 0 && breaksFlag == 0)
        {
            SetInterval();
            breaksFlag++;
        }
    }

    private IEnumerator BreakTimer()
    {
        while (!AllObjectsBroken())
        {
            yield return new WaitForSeconds(currentInterval);

            BreakRandomObject();

            breaksCounter++;

            breaksFlag = 0;
        }
    }

    private void GetObjects()
    {
        Object[] objArray = FindObjectsOfType<Object>();

        objects = new List<Object>(objArray);

    }

    private void BreakRandomObject()
    {
        Debug.Log("Break");

        int i = Random.Range(0, objects.Count);

        objects[i].Break();

        objects.Remove(objects[i]);
    }

    private bool AllObjectsBroken()
    {
        if (objects.Count >= 1) return false;

        return true;
    }

    private void SetInterval()
    {
        if (breakIntervals.Length - 1 >= currentIntervalFlag)
        {
            currentInterval = breakIntervals[currentIntervalFlag];
            currentIntervalFlag++;
        }
        else Debug.Log("No more break intervals");
    }
}
