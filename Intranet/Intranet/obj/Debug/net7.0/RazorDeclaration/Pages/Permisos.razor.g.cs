// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Intranet.Pages
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
#line 12 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Serilog;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Permisos.razor"
using Intranet.Modelos.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Permisos.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Permisos.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Permisos.razor"
           [Authorize(Roles = "SuperAdmin,EliminarPermiso,EditarPermiso,ConsultarPermiso")]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/Permisos")]
    public partial class Permisos : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    }
}
#pragma warning restore 1591
