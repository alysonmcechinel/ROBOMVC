var form = document.getElementById('roboForm');

document.addEventListener('DOMContentLoaded', function () {

    form.addEventListener('change', function (event) {
        if (event.target.tagName === 'SELECT') {
            const formData = new FormData(form);

            // Construa a query string
            const queryParams = new URLSearchParams();
            formData.forEach((value, key) => {
                queryParams.append(key, value);
            });

            fetch(`/Mov/SendCommands?${queryParams.toString()}`, {
                method: 'POST',
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
                alert('Comando enviado e status atualizado com sucesso!');
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
