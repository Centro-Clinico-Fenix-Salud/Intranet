

function DataCalendar() {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        initialDate: '2024-03-01',
        navLinks: true, // can click day/week names to navigate views
        businessHours: true, // display business hours
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
    var calendar = new FullCalendar.Calendar(calendarEl,
    {
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
        events: evento,
        eventClick: function (info)
        {
            var descripcion = info.event.extendedProps.description; 
            // Mostrar la descripción en el modal

            var modal = document.getElementById('eventoModal');
            var modalFondo = document.getElementById('fondoModal');
            var descripcionEvento = document.getElementById('descripcionEvento');
            //descripcionEvento.textContent = descripcion;
            var texto = 'Aitor Blanco\nOtro texto en la siguiente linea';
            texto = texto.replace(/\n/g, '<br>');
            descripcionEvento.innerHTML = texto;
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


