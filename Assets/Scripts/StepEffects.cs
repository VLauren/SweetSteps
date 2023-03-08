using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffects : MonoBehaviour
{
    void StepEffect()
    {
        Effects.SpawnEffect(0, transform.position);

        AudioManager.Play("footstep_concrete_00" + Random.Range(0, 5), false, 0.3f);
    }
}
