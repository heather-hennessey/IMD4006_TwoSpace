using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip Booster, crystalMine, death, beam, shipDeath;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        BoosterSound = Resources.Load<AudioClip> ("Booster");
        crystalMineSound = Resources.Load<AudioClip> ("crystalMine");
        deathSound = Resources.Load<AudioClip> ("death");
        beamSound = Resources.Load<AudioClip> ("beam");
        shipDeathSound = Resources.Load<AudioClip>("shipDeath");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip) {
            case "Booster":
                audioSrc.PlayOneShot(BoosterSound);
                break;
            case "crystalMine":
                audioSrc.PlayOneShot(crystalMineSound);
                break;
            case "death":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "beam":
                audioSrc.PlayOneShot(beamSound);
                break;
            case "shipDeath":
                audioSrc.PlayOneShot(shipDeathSound);
                break;
        }
    }
}
//insert in proper script SoundManaferScript.PlaySound("Booster")