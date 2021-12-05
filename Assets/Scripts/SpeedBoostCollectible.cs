using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostCollectible : MonoBehaviour
{

  public GameObject lightningEffectPrefab;

  public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
      RubyController controller = other.GetComponent<RubyController>();

      if (controller != null)
      {
        if(controller.health < controller.maxHealth)
        {
	       //controller.ChangeHealth(1);
         GameObject lightningEffectObject = Instantiate(lightningEffectPrefab, transform.position, Quaternion.identity);
	       Destroy(gameObject);
         
         controller.PlaySound(collectedClip);
        }
      }
    }

}

