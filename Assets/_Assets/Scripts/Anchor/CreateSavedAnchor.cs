using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSavedAnchor : MonoBehaviour
{
    [SerializeField]
    private String AnchorNames;

    [SerializeField]
    private GameObject visualAnchor;
    [SerializeField]
    private CreateWorldOrigin CreateWorldOrigin;
    void Start()
    {

        Load();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load()
    {
        if(!isGuid(AnchorNames))
        {
            CreateWorldOrigin.needAnchor();
            Debug.Log("aaaaa");
            return;
        }
        OVRSpatialAnchor.LoadOptions options = new()
        {
            Uuids = new Guid[]
            {
                Guid.Parse(PlayerPrefs.GetString(AnchorNames))
            }
        };
        OVRSpatialAnchor.LoadUnboundAnchors(options, anchors =>
        {
            if (anchors == null)
            {
                return;
            }
            foreach (var anchor in anchors)
            {
                anchor.Localize((anchor,iscomplete) =>
                {
                    if (iscomplete)
                    {
                        var pos = anchor.Pose;
                        GameObject gs = Instantiate(visualAnchor, pos.position, pos.rotation);
                        gs.transform.name = AnchorNames;
                        if(gs.GetComponent<OVRSpatialAnchor>())
                        {
                        } else
                        {
                            gs.AddComponent<OVRSpatialAnchor>();
                        }
                        OVRSpatialAnchor workingAnchor = gs.GetComponent<OVRSpatialAnchor>();
                        anchor.BindTo(workingAnchor);
                        CreateWorldOrigin.setAnchor(gs.transform, AnchorNames);
                    }
                });
                
            }
        });

    }

    private bool isGuid(string value)
    {
        Guid x;
        return Guid.TryParse(PlayerPrefs.GetString(value), out x);
    }
}
