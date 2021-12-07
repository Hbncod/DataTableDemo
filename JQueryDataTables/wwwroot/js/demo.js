$(document).ready(function () {
    var dataTable = $("#customerDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": false,
        "language": {
            "lengthMenu": "Exibir  _MENU_  registros por página",
            "zeroRecords": "Nenhum registro encontrado",
            "info": "Apresentando página _PAGE_ de _PAGES_ páginas",
            "infoEmpty": "No records available",
            "infoFiltered": "(filtered from _MAX_ total records)",
            "processing": "Buscando"
        },
        dom: 'Btrlp',
        buttons: [
            {
                text: 'Csv',
                action: function () {
                    exportToCsv();
                }
            }
        ],
        ajax: {

            url: "/api/customer/grid",
            type: "POST",
            "dataType": "json",
            "data": function (data) {
                var name = $('#searchByName').val();
                data.searchByName = name;
            }
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "FirstName", "autoWidth": true },
            { "data": "lastName", "name": "LastName", "autoWidth": true },
            { "data": "contact", "name": "Contact", "autoWidth": true, orderable: false },
            { "data": "email", "name": "Email", "autoWidth": true },
            { "data": "dateOfBirth", "name": "DateOfBirth", "autoWidth": true },
        ]
    });
    $('#searchByName').keyup(function () {
        dataTable.draw();
    });
});

function renderDownloadForm(format) {
    $('#export-to-file-form').attr('action', '/api/customer/ExportTable?format=' + format);

    var datatableParams = $("#customerDatatable").DataTable().ajax.params();

    if (!$("#export-to-file-form input[name=dtParametersJson]").val()) {
        var searchModelInput = $("<input>")
            .attr("type", "hidden")
            .attr("name", "dtParametersJson")

        $("#export-to-file-form").append(searchModelInput);
    }

    $("#export-to-file-form input[name=dtParametersJson]").val(JSON.stringify(datatableParams));
}

function exportToCsv() {
    renderDownloadForm("csv");
    $("#export-to-file-form").submit();
}