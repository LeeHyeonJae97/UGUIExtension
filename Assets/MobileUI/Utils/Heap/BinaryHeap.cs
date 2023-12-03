using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BinaryHeap<T> where T : IComparable<T>
{
    public int Count => _size;
    public bool IsEmpty => _size == 0;
    public bool IsFull => _size == _elements.Length;

    protected T[] _elements;
    protected int _size;

    protected abstract void SortDown();
    protected abstract void SortUp();

    public BinaryHeap(int size)
    {
        _elements = new T[size];
    }

    public T this[int index]
    {
        get
        {
            return _elements[index];
        }
    }

    public T Peek()
    {
        if (IsEmpty) throw new IndexOutOfRangeException();

        return _elements[0];
    }

    public T Pop()
    {
        if (IsEmpty) throw new IndexOutOfRangeException();

        var pop = _elements[0];
        _elements[0] = _elements[--_size];
        SortDown();

        return pop;
    }

    public void Add(T element)
    {
        if (IsFull) throw new IndexOutOfRangeException();

        _elements[_size++] = element;
        SortUp();
    }

    public void Clear()
    {
        _size = 0;

        for (int i = 0; i < _elements.Length; i++)
        {
            _elements[i] = default;
        }
    }

    //public bool Contains(T element)
    //{
    //    for (int i = 0; i < _size; i++)
    //    {
    //        if (_elements[i].Equals(element))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public void OnDrawGizmos()
    {
        var position = Vector2.zero;
        var spacing = 10f;

        DrawNode(0, position, true);

        void DrawNode(int index, Vector2 position, bool left)
        {
            if (_size == 0) return;

            position += new Vector2(spacing * Mathf.Pow(.5f, GetLevel(index)) * (left ? -1 : 1), -spacing / 2);

            if (HasLeftChild(index))
            {
                DrawNode(GetLeftChildIndex(index), position, true);
            }
            if (HasRightChild(index))
            {
                DrawNode(GetRightChildIndex(index), position, false);
            }

            Handles.Label(position, $"{_elements[index]}");
        }
    }

    protected void Swap(int i1, int i2)
    {
        var tmp = _elements[i1];
        _elements[i1] = _elements[i2];
        _elements[i2] = tmp;
    }

    protected bool HasLeftChild(int index)
    {
        return GetLeftChildIndex(index) < _size;
    }

    protected bool HasRightChild(int index)
    {
        return GetRightChildIndex(index) < _size;
    }

    protected bool IsRoot(int index)
    {
        return index == 0;
    }

    protected int GetLeftChildIndex(int index)
    {
        return 2 * index + 1;
    }

    protected int GetRightChildIndex(int index)
    {
        return 2 * index + 2;
    }

    protected int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }

    protected T GetLeftChild(int index)
    {
        return HasLeftChild(index) ? _elements[GetLeftChildIndex(index)] : default;
    }

    protected T GetRightChild(int index)
    {
        return HasRightChild(index) ? _elements[GetRightChildIndex(index)] : default;
    }

    protected T GetParent(int index)
    {
        return IsRoot(index) ? default : _elements[GetParentIndex(index)];
    }

    protected int GetLevel(int index)
    {
        return GetLevel(index, 0);

        int GetLevel(int index, int level)
        {
            if (IsRoot(index))
            {
                return level;
            }
            else
            {
                return GetLevel(GetParentIndex(index), level + 1);
            }
        }
    }
}
