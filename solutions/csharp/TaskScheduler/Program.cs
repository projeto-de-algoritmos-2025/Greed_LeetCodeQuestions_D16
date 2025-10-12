using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int LeastInterval(char[] tasks, int n) {
        Dictionary<char, int> freq = new Dictionary<char, int>();
        foreach (char task in tasks) {
            if (!freq.ContainsKey(task)) freq[task] = 0;
            freq[task]++;
        }
        
        var sortedFreq = freq.Values.OrderByDescending(f => f).ToList();
        
        int maxFreq = sortedFreq[0];
        int frames = maxFreq - 1;
        int idleSlots = frames * n;
        
        for (int i = 1; i < sortedFreq.Count; i++) {
            idleSlots -= Math.Min(frames, sortedFreq[i]);
        }
        
        return tasks.Length + Math.Max(0, idleSlots);
    }
}

class Program {
    static void Main(string[] args) {
        Solution solution = new Solution();
        
        char[] tasks1 = ['A','A','A','B','B','B'];
        Console.WriteLine($"Example 1: {solution.LeastInterval(tasks1, 2)}");
        
        char[] tasks2 = ['A','C','A','B','D','B'];
        Console.WriteLine($"Example 2: {solution.LeastInterval(tasks2, 1)}");
        
        char[] tasks3 = ['A','A','A','B','B','B'];
        Console.WriteLine($"Example 3: {solution.LeastInterval(tasks3, 3)}");
    }
}