using System;
using System.Collections;
using UnityEngine;

public class BookTester : MonoBehaviour {

    public Potion potion;
    public BookAnimation animator;
    
    private void OnEnable() {
        StartCoroutine(DelayedActionCoroutine());
        
    }

    IEnumerator DelayedActionCoroutine()
    {
        yield return new WaitForSeconds(1f);
        animator.ShowUnsolvedPotion(potion);
        yield return new WaitForSeconds(5f);
        animator.ShowSolvedPotion(potion, null);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
