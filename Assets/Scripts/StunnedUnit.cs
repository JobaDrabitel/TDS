using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedUnit : MonoBehaviour
{
    private bool _isStunned = false;
    public bool IsStunned { get => _isStunned; set { _isStunned = value; } }
    public IEnumerator StunnedForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _isStunned=false;
    }
    public void SetStunned(float seconds)
    {
        _isStunned=true;
        StartCoroutine(StunnedForSeconds(seconds));
    }
}
