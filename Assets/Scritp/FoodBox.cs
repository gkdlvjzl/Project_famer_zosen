using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBox : MonoBehaviour
{
    public static int slot_foodbox = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food") && slot_foodbox < 2)
        {
            Destroy(other.gameObject);
            StartCoroutine(Stayin());            
        }
    }

    private readonly WaitForSeconds Wait01f = new WaitForSeconds(0.2f);

    IEnumerator Stayin()
    {
        slot_foodbox++;
      //Pig_eating.isEat_pig = true;        
        yield return Wait01f;
    }    
}