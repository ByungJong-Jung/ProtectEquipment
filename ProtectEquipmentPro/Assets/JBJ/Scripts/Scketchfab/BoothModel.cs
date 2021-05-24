using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MODEL_TYPE
{
    OPEN_DEVICE,
    SEALED_DEVICE,
    LOAD_UNLOAD_DEVICE,
    REPAIR_DEVICE,
    WASET_TREATMENT_DEVICE,
    TESTWORK_DEVICE,
    OTHER_DEVICE,
    Counts
}

public class BoothModel : MonoBehaviour
{
    public MODEL_TYPE myType;
}
