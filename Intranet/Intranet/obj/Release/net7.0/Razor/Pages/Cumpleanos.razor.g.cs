#pragma checksum "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "e0e91256fa49b5f3edd92e670118b8c1a12c19e5bfc844b266a1c752006effa5"
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
#line 3 "C:\Intranet\repo\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

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
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/cumpleanos")]
    public partial class Cumpleanos : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "container-fluid vh-100");
            __builder.AddMarkupContent(2, "<div id=\"calendar\" class=\"w-100 h-50\"></div>");
#nullable restore
#line 5 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
     if (cumpleaneros.Count > 0)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "id", "dia");
            __builder.AddAttribute(5, "class", "w-100 h-50");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "row h-100 align-content-center justify-content-center");
            __builder.AddMarkupContent(8, "<div class=\"d-flex justify-content-center \"><h2 class=\"text-black\">Feliz cumpleaños</h2></div>\r\n                ");
            __builder.OpenElement(9, "div");
            __builder.AddAttribute(10, "class", " h-75 w-100");
            __builder.OpenComponent<global::MudBlazor.MudCarousel<object>>(11);
            __builder.AddAttribute(12, "Class", (object)("mud-width-full mud-height-full"));
            __builder.AddAttribute(13, "ShowArrows", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 11 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                                                     arrows

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(14, "ShowBullets", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 11 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                                                                           bullets

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(15, "EnableSwipeGesture", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 11 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                                                                                                         enableSwipeGesture

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(16, "AutoCycle", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 11 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                                                                                                                                         autocycle

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(17, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line 13 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                         foreach (var item in cumpleaneros)
                        {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<global::MudBlazor.MudCarouselItem>(18);
                __builder2.AddAttribute(19, "Transition", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Transition>(
#nullable restore
#line 15 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                         transition

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(20, "Color", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Color>(
#nullable restore
#line 15 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                                             Color.Tertiary

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(21, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenElement(22, "div");
                    __builder3.AddAttribute(23, "class", "d-flex align-content-center justify-content-center");
                    __builder3.AddAttribute(24, "style", "height:100%");
                    __builder3.OpenComponent<global::MudBlazor.MudCard>(25);
                    __builder3.AddAttribute(26, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<global::MudBlazor.MudCardMedia>(27);
                        __builder4.AddAttribute(28, "Image", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 18 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                              item.Imagen

#line default
#line hidden
#nullable disable
                        )));
                        __builder4.AddAttribute(29, "Height", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Int32>(
#nullable restore
#line 18 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                                                   200

#line default
#line hidden
#nullable disable
                        )));
                        __builder4.AddAttribute(30, "Class", (object)("h-75"));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(31, "\r\n                                        ");
                        __builder4.OpenComponent<global::MudBlazor.MudCardContent>(32);
                        __builder4.AddAttribute(33, "Class", (object)("h-25"));
                        __builder4.AddAttribute(34, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<global::MudBlazor.MudText>(35);
                            __builder5.AddAttribute(36, "Typo", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Typo>(
#nullable restore
#line 20 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                           Typo.h5

#line default
#line hidden
#nullable disable
                            )));
                            __builder5.AddAttribute(37, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
#nullable restore
#line (20,70)-(20,81) 26 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
__builder6.AddContent(38, item.Nombre);

#line default
#line hidden
#nullable disable
                                __builder6.AddContent(39, " ");
#nullable restore
#line (20,83)-(20,96) 26 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
__builder6.AddContent(40, item.Apellido);

#line default
#line hidden
#nullable disable
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(41, "\r\n                                                ");
                            __builder5.OpenComponent<global::MudBlazor.MudText>(42);
                            __builder5.AddAttribute(43, "Typo", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::MudBlazor.Typo>(
#nullable restore
#line 21 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                                                               Typo.body2

#line default
#line hidden
#nullable disable
                            )));
                            __builder5.AddAttribute(44, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
#nullable restore
#line (21,77)-(21,94) 26 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
__builder6.AddContent(45, item.Departamento);

#line default
#line hidden
#nullable disable
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.CloseElement();
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 26 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
                        }

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 32 "C:\Intranet\repo\Intranet\Intranet\Intranet\Pages\Cumpleanos.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
