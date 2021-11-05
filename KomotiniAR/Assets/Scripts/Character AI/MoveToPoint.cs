using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    #region Variables
    public GameObject[] targets;
    public bool circularPath;
    public bool reverse;
    int currentTargetIndex;
    GameObject target;
    float minDistance = 0.005f;
    float damping = 3f;

    // Events
    public delegate void PointAction(); // int _index
    public PointAction OnArrival;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        // Set target
        if (reverse)
            currentTargetIndex = targets.Length - 1;
        else
            currentTargetIndex = 0;
        target = targets[currentTargetIndex];
    }

    void Update()
    {
        // Check if has arrived at target position
        //Debug.Log(transform.parent.parent.parent.transform.lossyScale.magnitude);
        if ((Vector3.Distance(transform.position, target.transform.position) <= minDistance)) // * transform.parent.parent.parent.transform.lossyScale.magnitude
        {
            // Set next target
            NextTarget();

            // Invoke event
            OnArrival?.Invoke();
        }

        // Look at target
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        //transform.LookAt(target.transform.position, transform.up);
    }
    #endregion

    #region Methods
    void NextTarget()
    {
        // Set currentTargetIndex
        if (reverse)
            currentTargetIndex--;
        else
            currentTargetIndex++;

        if (!circularPath)
        {
            if (currentTargetIndex >= targets.Length)
            {
                currentTargetIndex = targets.Length - 1;
                reverse = true;
            }

            else if (currentTargetIndex <= 0)
            {
                currentTargetIndex = 0;
                reverse = false;
            }
        }

        if (currentTargetIndex >= targets.Length)
            currentTargetIndex = 0;
        else if (currentTargetIndex < 0)
            currentTargetIndex = targets.Length - 1;


        // Set target
        target = targets[currentTargetIndex];
        //Debug.Log(target.name);
    }
    #endregion


    // Event for when it reaches a point
}
