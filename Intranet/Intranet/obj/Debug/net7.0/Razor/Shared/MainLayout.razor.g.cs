<<<<<<< HEAD
#pragma checksum "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "818c173e69efd3ec7a4eab1d6db1a58dd1bebc0fedd805b80c228b7714610496"
=======
#pragma checksum "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "42da35929c279ade704e19010c921385c8ef5a4e2e2d6f4ed1916c78341be62f"
>>>>>>> origin/feature/publish
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
<<<<<<< HEAD
#line 1 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 1 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 2 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 2 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 3 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 4 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 5 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 5 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 6 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 6 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 7 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 7 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 8 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 8 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 9 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 9 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Intranet;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 10 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 10 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using Intranet.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
<<<<<<< HEAD
#line 11 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\_Imports.razor"
=======
#line 11 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
>>>>>>> origin/feature/publish
using MudBlazor;

#line default
#line hidden
#nullable disable
<<<<<<< HEAD
=======
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
>>>>>>> origin/feature/publish
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
<<<<<<< HEAD
#line (12,14)-(12,18) 25 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line (21,14)-(21,18) 25 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
__builder.AddContent(17, Body);

#line default
#line hidden
#nullable disable
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
<<<<<<< HEAD
#line 18 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 27 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
                              true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(23, "MaxWidth", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.MaxWidth?>(
#nullable restore
<<<<<<< HEAD
#line 19 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 28 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
                             MaxWidth.Small

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(24, "CloseButton", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< HEAD
#line 20 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 29 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
                                true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(25, "DisableBackdropClick", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< HEAD
#line 21 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 30 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
                                         true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(26, "NoHeader", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< HEAD
#line 22 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 31 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
                             true

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(27, "Position", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.DialogPosition?>(
#nullable restore
<<<<<<< HEAD
#line 23 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 32 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
                             DialogPosition.Center

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(28, "CloseOnEscapeKey", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean?>(
#nullable restore
<<<<<<< HEAD
#line 24 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
=======
#line 33 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
>>>>>>> origin/feature/publish
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
<<<<<<< HEAD
=======
#nullable restore
#line 37 "C:\Intranet\repo\Intranet\Intranet\Intranet\Shared\MainLayout.razor"
       

    private async Task CerrarSesion()
    {
        var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
        await autenticacionExt.ActualizarEstadoAutenticacion(null);
        navManager.NavigateTo("/", true);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider autenticacionProvider { get; set; }
>>>>>>> origin/feature/publish
    }
}
#pragma warning restore 1591
