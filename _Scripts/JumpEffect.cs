using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    [SerializeField] float explosionForce = 15f;

    private void OnMouseDown()
    {
        float explotionExtent = gameObject.GetComponent<MeshRenderer>().bounds.extents.x * 2f * 1.5f;
        
        Collider[] neighbors = Physics.OverlapSphere(transform.position, explotionExtent);

        foreach (var other in neighbors)
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce,this.transform.position,explotionExtent);
            }
        }
    }
}
