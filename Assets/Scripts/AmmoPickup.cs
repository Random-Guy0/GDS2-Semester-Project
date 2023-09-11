using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [field: SerializeField] public int AmmoAmount { get; private set; } = 3;
}
