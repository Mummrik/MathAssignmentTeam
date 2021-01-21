using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneratorTest : MonoBehaviour
{
	[Header("CITYGENERATION")]
	[SerializeField] private int citySizeX = 25;
	[SerializeField] private int citySizeZ = 12;
	[SerializeField] private float minXOffset = 0f;
	[SerializeField] private float maxXOffset = 1.5f;
	[SerializeField] private float minZOffset = 2.5f;
	[SerializeField] private float maxZOffset = 4f;
	[SerializeField, Range(0,1)] private float houseCreationChance = 0.65f;
	[Header("")]
	[SerializeField] private float houseSpacing = 8.0f;
	[SerializeField] private float minHouseThickness = 1.5f;
	[SerializeField] private float maxHouseThickness = 6f;
	[SerializeField] private float minHouseHeight = 8f;
	[SerializeField] private float maxHouseHeight = 28f;

	private float randomThickness, randomHeight;

	private void Start()
	{
		//CubeGrid(sizeX, sizeY);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}

			CubeGrid(citySizeX, citySizeZ);
		}
	}

	private void CubeGrid(int sizeX, int sizeY)
	{
		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				var spacingX = x * houseSpacing;
				var spacingY = y * houseSpacing;

				if (ChanceOfCreatingHouse(houseCreationChance))
				{
					randomThickness = UnityEngine.Random.Range(minHouseThickness, maxHouseThickness);
					randomHeight = UnityEngine.Random.Range(minHouseHeight, maxHouseHeight);

					GameObject house = GameObject.CreatePrimitive(PrimitiveType.Cube);
					house.transform.SetParent(transform);

					house.transform.localScale = new Vector3(randomThickness, randomHeight, randomThickness);

					house.transform.localPosition = new Vector3(spacingX, transform.position.y + randomHeight * 0.5f, spacingY)
						+ RandomOffset(minXOffset,maxXOffset,minZOffset,maxZOffset);
				}
			}
		}
	}

	private bool ChanceOfCreatingHouse(float chance)
	{
		float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
		
		return (rnd <= chance) ? true : false;
	}

	private Vector3 RandomOffset(float minXOffset, float maxXOffset, float minZOffset, float maxZOffset)
	{
		float x = UnityEngine.Random.Range(minXOffset, maxXOffset);
		float z = UnityEngine.Random.Range(minZOffset, maxZOffset);

		return new Vector3(x, 0f, z);
	}
}