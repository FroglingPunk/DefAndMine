using System;
using System.Collections;
using UnityEngine;

public class Morpher : Singleton<Morpher>
{
    [SerializeField] private Material fireMaterial;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material frostMaterial;

    [SerializeField] private ParticleSystem fireMorphParticle;
    [SerializeField] private ParticleSystem waterMorphParticle;
    [SerializeField] private ParticleSystem frostMorphParticle;


    public void UpdateMorph(Elemental elemental)
    {
        if (elemental.Element == EElement.Fire)
        {
            elemental.GetComponent<MeshRenderer>().sharedMaterial = fireMaterial;

            ParticleSystem particleSystem = Instantiate(fireMorphParticle, transform);
            particleSystem.transform.position = elemental.transform.position;
            particleSystem.Play();
            StartCoroutine(DelayAction(particleSystem.main.duration, () => Destroy(particleSystem.gameObject)));
        }
        else if (elemental.Element == EElement.Water)
        {
            elemental.GetComponent<MeshRenderer>().sharedMaterial = waterMaterial;

            ParticleSystem particleSystem = Instantiate(waterMorphParticle, transform);
            particleSystem.transform.position = elemental.transform.position;
            particleSystem.Play();
            StartCoroutine(DelayAction(particleSystem.main.duration, () => Destroy(particleSystem.gameObject)));
        }
        else if (elemental.Element == EElement.Frost)
        {
            elemental.GetComponent<MeshRenderer>().sharedMaterial = frostMaterial;

            ParticleSystem particleSystem = Instantiate(frostMorphParticle, transform);
            particleSystem.transform.position = elemental.transform.position;
            particleSystem.Play();
            StartCoroutine(DelayAction(particleSystem.main.duration, () => Destroy(particleSystem.gameObject)));
        }
    }


    private IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}