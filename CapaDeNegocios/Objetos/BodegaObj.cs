using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto bodega.
    /// </summary>
    public class BodegaObj
    {
        private int bodegaID;
        private String nombre;
        private String ubicacion;
        private bool activoSN;

        public BodegaObj()
        {
        }

        private BodegaObj(int bodegaID, string nombre, string ubicacion, bool activoSN)
        {
            this.bodegaID = bodegaID;
            this.nombre = nombre;
            this.ubicacion = ubicacion;
            this.activoSN = activoSN;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public int getBodegaID()
        {
            return this.bodegaID;
        }

        public String getUbicacion()
        {
            return this.ubicacion;
        }

        public bool getActivoSN()
        {
            return this.activoSN;
        }

        public BodegaObj getBodegaObj(int bodegaID, string nombre, string ubicacion, bool activoSN)
        {
            BodegaObj bodega = new BodegaObj(bodegaID, nombre, ubicacion, activoSN);
            return bodega;
        }

    }
}
