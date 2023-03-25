using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : BuffItem
{
    private bool _isMirrored = false;    
    public IEnumerator MirrorDelay()
    {
        yield return new WaitForSeconds(1f);

        _isMirrored = false;
    }
    public void MirrorItem()
    {
        if (!_isMirrored)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            _isMirrored = true;
           StartCoroutine(MirrorDelay());
        }
    }
    private void Update()
    {
        MirrorItem();
    }
}
