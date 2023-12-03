using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaxHeap<T> : BinaryHeap<T> where T : IComparable<T>
{
    public MaxHeap(int size) : base(size)
    {

    }

    protected override void SortDown()
    {
        var index = 0;

        while (HasLeftChild(index))
        {
            var biggerIndex = GetLeftChildIndex(index);

            if (HasRightChild(index) && GetLeftChild(index).CompareTo(GetRightChild(index)) < 0)
            {
                biggerIndex = GetRightChildIndex(index);
            }

            if (_elements[index].CompareTo(_elements[biggerIndex]) > 0)
            {
                break;
            }

            Swap(biggerIndex, index);
            index = biggerIndex;
        }
    }

    protected override void SortUp()
    {
        var index = _size - 1;

        while (!IsRoot(index) && _elements[index].CompareTo(GetParent(index)) > 0)
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }
}
