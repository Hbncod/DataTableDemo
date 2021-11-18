﻿$(document).ready(function () {
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
        "ajax": {
            "url": "/api/customer",
            "type": "POST",
            "datatype": "json",
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