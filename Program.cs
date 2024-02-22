using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AVLTree
{
    private class Node
    {
        public int Key, Height;
        public Node Left, Right;

        public Node(int key)
        {
            Key = key;
            Height = 1;
        }
    }

    private Node root;

    private int Height(Node node)
    {
        return node == null ? 0 : node.Height;
    }

    private int BalanceFactor(Node node)
    {
        return node == null ? 0 : Height(node.Left) - Height(node.Right);
    }

    private void UpdateHeight(Node node)
    {
        if (node != null)
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node T = x.Right;

        x.Right = y;
        y.Left = T;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node T = y.Left;

        y.Left = x;
        x.Right = T;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    private Node InsertRecursive(Node node, int key)
    {
        if (node == null)
            return new Node(key);

        if (key < node.Key)
            node.Left = InsertRecursive(node.Left, key);
        else if (key > node.Key)
            node.Right = InsertRecursive(node.Right, key);
        else
            return node; // Duplicate keys are not allowed

        UpdateHeight(node);

        int balance = BalanceFactor(node);

        // Left Left Case
        if (balance > 1 && key < node.Left.Key)
            return RotateRight(node);

        // Right Right Case
        if (balance < -1 && key > node.Right.Key)
            return RotateLeft(node);

        // Left Right Case
        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Right Left Case
        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    public void Insert(int key)
    {
        root = InsertRecursive(root, key);
    }

    private bool SearchRecursive(Node node, int key)
    {
        if (node == null)
            return false;

        if (key == node.Key)
            return true;
        else if (key < node.Key)
            return SearchRecursive(node.Left, key);
        else
            return SearchRecursive(node.Right, key);
    }

    public bool Search(int key)
    {
        return SearchRecursive(root, key);
    }

    private void InOrderTraversalRecursive(Node node)
    {
        if (node != null)
        {
            InOrderTraversalRecursive(node.Left);
            Console.Write($"{node.Key} ");
            InOrderTraversalRecursive(node.Right);
        }
    }

    public void InOrderTraversal()
    {
        InOrderTraversalRecursive(root);
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        AVLTree avlTree = new AVLTree();

        avlTree.Insert(10);
        avlTree.Insert(80);
        avlTree.Insert(30);
        avlTree.Insert(15);
        avlTree.Insert(5);
        avlTree.Insert(25);
        avlTree.Insert(3);
        avlTree.Insert(37);
        avlTree.Insert(92);

        Console.WriteLine("Obhojdane na AVL durvoto.");
        avlTree.InOrderTraversal();

        int searchKey = 20;
        if (avlTree.Search(searchKey))
        { 
        Console.WriteLine($"Neka proverim dali chisloto |{ searchKey }| prisustva v AVL durvoto.");
        Console.WriteLine($"Chisloto {searchKey} prisustva v AVL durvoto.");
        }
        else
        Console.WriteLine($"Neka proverim dali chisloto |{ searchKey}| prisustva v AVL durvoto.");
        Console.WriteLine($"Chisloto {searchKey} ne prisustva v AVL durvoto.");
        Console.ReadLine();
    }
}
