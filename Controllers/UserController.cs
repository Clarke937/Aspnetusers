using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Holamundo.Models;
using Holamundo.Database;

namespace Holamundo.Controllers{
    public class UserController: Controller{

        public DBConexion Db { get; }

        public UserController(DBConexion db){
            Db = db;
        }

        public async Task<IActionResult> Index(){
            ViewBag.Usuarios = await new UserQuerys(Db).selectAll();
            return View();
        }
        
        [BindProperty]
        public User usuario {get;set;}
        public async Task<IActionResult> Insertar(){
            await new UserQuerys(Db).Insertar(usuario);
            return RedirectToAction("Index");
        }

        [BindProperty]
        public int id_usuario {get;set;}
        public async Task<IActionResult> Delete(){
            await new UserQuerys(Db).Delete(id_usuario);
            return RedirectToAction("Index");
        }

    }
}