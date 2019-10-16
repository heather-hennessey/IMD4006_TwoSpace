using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip Booster, crystalMine, death, beam, shipDeath, ambient;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        Booster = Resources.Load<AudioClip>("Booster");
        crystalMine = Resources.Load<AudioClip>("crystalMine");
        death = Resources.Load<AudioClip>("death");
        beam = Resources.Load<AudioClip>("beam");
        shipDeath = Resources.Load<AudioClip>("shipDeath");
        ambient = Resources.Load<AudioClip>("heroes_of_the_moon");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Booster":
                audioSrc.PlayOneShot(Booster);
                break;
            case "crystalMine":
                audioSrc.PlayOneShot(crystalMine);
                break;
            case "death":
                audioSrc.PlayOneShot(death);
                break;
            case "beam":
                audioSrc.PlayOneShot(beam);
                break;
            case "shipDeath":
                audioSrc.PlayOneShot(shipDeath);
                break;
            case "ambient":
                audioSrc.PlayOneShot(ambient);
                break;
        }
    }
}
//insert in proper script SoundManagerScript.PlaySound("Booster")