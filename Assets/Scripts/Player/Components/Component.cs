using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : MonoBehaviour
{
    public Character Character => _character ??= GetComponent<Character>();
    private Character _character;
}
