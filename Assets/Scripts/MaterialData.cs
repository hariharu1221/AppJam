using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "SO/MaterialData", order = 3)]
public class MaterialData : ScriptableObject
{
    public List<Material> materials;
}
