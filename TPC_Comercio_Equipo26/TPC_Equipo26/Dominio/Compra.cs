﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Compra
    {
        public int ID { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdProveedor { get; set; }
        public float Total { get; set; }
        public List<Articulo> DetalleCompra { get; set; }

        //public bool Activo {get; set;}

    }
}