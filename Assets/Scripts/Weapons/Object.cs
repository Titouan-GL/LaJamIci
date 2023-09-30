using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Object : MonoBehaviour
{
    public abstract void Use();
    public abstract void Action2();

    public abstract void SwitchIn();
    public abstract void SwitchOut();

}
