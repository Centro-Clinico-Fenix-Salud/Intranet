

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
    
}