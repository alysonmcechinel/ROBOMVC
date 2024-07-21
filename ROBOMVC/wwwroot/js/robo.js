const form = document.getElementById('roboForm');

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

            fetch(`/Mov/EnviarComandos?${queryParams.toString()}`, {
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
                    // Simula quebras de linha usando '\n' e destaca "sistema corrompido!!" usando asteriscos
                    alert(`Erro:\n${error.message}\nSistema corrompido!!`);
                    location.reload(); // Reinicia a página para o estado inicial
                });
        }
    });

    // Initialize all dropdowns to their default "Em Repouso" state
    const initializeDropdowns = () => {
        const dropdowns = form.querySelectorAll('select');
        dropdowns.forEach(dropdown => {
            dropdown.value = 'EmRepouso';
        });
    };

    initializeDropdowns();
});
