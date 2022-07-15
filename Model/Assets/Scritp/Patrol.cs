using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] GameObject[] movePoint;
    [SerializeField] GameObject IdlePoint;

    Animator animator;

    public int speed;
    public float retrunTime;
    public float TunSpeed;

    int moveNum;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float vir = Vector3.Distance(transform.position, IdlePoint.transform.position);

        if (vir < 1.8f)
        {
            StartCoroutine(StayPoint());
        }
        else
        {
        Move();
        }
    }

    private void Move()
    {
        animator.SetBool("isWalking", true);

        float step = speed * Time.deltaTime;
        float nextPoint = Vector3.Distance(transform.position, movePoint[moveNum].transform.position);

        FollowTarget(movePoint[moveNum]);
        transform.position = Vector3.MoveTowards(transform.position, movePoint[moveNum].transform.position, step);

        if (nextPoint < 1f)
            moveNum++;

        if (moveNum == movePoint.Length)
            moveNum = 0;

    }

    IEnumerator StayPoint()
    {
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(retrunTime);
    }
   
    private void FollowTarget(GameObject STED)
    {
        if (STED != null)
        {
            Vector3 dir = STED.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * TunSpeed);
            // 맨뒤 스피드값 값에 따라 회전값조종가능
        }
    }
    
}
