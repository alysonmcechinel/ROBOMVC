document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('roboForm');

    form.addEventListener('change', function (event) {
        if (event.target.tagName === 'SELECT') {
            const formData = new FormData(form);

            // Construa a query string
            const queryParams = new URLSearchParams();
            formData.forEach((value, key) => {
                queryParams.append(key, value);
            });

            fetch(`/Mov/SendCommands?${queryParams.toString()}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json'
                }
            })
                .then(response => {
                    if (!response.ok) {
                        return response.text().then(text => { throw new Error(text); });
                    }
                    return response.json();
                })
                .then(data => {
                    alert('Comando enviado com sucesso!');
                })
                .catch(error => {
                    alert(`Erro:\n${error.message}\n\nSistema corrompido.\nOs comandos serão reiniciados.`);
                    location.reload();
                });
        }
    });
    
    const initializeDropdowns = () => {
        const dropdowns = form.querySelectorAll('select');
        dropdowns.forEach(dropdown => {
            dropdown.value = 'AtRest';
        });
    };

    initializeDropdowns();
});
