#pragma checksum "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "dc04fa92375d523c9ffc6e490fae10790d0aba7f1dce426da43928ba8cc963e1"
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
#line 2 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

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
#line 4 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLogin))]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/")]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/{errorMessage}")]
    public partial class Loggin : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<link href=\"/css/loggin.css\" rel=\"stylesheet\">  \r\n\r\n");
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Web.PageTitle>(1);
            __builder.AddAttribute(2, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(3, " Sistema Intranet Fenix Salud ");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(4, "\r\n");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "container py-5 h-100");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "row d-flex justify-content-center align-items-center h-100");
            __builder.OpenElement(9, "div");
            __builder.AddAttribute(10, "class", "col-12 col-md-8 col-lg-6 col-xl-5");
            __builder.OpenElement(11, "div");
            __builder.AddAttribute(12, "class", "card text-white");
            __builder.AddAttribute(13, "style", "border-radius: 1rem; background-color: #00bbb4");
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "class", "card-body p-5 text-center");
            __builder.OpenElement(16, "div");
            __builder.AddAttribute(17, "class", "mb-md-5 mt-md-4 pb-5");
            __builder.AddMarkupContent(18, "<h1> Intranet </h1>");
#nullable restore
#line 21 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                         if (!string.IsNullOrEmpty(errorMessage))
                        {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<global::MudBlazor.MudText>(19);
            __builder.AddAttribute(20, "Class", (object)("mt-2 text-danger"));
            __builder.AddAttribute(21, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line (23,64)-(23,76) 26 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
__builder2.AddContent(22, errorMessage);

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 24 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                        }

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.EditForm>(23);
            __builder.AddAttribute(24, "Model", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Object>(
#nullable restore
#line 25 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                          LoginDTO

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(25, "OnValidSubmit", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Forms.EditContext>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 25 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                   OnValidSubmit

#line default
#line hidden
#nullable disable
            ))));
            __builder.AddAttribute(26, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(27);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(28, "\r\n                            ");
                __builder2.OpenComponent<global::MudBlazor.MudGrid>(29);
                __builder2.AddAttribute(30, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<global::MudBlazor.MudItem>(31);
                    __builder3.AddAttribute(32, "xs", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Int32>(
#nullable restore
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                             12

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.AddAttribute(33, "sm", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Int32>(
#nullable restore
#line 28 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                     12

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.AddAttribute(34, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<global::MudBlazor.MudCard>(35);
                        __builder4.AddAttribute(36, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<global::MudBlazor.MudCardContent>(37);
                            __builder5.AddAttribute(38, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                global::__Blazor.Intranet.Pages.Loggin.TypeInference.CreateMudTextField_0(__builder6, 39, 40, 
#nullable restore
#line 31 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                   Variant.Filled

#line default
#line hidden
#nullable disable
                                , 41, "Usuario", 42, 
#nullable restore
#line 31 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                                                                                         () =>LoginDTO.Usuario

#line default
#line hidden
#nullable disable
                                , 43, 
#nullable restore
#line 31 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                                                                LoginDTO.Usuario

#line default
#line hidden
#nullable disable
                                , 44, global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => LoginDTO.Usuario = __value, LoginDTO.Usuario)));
                                __builder6.AddMarkupContent(45, "\r\n                                            ");
                                global::__Blazor.Intranet.Pages.Loggin.TypeInference.CreateMudTextField_1(__builder6, 46, 47, 
#nullable restore
#line 32 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                   Variant.Filled

#line default
#line hidden
#nullable disable
                                , 48, "Clave", 49, 
#nullable restore
#line 32 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                                                            InputType.Password

#line default
#line hidden
#nullable disable
                                , 50, 
#nullable restore
#line 32 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                                                                                                                    () => LoginDTO.Clave

#line default
#line hidden
#nullable disable
                                , 51, 
#nullable restore
#line 32 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                                                                                             LoginDTO.Clave

#line default
#line hidden
#nullable disable
                                , 52, global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => LoginDTO.Clave = __value, LoginDTO.Clave)));
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(53, "\r\n                                        ");
                            __builder5.OpenComponent<global::MudBlazor.MudCardActions>(54);
                            __builder5.AddAttribute(55, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.OpenElement(56, "div");
                                __builder6.AddAttribute(57, "class", "col-12 d-flex justify-content-center align-items-center");
                                __builder6.OpenElement(58, "button");
                                __builder6.AddAttribute(59, "data-mdb-button-init");
                                __builder6.AddAttribute(60, "data-mdb-ripple-init");
                                __builder6.AddAttribute(61, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 36 "C:\Users\programador\Desktop\proyectos\Intranet\Intranet\Intranet\Intranet\Pages\Loggin.razor"
                                                                                                        IniciarSesion

#line default
#line hidden
#nullable disable
                                ));
                                __builder6.AddAttribute(62, "class", "btn btn-outline-light btn-lg px-5");
                                __builder6.AddAttribute(63, "type", "submit");
                                __builder6.AddContent(64, "Login");
                                __builder6.CloseElement();
                                __builder6.CloseElement();
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
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider autenticacionProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
namespace __Blazor.Intranet.Pages.Loggin
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateMudTextField_0<T>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::MudBlazor.Variant __arg0, int __seq1, global::System.String __arg1, int __seq2, global::System.Linq.Expressions.Expression<global::System.Func<T>> __arg2, int __seq3, T __arg3, int __seq4, global::Microsoft.AspNetCore.Components.EventCallback<T> __arg4)
        {
        __builder.OpenComponent<global::MudBlazor.MudTextField<T>>(seq);
        __builder.AddAttribute(__seq0, "Variant", (object)__arg0);
        __builder.AddAttribute(__seq1, "Label", (object)__arg1);
        __builder.AddAttribute(__seq2, "For", (object)__arg2);
        __builder.AddAttribute(__seq3, "Value", (object)__arg3);
        __builder.AddAttribute(__seq4, "ValueChanged", (object)__arg4);
        __builder.CloseComponent();
        }
        public static void CreateMudTextField_1<T>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::MudBlazor.Variant __arg0, int __seq1, global::System.String __arg1, int __seq2, global::MudBlazor.InputType __arg2, int __seq3, global::System.Linq.Expressions.Expression<global::System.Func<T>> __arg3, int __seq4, T __arg4, int __seq5, global::Microsoft.AspNetCore.Components.EventCallback<T> __arg5)
        {
        __builder.OpenComponent<global::MudBlazor.MudTextField<T>>(seq);
        __builder.AddAttribute(__seq0, "Variant", (object)__arg0);
        __builder.AddAttribute(__seq1, "Label", (object)__arg1);
        __builder.AddAttribute(__seq2, "InputType", (object)__arg2);
        __builder.AddAttribute(__seq3, "For", (object)__arg3);
        __builder.AddAttribute(__seq4, "Value", (object)__arg4);
        __builder.AddAttribute(__seq5, "ValueChanged", (object)__arg5);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
