using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particelController : MonoBehaviour
{
    [SerializeField] Gradient wisdome;
    [SerializeField] Gradient wisdomeAndCourage;
    [SerializeField] Gradient AllTheGems;

    public ParticleSystem particleSystem;
    private void Start()
    {
         //particleSystem = GetComponent<ParticleSystem>();

        
    }

    public void JewelOfWisdom()
    {
        var main = particleSystem.main;
        main.startColor = wisdome;
    }
    public void GemOfCourage()
    {
        var main1 = particleSystem.main;
        main1.startColor = wisdomeAndCourage;
    }
    public void CrystalOfPower()
    {
        var main2 = particleSystem.main;
        main2.startColor = AllTheGems;
    }


}
