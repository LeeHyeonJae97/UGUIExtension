using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class UIBehaviour : MonoBehaviour
    {
        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return UIManager.Instance.StartCoroutine(routine);
        }

        // NOTICE :
        // can stop all ui animations at once
        //
        public new void StopAllCoroutines()
        {
            UIManager.Instance.StopAllCoroutines();
        }
    }
}
