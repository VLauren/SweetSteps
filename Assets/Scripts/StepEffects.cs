using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffects : MonoBehaviour
{
    void StepEffect()
    {
        if(!transform.parent.GetComponent<MainChar>().GhostActive)
        {
            Effects.SpawnEffect(0, transform.position);
            AudioManager.Play("footstep_concrete_00" + Random.Range(0, 5), false, 0.3f);
        }
        else
        {
            // TODO sonido de pasos fantasma
        }
    }
}
