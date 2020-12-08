using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace algo{
    class Graph {
        private int V; // No. of vertices
    
        // Array of lists for
        // Adjacency List Representation
        private List<int>[] adj;
    
        // Constructor
        public Graph(int v)
        {
            V = v;
            adj = new List<int>[ v ];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
        }
    
        // Function to Add an edge into the graph
        public void AddEdge(int v, int w)
        {
            adj[v].Add(w); // Add w to v's list.
        }
    
        // A function used by DFS
        public void DFSUtil(int v, bool[] visited)
        {
            // Mark the current node as visited
            // and print it
            visited[v] = true;
    
            // Recur for all the vertices
            // adjacent to this vertex
            List<int> vList = adj[v];
            foreach(var n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }
    
        // The function to do DFS traversal.
        // It uses recursive DFSUtil()
        public double DFS(int v)
        {
            // Mark all the vertices as not visited
            // (set as false by default in c#)
            bool[] visited = new bool[V];
    
            // Call the recursive helper function
            // to print DFS traversal
            var startTime = new Stopwatch();
            startTime.Start();
            for (int i = 0; i < V; ++i)
                {if (visited[i] == false) DFSUtil(i, visited);}
            startTime.Stop();
            return startTime.ElapsedMilliseconds;
        }
    }
}