using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Famer_1 : MonoBehaviour
{
    [SerializeField] Transform[] movePoint;
    [SerializeField] GameObject[] ries;
    [SerializeField] float speed;

    Animator anim;
    public float ReTurnTime;
    public float riesPoint;

    bool isWalking;
    int moveNum = 0;
    int R = 0;
    int C = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isWalking = true;
    }
    private void Update()
    {
        riesPoint = Vector3.Distance(this.transform.position, ries[R].transform.position);

        if (isWalking)
        {
            StartCoroutine(WalkTime());
        }

        if (riesPoint < 1.5f)
        {
            StartCoroutine(FamingTime());
            R++;
        }

        if (R == ries.Length)
        {
            StartCoroutine(StayTime());
            R = 0;
        }
        if (C.Equals(ries.Length))
        {
            C = 0;
        }
    }

    private readonly WaitForSeconds Wait06f = new WaitForSeconds(6f);
    IEnumerator FamingTime()
    {
        Action();
        yield return Wait06f;
        ries[C].SetActive(true);
        C++;
        isWalking = true;
    }

    IEnumerator StayTime()
    {
        yield return Wait06f;
        isWalking = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("Faming", false);
        yield return Wait06f;
        isWalking = true;
    }

    IEnumerator WalkTime()
    {
        Walk();
        yield return null;
    }

    void Action()
    {
        isWalking = false;
        //OnTool(Tool);
        anim.SetBool("isWalking", false);
        anim.SetBool("Faming", true);
    }

    void Walk()
    {
        //OffTool(Tool);
        anim.SetBool("Faming", false);
        anim.SetBool("isWalking", true);

        MovePoint();
    }
    void MovePoint()
    {
        float step = speed * Time.deltaTime;
        float nextPoint = Vector3.Distance(transform.position, movePoint[moveNum].position);

        FollowTarget(movePoint[moveNum]);
        transform.position = Vector3.MoveTowards(transform.position, movePoint[moveNum].transform.position, step);

        if (nextPoint < 0.7f)
            moveNum++;

        if (moveNum == movePoint.Length)
            moveNum = 0;
    }

    private void FollowTarget(Transform STED)
    {
        if (STED != null)
        {
            Vector3 dir = STED.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * ReTurnTime);
            // 맨뒤 스피드값 값에 따라 회전값조종가능
        }
    }
    /*
    void OnTool(GameObject item)
    {
        item.SetActive(true);
    }
    void OffTool(GameObject item)
    {
        item.SetActive(false);
    }
    */
}
