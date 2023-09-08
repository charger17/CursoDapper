var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $('#tblArticulos').DataTable({
        "ajax": {
            "url": "/admin/articulos/GetArticulos",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "idArticulo", "width": "5%" },
            { "data": "titulo", "width": "40%" },
            {
                "data": "imagen",
                "render": function (imagen) {
                    return `<img src=../${imagen} width="100"  />`
                },
                "width": "10%"
            },
            { "data": "estado", "width": "5%" },
            { "data": "categoria.nombre", "width": "10%" },
            { "data": "categoria.fechaCreacion", "width": "15%" },
            {
                "data": "idArticulo",
                "render": function (data) {
                    return `

                        <div class="text-center">
                            <a href="/admin/articulos/editar/${data}" class="btn btn-success text-white cursor-modif">
                            <i class="bi bi-pencil-square"></i> Editar </a>
                            &nbsp;
                            <a onclick=Delete("/admin/articulos/BorrarArticulo/${data}")  class="btn btn-danger text-white cursor-modif"> 
                            <i class="bi bi-x-square"></i> Borrar </a>
                            &nbsp;
                             <a href="/admin/articulos/asignarEtiquetas/${data}" class="btn btn-secondary text-white cursor-modif">
                            <i class="bi bi-tags-fill"></i> Asignar Etiquetas </a>
                        </div>

                    `;
                }, "width" :"40%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de borrar?",
        text: "¡Este contendio no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, Borrar.",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        })
    })
}