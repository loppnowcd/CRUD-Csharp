// Função para enviar os dados do formulário para a API
document.getElementById('formCadastro').addEventListener('submit', function(event) {
    event.preventDefault(); // Impede o envio do formulário padrão

    // Captura os dados do formulário
    const usuario = {
        nome: document.getElementById('nome').value,
        email: document.getElementById('email').value,
        telefone: document.getElementById('telefone').value,
        endereco: document.getElementById('endereco').value,
        cpf: document.getElementById('cpf').value,
        tipoUsuario: document.getElementById('tipoUsuario').value
    };

    // Envia os dados para a API via POST
    fetch('http://localhost:5118/api/usuarios', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(usuario)  // Envia os dados como JSON
    })
    .then(response => response.json())
    .then(data => {
        alert('Usuário cadastrado com sucesso!');
        console.log(data);
    })
    .catch(error => {
        console.error('Erro ao enviar dados:', error);
        alert('Erro ao cadastrar usuário');
    });
});
