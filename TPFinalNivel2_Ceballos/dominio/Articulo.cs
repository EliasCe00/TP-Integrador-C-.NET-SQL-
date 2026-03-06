using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        //private decimal precio;

        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public Marca Marca { get; set; }
            
        public Categoria Categoria { get; set; }

        public string ImagenUrl { get; set; }

        public decimal Precio { get; set; }
        /*{
            get
            {
                return precio;
            }
            set
            {
                precio = value;
            }
        }*/

        /*public string formatoMomeda
        {
            get { return precio.ToString("C2", CultureInfo.CreateSpecificCulture("es-Ar")); }
        }*/

    }
}
