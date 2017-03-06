using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {

    public ParticleSystem[] allParticles;

	// Use this for initialization
	void Start () {
       // allParticles = GetComponentsInChildren<ParticleSystem>();
	}
	

    public void PlayParticle()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem ps in allParticles)
        {
            Debug.Log("Play Particles");
            ps.Stop();
            ps.Play();
        }
    }

}
