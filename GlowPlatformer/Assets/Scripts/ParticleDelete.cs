using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelete : MonoBehaviour
{
    // called from animation event
    public void DeleteSelf()
    {
        Destroy(this.gameObject);
    }
}
