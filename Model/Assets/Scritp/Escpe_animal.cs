using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escpe_animal : MonoBehaviour
{
    public float Deitak_range;
    public float ReTurnTime;
    public float Speed;

    [SerializeField] GameObject Attacker;

    Animator animal;

    bool isRandom = false;
    bool isRotLeft = false;
    bool isRotRight = false;
    bool isWalking = false;

    Rigidbody rb;
    private void Start()
    {
        animal = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        PlayerCheck();

        if (!isRandom)
            StartCoroutine(Random_Move());
        if (isRotRight)
            transform.Rotate(transform.up * Time.deltaTime * ReTurnTime);
        if (isRotLeft)
            transform.Rotate(transform.up * Time.deltaTime * -ReTurnTime);
        if (isWalking)
        {
            rb.AddForce(transform.forward * ((Speed * Time.deltaTime) * 100));
            //Vector3 newPos = new Vector3(Random.Range(-4,4), 0, Random.Range(-4,4));
            //transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
            animal.SetBool("isWalk", true);
        }
        if (!isWalking)
        {
            animal.SetBool("isWalk", false);
        }
    }

    void PlayerCheck()
    {        
        float vir = Vector3.Distance(Attacker.transform.position, this.transform.position);
        float step = Speed * Time.deltaTime;

        if(vir < Deitak_range)
        {
            animal.SetBool("isRun", true);
            Vector3 vector3 = this.transform.position - Attacker.transform.position;

            Vector3 newPos = new Vector3 (transform.position.x + (this.transform.position.x - Attacker.transform.position.x),0, transform.position.z + (this.transform.position.z - Attacker.transform.position.z));
            
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(vector3), Time.deltaTime * ReTurnTime);

            transform.position = Vector3.MoveTowards(transform.position, newPos, step);            
        }
        else
            animal.SetBool("isRun", false);
    }
    /*
    void Patten_chiken()
    {
        StartCoroutine(EatTime());
    }
    IEnumerator EatTime()
    {
        animal.SetBool("isWalk", false);
        animal.SetBool("isEat", true);
        yield return new WaitForSeconds(6f);
        animal.SetBool("isEat", false);
        yield return new WaitForSeconds(4f);
    }
    */
    IEnumerator Random_Move()
    {
        int rotTime = Random.Range(1, 3);
        int rotWait = Random.Range(1, 4);
        int rotDirection = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isRandom = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotWait);

        if (rotDirection.Equals(1))
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotLeft = false;
        }
        if (rotDirection.Equals(2))
        {
            isRotRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotRight = false;
        }
        isRandom = false;
    }
}
