using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Meta.XR.BuildingBlocks;
using System;
using UnityEngine.InputSystem;

public class CreateAnchor : MonoBehaviour
{
    public string AnchorName;

    public void removeSpatialAnchor()
    {
        PlayerPrefs.DeleteKey(AnchorName);
        if (GetComponent<OVRSpatialAnchor>())
        {
            Destroy(GetComponent<OVRSpatialAnchor>());
        }
    }

    public void CreateSpatialAnchor()
    {
        //GameObject gs = new GameObject();
        OVRSpatialAnchor workingAnchor = gameObject.AddComponent<OVRSpatialAnchor>();
        StartCoroutine(anchorCreated(workingAnchor));
    }

    public IEnumerator anchorCreated(OVRSpatialAnchor anchor)
    {
        // keep checking for a valid and localized spatial anchor state
        while (!anchor.Created && !anchor.Localized)
        {
            yield return new WaitForEndOfFrame();
        }

        Guid anchorGuid = anchor.Uuid;
        anchor.Save((a, success) =>
        {

            if (success)
            {
                PlayerPrefs.SetString(AnchorName, anchorGuid.ToString());
            }

        });
    }
}
