using LogisticaCAIR.Context;
using LogisticaCAIR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Controllers
{
    public class Clientes : Controller
    {
        DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
         static string ResultAction;
        public IActionResult Index()
        {
            try
            {
                ViewBag.Correcto = ResultAction;
                ResultAction = null;
                var dataC = bd.CatClientes;
                return View("../Configuracion/Clientes", dataC.ToList());
            }
            catch (Exception)
            {

                throw;
            }

            
        }
        public IActionResult Create()
        {
            try
            {
                ViewBag.Action = "Create";
                return View("../Configuracion/ConfiguracionClientes");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatCliente cliente)
        {
            try
            {                
                var ValidaExistencia = from clie in bd.CatClientes where clie.CardCode == cliente.CardCode select clie;
                if (ValidaExistencia.Any())
                {
                    ViewBag.Action = "Create";
                    ViewBag.Correcto = "No";
                    TempData["ErrorConfPv"] = "El Id de este usuario ya existe";
                    ModelState.AddModelError("Error", "El Id de este cliente ya existe");
                    //return View();
                }
                else
                {                    
                    bd.Add(cliente);
                    await bd.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    ViewBag.Action = "Create"; //Define el tipo de vista que se abrira
                    ViewBag.Correcto = "No error"; //Indicara que no hubo error durante la transacción y mostrará el mensaje correcto
                }
                return View("../Configuracion/ConfiguracionClientes");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == "")
            {                
                return RedirectToAction("Index");
            }
            ViewBag.Action = "Update";
            var cliente = await bd.CatClientes.FindAsync(id);//busca el cliente
            if (cliente == null)
            {
                return RedirectToAction("Index");
            }
            //ViewData["Perfil"] = new SelectList(bd.Perfiles, "IdPerfil", "PerfilName", cliente.BancoPreferencia);
            return View("../Configuracion/ConfiguracionClientes", cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CatCliente cliente)
        {
            try
            {
                bd.Update(cliente);
                await bd.SaveChangesAsync();
                ResultAction = "Actualizado";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == "")
                {
                    return NotFound();
                }

                var clie = await bd.CatClientes
                    .FirstOrDefaultAsync(m => m.CardCode == id);
                if (clie == null)
                {
                    return NotFound();
                }
                //var listUsr = bd.Usuarios;
                //return View("../Configuracion/EliminaUsuario", listUsr.ToList());
                return View("../Configuracion/ConfiguracionClientes", clie);
            }
            catch (Exception exa)
            {

                return View("Index");
            }


        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cliente = await bd.CatClientes.FindAsync(id);
            bd.CatClientes.Remove(cliente);
            await bd.SaveChangesAsync();
            ViewBag.Action = "Delete";
            return RedirectToAction(nameof(Index));
        }

    }
}
