using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public bool isRagdoll = false;

    Collider[] limbsColliders;
    Rigidbody[] limbsRigidbodies;

    void Start()
    {
        GetLimbRagdollComponents();
        RagdollModeOff();
        isRagdoll = false;
    }

    void Update()
    {
        if (isRagdoll)
        {
            RagdollModeOn();
        }
    }

    void GetLimbRagdollComponents()
    {
        limbsColliders = transform.GetComponentsInChildren<Collider>();
        limbsRigidbodies = transform.GetComponentsInChildren<Rigidbody>();
    }

    public void RagdollModeOn()
    {
        foreach (Collider limb in limbsColliders)
        {
            limb.enabled = true;
        }
        foreach (Rigidbody limb in limbsRigidbodies)
        {
            limb.isKinematic = false;
        }
        GetComponent<Collider>().enabled = true;
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void RagdollModeOff()
    {
        GetComponent<Animator>().enabled = true;
        foreach (Collider limb in limbsColliders)
        {
            limb.enabled = false;
        }
        foreach (Rigidbody limb in limbsRigidbodies)
        {
            limb.isKinematic = true;
        }
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
