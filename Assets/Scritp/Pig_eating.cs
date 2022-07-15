using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Pig_eating : MonoBehaviour
{
    [SerializeField] GameObject[] Food;

    Animator anim;

    public float Eattime = 10f;
    bool isEating = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (FoodBox.slot_foodbox >= 0)
        {
            StartCoroutine(InFood());
            if (!isEating)
            {
                Eatgo();
            }
        }
    }

    public void Eatgo()
    {
        StartCoroutine(Eating());
    }

    public IEnumerator Eating()
    {
        isEating = true;
        anim.SetBool("isEat", true);
        yield return new WaitForSeconds(Eattime);
        isEating = false;
        Check_num();
        anim.SetBool("isEat", false);
    }
    void Check_num()
    {
        Food[FoodBox.slot_foodbox].SetActive(false);
        FoodBox.slot_foodbox--;
    }

    IEnumerator InFood()
    {
        Food[FoodBox.slot_foodbox].SetActive(true);
        yield return null;
    }

}
