
function DataCalendar() {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        initialDate: '2024-03-01',
        navLinks: true,
        businessHours: true,
        editable: true,
        locale: 'es',
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Dia'
        },
        selectable: true,
        events: [
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-04'
            },
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-04'
            },
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-01'
            },
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-15'
            },

            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-15'
            },
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-15'
            },
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-30'
            },
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-09'
            }
            ,
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-26'
            }
            ,
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-11'
            }
            ,
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-17'
            }
            ,
            {
                title: 'Aitor Blanco - Tecnologia',
                start: '2024-03-22'
            }
        ]
    });

    calendar.render();
    
};

function Data() {
    fetch('json/datos.json')
        .then((response) => response.json())
        .then((data) => {
            const tabla = document.querySelector("#tabla-datos");
            const tbody = tabla.querySelector("tbody");
            const busquedaInput = document.querySelector("#busqueda");

            function filtrarTabla() {
                const busqueda = busquedaInput.value.toLowerCase();
                const filas = tbody.querySelectorAll("tr");

                filas.forEach((fila) => {
                    const textoFila = fila.textContent.toLowerCase();

                    if (textoFila.includes(busqueda)) {
                        fila.style.display = "";
                    } else {
                        fila.style.display = "none";
                    }
                });
            }

            busquedaInput.addEventListener("input", filtrarTabla);

            data.forEach((dato) => {
                const row = document.createElement("tr");
                const usuarioCell = document.createElement("td");
                const ipCell = document.createElement("td");
                const extensionCell = document.createElement("td");
                const inventarioCell = document.createElement("td");
                const departamentoCell = document.createElement("td");
                const nombreEquipoCell = document.createElement("td");
                const observacionesCell = document.createElement("td");

                usuarioCell.textContent = dato.usuarioCell;
                ipCell.textContent = dato.ipCell;
                extensionCell.textContent = dato.extensionCell;
                inventarioCell.textContent = dato.inventarioCell;
                departamentoCell.textContent = dato.departamentoCell;
                nombreEquipoCell.textContent = dato.nombreEquipoCell;
                observacionesCell.textContent = dato.observacionesCell;

                row.append(usuarioCell, ipCell, extensionCell, inventarioCell, departamentoCell, nombreEquipoCell, observacionesCell);
                tbody.appendChild(row);
            });
        });
};

function DataCalendar2(eventos)
{
    var calendarEl = document.getElementById('calendar');
    var evento = JSON.parse(eventos); 
    var currentDate = new Date();
    var calendar = new FullCalendar.Calendar(calendarEl,
    {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        initialDate: currentDate.toISOString().slice(0, 10),
        navLinks: true,
        businessHours: true,
        editable: true,
        locale: 'es',
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Dia'
        },
        selectable: true,
        events: evento,
        eventClick: function (info)
        {
            var createdBy = info.event.extendedProps.createdBy;
            //var descripcion = info.event.extendedProps.description; 
            var descripcion = info.event.extendedProps.description !== null ? info.event.extendedProps.description : '';
            var title = info.event.title;
            var Fecha = convertirFecha(info.event.start);
            var FechaInicio = convertirFechaAHora(info.event.start);
            var FechaFin = convertirFechaAHora(info.event.end);
     
            // Mostrar la descripción en el modal

            var modal = document.getElementById('eventoModal');
            var modalFondo = document.getElementById('fondoModal');
            var descripcionEvento = document.getElementById('descripcionEvento');
            var tituloEvento = document.getElementById('tituloEvento');
            var FechaEvento = document.getElementById('FechaEvento');
            var FechaInicioEvento = document.getElementById('FechaInicioEvento');
            var FechaFinEvento = document.getElementById('FechaFinEvento');
            var AutorEvento = document.getElementById('AutorEvento');

            descripcionEvento.innerHTML = descripcion.replace(/\n/g, '<br>');
            tituloEvento.innerHTML = title.replace(/\n/g, '<br>');
            FechaEvento.innerHTML = Fecha;
            FechaInicioEvento.innerHTML = FechaInicio;
            FechaFinEvento.innerHTML = FechaFin;
            AutorEvento.innerHTML = createdBy.replace(/\n/g, '<br>');

            //mostrar modal

            modal.style.display = 'block';
            modal.classList.add('d-flex');
            modalFondo.classList.remove('invisible');
            modalFondo.classList.add('visible');
           
            var spans = document.getElementsByClassName('close');

            for (var i = 0; i < 2; i++) {
                spans[i].onclick = function () {
                    modal.style.display = 'none';
                    modal.classList.remove('d-flex');
                    modalFondo.classList.remove('visible');
                    modalFondo.classList.add('invisible');
                };
            }
            
        }
       
    });

    calendar.render();
}

