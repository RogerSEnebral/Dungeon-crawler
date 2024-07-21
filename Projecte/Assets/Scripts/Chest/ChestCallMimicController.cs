using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCallMimicController : ChestController
{
    public GameObject mimic;

    public override void Open()
    {
        base.Open();

        mimic.GetComponent<MimicController>().Open();
    }
}
