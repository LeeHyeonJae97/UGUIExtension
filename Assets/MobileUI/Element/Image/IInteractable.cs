using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public interface IInteractable
    {
        public bool interactable { get; set; }
        public bool pressed { get; set; }
    }
}