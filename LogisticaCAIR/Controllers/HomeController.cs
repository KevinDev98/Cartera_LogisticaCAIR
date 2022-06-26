using LogisticaCAIR.Context;
using LogisticaCAIR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]        
        [method: HttpPost]
        public IActionResult Index(LoginViewModel.InputModel loginDataModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginDataModel.UserName) && !string.IsNullOrEmpty(loginDataModel.Pass))
                {
                    //SqlCommand command = new SqlCommand();
                    //command.CommandText = "SELECT * from USUARIOS WHERE UserName='" + loginDataModel.UserName.ToString() + "' AND PASS='" + loginDataModel.Pass.ToString() + "'";
                    //SqlDataReader reader = command.ExecuteReader();                   
                    var ValidaExists = from User in bd.Usuarios where User.Username==loginDataModel.UserName && User.Pass==loginDataModel.Pass
                                       select User;
                    var Perfil = from Usuario in bd.Usuarios where Usuario.Username == loginDataModel.UserName select Usuario.Perfil;
                    if (ValidaExists.Count() > 0)
                    {
                        try
                        {//Actualiza facturas cuando inicia sesión
                            DataTable Update = new DataTable();
                            var Ejecuta = new BD.ExecProcedures();
                            Update = Ejecuta.SP_ACTUALIZA_ESTATUS_FACTURAS(Bandera: 0);
                        }
                        catch (Exception)
                        {
                        }
                        ViewData["Session"]="1";
                        var Configuracion = from config in bd.Perfiles where config.IdPerfil.Equals(Perfil) select config.PermisoCobroProveedor;
                        ViewData["Configuracion"] = Configuracion.Take(1);
                        var CobroP = from cobP in bd.Perfiles where cobP.IdPerfil.Equals(Perfil) select cobP.PermisoCobroProveedor;
                        ViewData["CobroP"] = CobroP.Take(1);
                        var CobroC = from cobC in bd.Perfiles where cobC.IdPerfil.Equals(Perfil) select cobC.PermisoCobroCliente;
                        ViewData["CobroC"] = CobroC.Take(1);
                        var Facturas = from fact in bd.Perfiles where fact.IdPerfil.Equals(Perfil) select fact.PermisoFacturas;
                        ViewData["Facturas"] = Facturas.Take(1);
                        var Tarifas = from Tar in bd.Perfiles where Tar.IdPerfil.Equals(Perfil) select Tar.PermisoTarifas;
                        ViewData["Tarifas"] = Facturas.Take(1);
                        var Cartas = from Cts in bd.Perfiles where Cts.IdPerfil.Equals(Perfil) select Cts.PermisoCartas;
                        ViewData["Cartas"] = Facturas.Take(1);
                        return RedirectToAction("LoginOk");
                    }
                    else
                    {
                        ModelState.AddModelError("User", "Ingresar un usuario y contraseña validos");
                        return View();
                    }
                    

                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }


        }
        public IActionResult LoginOK()
        {
            // LA VALIDACIÓN DEL USUARIO HA SIDO CORRECTA
            return View();
        }
    }
}
