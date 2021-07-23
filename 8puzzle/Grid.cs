using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace _8puzzle

{
    public class Grid
    {
        public Dictionary<String, String> stateHistory = new Dictionary<String, String>();

        public Queue<String> BFSQueue = new Queue<String>();

        public ArrayList steps = new ArrayList();

        public void Start(String str)
        {
            BFSQueue.Enqueue(str);
            stateHistory.Add(str, null);
        }
        public void add(String old, String next)
        {
            if (!stateHistory.ContainsKey(next))
            {
                BFSQueue.Enqueue(next);
                stateHistory.Add(next, old);
            }
        }

        public ArrayList check(String old, String next)
        {          
            add(old, next);
            if (next.Equals("123456780"))
            {   
                String solution = next;
                while (solution != null)
                {              
                    steps.Add(solution);
                    stateHistory.TryGetValue(solution, out solution);
                }
                return steps;
            }
            return null;
        }

        public void move(String current)
        {
            int a = current.IndexOf("0");
            if (a > 2)
            {
                check(current, swap(a, a - 3, current));
            }
            if (a < 6)
            {  
                check(current, swap(a, a + 3, current));
            }
            if (a != 0 && a != 3 && a != 6)
            {                   
                check(current, swap(a, a - 1, current));
            }
            if (a != 2 && a != 5 && a != 8)
            {  
                check(current, swap(a, a + 1, current));
            }
        }
        public String swap(int a, int b, String s)
        {
            char[] rarr = s.ToCharArray();
            rarr[b] = s[a];
            rarr[a] = s[b];
            return new string(rarr);
        }
    }
}
