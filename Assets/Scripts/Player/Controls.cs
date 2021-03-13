using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	public CharacterController controller;
	public float speed;

	void Update()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 100))
		{
			transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
			if (Input.GetMouseButtonDown(0))
			{
				TryOpenTrade(hit.transform);
			}
		}

		Vector3 direction = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward;
		controller.Move(direction.normalized * speed * Time.deltaTime);
	}

	void TryOpenTrade(Transform t)
	{
		Shopkeeper shopkeeper = t.GetComponent<Shopkeeper>();
		if (shopkeeper == null)
		{
			return;
		}
		shopkeeper.StartTrade();
	}
}
