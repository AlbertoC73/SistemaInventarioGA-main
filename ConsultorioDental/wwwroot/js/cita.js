let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Cita/ObtenerTodos"
        },
        "columns": [
            { "data": "fecha" },
            { "data": "hora" },
            {
                "data": "tratamiento.nombre",
                "render": function (data, type, row) {
                    if (row.usuarioAplicacion.id == loggedInUserId) {
                        return data; // Retorna los nombres
                    } else {
                        return "---";
                    }
                },
            },
            {
                "data": "tratamiento.costo",
                "render": function (data, type, row) {
                    if (row.usuarioAplicacion.id == loggedInUserId) {
                        return "$ "+data; // Retorna los nombres
                    } else {
                        return "---";
                    }
                },
            },
            {
                "data": function (row, type, set, meta) {
                    return row.usuarioAplicacion.nombres + " " + row.usuarioAplicacion.apellidos;
                },
                "render": function (data, type, row) {
                    if (row.usuarioAplicacion.id == loggedInUserId) {
                        return data; // Retorna los nombres y apellidos
                    } else {
                        return "---";
                    }
                },
            },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Completada";
                    } else {
                        return "Pendiente";
                    }
                }, 
            },
            {
                "data": "id",
                "render": function (data, type, row) {
                    if (row.usuarioAplicacion.id == loggedInUserId) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/Cita/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>  
                                </a>
                                <a onclick=Delete("/Admin/Cita/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="bi bi-trash3-fill"></i>
                                </a> 
                            </div>
                        `;
                    } else {
                        return `
                            <div class="text-center">
                                --- ---
                            </div>
                        `;
                    }
                },
            }
        ],
        "createdRow": function (row, data, dataIndex) {
            $(row).attr('id', '' + (data.estado ? 'completada' : 'pendiente'));
        },
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar pagina _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }

    });
}

function Delete(url) {

    swal({
        title: "Esta seguro de Eliminar el Cita?",
        text: "Este registro no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }

    });
}
