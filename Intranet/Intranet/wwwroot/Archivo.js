
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

    //const image = await loadImage(savePath);
    //const pdf = new jsPDF();
    //pdf.addImage(image, 'PNG', 0, 0, 565, 792);
    //pdf.save("example.pdf");

    //var pdf = new jsPDF();
    //pdf.text(30, 30, 'hello world');
    //pdf.save("example.pdf");

    // Generar PDF con jsPDF
    var doc = new jsPDF();
    doc.text('¡Hola, mundo!', 10, 20);

    var base64PDF = doc.output('dataurlnew');

    // Crear elemento img para mostrar el PDF
    var imgElement = document.createElement('img');
    imgElement.src = base64PDF;
    imgElement.width = '792';
    imgElement.height = '565';

    // Obtener el div con ID "PrevisualizacionPDF"
    var previsualizacionDiv = document.getElementById('PrevisualizacionPDF');

    // Agregar el elemento img al div
    previsualizacionDiv.appendChild(imgElement);

}


