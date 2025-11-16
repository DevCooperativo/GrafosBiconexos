namespace GrafosBiconexos.Models;

using System;
using System.Collections.Generic;

public class BiconnectedAlgorithm
{
    public int[] Low { get; private set; }
    public int[] Pre { get; private set; }
    public bool[] Articulation { get; private set; }
    public int Counter { get; private set; }

    public BiconnectedAlgorithm(Graph g)
    {
        int n = g.VertexCount;

        Low = new int[n];
        Pre = new int[n];
        Articulation = new bool[n];

        Array.Fill(Low, -1);
        Array.Fill(Pre, -1);

        for (int v = 0; v < n; v++)
        {
            if (Pre[v] == -1)
                DFS(g, v, v);
        }
    }

    private void DFS(Graph g, int parent, int v)
    {
        int children = 0;
        Pre[v] = Counter++;
        Low[v] = Pre[v];

        foreach (var edge in g.Adj(v))
        {
            int w = edge.Other(v);

            if (Pre[w] == -1)
            {
                children++;
                DFS(g, v, w);

                Low[v] = Math.Min(Low[v], Low[w]);

                if (Low[w] >= Pre[v] && parent != v)
                    Articulation[v] = true;
            }
            else if (w != parent)
            {
                Low[v] = Math.Min(Low[v], Pre[w]);
            }
        }

        if (parent == v && children > 1)
            Articulation[v] = true;
    }

    public bool IsArticulation(int v) => Articulation[v];

    public IEnumerable<int> ArticulationPoints()
    {
        for (int i = 0; i < Articulation.Length; i++)
            if (Articulation[i])
                yield return i;
    }
}

