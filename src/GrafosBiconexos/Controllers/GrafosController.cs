namespace GrafosBiconexos.Controllers;

using GrafosBiconexos.Models;
// using GrafosBiconexos.Services;
using Microsoft.AspNetCore.Mvc;

public class GrafosController : Controller
{
    // private readonly GrafoService _service;

    // public GrafosController(GrafoService service)
    // {
    //     _service = service;
    // }

    public IActionResult Index()
    {
        var graph1 = new Graph(Path.Combine("Arquivos/biconexo.txt"));
        var alg1 = new BiconnectedAlgorithm(graph1);
        var g1 = new GrafoModel("biconexo.txt", graph1, alg1);

        var graph2 = new Graph(Path.Combine("Arquivos/nao_biconexo.txt"));
        var alg2 = new BiconnectedAlgorithm(graph2);
        var g2 = new GrafoModel("nao_biconexo.txt", graph2, alg2);

        return View(new List<GrafoModel> { g1, g2 });
    }

}

