using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CityGenerator))]
public class GityGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		CityGenerator cityGen = (CityGenerator)target;

		DrawDefaultInspector();

		if (GUILayout.Button("Re-Generate City"))
			cityGen.GenerateCity(cityGen.citySizeX, cityGen.citySizeZ);

		if (GUILayout.Button("Clear City"))
			cityGen.ClearCity();
	}
}