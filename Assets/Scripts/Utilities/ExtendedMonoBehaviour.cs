using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMonoBehaviour : MonoBehaviour
{
    //Can't make this work...?
    protected T SafeGetComponent<T>()
    {
        T c = GetComponent<T>();
        if (object.Equals(c, default(T))) // This line never seems to be true...
        {
            Debug.Log("Error - Vital component not found.");
            //Destroy(gameObject);
        }
        return c;
    }
}
