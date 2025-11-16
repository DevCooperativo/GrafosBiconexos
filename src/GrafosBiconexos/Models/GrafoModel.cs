namespace GrafosBiconexos.Models;

public class GrafoModel
{
    public string Nome { get; set; } = "";
    public int Vertices { get; set; }
    public List<(int v1, int v2)> Arestas { get; set; } = new();
    public int[] Low { get; set; }
    public int[] Pre { get; set; }
    public bool[] Articulacoes { get; set; }
    public List<int> ListaArticulacoes { get; set; } = new();
    public Dictionary<int, (int low, int pre)> ValoresPorVertice { get; set; } = new();

    public GrafoModel() { }
    public GrafoModel(string nomeArquivo, Graph graph)
    {
        Nome = nomeArquivo;
        Vertices = graph.VertexCount;

        var conjunto = new HashSet<string>();

        for (int v = 0; v < graph.VertexCount; v++)
        {
            foreach (var edge in graph.Adj(v))
            {
                int v1 = edge.V1;
                int v2 = edge.V2;

                // evita duplicatas
                string chave = v1 < v2 ? $"{v1}-{v2}" : $"{v2}-{v1}";

                if (!conjunto.Contains(chave))
                {
                    conjunto.Add(chave);
                    Arestas.Add((Math.Min(v1, v2), Math.Max(v1, v2)));
                }
            }
        }
    }

    public GrafoModel(string nomeArquivo, Graph graph, BiconnectedAlgorithm algorithm)
        : this(nomeArquivo, graph)
    {
        Low = algorithm.Low;
        Pre = algorithm.Pre;
        Articulacoes = algorithm.Articulation;

        ListaArticulacoes = Enumerable
            .Range(0, Articulacoes.Length)
            .Where(i => Articulacoes[i])
            .ToList();

        ValoresPorVertice = new Dictionary<int, (int low, int pre)>();

        for (int i = 0; i < graph.VertexCount; i++)
        {
            ValoresPorVertice[i] = (Low[i], Pre[i]);
        }
    }
}
