using UnityEngine;

public class FlyCamera : MonoBehaviour
{
	private const string MOUSE_X = "Mouse X";
	private const string MOUSE_Y = "Mouse Y";
	private const string VERTICAL = "Vertical";
	private const string HORIZONTAL = "Horizontal";

	[SerializeField] private KeyCode boostKey, upKey, downKey;
	[SerializeField] private float cameraMoveSpeed = 10.0f;
	[SerializeField] private int boostSpeedMultiplyer = 5;
	[SerializeField] private float sensitivity = 5.0f;
	private float turnRotation, lookRotation;
	private float clampY = 85f; 

	private Camera cam;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		cam = FindObjectOfType<Camera>();
		cam = Camera.main;
		transform.SetParent(cam.transform);
	}

	private void LateUpdate()
	{
		MouseLoook(cam, sensitivity);
		MoveCamera(cam, cameraMoveSpeed);
	}

	private void MouseLoook(Camera cam, float sensitifivy)
	{
		turnRotation += Input.GetAxis(MOUSE_X) * sensitivity;
		lookRotation += Input.GetAxis(MOUSE_Y) * sensitivity;

		lookRotation = Mathf.Clamp(lookRotation, -clampY, clampY);

		cam.transform.eulerAngles = new Vector3(-lookRotation, turnRotation, 0);
		transform.eulerAngles = new Vector3(0f, turnRotation, 0f);
	}

	public void MoveCamera(Camera camera, float moveSpeed)
	{
		moveSpeed *= Input.GetKey(boostKey) ? boostSpeedMultiplyer : 1;

		float translationZ;
		float translationX;

		translationZ = Input.GetAxis(VERTICAL) * moveSpeed * Time.deltaTime;
		translationX = Input.GetAxis(HORIZONTAL) * moveSpeed * Time.deltaTime;

		if (Input.GetKey(upKey))
			camera.transform.position += new Vector3(0f, moveSpeed, 0f) * Time.deltaTime;

		if (Input.GetKey(downKey))
			camera.transform.position -= new Vector3(0f, moveSpeed, 0f) * Time.deltaTime;

		camera.transform.Translate(translationX, 0, translationZ);
	}
}