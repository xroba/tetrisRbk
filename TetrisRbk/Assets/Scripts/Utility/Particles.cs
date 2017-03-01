using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {

    public ParticleSystem[] allParticles;

	// Use this for initialization
	void Start () {
        allParticles = GetComponentsInChildren<ParticleSystem>();
	}
	

    public void PlayParticle()
    {
        foreach(ParticleSystem ps in allParticles)
        {
            ps.Stop();
            ps.Play();
        }
    }

}
