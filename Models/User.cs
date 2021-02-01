using System;
using System.ComponentModel.DataAnnotations;

namespace Holamundo.Models{

    
    public class User{

        public int id_usuario {get;set;}

        [Required(ErrorMessage="Escriba sus nombres")]
        public string nombres {get;set;}

        [Required(ErrorMessage="Escriba sus apellidos")]
        public string apellidos {get;set;}

        [Required(ErrorMessage="Escriba su correo")]
        [EmailAddress(ErrorMessage="Formato de correo no valido")]
        public string correo {get;set;}

        [Range(18,120)]
        public int edad {get;set;}

    }

}