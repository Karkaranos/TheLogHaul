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

    [SerializeField] private bool moonwalking;
    private Animator foxAnim;



    private bool waiting;
    private float targetPoint;

    private void Start()
    {
        targetPoint = leftPatrolPoint;
        foxAnim = GetComponent<Animator>();
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
                    if (moonwalking)
                    {
                        StartCoroutine(StopThenGo(rightPatrolPoint, Vector3.zero));
                    }
                    else
                    {
                        StartCoroutine(StopThenGo(rightPatrolPoint, new Vector3(0, 180, 0)));
                    }
                }
                else
                {
                    if (moonwalking)
                    {
                        transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                    targetPoint = rightPatrolPoint;
                }
            }
            if (transform.position.x == rightPatrolPoint && targetPoint == rightPatrolPoint)
            {
                if (stopAtEnds)
                {
                    if (moonwalking)
                    {
                        StartCoroutine(StopThenGo(leftPatrolPoint, new Vector3(0, 180, 0)));
                    }
                    else
                    {
                        StartCoroutine(StopThenGo(leftPatrolPoint, Vector3.zero));
                    }
                    
                }
                else
                {
                    if (moonwalking)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                    
                    targetPoint = leftPatrolPoint;
                }
            }
        }
    }
        

    private IEnumerator StopThenGo(float targetPoint, Vector3 rot)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(stopDelayTime);
        this.targetPoint = targetPoint;
        transform.rotation = Quaternion.Euler(rot);
        waiting = false;
    }
}
