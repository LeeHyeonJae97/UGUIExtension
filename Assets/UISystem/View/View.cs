using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public static class View
    {
        public static void Set<T1>() where T1 : Window
        {
            Window.Clear();

            Window.Get<T1>().Open(true);
        }

        public static void Set<T1, T2>() where T1 : Window where T2 : Window
        {
            Window.Clear();

            Window.Get<T1>().Open(true);
            Window.Get<T2>().Open(true);
        }

        public static void Set<T1, T2, T3>() where T1 : Window where T2 : Window where T3 : Window
        {
            Window.Clear();

            Window.Get<T1>().Open(true);
            Window.Get<T2>().Open(true);
            Window.Get<T3>().Open(true);
        }
    }
}