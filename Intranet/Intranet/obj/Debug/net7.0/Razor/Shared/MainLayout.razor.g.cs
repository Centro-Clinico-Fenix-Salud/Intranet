#pragma checksum "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1549c85174823646e6d936c1d6c875a3dbd339ed2bb2cf9595a68b363411842b"
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
#line 1 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Intranet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Intranet.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
using Intranet.Extension;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
using Intranet.Modelos.LoginModel;

#line default
#line hidden
#nullable disable
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
            __builder.OpenComponent<global::MudBlazor.MudAppBar>(14);
            __builder.AddAttribute(15, "style", (object)("background-color: #00bbb4; height : 55px"));
            __builder.AddAttribute(16, "Fixed", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 17 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                                                                           false

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(17, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(18);
                __builder2.AddAttribute(19, "Authorized", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder3) => {
                    __builder3.OpenElement(20, "spn");
                    __builder3.AddAttribute(21, "b-lbu036tguj");
                    __builder3.AddContent(22, "Bienvenido, ");
#nullable restore
#line (20,39)-(20,66) 26 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
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
#line (27,14)-(27,18) 25 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
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
#line 33 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                              true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(34, "MaxWidth", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.MaxWidth?>(
#nullable restore
#line 34 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                             MaxWidth.Small

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(35, "CloseButton", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
#line 35 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                                true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(36, "DisableBackdropClick", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
#line 36 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                                         true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(37, "NoHeader", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
#line 37 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                             true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(38, "Position", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.DialogPosition?>(
#nullable restore
#line 38 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                             DialogPosition.Center

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(39, "CloseOnEscapeKey", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
#line 39 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
                                     true

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.AddMarkupContent(40, "\r\n");
            __builder.OpenComponent<global::MudBlazor.MudSnackbarProvider>(41);
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 42 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
       
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
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider autenticacionProvider { get; set; }
    }
}
#pragma warning restore 1591
