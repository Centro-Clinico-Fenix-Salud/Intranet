#pragma checksum "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Pages\Direccionesip.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "dca4151c9e2ae91d7b44d8e270b148aa4aec44effc4264edf80633e43fb040a4"
// <auto-generated/>
#pragma warning disable 1591
namespace Intranet.Pages
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Intranet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Intranet.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/direccionesip")]
    public partial class Direccionesip : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<title>Direcciones IP</title>\r\n    <link href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n\r\n    ");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "container mt-5");
            __builder.AddMarkupContent(3, "<h1 class=\"text-center\">CRUD Local con JSON</h1>\r\n        ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "row");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "col-md-12");
            __builder.AddMarkupContent(8, "<button id=\"btn-add\" class=\"btn btn-primary mb-3\" data-toggle=\"modal\" data-target=\"#exampleModal\">Agregar Registro</button>\r\n                <input type=\"text\" id=\"search\" class=\"form-control mb-3\" placeholder=\"Buscar...\">\r\n                ");
            __builder.OpenElement(9, "select");
            __builder.AddAttribute(10, "id", "itemsPerPage");
            __builder.AddAttribute(11, "class", "form-control mb-3");
            __builder.OpenElement(12, "option");
            __builder.AddAttribute(13, "value", "5");
            __builder.AddContent(14, "5");
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n                    ");
            __builder.OpenElement(16, "option");
            __builder.AddAttribute(17, "value", "10");
            __builder.AddContent(18, "10");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n                    ");
            __builder.OpenElement(20, "option");
            __builder.AddAttribute(21, "value", "15");
            __builder.AddContent(22, "15");
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n                    ");
            __builder.OpenElement(24, "option");
            __builder.AddAttribute(25, "value", "20");
            __builder.AddContent(26, "20");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n                ");
            __builder.AddMarkupContent(28, @"<table class=""table table-bordered""><thead><tr><th>Nombre</th>
                            <th>Dirección IP</th>
                            <th>Extensión</th>
                            <th>Nombre del Equipo</th>
                            <th>Comentario</th>
                            <th>Acciones</th></tr></thead>
                    <tbody id=""table-body""></tbody></table>
                ");
            __builder.AddMarkupContent(29, "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"#\" id=\"prevPage\">Anterior</a></li>\r\n                        <li class=\"page-item\"><a class=\"page-link\" href=\"#\" id=\"nextPage\">Siguiente</a></li></ul></nav>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n\r\n    \r\n    ");
            __builder.AddMarkupContent(31, "<div class=\"modal fade\" id=\"exampleModal\" tabindex=\"-1\" aria-labelledby=\"exampleModalLabel\" aria-hidden=\"true\"><div class=\"modal-dialog\"><div class=\"modal-content\"><div class=\"modal-header\"><h5 class=\"modal-title\" id=\"exampleModalLabel\">Agregar Registro</h5>\r\n                    <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div>\r\n                <div class=\"modal-body\"><form id=\"form\"><input type=\"hidden\" id=\"id\" name=\"id\">\r\n                        <div class=\"form-group\"><label for=\"name\">Nombre</label>\r\n                            <input type=\"text\" class=\"form-control\" id=\"name\" name=\"name\"></div>\r\n                        <div class=\"form-group\"><label for=\"ip\">Dirección IP</label>\r\n                            <input type=\"text\" class=\"form-control\" id=\"ip\" name=\"ip\"></div>\r\n                        <div class=\"form-group\"><label for=\"extension\">Extensión</label>\r\n                            <input type=\"text\" class=\"form-control\" id=\"extension\" name=\"extension\"></div>\r\n                        <div class=\"form-group\"><label for=\"team\">Nombre del Equipo</label>\r\n                            <input type=\"text\" class=\"form-control\" id=\"team\" name=\"team\"></div>\r\n                        <div class=\"form-group\"><label for=\"comment\">Comentario</label>\r\n                            <input type=\"text\" class=\"form-control\" id=\"comment\" name=\"comment\"></div>\r\n                        <button type=\"submit\" class=\"btn btn-primary\">Guardar</button></form></div></div></div></div>\r\n\r\n    ");
            __builder.OpenElement(32, "script");
            __builder.AddAttribute(33, "src", "https://code.jquery.com/jquery-3.5.1.slim.min.js");
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "\r\n    ");
            __builder.OpenElement(35, "script");
            __builder.AddAttribute(36, "src", "https://cdn.jsdelivr.net/npm/" + (
#nullable restore
#line 85 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Pages\Direccionesip.razor"
                                               popperjs

#line default
#line hidden
#nullable disable
            ) + "/core@2.5.4/dist/umd/popper.min.js");
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n    ");
            __builder.OpenElement(38, "script");
            __builder.AddAttribute(39, "src", "https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js");
            __builder.CloseElement();
            __builder.AddMarkupContent(40, "\r\n    ");
            __builder.OpenElement(41, "script");
            __builder.AddMarkupContent(42, "\r\n        document.addEventListener(\"DOMContentLoaded\", function () {\r\n            const form = document.getElementById(\'form\');\r\n            const tableBody = document.getElementById(\'table-body\');\r\n            const searchInput = document.getElementById(\'search\');\r\n            const itemsPerPageSelect = document.getElementById(\'itemsPerPage\');\r\n            const prevPageBtn = document.getElementById(\'prevPage\');\r\n            const nextPageBtn = document.getElementById(\'nextPage\');\r\n            const modal = $(\'#exampleModal\');\r\n\r\n            let currentPage = 1;\r\n            let itemsPerPage = parseInt(itemsPerPageSelect.value);\r\n\r\n            function loadFromLocalStorage() {\r\n                const data = localStorage.getItem(\'crudData\');\r\n                return data ? JSON.parse(data) : [];\r\n            }\r\n\r\n            function saveToLocalStorage(data) {\r\n                localStorage.setItem(\'crudData\', JSON.stringify(data));\r\n            }\r\n\r\n            function renderTable(data = loadFromLocalStorage(), page = 1, itemsPerPage = 5) {\r\n                tableBody.innerHTML = \'\';\r\n                const start = (page - 1) * itemsPerPage;\r\n                const end = start + itemsPerPage;\r\n                const paginatedData = data.slice(start, end);\r\n\r\n                paginatedData.forEach((item, index) => {\r\n                    const row = `<tr>\r\n                        <td>${item.name}</td>\r\n                        <td>${item.ip}</td>\r\n                        <td>${item.extension}</td>\r\n                        <td>${item.team}</td>\r\n                        <td>${item.comment}</td>\r\n                        <td>\r\n                            <button class=\"btn btn-warning btn-sm btn-edit\" data-id=\"${index + start}\">Editar</button>\r\n                            <button class=\"btn btn-danger btn-sm btn-delete\" data-id=\"${index + start}\">Eliminar</button>\r\n                        </td>\r\n                    </tr>`;\r\n                    tableBody.insertAdjacentHTML(\'beforeend\', row);\r\n                });\r\n\r\n                updatePagination(data.length, page, itemsPerPage);\r\n            }\r\n\r\n            function updatePagination(totalItems, page, itemsPerPage) {\r\n                prevPageBtn.parentElement.classList.toggle(\'disabled\', page === 1);\r\n                nextPageBtn.parentElement.classList.toggle(\'disabled\', page * itemsPerPage >= totalItems);\r\n            }\r\n\r\n            function handleFormSubmit(event) {\r\n                event.preventDefault();\r\n                const id = document.getElementById(\'id\').value;\r\n                const name = document.getElementById(\'name\').value || \'\';\r\n                const ip = document.getElementById(\'ip\').value || \'\';\r\n                const extension = document.getElementById(\'extension\').value || \'\';\r\n                const team = document.getElementById(\'team\').value || \'\';\r\n                const comment = document.getElementById(\'comment\').value || \'\';\r\n                const data = loadFromLocalStorage();\r\n\r\n                if (id !== \'\') {\r\n                    data[id] = { name, ip, extension, team, comment };\r\n                } else {\r\n                    data.push({ name, ip, extension, team, comment });\r\n                }\r\n\r\n                saveToLocalStorage(data);\r\n                renderTable(loadFromLocalStorage(), currentPage, itemsPerPage);\r\n                form.reset();\r\n                document.getElementById(\'id\').value = \'\';\r\n                modal.modal(\'hide\');\r\n            }\r\n\r\n            function handleEditButtonClick(event) {\r\n                const id = event.target.getAttribute(\'data-id\');\r\n                const data = loadFromLocalStorage();\r\n                const record = data[id];\r\n                document.getElementById(\'id\').value = id;\r\n                document.getElementById(\'name\').value = record.name;\r\n                document.getElementById(\'ip\').value = record.ip;\r\n                document.getElementById(\'extension\').value = record.extension;\r\n                document.getElementById(\'team\').value = record.team;\r\n                document.getElementById(\'comment\').value = record.comment;\r\n                modal.modal(\'show\');\r\n            }\r\n\r\n            function handleDeleteButtonClick(event) {\r\n                const id = event.target.getAttribute(\'data-id\');\r\n                if (confirm(\"¿Estás seguro de que deseas eliminar este registro?\")) {\r\n                    const data = loadFromLocalStorage();\r\n                    data.splice(id, 1);\r\n                    saveToLocalStorage(data);\r\n                    renderTable(loadFromLocalStorage(), currentPage, itemsPerPage);\r\n                }\r\n            }\r\n\r\n            function handleSearch(event) {\r\n                const searchText = event.target.value.toLowerCase();\r\n                const data = loadFromLocalStorage();\r\n                const filteredData = data.filter(item => {\r\n                    return Object.values(item).some(value =>\r\n                        value.toLowerCase().includes(searchText)\r\n                    );\r\n                });\r\n                renderTable(filteredData, currentPage, itemsPerPage);\r\n            }\r\n\r\n            function handleItemsPerPageChange(event) {\r\n                itemsPerPage = parseInt(event.target.value);\r\n                currentPage = 1;\r\n                renderTable(loadFromLocalStorage(), currentPage, itemsPerPage);\r\n            }\r\n\r\n            function handlePrevPage(event) {\r\n                if (currentPage > 1) {\r\n                    currentPage--;\r\n                    renderTable(loadFromLocalStorage(), currentPage, itemsPerPage);\r\n                }\r\n            }\r\n\r\n            function handleNextPage(event) {\r\n                const data = loadFromLocalStorage();\r\n                if (currentPage * itemsPerPage < data.length) {\r\n                    currentPage++;\r\n                    renderTable(loadFromLocalStorage(), currentPage, itemsPerPage);\r\n                }\r\n            }\r\n\r\n            form.addEventListener(\'submit\', handleFormSubmit);\r\n            tableBody.addEventListener(\'click\', function (event) {\r\n                if (event.target.classList.contains(\'btn-edit\')) {\r\n                    handleEditButtonClick(event);\r\n                } else if (event.target.classList.contains(\'btn-delete\')) {\r\n                    handleDeleteButtonClick(event);\r\n                }\r\n            });\r\n\r\n            searchInput.addEventListener(\'input\', handleSearch);\r\n            itemsPerPageSelect.addEventListener(\'change\', handleItemsPerPageChange);\r\n            prevPageBtn.addEventListener(\'click\', handlePrevPage);\r\n            nextPageBtn.addEventListener(\'click\', handleNextPage);\r\n\r\n            renderTable(loadFromLocalStorage(), currentPage, itemsPerPage);\r\n        });\r\n\r\n    ");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
