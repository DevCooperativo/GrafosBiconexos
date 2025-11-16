namespace GrafosBiconexos.Models;

public class Edge : IComparable<Edge>
{
    public int V1 { get; }
    public int V2 { get; }
    public double Weight { get; }

    public Edge(int v1, int v2, double weight = 0)
    {
        if (v1 < 0 || v2 < 0)
            throw new ArgumentOutOfRangeException("Vértices devem ser não negativos.");

        V1 = v1;
        V2 = v2;
        Weight = weight;
    }

    public int Other(int vertex)
    {
        if (vertex == V1) return V2;
        if (vertex == V2) return V1;
        throw new ArgumentException("Vértice inválido.");
    }

    public override string ToString()
    {
        return $"{V1}-{V2} ({Weight})";
    }

    public int CompareTo(Edge other)
    {
        return Weight.CompareTo(other.Weight);
    }
}
