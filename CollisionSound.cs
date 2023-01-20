using System.Collections;
using UnityEngine;


public class CollisionSound : MonoBehaviour
{

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