using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxPatrol : BaseDeath
{

    [Header("Patrol Points")]
    [SerializeField] private float leftPatrolPoint;
    [SerializeField] private float rightPatrolPoint;
    [SerializeField] private bool stopAtEnds;
    [SerializeField] private float stopDelayTime;

    [SerializeField] private float moveSpeed;


    private bool waiting;
    private float targetPoint;

    private void Start()
    {
        targetPoint = leftPatrolPoint;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!waiting)
        {
            float dir = targetPoint - transform.position.x;
            if (dir < 0)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + (dir * moveSpeed * Time.deltaTime),
                leftPatrolPoint, rightPatrolPoint), transform.position.y, transform.position.z);

            if (transform.position.x == leftPatrolPoint && targetPoint == leftPatrolPoint)
            {
                if (stopAtEnds)
                {
                    StartCoroutine(StopThenGo(rightPatrolPoint));
                }
                else
                {
                    targetPoint = rightPatrolPoint;
                }
            }
            if (transform.position.x == rightPatrolPoint && targetPoint == rightPatrolPoint)
            {
                if (stopAtEnds)
                {
                    StartCoroutine(StopThenGo(leftPatrolPoint));
                }
                else
                {
                    targetPoint = leftPatrolPoint;
                }
            }
        }
    }
        

    private IEnumerator StopThenGo(float targetPoint)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(stopDelayTime);
        this.targetPoint = targetPoint;
        waiting = false;
    }
}
