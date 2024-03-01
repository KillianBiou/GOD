using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RagdollPreset
{
    FREE_ARM,
    FREE_ARM_HEAD,
    FREE_ALL,
    LOCKED_ALL
}

public enum BoneType
{
    Root,
    Head,
    Chest,
    L_UpperArm,
    L_LowerArm,
    R_UpperArm,
    R_LowerArm,
    L_UpperLeg,
    L_LowerLeg,
    R_UpperLeg,
    R_LowerLeg,
}

[System.Serializable]
public struct BonesMatching
{
    public BoneType type;
    public GameObject bone;
}

public class PeonRagdoll : MonoBehaviour
{
    public List<BonesMatching> bonesMapping;

    private void Awake()
    {
    }

    public void MatchBones()
    {
        bonesMapping = new List<BonesMatching>();
        foreach(BoneType type in Enum.GetValues(typeof(BoneType)).Cast<BoneType>().ToList())
        {
            BonesMatching b = new BonesMatching();
            b.type = type;
            Transform temp = transform.FindChildRecursive(type.ToString());
            Debug.Log(type.ToString());
            if (temp)
            {
                b.bone = temp.gameObject;
                bonesMapping.Add(b);
            }
        }
    }

    public void ChangeState(RagdollPreset ragdollPreset)
    {
        switch (ragdollPreset)
        {
            case RagdollPreset.FREE_ARM:
                ToFreeArm();
                break;
            case RagdollPreset.FREE_ARM_HEAD:
                ToFreeArmHead();
                break;
            case RagdollPreset.FREE_ALL:
                ToFreeAll();
                break;
            case RagdollPreset.LOCKED_ALL:
                ToLockedAll();
                break;
        }
    }

    private void ToFreeArm()
    {
        List<BoneType> freeList = new List<BoneType>(){ BoneType.R_LowerArm, BoneType.L_LowerArm };

        foreach(BonesMatching b in bonesMapping) {
            if (freeList.Contains(b.type))
            {
                b.bone.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                b.bone.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void ToFreeArmHead()
    {
        List<BoneType> freeList = new List<BoneType>() { BoneType.R_LowerArm, BoneType.L_LowerArm, BoneType.Head };

        foreach (BonesMatching b in bonesMapping)
        {
            if (freeList.Contains(b.type))
            {
                b.bone.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                b.bone.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void ToFreeAll()
    {
        List<BoneType> freeList = Enum.GetValues(typeof(BoneType)).Cast<BoneType>().ToList();

        foreach (BonesMatching b in bonesMapping)
        {
            if (freeList.Contains(b.type))
            {
                b.bone.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                b.bone.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void ToLockedAll()
    {
        foreach (BonesMatching b in bonesMapping)
        {
            b.bone.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}