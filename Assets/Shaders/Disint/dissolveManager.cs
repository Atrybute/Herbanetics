using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;

    public float progressRate;

    public float dissolveRate;

    public float dissolveDelay;

    public GameObject dissolveParticle;

    private Material[] skinnedMaterials;

    private bool canDisolve;
    

   
    private void Awake()
    {
        canDisolve = true;

        if (skinnedMesh != null)
            skinnedMaterials = skinnedMesh.materials;
    }

    private void OnEnable()
    {
        //PlayerDamage.Death += StartDissolve;
    }

    private void OnDisable()
    {
        // PlayerDamage.Death -= StartDissolve;
    }
    

    IEnumerator DissolveCo ()
    {
        
        yield return new WaitForSeconds(dissolveDelay);

        

        if (skinnedMaterials.Length > 0)
        {
            dissolveParticle.SetActive(true);
            float counter = 0;

            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for(int i=0; i<skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds (progressRate);
            }
        }
    }

    void StartDissolve()
    {   
        if(canDisolve)
        {
            StartCoroutine(DissolveCo());
            
            canDisolve = false;
        }
        
    }
}