function convertirFecha(fechaFullCalendar) {
    var fecha = new Date(fechaFullCalendar);

    var dia = ('0' + fecha.getDate()).slice(-2);
    var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
    var ano = fecha.getFullYear();
    var hora = ('0' + fecha.getHours()).slice(-2);
    var minutos = ('0' + fecha.getMinutes()).slice(-2);
    var segundos = ('0' + fecha.getSeconds()).slice(-2);
   
    //var fechaFormateada = dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minutos + ':' + segundos;
    var fechaFormateada = dia + '/' + mes + '/' + ano ;

    return fechaFormateada;
}

function convertirFechaAHora(fechaFullCalendar) {
    var fecha = new Date(fechaFullCalendar);

    var dia = ('0' + fecha.getDate()).slice(-2);
    var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
    var ano = fecha.getFullYear();
    var hora = ('0' + fecha.getHours()).slice(-2);
    var minutos = ('0' + fecha.getMinutes()).slice(-2);
    var segundos = ('0' + fecha.getSeconds()).slice(-2);

    // var fechaFormateada = dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minutos + ':' + segundos;
    var fechaFormateada = hora + ':' + minutos + ':' + segundos;

    return fechaFormateada;
}
function formatoFecha(fechaFullCalendar) {
    var fecha = new Date(fechaFullCalendar);

    var dia = ('0' + fecha.getDate()).slice(-2);
    var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
    var ano = fecha.getFullYear();
   
    // var fechaFormateada = dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minutos + ':' + segundos;
    var fechaFormateada = dia + '/' + mes + '/' + ano;

    return fechaFormateada;
}

let signaturePad = null;
let pdfDataGlobal = null;
let ClonDoc = null;
let base64StringResult = null;

window.ActivarCanvas = async () => {

        const canvas = document.querySelector("canvas");
        canvas.height = canvas.offsetHeight;
        canvas.width = canvas.offsetWidth;

    signaturePad = new SignaturePad(canvas, {});

    //const image = await loadImage(savePath);
    //console.log(image);
}

function loadImage(url) {
    return new Promise(resolve => {
        const xhr = new XMLHttpRequest();
        xhr.open('GET', url, true);
        xhr.responseType = "blob";
        xhr.onload = function (e) {
            const reader = new FileReader();
            reader.onload = function (event) {
                const res = event.target.result;
                resolve(res);
            }
            const file = this.response;
            reader.readAsDataURL(file);
        }
        xhr.send();
    });
}

window.generatePDF = async (savePath) => {

    // Bueno
    const image = await loadImage(savePath);
    const signatureImage = signaturePad.toDataURL();

    const doc = new jsPDF('p', 'pt', 'letter');
    doc.addImage(image, 'PNG', 0, 0, 565, 792);
    doc.addImage(signatureImage, 'PNG', 200, 605, 300, 60);

    //obteniendo data 

    let curso = document.getElementById('curso').value;
    let nombres = document.getElementById('nombre').value;
    let apellidos = document.getElementById('apellido').value;
    let email = document.getElementById('email').value;
    let direccion = document.getElementById('direccion').value;
    let telefono = document.getElementById('telefono').value;
    let hijos = document.querySelector('input[name="hijos"]:checked').value;
    let numeroHijos = document.getElementById('numeroHijos').value;
    let discapacidad = document.querySelector('input[name="discapacidad"]:checked').value;
    let discapacidadDesc = document.getElementById('discapacidad-desc').value;


    doc.setFontSize(12);
    doc.text(curso, 260, 125);

    const date = new Date();
    doc.text(date.getUTCDate().toString(), 235, 150);
    doc.text((date.getUTCMonth() + 1).toString(), 275, 150);
    doc.text(date.getUTCFullYear().toString(), 320, 150);

    doc.setFontSize(10);
    doc.text(nombres, 170, 213);
    doc.text(apellidos, 170, 200);
    doc.text(direccion, 170, 400);
    doc.text(telefono, 170, 456);
    doc.text(email, 170, 475);

    doc.setFillColor(0, 0, 0);

    if (parseInt(hijos) === 0) {
        doc.circle(255, 374, 4, 'FD');
    } else {
        doc.circle(190, 374, 4, 'FD');
        doc.text(numeroHijos.toString(), 355, 378);
    }

    if (parseInt(discapacidad) === 0) {
        doc.circle(285, 718, 4, 'FD');
    } else {
        doc.circle(240, 718, 4, 'FD');
        doc.text(discapacidadDesc, 350, 720);
    }

    doc.addPage();
    doc.addImage(image, 'PNG', 0, 0, 565, 792);
    //doc.save("example.pdf"); 

    //return
    //var pdf = new jsPDF();
    //pdf.text(30, 30, 'hello world');
    //pdf.save("example.pdf");

    // Generar PDF con jsPDF
    //var doc = new jsPDF();

    // Agregar contenido al documento
   // doc.text('Hola, este es un PDF generado con jsPDF', 10, 10);

    // Guardar el documento en una variable
    var pdfData = doc.output('blob');

    // Crear un objeto URL para el blob del PDF
    var pdfUrl = URL.createObjectURL(pdfData);

    // Obtener el elemento del DOM con el id "PrevisualizacionPDF"
    var previsualizacionPDF = document.getElementById('PrevisualizacionPDF');

    // Limpiar el contenido existente del div
    previsualizacionPDF.innerHTML = ''; // Vaciar el contenido HTML

    // Mostrar el PDF en un elemento iframe
    var iframe = document.createElement('iframe');
    iframe.src = pdfUrl;
    iframe.style.width = '100%';
    iframe.style.height = '700px'; // Establecer la altura deseada
    previsualizacionPDF.appendChild(iframe);

}

