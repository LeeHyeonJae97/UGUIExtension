using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public interface ISelectable
    {
        public bool interactable { get; set; }
        public bool selected { get; set; }
        public bool pressed { get; set; }
    }
}