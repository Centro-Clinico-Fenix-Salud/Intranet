#pragma checksum "C:\Users\programador\Desktop\intranet\Intranet\Intranet\Pages\Recursos-Humanos.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "85e2a2b1d48c06901a5c072a7eda91122e05a321bc9a58402a09a4060175a83c"
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
#line 1 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Intranet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using Intranet.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\programador\Desktop\intranet\Intranet\Intranet\_Imports.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/recursos-humanos")]
    public partial class Recursos_Humanos : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, @"<style>
    .card {
        background-color: rgba(0, 187, 180, 0.25);
        backdrop-filter: blur(10px);
        border-radius: 10px;
        border: 1px solid rgba(255, 255, 255, 0.25);
        box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
    }

        .card:hover {
            box-shadow: 5px 10px rgba(0, 0, 0, 0.3);
        }

    a:hover h4 {
        color: black;
    }
</style>


");
            __builder.AddMarkupContent(1, "<nav class=\"navbar navbar-expand-lg justify-content-center\" style=\"border-radius: 10px; background-color: #00bbb4\"><h1 class=\"navbar-brand\" href=\"#\" style=\"color: black; font-weight: bold\">\r\n        Recursos Humanos\r\n    </h1></nav>\r\n\r\n\r\n");
            __builder.AddMarkupContent(2, @"<div class=""container-fluid mt-4""><div class=""row""><div class=""col-md-4 mb-4 d-none""><a href=""#""><div class=""card d-flex flex-column justify-content-center align-items-center"" style=""cursor: pointer;""><img src=""img/32.png"" alt=""Imagen 1"" class=""img-fluid w-50 m-2 p-2"">
                    <h4 class=""mt-3"" style=""text-align: center;"">Politicas</h4></div></a></div>
        <div class=""col-md-4 mb-4 d-none""><a href=""#""><div class=""card d-flex flex-column justify-content-center align-items-center"" style=""cursor: pointer;""><img src=""img/34.png"" alt=""Imagen 2"" class=""img-fluid w-50 m-2 p-2"">
                    <h4 class=""mt-3"" style=""text-align: center;"">Beneficios</h4></div></a></div>
        <div class=""col-md-4 mb-4 d-none""><a href=""#""><div class=""card d-flex flex-column justify-content-center align-items-center"" style=""cursor: pointer;""><img src=""img/35.png"" alt=""Imagen 3"" class=""img-fluid w-50 m-2 p-2"">
                    <h4 class=""mt-3"" style=""text-align: center;"">Capacitacion</h4></div></a></div></div></div>

");
            __builder.AddMarkupContent(3, @"<div class=""container-fluid mt-4""><div class=""row""><div class=""col-md-4 mb-4 d-none""><a href=""#""><div class=""card d-flex flex-column justify-content-center align-items-center"" style=""cursor: pointer;""><img src=""img/33.png"" alt=""Imagen 2"" class=""img-fluid w-50 m-2 p-2"">
                    <h4 class=""mt-3"" style=""text-align: center;"">Desempeño</h4></div></a></div>
        <div class=""col-md-4 mb-4 d-none""><a href=""#""><div class=""card d-flex flex-column justify-content-center align-items-center"" style=""cursor: pointer;""><img src=""img/37.png"" alt=""Imagen 2"" class=""img-fluid w-50 m-2 p-2"">
                    <h4 class=""mt-3"" style=""text-align: center;"">Cultura</h4></div></a></div>
        <div class=""col-md-4 mb-4""><a href=""sdocumentos""><div class=""card d-flex flex-column justify-content-center align-items-center"" style=""cursor: pointer;""><img src=""img/36.png"" alt=""Imagen 3"" class=""img-fluid w-50 m-2 p-2"">
                    <h4 class=""mt-3"" style=""text-align: center;"">Solicitud De Documentos</h4></div></a></div></div></div>");
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
