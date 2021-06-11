using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序的方式存储的，并且它们的每个节点只能存储 一位数字。

//如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。

//您可以假设除了数字 0 之外，这两个数都不会以 0 开头。

//示例：

//输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
//输出：7 -> 0 -> 8
//原因：342 + 465 = 807
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode (int x)
    {
        val = x;
    }
}


public class SumTwoListNode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ListNode node1 = new ListNode (1);
        ListNode node2 = new ListNode (8);
        //ListNode node3 = new ListNode (3);
        node1.next = node2;
        node2.next = null;
        //node3.next = null;

        ListNode log = node1;
        while (log != null)
        {
            Debug.LogError (log.val);
            log = log.next;
        }


        ListNode node4 = new ListNode (0);
        ListNode node5 = new ListNode (6);
        ListNode node6 = new ListNode (4);
        node4.next = null;
        //node5.next = node6;
        //node6.next = null;

        log = node4;
        while (log != null)
        {
            Debug.LogError (log.val);
            log = log.next;
        }

        ListNode _result = Solution (node1 , node4);
        List<int> _list = new List<int> ();
        while (_result != null)
        {
            Debug.LogError (_result.val);
            _list.Add (_result.val);
            _result = _result.next;
        }
        for (int i = _list.Count -1 ; i >= 0 ; i--)
        {
            Debug.LogError (_list[i]);
        }
    }

    ListNode Solution (ListNode ln1 , ListNode ln2)
    {
        int overNum = 0;

        ListNode root = new ListNode (-1);
        ListNode result = root;

        while (ln1 != null || ln2 != null)
        {
            int ln1v = 0;
            int ln2v = 0;
            if (ln1 != null)
            {
                ln1v = ln1.val;
            }

            if (ln2 != null)
            {
                ln2v = ln2.val;
            }

            int count = ln1v + ln2v + overNum;
            overNum = count / 10;
            result.next = new ListNode (count % 10);

            result = result.next;

            ln1 = ln1?.next;
            ln2 = ln2?.next;

            if (ln1 == null && ln2 == null)
            {
                if (overNum > 0)
                {
                    result.next = new ListNode (overNum);
                }
                else
                {
                    result.next = null;
                }
                
            }
        }
        return root.next;
    }
}
