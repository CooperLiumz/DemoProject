using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public string val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode (string x)
    {
        val = x;
    }

    public void SetChild (TreeNode _left , TreeNode _right)
    {
        left = _left;
        right = _right;
    }
}

public class BinaryTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        TreeNode root = CreatTree ();
        Debug.LogError ("递归前序");
        PreorderTraversal (root);
        Debug.LogError ("========");

        Debug.LogError ("迭代前序");
        Preorder (root);
        Debug.LogError ("========");

        Debug.LogError ("递归中序");
        InorderTraversal (root);
        Debug.LogError ("========");

        Debug.LogError ("迭代中序");
        Inorder (root);
        Debug.LogError ("========");

        Debug.LogError ("递归后序");
        PostorderTraversal (root);
        Debug.LogError ("========");

        Debug.LogError ("迭代后序");
        Postorder (root);
        Debug.LogError ("========");



    }
    /// <summary>
    ///                     A            
    /// 
    ///         B                       C  
    ///             
    ///             D               E       F 
    ///         G       H 
    /// </summary>


    TreeNode CreatTree ()
    {
        TreeNode nodeA = new TreeNode ("A");
        TreeNode nodeB = new TreeNode ("B");
        TreeNode nodeC = new TreeNode ("C");
        TreeNode nodeD = new TreeNode ("D");
        TreeNode nodeE = new TreeNode ("E");
        TreeNode nodeF = new TreeNode ("F");
        TreeNode nodeG = new TreeNode ("G");
        TreeNode nodeH = new TreeNode ("H");
        nodeA.SetChild (nodeB, nodeC);

        nodeB.SetChild (null, nodeD);

        nodeD.SetChild (nodeG , nodeH);

        nodeC.SetChild (nodeE , nodeF);
        return nodeA;
    }

    
    void Preorder (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }

        Stack<TreeNode> stack = new Stack<TreeNode> ();
        stack.Push (_root);

        TreeNode _node = null;

        while (stack != null && stack.Count > 0)
        {
            _node = stack.Pop ();
            Debug.LogError (_node.val);

            if (_node.right != null)
            {
                stack.Push (_node.right);
            }

            if (_node.left != null)
            {
                stack.Push (_node.left);
            }
        }
    }

    // 前序递归遍历 根 左 右
    void PreorderTraversal (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }
        Debug.LogError (_root.val);
        PreorderTraversal (_root.left);
        PreorderTraversal (_root.right);
    }
    
    void Inorder (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }

        Stack<TreeNode> stack = new Stack<TreeNode> ();
        stack.Push (_root);

        while (stack.Count > 0)
        {
            // 将左节点和根节点入栈
            while (stack.Peek().left != null)
            {
                stack.Push (stack.Peek ().left);
            }

            while (stack.Count > 0)
            {
                TreeNode _node = stack.Pop ();

                Debug.LogError (_node.val);

                if (_node.right != null)
                {
                    stack.Push (_node.right);
                    break;
                }
            }
        }
    }

    // 中序递归遍历 左 根 右
    void InorderTraversal (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }
        InorderTraversal (_root.left);
        Debug.LogError (_root.val);
        InorderTraversal (_root.right);
    }

    void Postorder (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }

        Stack<TreeNode> stack = new Stack<TreeNode> ();
        stack.Push (_root);

        TreeNode lastNode = null;
        while (stack.Count > 0)
        {
            while (stack.Peek ().left != null)
            {
                stack.Push (stack.Peek ().left);
            }
            while (stack.Count > 0)
            {
                if (lastNode == stack.Peek ().right || stack.Peek ().right == null)
                {
                    TreeNode node = stack.Pop ();
                    Debug.LogError (node.val);
                    lastNode = node;
                }
                else if (stack.Peek ().right != null)
                {
                    stack.Push (stack.Peek ().right);
                    break;
                }
            }
        }
    }


    // 后序递归遍历 左 右 根
    void PostorderTraversal (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }
        PostorderTraversal (_root.left);
        PostorderTraversal (_root.right);
        Debug.LogError (_root.val);
    }
    // 层遍历
    void LevelOrder (TreeNode _root)
    {
        if (_root == null)
        {
            return;
        }

        Queue<TreeNode> queue = new Queue<TreeNode> ();

        queue.Enqueue (_root);

        TreeNode front = null;

        while (queue.Count > 0)
        {
            front = queue.Dequeue ();

            if (front.left != null)
            {
                queue.Enqueue (front.left);
            }

            if (front.right != null)
            {
                queue.Enqueue (front.right);
            }

            Debug.LogError (front.val);
        }
    }


}
