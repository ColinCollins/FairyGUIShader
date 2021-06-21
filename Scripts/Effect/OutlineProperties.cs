using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Outline Properties", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class OutlineProperties : ScriptableObject
{
	public Color LineColor;
	[Range(0, 1.0f)]
	public float LineWidth;
}
