using UnityEngine;

public class BSTNode<T>
{
    public T Value;
    public BSTNode<T> Left;
    public BSTNode<T> Right;

    public BSTNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

