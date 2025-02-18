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
        exibirUsuarios(); // Atualiza a tabela após cadastrar um novo usuário
    })
    .catch(error => {
        console.error('Erro ao enviar dados:', error);
        alert('Erro ao cadastrar usuário');
    });
});

// Função para exibir os usuários na tabela
function exibirUsuarios() {
    fetch('http://localhost:5118/api/usuarios')  // URL da sua API
        .then(response => response.json())
        .then(data => {
            const tabelaUsuarios = document.getElementById('tabela-usuarios');
            const tbody = document.getElementById('usuarios-tbody');

            tbody.innerHTML = ''; // Limpa a tabela antes de adicionar novos dados

            data.forEach(usuario => {
                const tr = document.createElement('tr');

                tr.innerHTML = `
                    <td>${usuario.nome}</td>
                    <td>${usuario.email}</td>
                    <td>${usuario.telefone}</td>
                    <td>${usuario.endereco}</td>
                    <td>${usuario.cpf}</td>
                    <td>${usuario.tipoUsuario}</td>
                `;

                tbody.appendChild(tr);
            });
        })
        .catch(error => {
            console.error('Erro ao carregar os dados:', error);
            alert('Erro ao carregar os dados');
        });
}

// Chama a função para exibir os dados quando a página carregar
window.onloa
