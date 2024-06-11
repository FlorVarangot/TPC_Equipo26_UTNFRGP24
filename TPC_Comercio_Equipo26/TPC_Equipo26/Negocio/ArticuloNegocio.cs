﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;


namespace TPC_Equipo26.Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Ganancia_Porcentaje, A.Stock_Minimo, A.Imagen, A.Activo FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt64(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["Descripcion"].ToString();
                        arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString() };
                        arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString() };
                        arti.Ganancia = (decimal)datos.Lector["Ganancia_Porcentaje"];
                        arti.StockMin = datos.Lector.GetInt32(7);
                        arti.Imagen = datos.Lector["Imagen"].ToString();
                        arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    };

                    //AccesoDatos datosImagenes = new AccesoDatos();
                    //datosImagenes.setearConsulta("SELECT * FROM IMAGENES WHERE IdArticulo = @idArticulo");
                    //datosImagenes.setearParametro("@idArticulo", arti.ID);
                    //datosImagenes.ejecutarLectura();

                    //arti.Imagenes = new List<Imagen>();
                    //while (datosImagenes.Lector.Read())
                    //{
                    //    Imagen imagen = new Imagen
                    //    {
                    //        ID = datosImagenes.Lector.GetInt64(0),
                    //        IdArticulo = datosImagenes.Lector.GetInt64(1),
                    //        UrlImagen = datosImagenes.Lector["ImagenUrl"].ToString(),
                    //        Activo = (bool)datosImagenes.Lector["Activo"]
                    //    };
                    //    arti.Imagenes.Add(imagen);
                    //}
                    //datosImagenes.cerrarConexion();

                    if (arti.Imagen == null)
                    {
                        arti.Imagen = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
                    }

                    listaArticulos.Add(arti);
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
            return listaArticulos;
        }

        public void Agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            //AccesoDatos datosImagenes = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Stock_Minimo, Imagen, Activo, Ganancia_Porcentaje) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Stock_Minimo, @Imagen, @Activo, @Ganancia_Porcentaje)");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@IdMarca", articulo.Marca.ID);
                datos.setearParametro("@IdCategoria", articulo.Categoria.ID);
                datos.setearParametro("@Stock_Minimo", articulo.StockMin);
                datos.setearParametro("@Ganancia_Porcentaje", articulo.Ganancia);
                datos.setearParametro("@Imagen", articulo.Imagen);
                datos.setearParametro("@Activo", articulo.Activo);

                datos.ejecutarAccion();

                //int Id = SeleccionarUltimoRegistro();
                //foreach (Imagen imagen in articulo.Imagenes)
                //{
                //    datosImagenes.setearConsulta("INSERT INTO IMAGENES IdArticulo, ImagenUrl, Activo VALUES @IdArticulo, @ImagenUrl, @ActivoImagen");
                //    datosImagenes.setearParametro("@IdArticulo", Id);
                //    datosImagenes.setearParametro("@ImagenUrl", imagen.UrlImagen);
                //    datosImagenes.setearParametro("@ActivoImagen", imagen.Activo);
                //    datosImagenes.ejecutarAccion();
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
                //datosImagenes.cerrarConexion();
            }
        }

        public int SeleccionarUltimoRegistro()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //datos.setearConsulta("SELECT SCOPE_IDENTITY()");
                //object ultimoId = datos.ejecutarAccionScalar();
                datos.setearConsulta("SELECT TOP(1) Id FROM ARTICULOS ORDER BY Id DESC");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["Id"]);
                }
                else
                {
                    throw new Exception("No se pudo obtener el ID del último registro insertado.");
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

        public Articulo ObtenerArticuloPorID(long idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM ARTICULOS WHERE Id = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    arti.ID = long.Parse(datos.Lector["Id"].ToString());
                    arti.Codigo = datos.Lector["Codigo"].ToString();
                    arti.Nombre = datos.Lector["Nombre"].ToString();
                    arti.Descripcion = datos.Lector["Descripcion"].ToString();
                    
                    arti.Marca = new Marca();
                    arti.Marca.ID = int.Parse(datos.Lector["IdMarca"].ToString());

                    arti.Categoria = new Categoria();
                    arti.Categoria.ID = int.Parse(datos.Lector["IdCategoria"].ToString());

                    arti.Ganancia = decimal.Parse(datos.Lector["Ganancia_Porcentaje"].ToString());
                    arti.Imagen = datos.Lector["Imagen"].ToString();
                    arti.StockMin = int.Parse(datos.Lector["Stock_Minimo"].ToString());

                    return arti;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el artículo por ID", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //public List<Imagen> ObtenerImagenesPorID(long idArticulo)
        //{
        //    List<Imagen> imagenes = new List<Imagen>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("SELECT ImagenUrl FROM IMAGENES WHERE IdArticulo = @IdArticulo");
        //        datos.setearParametro("@IdArticulo", idArticulo);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Imagen imagen = new Imagen
        //            {
        //                UrlImagen = (string)datos.Lector["ImagenUrl"]
        //            };

        //            imagenes.Add(imagen);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //    return imagenes;
        //}

        public void EliminarLogico(long Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Activo = 0 WHERE Id = @Id");
                datos.setearParametro("@Id", Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion();}

        }

        public void Modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Ganancia_Porcentaje = @Ganancia_Porcentaje, Stock_Minimo = @Stock_Minimo, Imagen = @Imagen, Activo = @Activo WHERE Id = @Id");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.ID);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.ID);
                datos.setearParametro("@Ganancia_Porcentaje", nuevo.Ganancia);
                datos.setearParametro("@Stock_Minimo", nuevo.StockMin);
                datos.setearParametro("@Imagen", nuevo.Imagen);
                datos.setearParametro("@Activo", nuevo.Activo);
                datos.setearParametro("@Id", nuevo.ID);

                datos.ejecutarAccion();

                //List<Imagen> imagenes = ObtenerImagenesPorID(nuevo.ID);

                //foreach (Imagen imagen in nuevo.Imagenes)
                //{
                //    datos.setearConsulta("INSERT INTO IMAGENES IdArticulo, ImagenUrl, Activo VALUES @IdArticulo, @ImagenUrl, @ActivoImagen");
                //    datos.setearParametro("@IdArticulo", nuevo.ID);
                //    datos.setearParametro("@ImagenUrl", imagen.UrlImagen);
                //    datos.setearParametro("@ActivoImagen", imagen.Activo);
                //    datos.ejecutarAccion();
                //}
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

        public List<Articulo> Filtrar(string campo, string criterio, string filtro, bool incluirInactivos)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion AS DescripcionArticulo, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Ganancia_Porcentaje, A.Stock_Minimo, A.Imagen, A.Activo FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE ";

                if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion LIKE '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Categoría")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion LIKE '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Precio Unitario ($)")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.PrecioUnitario > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.PrecioUnitario < " + filtro;
                            break;
                        default:
                            consulta += "A.PrecioUnitario = " + filtro;
                            break;
                    }
                }
                else if (campo == "Descripción")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Descripcion LIKE '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "A.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }

                if (!incluirInactivos)
                {
                    consulta += " AND A.Activo = 1";
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt64(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["DescripcionArticulo"].ToString();
                        arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString() };
                        arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString() };
                        arti.Ganancia = (decimal)datos.Lector["Ganancia_Porcentaje"];
                        arti.StockMin = datos.Lector.GetInt32(7);
                        arti.Imagen = datos.Lector["Imagen"].ToString();
                        arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    };

                    if (string.IsNullOrEmpty(arti.Imagen))
                    {
                        arti.Imagen = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
                    }

                    lista.Add(arti);
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

            return lista;
        }


    }
}