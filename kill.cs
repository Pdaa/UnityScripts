using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {  
       GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {

    Destroy(this.gameObject,10);   
    }

    void OnCollisionEnter (Collision col)
    {

         if (col.relativeVelocity.magnitude > 1)
    {
            var audios = GetComponents<AudioSource>();
            var audio0 = audios[0];

              if (!audio0.isPlaying)
                {

                audio0.Play();
                
                }
           
	}

    }
}
