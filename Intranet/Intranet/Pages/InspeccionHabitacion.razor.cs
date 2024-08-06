﻿using Intranet.Modelos.Admin;
using Intranet.Modelos.Planillas.InspeccionHabitaciones;
using Intranet.Services.Admin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.Drawing;
using System.Text.Json;
using BlazorBootstrap;
using System.Buffers.Text;
using System.Reflection.Metadata;
using System.Text;

namespace Intranet.Pages
{
    public partial class InspeccionHabitacion
    {
        KitIngreso kitIngreso {  get; set; }
        DatosPaciente datosPaciente { get; set; }
        Habitacion habitacion { get; set; }
        DatosResponsablePaciente datosResponsablePaciente { get; set; }

        private bool CheckedVaribale;

        private bool mostrarformualarioDatosPaciente = true;
        private bool mostrarformualarioHabitacion = false;
        private bool mostrarformualarioKitIngreso = false;
        private bool MostrarsResponsablePaciente = false;
        private bool MostrarFirmaDigital = false;
        private bool GeneroSeleccionadaValid = true;
        private bool MostrarsPDF = false;
        private bool QuitarCanvasFirma = false;
        private bool JSFirmaDigital { get; set; }
        private List<string> ListGenero = new List<string>();
        DateTime? date = DateTime.Today;
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private string pdfBase64String;
        private string base64Pdf { get; set; } = string.Empty;
        private string base64PdfFin { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            kitIngreso = new KitIngreso();
            datosPaciente = new DatosPaciente();
            datosPaciente.FechaIngreso = DateTime.Today;
            habitacion = new Habitacion();
            datosResponsablePaciente = new DatosResponsablePaciente();
            await obtenerListaGenero();



        }

      
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
            }
            if (JSFirmaDigital)
            {
                await JSRuntime.InvokeVoidAsync("ActivarCanvas");
                JSFirmaDigital = false;
            }

        }

        private void AtrasKitIngreso()
        {
            mostrarformualarioKitIngreso = false;
            mostrarformualarioHabitacion = true;

        }
        private void AtrasFirmaResponsable()
        {
            MostrarsResponsablePaciente = false;
            mostrarformualarioKitIngreso = true;          
        }
        private void AtrasPrevisualizacionPDF()
        {
            MostrarsPDF = false;
            MostrarsResponsablePaciente = true;
        }
        private void AtrasFirma()
        {
            MostrarFirmaDigital = false;
            MostrarsPDF = true;
        }
        private void BotonPrevisualizacionPDF()
        {
            MostrarsPDF = false;
            JSFirmaDigital = true;
            MostrarFirmaDigital = true;
            
        }
        private async Task BotonFirmaDigital()
        {
            string base64String = await JSRuntime.InvokeAsync<string>("InsertarFirmaDigitalEnPDF");
            await GetPdfBase64(2, int.Parse(base64String));
            QuitarCanvasFirma = true;
            base64Pdf = string.Empty;
        }
        private void AtrasHabitacion()
        {
            mostrarformualarioDatosPaciente = true;
            mostrarformualarioHabitacion = false;

        }

        private async Task EnvioDatosPacientes(EditContext context)
        {
         mostrarformualarioDatosPaciente = false;
         mostrarformualarioHabitacion = true;
        //var resultado = await ServicioAdmin.GuardarUsuario(EditarUsuario);


        //if (resultado)
        //{
        //    await RefrescarDataGrid();
        //    Snackbar.Add("Modificación exitosa", Severity.Info);
        //    CerrarModalEditar();
        //}
        //else
        //    Snackbar.Add("Ocurrio un error", Severity.Error);
        }

        [JSInvokable]

        private async Task EnvioFirmaResponsable(EditContext context)
        {
            MostrarsResponsablePaciente = false;
            MostrarsPDF = true;

            try
            {
                //var pdf = GenerarPDF();

                var PaginaParte1 = "planillas/InspecHabPart1.jpg";
                var PaginaParte2 = "planillas/InspecHabPart2.jpg";
                string datosPacienteJSON = JsonSerializer.Serialize(datosPaciente);
                string habitacionJSON = JsonSerializer.Serialize(habitacion);
                string kitIngresoJSON = JsonSerializer.Serialize(kitIngreso);
                string datosResponsablePacienteJSON = JsonSerializer.Serialize(datosResponsablePaciente);
                string base64String = await JSRuntime.InvokeAsync<string>("generatePDFMejorado", PaginaParte1, PaginaParte2, datosPacienteJSON, habitacionJSON, kitIngresoJSON, datosResponsablePacienteJSON);
                await GetPdfBase64(1, int.Parse(base64String));
            }
            catch (Exception ex)
            { }

            //var resultado = await ServicioAdmin.GuardarUsuario(EditarUsuario);


            //if (resultado)
            //{
            //    await RefrescarDataGrid();
            //    Snackbar.Add("Modificación exitosa", Severity.Info);
            //    CerrarModalEditar();
            //}
            //else
            //    Snackbar.Add("Ocurrio un error", Severity.Error);
        }
        private async Task EnvioHabiacion(EditContext context)
        {

            mostrarformualarioHabitacion = false;
            mostrarformualarioKitIngreso = true;
            //var resultado = await ServicioAdmin.GuardarUsuario(EditarUsuario);


            //if (resultado)
            //{
            //    await RefrescarDataGrid();
            //    Snackbar.Add("Modificación exitosa", Severity.Info);
            //    CerrarModalEditar();
            //}
            //else
            //    Snackbar.Add("Ocurrio un error", Severity.Error);
        }
        private async Task EnvioKitIngreso(EditContext context)
        {

            mostrarformualarioKitIngreso = false;
            MostrarsResponsablePaciente = true;
            //var resultado = await ServicioAdmin.GuardarUsuario(EditarUsuario);


            //if (resultado)
            //{
            //    await RefrescarDataGrid();
            //    Snackbar.Add("Modificación exitosa", Severity.Info);
            //    CerrarModalEditar();
            //}
            //else
            //    Snackbar.Add("Ocurrio un error", Severity.Error);
        }
        private void OnGeneroSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                datosPaciente.Genero = value;

            GeneroSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private async Task obtenerListaGenero()
        {
            ListGenero.Add("Femenino");
            ListGenero.Add("Masculino");

            ListGenero.OrderBy(x => x).ToList();
        }
        public void OnCheckedChanged(bool checkedvalue)
        {
            CheckedVaribale = checkedvalue;

            if (CheckedVaribale)
            {
                datosResponsablePaciente.NombreApellido = datosPaciente.NombreApellido;
                datosResponsablePaciente.Direccion = datosPaciente.Direccion;
                datosResponsablePaciente.Cedula = datosPaciente.Cedula;
                datosResponsablePaciente.FechaIngreso = datosPaciente.FechaIngreso;
                datosResponsablePaciente.NumeroHabitacion = datosPaciente.NumeroHabitacion;

            }
            else {
                datosResponsablePaciente= new DatosResponsablePaciente();
            }
        }

        void ConvertirPDFaJPG()
        {
            // Load PDF file from a local path or URL
           // var pdfUrl = GenerarPDF();

            // Create a BlazorPDF document
            //var document = new BlazorPdf.GeneratePdf(pdfUrl);

            // Load the PDF file into the document
            //await document.LoadFromUrlAsync(pdfUrl);

            // Render the PDF document in the viewer
           
        }
        public async Task GetPdfBase64(int valor , int total)
        {
            
            StringBuilder base64PdfBuilder = new StringBuilder();
            int chunkSize = 3200; // Ajusta este valor según tus necesidades
            for (int i = 0; i < total; i += chunkSize)
            {
                string chunk = await JSRuntime.InvokeAsync<string>("getBase64PdfChunk", i, i + chunkSize);
                base64PdfBuilder.Append(chunk);
            }
            if(valor == 1)
             base64Pdf = base64PdfBuilder.ToString();
            if (valor == 2)
                base64PdfFin = base64PdfBuilder.ToString();
            // Ahora puedes trabajar con base64Pdf como un PDF en base64
        }


    }
}