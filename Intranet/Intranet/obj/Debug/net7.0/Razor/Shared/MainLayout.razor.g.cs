<<<<<<< Updated upstream
#pragma checksum "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "05f626eb8a48ca0fc90a0c4b66fa1ff14f79028328f1da45d136b4d279e44923"
=======
#pragma checksum "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "42da35929c279ade704e19010c921385c8ef5a4e2e2d6f4ed1916c78341be62f"
>>>>>>> Stashed changes
// <auto-generated/>
#pragma warning disable 1591
namespace Intranet.Shared
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
#nullable restore
<<<<<<< Updated upstream
#line 21 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 2 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
using Intranet.Extension;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< Updated upstream
#line 22 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 3 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
<<<<<<< Updated upstream
#nullable restore
#line 23 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
using Intranet.Modelos.LoginModel;

#line default
#line hidden
#nullable disable
=======
>>>>>>> Stashed changes
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Web.PageTitle>(0);
            __builder.AddAttribute(1, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(2, "Intranet");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(3, "\r\n\r\n");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "page");
            __builder.AddAttribute(6, "b-lbu036tguj");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "sidebar");
            __builder.AddAttribute(9, "b-lbu036tguj");
            __builder.OpenComponent<global::Intranet.Shared.NavMenu>(10);
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n\r\n    ");
            __builder.OpenElement(12, "main");
            __builder.AddAttribute(13, "b-lbu036tguj");
            __builder.OpenElement(14, "article");
            __builder.AddAttribute(15, "class", "content px-4");
            __builder.AddAttribute(16, "b-lbu036tguj");
#nullable restore
<<<<<<< Updated upstream
#line 36 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                                                            false
=======
#line (21,14)-(21,18) 25 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
__builder.AddContent(17, Body);
>>>>>>> Stashed changes

#line default
#line hidden
#nullable disable
<<<<<<< Updated upstream
            )));
            __builder.AddAttribute(17, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(18);
                __builder2.AddAttribute(19, "Authorized", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder3) => {
                    __builder3.OpenElement(20, "spn");
                    __builder3.AddAttribute(21, "b-lbu036tguj");
                    __builder3.AddContent(22, "Bienvenido, ");
#nullable restore
#line (39,39)-(39,66) 26 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
__builder3.AddContent(23, context.User.Identity!.Name);

#line default
#line hidden
#nullable disable
                    __builder3.CloseElement();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(24, "\r\n\r\n        ");
            __builder.OpenElement(25, "article");
            __builder.AddAttribute(26, "class", "content px-4");
            __builder.AddAttribute(27, "b-lbu036tguj");
#nullable restore
#line (46,14)-(46,18) 25 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
__builder.AddContent(28, Body);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n\r\n");
            __builder.OpenComponent<global::MudBlazor.MudThemeProvider>(30);
            __builder.CloseComponent();
            __builder.AddMarkupContent(31, "\r\n");
            __builder.OpenComponent<global::MudBlazor.MudDialogProvider>(32);
            __builder.AddAttribute(33, "FullWidth", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
#line 52 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n\r\n");
            __builder.OpenComponent<global::MudBlazor.MudThemeProvider>(19);
            __builder.CloseComponent();
            __builder.AddMarkupContent(20, "\r\n");
            __builder.OpenComponent<global::MudBlazor.MudDialogProvider>(21);
            __builder.AddAttribute(22, "FullWidth", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
#line 27 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                              true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(23, "MaxWidth", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.MaxWidth?>(
#nullable restore
<<<<<<< Updated upstream
#line 53 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                             MaxWidth.Small

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(24, "CloseButton", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< Updated upstream
#line 54 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 29 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                                true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(25, "DisableBackdropClick", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< Updated upstream
#line 55 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 30 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                                         true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(26, "NoHeader", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< Updated upstream
#line 56 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 31 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                             true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(27, "Position", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.DialogPosition?>(
#nullable restore
<<<<<<< Updated upstream
#line 57 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 32 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                             DialogPosition.Center

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(28, "CloseOnEscapeKey", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< Updated upstream
#line 58 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 33 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> Stashed changes
                                     true

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.AddMarkupContent(29, "\r\n");
            __builder.OpenComponent<global::MudBlazor.MudSnackbarProvider>(30);
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
<<<<<<< Updated upstream
#line 61 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
       
    private string Nombre = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        // Nombre = "Bienvenido";

    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Nombre = "Bienvenido";
        }

    }



    private async Task CerrarSesion()
    {
        // await _sessionStorage.GuardarLogin(false);
        //var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
        //await autenticacionExt.ActualizarEstadoAutenticacion(null);
        // navManager.NavigateTo("/", true);
=======
#line 37 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
       

    private async Task CerrarSesion()
    {
        var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
        await autenticacionExt.ActualizarEstadoAutenticacion(null);
        navManager.NavigateTo("/", true);
>>>>>>> Stashed changes
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider autenticacionProvider { get; set; }
    }
}
#pragma warning restore 1591
