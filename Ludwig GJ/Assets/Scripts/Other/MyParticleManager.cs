using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleManager : CoreComponent
{

    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();

        //  particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;

    }

    public GameObject StartParticles(GameObject particlePrefab, Vector2 postion, Quaternion rotation)
    {
        return Instantiate(particlePrefab, postion, rotation, particleContainer);
    }

    public GameObject StartParticles(GameObject particlePrefab) //plays the particle animation with rotation to the object
    {
        return StartParticles(particlePrefab, transform.position, transform.rotation);
    }

    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotaion = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        return StartParticles(particlePrefab, transform.position, randomRotaion);
    }

    public GameObject StartParticlesNoRotation(GameObject particlePrefab) //plays the particle animation with no rotation
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }

    public GameObject StartParticlesCustomTransform(GameObject particlePrefab, Vector3 particlePostion) //plays the particle animation with rotation to the object
    {
        return StartParticles(particlePrefab, particlePostion, transform.rotation);
    }

}
