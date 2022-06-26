using LogisticaCAIR.Context;
using LogisticaCAIR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogisticaCAIR.Controllers
{
    public class ProveedoresController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
        public static string ActionRealizada=""; 
        public IActionResult Index()
        {
            try
            {
                
                ViewBag.DatoActualizado = ActionRealizada;
                var dataC = bd.CatProveedores;
                return View("../Configuracion/Proveedores", dataC.ToList());
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
                return View("../Configuracion/ConfiguracionProveedores");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatProveedore Prov)
        {
            try
            {
                var returnViewResult = "";
                var ValidaProv = from Pv in bd.CatProveedores where Pv.CardCode == Prov.CardCode select Pv.CardCode;
                if (ValidaProv.Any())
                {
                    ViewBag.Action = "Create";
                    ViewBag.Correcto = "No";
                    TempData["ErrorConfPv"] = "El Id de este usuario ya existe";
                    //TempData["ErrorConfPv"]= HttpUtility.UrlDecode(TempData["ErrorConfPv"].ToString());
                    ModelState.AddModelError("Error", "El Id de este proveedor ya existe");
                    returnViewResult = "../Configuracion/ConfiguracionProveedores";
                }
                else
                {
                    bd.Add(Prov);
                    await bd.SaveChangesAsync();
                    ActionRealizada = "Create"; //define el tipo de vista con el que se abrira el esta página
                    //return RedirectToAction(nameof(Index));
                    //var dataP = bd.CatProveedores;
                    returnViewResult = "../Configuracion/ConfiguracionProveedores";
                    ViewBag.Correcto = "No error";//Indicara que no hubo error durante la transacción y mostrará el mensaje correcto

                }

                return View(returnViewResult);
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
            var prov = await bd.CatProveedores.FindAsync(id);//busca el cliente
            if (prov == null)
            {
                return RedirectToAction("Index");
            }
            //ViewData["Perfil"] = new SelectList(bd.Perfiles, "IdPerfil", "PerfilName", cliente.BancoPreferencia);
            return View("../Configuracion/ConfiguracionProveedores", prov);
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, CatProveedore prov)
        public IActionResult Edit(string id, CatProveedore prov)
        {
            try
            {
                bd.Update(prov);
                bd.SaveChanges();
                ViewBag.Update = "Actualizado";
                ActionRealizada = "Actualizado";
                //await bd.SaveChangesAsync();
                //StringBuilder cstext1 = new StringBuilder();
                //cstext1.Append("<script type=text/javascript> function Actualizado(){alert(Actualizado);} </");
                //cstext1.Append("script>");
                //swal('Correcto!', 'Proceso terminado con exito', 'success');
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorConfPv"] = "El Id de este usuario ya existe";
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

                var prov = await bd.CatProveedores
                    .FirstOrDefaultAsync(m => m.CardCode == id);
                if (prov == null)
                {
                    return NotFound();
                }
                //var listUsr = bd.Usuarios;
                //return View("../Configuracion/EliminaUsuario", listUsr.ToList());
                return View("../Configuracion/ConfiguracionProveedores", prov);
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
            var prov = await bd.CatProveedores.FindAsync(id);
            bd.CatProveedores.Remove(prov);
            await bd.SaveChangesAsync();
            ViewBag.Action = "Delete";
            return RedirectToAction(nameof(Index));
        }
    }
}
