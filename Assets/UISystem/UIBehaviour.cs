using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIBehaviour : MonoBehaviour
    {
        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            if (gameObject.activeInHierarchy)
            {
                return base.StartCoroutine(routine);
            }
            else
            {
                return UIManager.Instance.StartCoroutine(routine);
            }
        }
    }
}
