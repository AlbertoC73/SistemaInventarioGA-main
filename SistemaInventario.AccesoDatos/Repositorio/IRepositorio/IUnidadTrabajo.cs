﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IBodegaRepositorio Bodega {  get; }
        ICategoriaRepositorio Categoria { get; }
        IMarcaRepositorio Marca { get; }
        IProductoRepositorio Producto { get; }
        IUsuarioiAplicacionRepositorio UsuarioAplicacion {  get; }
        IBodegaProductoRepositorio BodegaProducto { get; }
        IInventarioRepositorio Inventario { get; }
        IInventarioDetalleRepositorio InventarioDetalle { get; }
        IKardexInventarioRepositorio KardexInventario { get; }

        Task Guardar();
    }
}