window.generatePDFMejorado = async (PaginaParte1, PaginaParte2, datosPacienteJSON, habitacionJSON, kitIngresoJSON, datosResponsablePacienteJSON) => {

    // Bueno
    const image1 = await loadImage(PaginaParte1);
    const image2 = await loadImage(PaginaParte2);
    // const signatureImage = signaturePad.toDataURL();
    const datosPaciente = JSON.parse(datosPacienteJSON);
    const habitacion = JSON.parse(habitacionJSON);
    const kitIngreso = JSON.parse(kitIngresoJSON);
    const datosResponsablePaciente = JSON.parse(datosResponsablePacienteJSON);

    var doc = new jsPDF('p', 'pt', 'letter');
    doc.addImage(image1, 'PNG', 0, 0, 565, 792);
    doc.setFontSize(10);

    doc.text(datosPaciente.NombreApellido, 140, 107);
    doc.text(formatoFecha(datosPaciente.FechaIngreso), 470, 107);
    doc.text(datosPaciente.Cedula, 150, 125);
    doc.text(datosPaciente.Direccion, 27, 125);
    doc.text(datosPaciente.Correo, 290, 125);
    doc.text(formatoFecha(datosPaciente.FechaEgreso), 470, 125);
    doc.text(datosPaciente.Edad, 27, 145);
    if (datosPaciente.Genero == 'Masculino')
        doc.text('x', 174, 145);
    if (datosPaciente.Genero == 'Femenino')
       doc.text('x', 192 , 145);
    doc.text(datosPaciente.Telefono, 250, 145);
    doc.text(datosPaciente.NumeroHabitacion, 350, 145);
    doc.text(datosPaciente.Piso, 470, 145);

    const posicionesIzquierda = {
        'Optimo': 150,
        'Regular': 190,
        'Bueno': 230
    };
    const posicionesDerecha = {
        'Optimo': 423,
        'Regular': 459,
        'Bueno': 491
    };
    const posicionesCondicionHigiene = {
        'Excelente': 88,
        'Regular': 136,
        'Deficiente': 187
    };

    if (habitacion.Espejo !== null && habitacion.Espejo.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.Espejo];
        if (posicion) {
            doc.text('x', posicion, 219);
        }

    }
    if (habitacion.Televisor !== null && habitacion.Televisor.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.Televisor];
        if (posicion) {
            doc.text('x', posicion, 240);
        }
    }
    if (habitacion.ControlAireAcondicionado !== null && habitacion.ControlAireAcondicionado.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.ControlAireAcondicionado];
        if (posicion) {
            doc.text('x', posicion, 262);
        }
    }
    if (habitacion.ControlTvCable !== null && habitacion.ControlTvCable.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.ControlTvCable];
        if (posicion) {
            doc.text('x', posicion, 284);
        }
    }
    if (habitacion.ControlRemotoTv !== null && habitacion.ControlRemotoTv.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.ControlRemotoTv];
        if (posicion) {
            doc.text('x', posicion, 300);
        }
    }
    if (habitacion.Ducha !== null && habitacion.Ducha.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.Ducha];
        if (posicion) {
            doc.text('x', posicion, 319);
        }
    }
    if (habitacion.DuchaTelefono !== null && habitacion.DuchaTelefono.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.DuchaTelefono];
        if (posicion) { 
            doc.text('x', posicion, 337);
        }
    }
    if (habitacion.Sanitario !== null && habitacion.Sanitario.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.Sanitario];
        if (posicion) {
            doc.text('x', posicion, 356);
        }
    }
    if (habitacion.Lavamanos !== null && habitacion.Lavamanos.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.Lavamanos];
        if (posicion) {
            doc.text('x', posicion, 375);
        }
    }
    if (habitacion.Lamparas !== null && habitacion.Lamparas.trim() !== "") {
        const posicion = posicionesIzquierda[habitacion.Lamparas];
        if (posicion) {
            doc.text('x', posicion, 396);
        }
    }
    if (habitacion.ParedesPintura !== null && habitacion.ParedesPintura.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.ParedesPintura];
        if (posicion) {
            doc.text('x', posicion, 219);
        }

    }
    if (habitacion.JuegosSabanas !== null && habitacion.JuegosSabanas.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.JuegosSabanas];
        if (posicion) {
            doc.text('x', posicion, 240);
        }
    }
    if (habitacion.Escabel !== null && habitacion.Escabel.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.Escabel];
        if (posicion) {
            doc.text('x', posicion, 262);
        }
    }
    if (habitacion.CamaHospitalaria !== null && habitacion.CamaHospitalaria.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.CamaHospitalaria];
        if (posicion) {
            doc.text('x', posicion, 284);
        }
    }
    if (habitacion.Mesa !== null && habitacion.Mesa.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.Mesa];
        if (posicion) {
            doc.text('x', posicion, 300);
        }
    }
    if (habitacion.Divan !== null && habitacion.Divan.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.Divan];
        if (posicion) {
            doc.text('x', posicion, 319);
        }
    }
    if (habitacion.ParalMedicamentos !== null && habitacion.ParalMedicamentos.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.ParalMedicamentos];
        if (posicion) {
            doc.text('x', posicion, 337);
        }
    }
    if (habitacion.Papelera !== null && habitacion.Papelera.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.Papelera];
        if (posicion) {
            doc.text('x', posicion, 356);
        }
    }
    if (habitacion.Cobija !== null && habitacion.Cobija.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.Cobija];
        if (posicion) {
            doc.text('x', posicion, 375);
        }
    }
    if (habitacion.Almohada !== null && habitacion.Almohada.trim() !== "") {
        const posicion = posicionesDerecha[habitacion.Almohada];
        if (posicion) {
            doc.text('x', posicion, 396);
        }
    }
    if (habitacion.Observaciones !== null && habitacion.Observaciones.trim() !== "") {
        const textLength = habitacion.Observaciones.length;
        if (textLength <= 100) {
            doc.text(habitacion.Observaciones, 77, 415);
        } else {
            doc.text(habitacion.Observaciones.substring(0, 100), 77, 415);
            doc.text(habitacion.Observaciones.substring(100), 77, 427);
        }
 
    }

    if (kitIngreso.Jarra)
    {
        doc.text('x', 23, 463);
    }
    if (kitIngreso.Vaso) {
        doc.text('x', 23, 472);
    }
    if (kitIngreso.Ponchera) {
        doc.text('x', 129, 463);
    }
    if (kitIngreso.Bandeja) {
        doc.text('x', 129, 472);
    }
    if (kitIngreso.Rinonera) {
        doc.text('x', 236, 463);
    }
    if (kitIngreso.Pato) {
        doc.text('x', 236, 472);
    }
    if (kitIngreso.Pito) {
        doc.text('x', 342, 463);
    }
    if (kitIngreso.Jabonera) {
        doc.text('x', 342, 472);
    }
    if (kitIngreso.PapelSanitario) {
        doc.text('x', 449, 453);
    }
    if (kitIngreso.KitEspecial) {
        doc.text('x', 449, 462);
    }
    if (kitIngreso.Esquinero) {
        doc.text('x', 300, 494);
    }
    if (kitIngreso.Cobija) {
        doc.text('x', 371, 494);
    }
    if (kitIngreso.Almohada) {
        doc.text('x', 431, 494);
    }
    if (kitIngreso.Toalla) {
        doc.text('x', 502, 494);
    }

    if (kitIngreso.Cama !== null && kitIngreso.Cama.trim() !== "") {
        const posicion = posicionesCondicionHigiene[kitIngreso.Cama];
        if (posicion) {
            doc.text('x', posicion, 547);
        }
    }
    if (kitIngreso.Piso !== null && kitIngreso.Piso.trim() !== "") {
        const posicion = posicionesCondicionHigiene[kitIngreso.Piso];
        if (posicion) {
            doc.text('x', posicion, 564);
        }
    }
    if (kitIngreso.Paredes !== null && kitIngreso.Paredes.trim() !== "") {
        const posicion = posicionesCondicionHigiene[kitIngreso.Paredes];
        if (posicion) {
            doc.text('x', posicion, 582);
        }
    }
    if (kitIngreso.Banos !== null && kitIngreso.Banos.trim() !== "") {
        const posicion = posicionesCondicionHigiene[kitIngreso.Banos];
        if (posicion) {
            doc.text('x', posicion, 598);
        }
    }
    doc.setFontSize(9);

    if (kitIngreso.Observaciones !== null && kitIngreso.Observaciones.trim() !== "") {
        const textLength = kitIngreso.Observaciones.length;
        const maxLines = Math.ceil(textLength / 14);
        const startY = 537;
        const lineHeight = 12;

        for (let i = 0; i < maxLines; i++) {
            const startIndex = i * 14;
            const endIndex = startIndex + 14;
            const text = kitIngreso.Observaciones.substring(startIndex, endIndex);
            const y = startY + (i * lineHeight);

            doc.text(text + (endIndex < textLength ? ' - ' : ''), 218, y);
        }
    }
    if (kitIngreso.DatosCamarera1 !== null && kitIngreso.DatosCamarera1.trim() !== "") {
        const textLength = kitIngreso.DatosCamarera1.length;
        const anchoTexto = 36;
        const maxLines = Math.ceil(textLength / anchoTexto);
        const startY = 527;
        const lineHeight = 12;

        for (let i = 0; i < maxLines; i++) {
            const startIndex = i * anchoTexto;
            const endIndex = startIndex + anchoTexto;
            const text = kitIngreso.DatosCamarera1.substring(startIndex, endIndex);
            const y = startY + (i * lineHeight);

            doc.text(text + (endIndex < textLength ? ' - ' : ''), 290, y);
        }
    }

    if (kitIngreso.Jabon) {
        doc.text('x', 36, 626);
    }
    if (kitIngreso.Cloro) {
        doc.text('x', 98, 626);
    }
    if (kitIngreso.Desengrasante) {
        doc.text('x', 148, 626);
    }
    if (kitIngreso.AntimohoCal) {
        doc.text('x', 227, 626);
    }

    if (kitIngreso.Otro !== null && kitIngreso.Otro.trim() !== "") {
        doc.text(kitIngreso.Otro, 316, 628);
    }
    

    doc.setFontSize(10);
    doc.text(datosResponsablePaciente.NombreApellido, 40, 647);
    doc.text(datosResponsablePaciente.Direccion, 283, 647);
    doc.text(datosResponsablePaciente.Cedula, 27, 658);
    doc.text(formatoFecha(datosResponsablePaciente.FechaIngreso), 85, 667);
    doc.text(datosResponsablePaciente.NumeroHabitacion, 465, 667);


    //doc.text(apellidos, 170, 200);
    //doc.text(direccion, 170, 400);
    //doc.text(telefono, 170, 456);
    //doc.text(email, 170, 475);

   // doc.addImage(signatureImage, 'PNG', 200, 605, 300, 60);




    //doc.save("example.pdf");

    //return
    //var pdf = new jsPDF();
    //pdf.text(30, 30, 'hello world');
    //pdf.save("example.pdf");

    // Generar PDF con jsPDF
    //var doc = new jsPDF();

    // Agregar contenido al documento
    // doc.text('Hola, este es un PDF generado con jsPDF', 10, 10);

    doc.addPage();
    doc.addImage(image2, 'PNG', 0, 0, 565, 792);

    ClonDoc = doc;

    // Guardar el documento en una variable
    pdfDataGlobal = doc.output('blob');
    base64String = doc.output('datauristring').split(',')[1];
    console.log('Base64:', base64String);

    // Crear un objeto URL para el blob del PDF
    var pdfUrl = URL.createObjectURL(pdfDataGlobal);
    var pdfUrl2 = 'data:application/pdf;base64,' + base64String;


    console.log('termino funcion');
    console.log('cantidad : ' + base64String.length);

    base64StringResult = base64String;

    // return base64String.substring(0, 32000);;
    return String(base64String.length)

}

window.InsertarFirmaDigitalEnPDF = async () =>
{
    const signatureImage = signaturePad.toDataURL();

    ClonDoc.setPage(1);
    ClonDoc.addImage(signatureImage, 'PNG', 130, 690, 300, 60, 'page1');

    base64String = ClonDoc.output('datauristring').split(',')[1];
    base64StringResult = base64String;

    return String(base64String.length)


}
window.getBase64PdfChunk = function (startIndex, endIndex) {

    return base64StringResult.substring(startIndex, endIndex);
};

