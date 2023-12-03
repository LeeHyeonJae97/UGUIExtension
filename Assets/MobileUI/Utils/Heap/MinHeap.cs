using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinHeap<T> : BinaryHeap<T> where T : IComparable<T>
{
    public MinHeap(int size) : base(size)
    {

    }

    protected override void SortDown()
    {
        int index = 0;

        while (HasLeftChild(index))
        {
            var smallerIndex = GetLeftChildIndex(index);

            if (HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) < 0)
            {
                smallerIndex = GetRightChildIndex(index);
            }

            if (_elements[index].CompareTo(_elements[smallerIndex]) < 0)
            {
                break;
            }

            Swap(smallerIndex, index);
            index = smallerIndex;
        }
    }

    protected override void SortUp()
    {
        var index = _size - 1;

        while (!IsRoot(index) && _elements[index].CompareTo(GetParent(index)) < 0)
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }
}
