public class Trie {
    private class TrieNode {
        public TrieNode[] Children;
        public bool IsEndOfWord;
        public int Depth;
        
        public TrieNode(int depth) {
            Children = new TrieNode[26];
            IsEndOfWord = false;
            Depth = depth;
        }
    }
    
    private TrieNode root;
    private List<(int start, int end)> wordIntervals;
    
    public Trie() {
        root = new TrieNode(0);
        wordIntervals = new List<(int, int)>();
    }
    
    public void Insert(string word) {
        TrieNode current = root;
        int startDepth = 0;
        
        foreach (char c in word) {
            int index = c - 'a';
            if (current.Children[index] == null) {
                current.Children[index] = new TrieNode(current.Depth + 1);
            }
            current = current.Children[index];
        }
        current.IsEndOfWord = true;
        
        int endDepth = current.Depth;
        wordIntervals.Add((startDepth, endDepth));
        wordIntervals.Sort((a, b) => a.end.CompareTo(b.end));
    }
    
    public bool Search(string word) {
        TrieNode current = root;
        
        foreach (char c in word) {
            int index = c - 'a';
            if (current.Children[index] == null) {
                return false;
            }
            current = current.Children[index];
        }
        return current.IsEndOfWord;
    }
    
    public bool StartsWith(string prefix) {
        TrieNode current = root;
        
        foreach (char c in prefix) {
            int index = c - 'a';
            if (current.Children[index] == null) {
                return false;
            }
            current = current.Children[index];
        }
        return true;
    }
    
    public int GetMaxNonOverlappingWords() {
        if (wordIntervals.Count == 0) return 0;
        
        int count = 1;
        int lastEnd = wordIntervals[0].end;
        
        for (int i = 1; i < wordIntervals.Count; i++) {
            if (wordIntervals[i].start >= lastEnd) {
                count++;
                lastEnd = wordIntervals[i].end;
            }
        }
        
        return count;
    }
}

class Program {
    static void Main(string[] args) {
        Trie trie = new Trie();
        
        trie.Insert("apple");
        Console.WriteLine($"Search 'apple': {trie.Search("apple")}");
        Console.WriteLine($"Search 'app': {trie.Search("app")}");
        Console.WriteLine($"StartsWith 'app': {trie.StartsWith("app")}");
        
        trie.Insert("app");
        Console.WriteLine($"Search 'app': {trie.Search("app")}");
        
        Console.WriteLine($"Max non-overlapping words: {trie.GetMaxNonOverlappingWords()}");
    }
}