using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locar.Data;
using Locar.Models;
using Locar.AcessoDados.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Remotion.Linq.Clauses.ResultOperators;
using System.IO;

namespace Locar.Controllers
{
    public class CarroController : Controller
    {
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly ILogger<CarroController> _logger;
        private readonly IHostingEnvironment _hostingEnv;

        public CarroController(ICarroRepositorio carroRepositorio, ILogger<CarroController> logger, IHostingEnvironment hostingEnv)
        {
            _carroRepositorio = carroRepositorio;
            _logger = logger;
            _hostingEnv = hostingEnv;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _carroRepositorio.PegarTodos().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarroId,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                if(arquivo != null)
                {
                    var linkUpload = Path.Combine(_hostingEnv.WebRootPath, "Imagens");

                    using(FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Tentando criar o arquivo para foto...");

                        await arquivo.CopyToAsync(fileStream);

                        carro.Foto = "~/Imagens/" + arquivo.FileName;
                    }
                }

                await _carroRepositorio.Inserir(carro);
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var carro = await _carroRepositorio.PegarPeloId(id);

            if (carro == null)
            {
                return NotFound();
            }

            TempData["FotoCarro"] = carro.Foto;

            return View(carro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarroId,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (id != carro.CarroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    var linkUpload = Path.Combine(_hostingEnv.WebRootPath, "Imagens");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Tentando criar o arquivo para foto...");

                        await arquivo.CopyToAsync(fileStream);
                         
                        carro.Foto = "~/Imagens/" + arquivo.FileName;
                    }
                    return RedirectToAction("Index");
                }
                else 
                    carro.Foto = TempData["FotoCarro"].ToString();
                
                await _carroRepositorio.Atualizar(carro);

                return RedirectToAction("Index");                   
            }
                       
            return View(carro);
        }

        // POST: Carro/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var carro = await _carroRepositorio.PegarPeloId(id);
            string fotoCarro = carro.Foto;
            fotoCarro = fotoCarro.Replace("~", "wwwroot");
            System.IO.File.Delete(fotoCarro);


            await _carroRepositorio.Excluir(id);

            return Json("Carro excluído");
        }

    }
}
