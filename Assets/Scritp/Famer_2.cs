using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Famer_2 : MonoBehaviour
{
    [SerializeField] GameObject[] plant;
    [SerializeField] GameObject[] ries;
    [SerializeField] float speed;

    Animator anim;

    public float ReTurnTime;
    public float riesPoint;

    bool isWalking;    
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
        if (riesPoint < 0.5f)
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
    IEnumerator StayTime()
    {
        yield return Wait06f;
        isWalking = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("Faming", false);
        yield return Wait06f;
        isWalking = true;
    }

    IEnumerator FamingTime()
    {
        Action();
        yield return Wait06f;        
        plant[C].SetActive(true);
        C++;
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

        if (ries[R].activeSelf.Equals(true))
        {
            FollowTarget(ries[R].transform);
            transform.position = Vector3.MoveTowards(transform.position, ries[R].transform.position, step);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
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
