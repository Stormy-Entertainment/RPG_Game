using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public Item item;

	private void PickUp()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);

		// If successfully picked up
		if (wasPickedUp)
			Destroy(gameObject);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
		{
			Debug.Log("Item collided with player");
			PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("Item collided with player");
			PickUp();
		}
	}
}
