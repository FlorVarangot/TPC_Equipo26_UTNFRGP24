﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion, Activo FROM CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.ID = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Categoria ObtenerCategoriaPorId(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT ID, Descripcion, Activo FROM CATEGORIAS WHERE ID = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.ID = int.Parse(datos.Lector["Id"].ToString());
                    categoria.Descripcion = datos.Lector["Descripcion"].ToString();
                    categoria.Activo = (bool)datos.Lector["Activo"];

                    return categoria;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion, Activo) VALUES (@Descripcion, @Activo)");
                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.setearParametro("@Activo", categoria.Activo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET Descripcion = @Descripcion, Activo = @Activo WHERE Id = @Id");

                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.setearParametro("@Activo", categoria.Activo);
                datos.setearParametro("@Id", categoria.ID);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        internal void EliminarLogico(int Id, bool activo=false)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET Activo = @activo WHERE Id = @Id");
                datos.setearParametro("@Id", Id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}