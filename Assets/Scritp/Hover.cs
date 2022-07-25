using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] GameObject Fam_Ground;
    [SerializeField] ParticleSystem Hove;


    private void Start()
    {
        Hove = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Hove.Play();
            GameObject obj = Instantiate(Fam_Ground, this.transform.position, Quaternion.LookRotation(this.transform.forward));

            Destroy(obj, 3f);
        }
    }
}
