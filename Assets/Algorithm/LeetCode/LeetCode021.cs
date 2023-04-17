using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//将两个升序链表合并为一个新的 升序 链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 

//输入：l1 = [1,2,4], l2 = [1,3,4]
//输出：[1,1,2,3,4,4]
//示例 2：

//输入：l1 = [], l2 = []
//输出：[]
//示例 3：

//输入：l1 = [], l2 = [ 0 ]
//输出：[0]

public class LeetCode021 : LeetCode
{
    public class ListNode
    {
        public int val;

        public ListNode next;

        public ListNode ( int _val = 0, ListNode _next = null )
        {
            this.val = _val;
            this.next = _next;
        }
    }


    public override void DoSolution ( )
    {
        //Debug.LogError ( MergeTwoLists ( "()[]{}" ) );
    }

    public ListNode MergeTwoLists ( ListNode list1, ListNode list2 )
    {

        return null;
    }

}
