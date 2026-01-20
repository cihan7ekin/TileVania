using NUnit.Framework.Constraints;
using Unity.Cinemachine;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag== "Player")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinPickupSFX, new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z));
        }
    }
}
