using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
	[Header("CITYGENERATION")]
	public int citySizeX = 25;
	public int citySizeZ = 12;

	[SerializeField] private float minXOffset = 0f;
	[SerializeField] private float maxXOffset = 1.5f;
	[SerializeField] private float minZOffset = 2.5f;
	[SerializeField] private float maxZOffset = 4f;
	[SerializeField, Range(0,1)] private float houseCreationChance = 0.4f;
	[Header("House Setings")]
	[SerializeField] private float houseSpacing = 9.0f;
	[SerializeField] private float minHouseThickness = 2.5f;
	[SerializeField] private float maxHouseThickness = 8f;
	[SerializeField] private float minHouseHeight = 8f;
	[SerializeField] private float maxHouseHeight = 28f;
	[Header("House Decoration")]
	[Tooltip("1.0 = The tallest house")]
	[SerializeField, Range(0,1)] private float heightRequiredForChimney = 0.95f;
	[SerializeField] private KeyCode reGenerateInRuntime;

	[SerializeField] List<GameObject> generatedHouses = new List<GameObject>();

	private float randomThickness, randomHeight;

	private void Update()
	{
		if (!Input.GetKeyDown(reGenerateInRuntime))
			return;

		GenerateCity(citySizeX, citySizeZ);
	}

	public void GenerateCity(int sizeX, int sizeZ)
	{
		ClearCity();

		float tallestHouse = 0f;

		for (int x = 0; x < sizeX; x++)
		{
			for (int z = 0; z < sizeZ; z++)
			{
				var spacingX = x * houseSpacing;
				var spacingZ = z * houseSpacing;

				if (ChanceRoll(houseCreationChance))
				{
					randomThickness = Random.Range(minHouseThickness, maxHouseThickness);
					randomHeight = Random.Range(minHouseHeight, maxHouseHeight);

					if (randomHeight > tallestHouse)
						tallestHouse = randomHeight;

					GameObject house = GameObject.CreatePrimitive(PrimitiveType.Cube);
					house.transform.SetParent(transform);
					house.transform.localScale = new Vector3(randomThickness, randomHeight, randomThickness);
					house.name = ($"House: {transform.childCount}");

					float halfHeight = randomHeight * 0.5f;

					house.transform.localPosition = new Vector3(spacingX, transform.position.y + halfHeight, spacingZ)
						+ RandomOffset(minXOffset, maxXOffset, minZOffset, maxZOffset);

					RaycastHit hit;
					if (Physics.Raycast(house.transform.position, house.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
					{
						float heightPlacement = hit.point.y + (house.transform.localScale.y * 0.4f);
						house.transform.position = new Vector3(house.transform.localPosition.x, heightPlacement, house.transform.localPosition.z);
					}

					generatedHouses.Add(house);
				}
			}
		}

		AddRoofMeshes(tallestHouse, heightRequiredForChimney);
	}

	private void AddRoofMeshes(float tallestHouse, float amount)
	{
		float heightVariation;
		float widthVariation;
		foreach (var house in generatedHouses)
		{
			heightVariation = Random.Range(0.05f, 0.2f);
			widthVariation = Random.Range(0.4f, 0.85f);
			if (house.transform.localScale.y >= tallestHouse * amount)
			{
				GameObject roof = RoofDecoration();

				roof.transform.localScale = new Vector3(house.transform.localScale.x * widthVariation, house.transform.localScale.y * heightVariation, house.transform.localScale.z * widthVariation);
				roof.transform.position = new Vector3(house.transform.position.x, house.transform.position.y + house.transform.localScale.y * 0.5f + roof.transform.localScale.y, house.transform.position.z);
				roof.transform.SetParent(house.transform);
			}
		}
	}

	private GameObject RoofDecoration()
	{
		return GameObject.CreatePrimitive(PrimitiveType.Cylinder);
	}

	public void ClearCity()
	{
		if (transform.childCount != 0)
		{
			GameObject[] allChildren = new GameObject[transform.childCount];
			int childIndex = 0;
			foreach (Transform child in transform)
			{
				allChildren[childIndex] = child.gameObject;
				childIndex += 1;
			}

			foreach (GameObject child in allChildren)
				DestroyImmediate(child.gameObject);
		}

		generatedHouses.Clear();
	}

	public bool ChanceRoll(float chance)
	{
		float rnd = Random.Range(0.0f, 1.0f);

		return (rnd <= chance) ? true : false;
	}

	public Vector3 RandomOffset(float minXOffset, float maxXOffset, float minZOffset, float maxZOffset)
	{
		float x = Random.Range(minXOffset, maxXOffset);
		float z = Random.Range(minZOffset, maxZOffset);

		return new Vector3(x, 0f, z);
	}
}