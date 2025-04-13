using System.Collections;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
   public float delay;

    public void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
