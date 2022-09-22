using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffects : MonoBehaviour
{
    void StepEffect()
    {
        Effects.SpawnEffect(0, transform.position);
    }
}
