namespace GrafosBiconexos.Models;

using System;
using System.Collections.Generic;
using System.IO;

public class Graph
{
    public int VertexCount { get; private set; }
    public int EdgeCount { get; private set; }

    private readonly List<Edge>[] _adj;

    public Graph(int vertices)
    {
        if (vertices < 0)
            throw new ArgumentException("Número de vértices deve ser não negativo.");

        VertexCount = vertices;
        EdgeCount = 0;

        _adj = new List<Edge>[vertices];
        for (int i = 0; i < vertices; i++)
            _adj[i] = new List<Edge>();
    }

    public Graph(string filePath)
    {
        using var reader = new StreamReader(filePath);

        VertexCount = int.Parse(reader.ReadLine());
        int edges = int.Parse(reader.ReadLine());

        _adj = new List<Edge>[VertexCount];
        for (int i = 0; i < VertexCount; i++)
            _adj[i] = new List<Edge>();

        for (int i = 0; i < edges; i++)
        {
            var parts = reader.ReadLine().Split();
            int v1 = int.Parse(parts[0]);
            int v2 = int.Parse(parts[1]);
            AddEdge(new Edge(v1, v2));
        }
    }

    public void AddEdge(Edge e)
    {
        int v1 = e.V1;
        int v2 = e.V2;

        Validate(v1);
        Validate(v2);

        _adj[v1].Add(e);
        _adj[v2].Add(new Edge(v2, v1, e.Weight));
        EdgeCount++;
    }

    public IEnumerable<Edge> Adj(int v)
    {
        Validate(v);
        return _adj[v];
    }

    public int Degree(int v)
    {
        Validate(v);
        return _adj[v].Count;
    }

    private void Validate(int v)
    {
        if (v < 0 || v >= VertexCount)
            throw new IndexOutOfRangeException($"Vértice {v} está fora dos limites.");
    }

    public override string ToString()
    {
        string result = $"{VertexCount} {EdgeCount}\n";

        for (int v = 0; v < VertexCount; v++)
        {
            result += $"{v}: ";
            foreach (var e in _adj[v])
                result += $"{e.Other(v)} ";
            result += "\n";
        }
        return result;
    }
}
