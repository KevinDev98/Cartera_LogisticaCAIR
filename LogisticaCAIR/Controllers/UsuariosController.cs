using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogisticaCAIR.Context;
using LogisticaCAIR.Models;
using Microsoft.Extensions.Logging;
using System.Web;

namespace LogisticaCAIR.Controllers
{
    public class UsuariosController : Controller
    {
        //private readonly DB_LOGISTICA_CAIRContext _context;

        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }
        //public UsuariosController(DB_LOGISTICA_CAIRContext context)
        //{
        //    _context = context;
        //}
        DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
        // GET: Usuarios
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            var data = bd.Usuarios;
            
            //var dB_LOGISTICA_CAIRContext = bd.Usuarios.Include(u => u.PerfilNavigation);
            //return View(await dB_LOGISTICA_CAIRContext.ToListAsync());
            return View("../Configuracion/Usuarios", data.ToList());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await bd.Usuarios
                .Include(u => u.PerfilNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            //ViewData["Perfil"] = new SelectList(bd.Perfiles, "IdPerfil", "PerfilName");
            //return View();
            try
            {
                List<Perfile> list = (
                    from d in bd.Perfiles
                    select new Perfile
                    {
                        IdPerfil = d.IdPerfil,
                        PerfilName = d.PerfilName
                    }
                    ).ToList();
                List<SelectListItem> items = list.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.PerfilName.ToString(),
                        Value = d.IdPerfil.ToString(),
                        Selected = false
                    };
                }
                 );
                ViewBag.Perfiles = items;
                return View("../Configuracion/CrearUsuarios");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        //if (!bd.Usuarios.Any(e => e.IdUsuario == usuario.IdUsuario))
        //        {
                    
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Existente", "Ya existe un usuaro con este nombre");
        //        }
    // POST: Usuarios/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,NombreEmpleado,Username,Useremail,Perfil,Pass")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(usuario.Username) && !string.IsNullOrEmpty(usuario.NombreEmpleado) && !string.IsNullOrEmpty(usuario.Pass) && !string.IsNullOrEmpty(usuario.Perfil.ToString()))//(usuario.Username=="" || usuario.NombreEmpleado=="" || usuario.Pass=="" || usuario.Perfil==null)
                    {
                        bd.Add(usuario);
                        await bd.SaveChangesAsync();
                        ViewBag.Correcto = "No error";
                        return RedirectToAction(nameof(Index));
                        //ViewData["Perfil"] = new SelectList(bd.Perfiles, "IdPerfil", "PerfilName", usuario.Perfil);
                        //return View(usuario);
                    }
                    else
                    {
                        ViewBag.ErrorConfigUser = "Error al intentar crear/editar un usuario";
                        ModelState.AddModelError("Error", "Campos obligatorios; usuario, nombre,  contrase&ntilde;a  y puesto");
                    }
                    
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Ocurrio un error durante el proceso, contacte al administrador del sistema");
                }
            }
            else
            {
                ViewBag.ErrorConfigUser = "Error al intentar crear/editar un usuario. Campos obligatorios; usuario, nombre, contraseña y puesto";
                TempData["Error"] = ViewBag.ErrorConfigUser;
                ModelState.AddModelError("Error", "Campos obligatorios; usuario, nombre, contraseña y puesto");
            }           
            var data = bd.Usuarios;
            return View("../Configuracion/Usuarios", data.ToList());
            //ModelState.AddModelError("User", "Ingresar un usuario y contraseña validos");
        }

        // GET: Usuarios/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await bd.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["Perfil"] = new SelectList(bd.Perfiles, "IdPerfil", "PerfilName", usuario.Perfil);
            return View("../Configuracion/EditarUsuarios", usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,NombreEmpleado,Username,Useremail,Perfil,Pass")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (usuario.Pass=="" || usuario.Pass==null)
                    {
                        var LastPast = from pass in bd.Usuarios where usuario.IdUsuario == id select pass.Pass;
                        usuario.Pass = LastPast.FirstOrDefault();
                    }
                    bd.Update(usuario);
                    await bd.SaveChangesAsync();
                    ViewBag.Correcto = "No error";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("Error", ex.Message.ToString());
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Perfil"] = new SelectList(bd.Perfiles, "IdPerfil", "PerfilName", usuario.Perfil);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var usuario = await bd.Usuarios
                    .Include(u => u.PerfilNavigation)
                    .FirstOrDefaultAsync(m => m.IdUsuario == id);
                if (usuario == null)
                {
                    return NotFound();
                }
                //var listUsr = bd.Usuarios;
                //return View("../Configuracion/EliminaUsuario", listUsr.ToList());
                return View("../Configuracion/EliminaUsuario", usuario);
            }
            catch (Exception exa)
            {

                return View("Index");
            }


        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await bd.Usuarios.FindAsync(id);
            bd.Usuarios.Remove(usuario);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return bd.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
