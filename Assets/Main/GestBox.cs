using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GestBox : MonoBehaviour
{
    // private ParticleSystem particles;
    Animator animator;
    Vector3 lefthandpos = Vector3.zero;
    AudioSource trumpetOnly;
    AudioSource allMusic;

    private float min = 1;
    private float max = 0;

    // Start is called before the first frame update
    void Start()
    {
        Gesture.gen.drawLandmarks = false;
        // particles = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        animator = GameObject.Find("Character").GetComponent<Animator>();
        
        trumpetOnly = GameObject.Find("Trumpets").GetComponent<AudioSource>();
        allMusic = GameObject.Find("All Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // var ps = particles.main;

        float y_pos = Gesture.gen.righthandpos[0].y;

        /* 
         * This if ladder thing dynamically retrieves the max and min height values for user's hand, 
         * then changes thresholds for levels based on it
         * As of now, it only changes colors
        */
        
        if (y_pos != 0)
        {
            if (y_pos > max)
            {
                max = y_pos;
                Debug.Log("(readjusting the max");
            }
            else if (y_pos < min) 
            { 
                min = y_pos;
                Debug.Log("(readjusting the min");
            }
            float range = max - min;

            // below 1/2 
            if (y_pos > (min + (2 * range)/4))
            {
                animator.SetBool("IsActive", false);
                
                if (trumpetOnly.volume != 1)
                {
                    trumpetOnly.volume += 0.05f;
                }
                else if (allMusic.volume != 0)
                {
                    allMusic.volume -= 0.05f;
                }

                Debug.Log("1 (idle) " + (min + (2 * range) / 4) + "        " + Gesture.gen.righthandpos[0].y);
            }

            // above 1/2 max height
            // else if (y_pos > (min + (2 * range) / 4))
            // {
            //     animator.SetBool("IsActive", false);
            //     Debug.Log("2 (start) " + (min + (2 * range) / 4) + "        " + Gesture.gen.righthandpos[0].y);
            // }

            // above 1/2
            else if (y_pos > min)
            {
                animator.SetBool("IsActive", true);

                if (allMusic.volume != 1)
                {
                    allMusic.volume += 0.05f;
                }
                else if (trumpetOnly.volume != 0)
                {
                    trumpetOnly.volume -= 0.05f;
                }

                Debug.Log("3 (fight) " + min + "        " + Gesture.gen.righthandpos[0].y);
            }

            // above 0 
            // else if (y_pos > (min + (0 * range) / 4))
            // { 
            //     animator.SetBool("IsActive", true);
            //     Debug.Log("4 (whatever) " + (min + (0 * range) / 4) + "        " + Gesture.gen.righthandpos[0].y);
            // }

            //readjusting the min (which is the max height)
            else
            {

                Debug.Log("(readjusting the range " + Gesture.gen.righthandpos[0].y);
            }

            //Debug.Log("MIN " + min + " MAX " + max);
            //Debug.Log("RANGE " + range);
        }

        lefthandpos = new Vector3(0, 0, 0);
        for (int i = 0; i < 20; i++)
        {
            lefthandpos = lefthandpos - Gesture.gen.lefthandpos[i];
        }
        lefthandpos = lefthandpos / 20;

    }
}
