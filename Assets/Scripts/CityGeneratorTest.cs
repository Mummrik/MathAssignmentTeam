using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneratorTest : MonoBehaviour
{
	[SerializeField] private int sizeX = 20, sizeY = 20;
	[SerializeField] private float houseSpacing = 1.5f;

	private void Start()
	{
		CubeGrid(sizeX, sizeY);
	}
	private void CubeGrid(int sizeX, int sizeY)
	{
		for (int x = 0; x < sizeX; x++)
		{
			// fixa spacing
			for (int y = 0; y < sizeY; y++)
			{
				GameObject house = GameObject.CreatePrimitive(PrimitiveType.Cube);
				
			}
		}
	}
}