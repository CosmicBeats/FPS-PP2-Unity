using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class GunStats : ScriptableObject
{
    public GameObject gunModel;
    [Range(1, 10)] public int shootDamage;
    [Range(25, 1000)] public int shootDistance;
    [Range(0.1f, 5)] public float shootRate;
    public int currentAmmo; 
    public int maxAmmo; 
    public float reloadTime; 
    public bool isReloading; 
    public Animator gunAnimator;
    public ParticleSystem gunParticle; 
    public AudioClip shootSound; 
    [Range(0, 1)] public float shootVolume;
    public Image icon;

    public int totalAmmo;

    public int totalAmmoStash;
    


}
