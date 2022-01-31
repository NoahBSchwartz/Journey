using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopper : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particles;
    void Start()
    {
        }

    // Update is called once per frame
    void Update()
    {
    particles.Pause();
    }
}
