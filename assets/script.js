// assets/script.js

// Garante que o script só execute depois que o HTML estiver completamente carregado
document.addEventListener('DOMContentLoaded', () => {
    // Função para enviar os dados do formulário para a API
    document.getElementById('formCadastro').addEventListener('submit', async function(event) { // Adicionado 'async'
        event.preventDefault(); // Impede o envio do formulário padrão

        // Captura os dados do formulário
        const usuario = {
            nome: document.getElementById('nome').value,
            email: document.getElementById('email').value,
            telefone: document.getElementById('telefone').value, // Nome do ID está 'telefone'
            endereco: document.getElementById('endereco').value,
            cpf: document.getElementById('cpf').value,
            tipoDeUsuario: document.getElementById('tipoUsuario').value // Corrigido para 'tipoDeUsuario' (camelCase)
        };

        // Envia os dados para a API via POST
        try { // Adicionado try/catch para melhor tratamento de erros de rede
            const response = await fetch('http://localhost:5118/api/usuarios', { // Adicionado 'await'
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(usuario) // Envia os dados como JSON
            });

            if (response.ok) { // Verifica se a resposta foi bem-sucedida (status 2xx)
                const data = await response.json(); // Adicionado 'await'
                alert('Usuário cadastrado com sucesso!');
                console.log(data);
                formCadastro.reset(); // Adicionado para limpar o formulário
                exibirUsuarios(); // Atualiza a tabela após cadastrar um novo usuário
            } else {
                // Lidar com erros da API (ex: validação, erro no servidor)
                const errorData = await response.json(); // Adicionado 'await'
                alert(`Erro ao cadastrar usuário: ${errorData.title || errorData.detail || response.statusText}`);
                console.error('Erro na resposta da API:', errorData);
            }
        } catch (error) {
            // Lidar com erros de rede (ex: API não está rodando)
            console.error('Erro ao enviar dados ou conectar à API:', error);
            alert('Erro de conexão: Verifique se a API está rodando.');
        }
    });

    // Função para exibir os usuários na tabela
    async function exibirUsuarios() { // Adicionado 'async'
        try { // Adicionado try/catch
            const response = await fetch('http://localhost:5118/api/usuarios'); // Adicionado 'await'
            if (!response.ok) { // Verifica se a resposta foi bem-sucedida
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json(); // Adicionado 'await'

            const tbody = document.getElementById('usuarios-tbody');

            tbody.innerHTML = ''; // Limpa a tabela antes de adicionar novos dados

            data.forEach(usuario => {
                const tr = document.createElement('tr');

                tr.innerHTML = `
                    <td>${usuario.nome}</td>
                    <td>${usuario.email}</td>
                    <td>${usuario.fone}</td> // Corrigido para 'fone' (verifique se o backend retorna 'fone' ou 'telefone')
                    <td>${usuario.endereco}</td>
                    <td>${usuario.cpf}</td>
                    <td>${usuario.tipoDeUsuario}</td> // Corrigido para 'tipoDeUsuario' (verifique o backend)
                `;

                tbody.appendChild(tr);
            });
            console.log('Usuários carregados e exibidos com sucesso.');
        } catch (error) {
            console.error('Erro ao carregar os dados:', error);
            alert('Erro ao carregar os dados. Verifique o console para mais detalhes.');
        }
    }

    // Chama a função para exibir os dados quando a página carregar
    exibirUsuarios();
});
