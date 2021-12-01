using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Useable : MonoBehaviour
{
    private Character _user;
    public Character User => _user ??= GetComponentInParent<Character>();

    public abstract void Use();
}
