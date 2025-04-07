using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSystem : MonoBehaviour, IInteractable
{
    [SerializeField]private bool valveActive = false;
    [SerializeField] private Collider[] colliderslocks;
    [SerializeField] private Collider[] otherColliders;
    [SerializeField] private ParticleSystem[] steamParticlesA;
    [SerializeField] private ParticleSystem[] steamParticlesB;
    [SerializeField] private AudioSource[] steamSoundA;
    [SerializeField] private AudioSource[] steamSoundB;
    public void Interact(GameObject interactant)
    {
        Debug.Log("ativou");
        if (colliderslocks != null)
        {
                
                foreach (Collider collider in colliderslocks)
                {
                    collider.enabled = false;
                    SteamStopParticle();

                }

                Debug.Log("Entrou no else");
                foreach (Collider collider in otherColliders)
                {
                    collider.enabled = true;
                    SteamPlayParticle();
                }
            
        }

        void SteamStopParticle()
        {
            foreach (ParticleSystem steam in steamParticlesA)
            {
                steam.Stop();
                foreach(AudioSource fxsteam in steamSoundA)
                {
                    fxsteam.Stop();
                }
            }
            
        }
        void SteamPlayParticle()
        {
            foreach (ParticleSystem steam in steamParticlesB)
            {
                steam.Play();
                foreach (AudioSource fxsteam in steamSoundB)
                {
                    fxsteam.Play();
                }
            }

        }
    }
}
