using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDissolveManager : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;

    public float progressRate;

    public float dissolveRate;

    public GameObject dissolveParticle;

    private Material[] skinnedMaterials;

    

    private void Awake()
    {
        if (skinnedMesh != null)
            skinnedMaterials = skinnedMesh.materials;
    }

    

    IEnumerator DissolveCo ()
    {     

        if(skinnedMaterials.Length > 0)
        {
            float counter = 0;

            while(skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
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

    public void StartDissolve()
    {  

        StartCoroutine(DissolveCo());

        dissolveParticle.SetActive(true);
    }
}
