using Intranet.Modelos.LoginModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Serilog;

namespace Intranet.Pages
{
    public partial class InspHab
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private bool FirmaDigital { get; set; }
        private bool JSFirmaDigital { get; set; }
        private bool formularioPrincipal { get; set; }
        private bool showForm { get; set; } = true;
        private string valor = "block";
        protected override async Task OnInitializedAsync()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               // await JSRuntime.InvokeVoidAsync("ActivarCanvas");
            }
            try
            {
                if (JSFirmaDigital) {
                    await JSRuntime.InvokeVoidAsync("ActivarCanvas");
                    JSFirmaDigital = false;
                }
                   


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }
        }

        private async Task GenerarPDF()
        {
           

        }
        private async Task export()
        {
            try {
                FirmaDigital = !FirmaDigital;
                JSFirmaDigital = !JSFirmaDigital;
                valor = "none"; 
                var savePath = "planillas/formulario.jpg";
                await JSRuntime.InvokeVoidAsync("generatePDF", savePath);
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }
           
        }
    }
}
