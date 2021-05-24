using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineModel : MonoBehaviour
{
    public MODEL_TYPE myType;

    public void ActiveObject(bool _activity)
    {
        gameObject.SetActive(_activity);
    }
}
