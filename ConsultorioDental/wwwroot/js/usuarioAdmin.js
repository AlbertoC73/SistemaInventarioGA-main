let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url":"/Admin/Usuario/ObtenerTodos"
        },
        "columns": [
            { "data": "email" },
            { "data": "nombres" },
            { "data": "apellidos" },
            { "data": "phoneNumber" },
            { "data": "role" },
            
            {
                "data": {
                    id:"id",lockoutEnd:"lockoutEnd"
                },
                "render": function (data) {
                    let hoy = new Date().getTime();
                    let bloqueo = new Date(data.lockoutEnd).getTime();
                    if (bloqueo > hoy) {
                        //usuario Bloqueado
                        return `
                            <div class="text-center">
                                <a onclick=BloquearDesbloquear('${data.id}') 
                                    class="btn btn-danger text-white" style="cursor:pointer", width:150px >
                                    <i class="bi bi-unlock-fill"></i>
                                    Desbloquear
                                </a>
                            </div>
                        `;
                    }
                    else
                    {
                        //usuario Desbloqueado
                        return `
                            <div class="text-center">
                                <a onclick=BloquearDesbloquear('${data.id}') 
                                    class="btn btn-success text-white" style="cursor:pointer", width:150px >
                                    <i class="bi bi-lock-fill"></i>
                                    Bloquear
                                </a>
                            </div>
                        `;
                    }

                    
                }
            },
        ],
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

function BloquearDesbloquear(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/Usuario/BloquearDesbloquear',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                //actualizar la tabla
                datatable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}