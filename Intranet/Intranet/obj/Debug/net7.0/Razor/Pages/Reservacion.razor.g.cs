#pragma checksum "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "c71c09c47b3d52a8fef7a8599fc2f2c142a5de51ddd55caa95093d3a94e45ee9"
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
#line 1 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Intranet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Intranet.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/Reunion-Agenda")]
    public partial class Reservacion : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<nav class=\"navbar navbar-expand-lg justify-content-center\" style=\"border-radius: 10px; background-color: #00bbb4\"><h1 class=\"navbar-brand\" href=\"#\" style=\"color: black; font-weight: bold\">\r\n        Reservación de salas\r\n    </h1></nav>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "mb-3 mt-3");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "col-6");
            __builder.OpenComponent<global::MudBlazor.MudSelect<string>>(5);
            __builder.AddAttribute(6, "Label", (object)("Selecciona una sala de reunión"));
            __builder.AddAttribute(7, "Value", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<string>(
#nullable restore
#line 16 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                              SalaReunion

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(8, "ValueChanged", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<string>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<string>(this, 
#nullable restore
#line 16 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                         OnSalaReunionSeleccionadaChanged

#line default
#line hidden
#nullable disable
            ))));
            __builder.AddAttribute(9, "AnchorOrigin", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Origin>(
#nullable restore
#line 16 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                         Origin.BottomCenter

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(10, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line 17 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                 foreach (var salaReunion in ListSalaReunion)
                {

#line default
#line hidden
#nullable disable
                global::__Blazor.Intranet.Pages.Reservacion.TypeInference.CreateMudSelectItem_0(__builder2, 11, 12, 
#nullable restore
#line 19 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                           salaReunion

#line default
#line hidden
#nullable disable
                );
#nullable restore
#line 20 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                }

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 25 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
 if (mostrarCalendario)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(13, "div");
            __builder.AddAttribute(14, "class", "mb-3");
            __builder.OpenComponent<global::MudBlazor.MudButton>(15);
            __builder.AddAttribute(16, "ButtonType", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.ButtonType>(
#nullable restore
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                           ButtonType.Submit

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(17, "Variant", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Variant>(
#nullable restore
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                       Variant.Filled

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(18, "OnClick", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                  () => NuevoModalReservacion()

#line default
#line hidden
#nullable disable
            ))));
            __builder.AddAttribute(19, "Color", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Color>(
#nullable restore
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                         Color.Success

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(20, "Class", (object)("ml-auto"));
            __builder.AddAttribute(21, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(22, "+ reservación");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n    <div id=\"calendar\" class=\"w-100 h-50\"></div>");
            __builder.AddMarkupContent(24, @"<div class=""modal justify-content-center align-items-center"" id=""eventoModal"" tabindex=""-1"" role=""dialog""><div class=""modal-dialog"" role=""document""><div class=""modal-content""><div class=""modal-header""><h5 class=""modal-title"" id=""tituloEvento""></h5>
                <button type=""button"" class=""close""><span aria-hidden=""true"">&times;</span></button></div>
            <div class=""modal-body""><div class=""container""><div class=""row""><div class=""col""><strong> Creado por :</strong></div>
                        <div class=""col"" id=""AutorEvento""></div></div>
                        <div class=""row""><div class=""col""><strong> Hora de Inicio :</strong></div>
                            <div class=""col"" id=""FechaEvento""></div></div>
                    <div class=""row""><div class=""col""><strong> Hora de Inicio :</strong></div>
                        <div class=""col"" id=""FechaInicioEvento""></div></div>
                    <div class=""row""><div class=""col""><strong> Hora de culminación :</strong></div>
                        <div class=""col"" id=""FechaFinEvento""></div></div>
                    <div class=""row""><div class=""col""><strong>Descripción:</strong></div></div>
                    <div class=""row""><div class=""col""><p id=""descripcionEvento""></p></div></div></div></div>
            <div class=""modal-footer""><button type=""button"" class=""close btn btn-secondary"">Cerrar</button></div></div></div></div>
<div class=""modal-backdrop show invisible"" id=""fondoModal""></div>");
#nullable restore
#line 91 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 93 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
 if (mostrarModalNuevo)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(25, "div");
            __builder.AddAttribute(26, "class", "modal show");
            __builder.AddAttribute(27, "tabindex", "-1");
            __builder.AddAttribute(28, "role", "dialog");
            __builder.AddAttribute(29, "style", "display: block;");
            __builder.OpenElement(30, "div");
            __builder.AddAttribute(31, "class", "modal-dialog modal-dialog-centered justify-content-center");
            __builder.AddAttribute(32, "role", "document");
            __builder.OpenElement(33, "div");
            __builder.AddAttribute(34, "class", "modal-content");
            __builder.OpenElement(35, "div");
            __builder.AddAttribute(36, "class", "modal-header");
            __builder.AddMarkupContent(37, "<h5 class=\"modal-title\">Agregar</h5>\r\n                    ");
            __builder.OpenElement(38, "button");
            __builder.AddAttribute(39, "type", "button");
            __builder.AddAttribute(40, "class", "close");
            __builder.AddAttribute(41, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 100 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                  CerrarModalNuevo

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(42, "<span aria-hidden=\"true\">&times;</span>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n                ");
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.EditForm>(44);
            __builder.AddAttribute(45, "Model", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Object>(
#nullable restore
#line 104 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                  CreateReservacion

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(46, "OnValidSubmit", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Forms.EditContext>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 104 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                    OnValidSubmit

#line default
#line hidden
#nullable disable
            ))));
            __builder.AddAttribute(47, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(48);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(49, "\r\n                    ");
                __builder2.OpenComponent<global::MudBlazor.MudGrid>(50);
                __builder2.AddAttribute(51, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<global::MudBlazor.MudItem>(52);
                    __builder3.AddAttribute(53, "xs", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Int32>(
#nullable restore
#line 107 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                     12

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.AddAttribute(54, "sm", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Int32>(
#nullable restore
#line 107 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                             12

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.AddAttribute(55, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<global::MudBlazor.MudCard>(56);
                        __builder4.AddAttribute(57, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<global::MudBlazor.MudCardContent>(58);
                            __builder5.AddAttribute(59, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                global::__Blazor.Intranet.Pages.Reservacion.TypeInference.CreateMudTextField_1(__builder6, 60, 61, "Titulo", 62, 
#nullable restore
#line 110 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                              () => CreateReservacion.title

#line default
#line hidden
#nullable disable
                                , 63, 
#nullable restore
#line 110 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                              CreateReservacion.title

#line default
#line hidden
#nullable disable
                                , 64, global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => CreateReservacion.title = __value, CreateReservacion.title)));
                                __builder6.AddMarkupContent(65, "\r\n                                    ");
                                __builder6.OpenComponent<global::MudBlazor.MudDatePicker>(66);
                                __builder6.AddAttribute(67, "Label", (object)("Fecha"));
                                __builder6.AddAttribute(68, "Date", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.DateTime?>(
#nullable restore
#line 111 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                             CreateReservacion.Fecha

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(69, "DateChanged", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.DateTime?>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.DateTime?>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => CreateReservacion.Fecha = __value, CreateReservacion.Fecha)))));
                                __builder6.CloseComponent();
                                __builder6.AddMarkupContent(70, "\r\n                                    ");
                                global::__Blazor.Intranet.Pages.Reservacion.TypeInference.CreateValidationMessage_2(__builder6, 71, 72, 
#nullable restore
#line 112 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                              () => CreateReservacion.Fecha

#line default
#line hidden
#nullable disable
                                );
                                __builder6.AddMarkupContent(73, "\r\n                                    ");
                                __builder6.OpenComponent<global::MudBlazor.MudTimePicker>(74);
                                __builder6.AddAttribute(75, "Label", (object)("Desde"));
                                __builder6.AddAttribute(76, "AmPm", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 113 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                            true

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(77, "Color", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Color>(
#nullable restore
#line 113 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                         Color.Success

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(78, "Time", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.TimeSpan?>(
#nullable restore
#line 113 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                             CreateReservacion.start

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(79, "TimeChanged", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.TimeSpan?>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.TimeSpan?>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => CreateReservacion.start = __value, CreateReservacion.start)))));
                                __builder6.CloseComponent();
                                __builder6.AddMarkupContent(80, "\r\n                                    ");
                                global::__Blazor.Intranet.Pages.Reservacion.TypeInference.CreateValidationMessage_3(__builder6, 81, 82, 
#nullable restore
#line 114 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                              () => CreateReservacion.start

#line default
#line hidden
#nullable disable
                                );
                                __builder6.AddMarkupContent(83, "\r\n                                    ");
                                __builder6.OpenComponent<global::MudBlazor.MudTimePicker>(84);
                                __builder6.AddAttribute(85, "Label", (object)("Hasta"));
                                __builder6.AddAttribute(86, "AmPm", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 115 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                          true

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(87, "Color", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Color>(
#nullable restore
#line 115 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                       Color.Secondary

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(88, "Time", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.TimeSpan?>(
#nullable restore
#line 115 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                             CreateReservacion.end

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(89, "TimeChanged", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.TimeSpan?>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.TimeSpan?>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => CreateReservacion.end = __value, CreateReservacion.end)))));
                                __builder6.CloseComponent();
                                __builder6.AddMarkupContent(90, "\r\n                                    ");
                                global::__Blazor.Intranet.Pages.Reservacion.TypeInference.CreateValidationMessage_4(__builder6, 91, 92, 
#nullable restore
#line 116 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                              () => CreateReservacion.end

#line default
#line hidden
#nullable disable
                                );
#nullable restore
#line 117 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                     if (!string.IsNullOrEmpty(validationError))
                                    {

#line default
#line hidden
#nullable disable
                                __builder6.OpenComponent<global::MudBlazor.MudAlert>(93);
                                __builder6.AddAttribute(94, "Severity", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Severity>(
#nullable restore
#line 119 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                            Severity.Error

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(95, "Variant", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Variant>(
#nullable restore
#line 119 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                     Variant.Filled

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(96, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
#nullable restore
#line (119,103)-(119,118) 26 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
__builder7.AddContent(97, validationError);

#line default
#line hidden
#nullable disable
                                }
                                ));
                                __builder6.CloseComponent();
#nullable restore
#line 120 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                    }

#line default
#line hidden
#nullable disable
                                global::__Blazor.Intranet.Pages.Reservacion.TypeInference.CreateMudTextField_5(__builder6, 98, 99, "Descripción (Opcional)", 100, 
#nullable restore
#line 121 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                                    2

#line default
#line hidden
#nullable disable
                                , 101, 
#nullable restore
#line 121 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                                              () => CreateReservacion.description

#line default
#line hidden
#nullable disable
                                , 102, 
#nullable restore
#line 121 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                              CreateReservacion.description

#line default
#line hidden
#nullable disable
                                , 103, global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => CreateReservacion.description = __value, CreateReservacion.description)));
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(104, "\r\n                                ");
                            __builder5.OpenComponent<global::MudBlazor.MudCardActions>(105);
                            __builder5.AddAttribute(106, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.OpenComponent<global::MudBlazor.MudButton>(107);
                                __builder6.AddAttribute(108, "ButtonType", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.ButtonType>(
#nullable restore
#line 124 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                           ButtonType.Button

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(109, "OnClick", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 124 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                       CerrarModalNuevo

#line default
#line hidden
#nullable disable
                                ))));
                                __builder6.AddAttribute(110, "Variant", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Variant>(
#nullable restore
#line 124 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                  Variant.Filled

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(111, "Color", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Color>(
#nullable restore
#line 124 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                                                         Color.Error

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(112, "Class", (object)("ml-auto"));
                                __builder6.AddAttribute(113, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(114, "Cancelar");
                                }
                                ));
                                __builder6.CloseComponent();
                                __builder6.AddMarkupContent(115, "\r\n                                    ");
                                __builder6.OpenComponent<global::MudBlazor.MudButton>(116);
                                __builder6.AddAttribute(117, "ButtonType", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.ButtonType>(
#nullable restore
#line 125 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                           ButtonType.Submit

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(118, "Variant", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Variant>(
#nullable restore
#line 125 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                       Variant.Filled

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(119, "Color", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Color>(
#nullable restore
#line 125 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"
                                                                                                              Color.Success

#line default
#line hidden
#nullable disable
                                )));
                                __builder6.AddAttribute(120, "Class", (object)("ml-auto"));
                                __builder6.AddAttribute(121, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(122, "Guardar");
                                }
                                ));
                                __builder6.CloseComponent();
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(123, "\r\n    <div class=\"modal-backdrop show\"></div>");
#nullable restore
#line 135 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Reservacion.razor"


}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
namespace __Blazor.Intranet.Pages.Reservacion
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateMudSelectItem_0<T>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, T __arg0)
        {
        __builder.OpenComponent<global::MudBlazor.MudSelectItem<T>>(seq);
        __builder.AddAttribute(__seq0, "Value", (object)__arg0);
        __builder.CloseComponent();
        }
        public static void CreateMudTextField_1<T>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Linq.Expressions.Expression<global::System.Func<T>> __arg1, int __seq2, T __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<T> __arg3)
        {
        __builder.OpenComponent<global::MudBlazor.MudTextField<T>>(seq);
        __builder.AddAttribute(__seq0, "Label", (object)__arg0);
        __builder.AddAttribute(__seq1, "For", (object)__arg1);
        __builder.AddAttribute(__seq2, "Value", (object)__arg2);
        __builder.AddAttribute(__seq3, "ValueChanged", (object)__arg3);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_2<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", (object)__arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_3<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", (object)__arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_4<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", (object)__arg0);
        __builder.CloseComponent();
        }
        public static void CreateMudTextField_5<T>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Int32 __arg1, int __seq2, global::System.Linq.Expressions.Expression<global::System.Func<T>> __arg2, int __seq3, T __arg3, int __seq4, global::Microsoft.AspNetCore.Components.EventCallback<T> __arg4)
        {
        __builder.OpenComponent<global::MudBlazor.MudTextField<T>>(seq);
        __builder.AddAttribute(__seq0, "Label", (object)__arg0);
        __builder.AddAttribute(__seq1, "Lines", (object)__arg1);
        __builder.AddAttribute(__seq2, "For", (object)__arg2);
        __builder.AddAttribute(__seq3, "Value", (object)__arg3);
        __builder.AddAttribute(__seq4, "ValueChanged", (object)__arg4);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
