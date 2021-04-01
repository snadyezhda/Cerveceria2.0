using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria2._0.Validations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal class CustomCheckAttribute : AuthorizeAttribute
    {
        //Puedes crear propiedades para OPCIONALMENTE agregar filtros al usuario
        private string rol;
        public string Rol
        {
            get
            {
                return this.rol;
            }
            set
            {
                this.rol = value;
            }
        }

        //Para crear un atributo de filtro personalizado, basta con
        //hacer "override" en dos métodos:

    }
}
