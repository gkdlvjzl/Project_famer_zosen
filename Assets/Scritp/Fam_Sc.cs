using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fam_Sc : MonoBehaviour
{
    [SerializeField] GameObject Hover;
    [SerializeField] ParticleSystem Hove;


    private void Start()
    {
        Hove = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hove_Ps"))
        {
            
            Hove.Play();
            GameObject obj = Instantiate(Hover, this.transform.position, Quaternion.LookRotation(this.transform.forward));
        }
    }
}
