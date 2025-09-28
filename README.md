# Projeto Final da Disciplina

# Pós-Graduação em Desenvolvimento Mobile e Cloud Computing – Inatel
## DM117 – Desenvolvimento de jogos com Unity

## Projeto Final da Disciplina : 
Implementação do jogo **Apocalipse Z: O Protótipo**
### 👤 Autor: 
José Enderson Ferreira Rodrigues   
jose.rodrigues@pg.inatel.br, jose.e.f.rodrigues.br@gmail.com

## 📌 Descrição do jogo
**Apocalipse Z: O Protótipo** é um protótipo de jogo de ação e sobrevivência em terceira pessoa, onde o jogador assume o papel de um sobrevivente em um mundo pós-apocalíptico dominado por zumbis. A fantasia principal é a de lutar contra todas as adversidades para sobreviver, explorando um mundo hostil e enfrentando ameaça de zumbis. O objetivo principal deste projeto é testar e validar as mecânicas centrais de movimento, combate e confronto com inimigos. A experiência de jogo será focada em um cenário de combate básico contra zumbis, provando a viabilidade do conceito do jogo.

## 📌 Requisitos atendidos

✅ O projeto deve ter no mínimo 2 cenas

✅ Cena Inicial (Menu): Contém pelo menos um botão funcional “Start Game”.

✅ Cena Inicial (Menu): Pode conter outros botões opcionais (Sair, Créditos, etc.).

✅ Cena do Jogo: Ambiente simples em 2D ou 3D.

✅ Cena do Jogo: Deve implementar pelo menos duas mecânicas básicas (ex.: movimentar personagem, coletar objeto, pular, atirar, etc.).

✅ Funcionalidade: O botão Start Game deve carregar a cena do jogo.

✅ Funcionalidade: O jogador deve ter algum tipo de objetivo ou interação mínima (ex.: coletar 3 itens, atravessar de um lado ao outro, alcançar um ponto específico).

✅ Organização: Nomear corretamente as cenas, scripts e GameObjects principais.

✅ Organização: Usar pastas para organizar Scenes, Scripts e Assets.

✅ Extras Opcionais: Cena final de vitória ou tela de “Game Over”.

✅ Extras Opcionais: Feedback visual ou sonoro (efeito ao coletar item, música de fundo, etc.).

✅ Extras Opcionais: Um contador de pontos ou timer simples.

## 📌 Controles
* Setas direcionas do teclado para movimentação do player
* Botão CTRL para tiro
* Botão Espaço para pular (física não ajustada para essa mecânica)

## 📌 Imagens do jogo
<img style="margin-right: 30px" src="./DiagramaProjetoFinal.jpg" width="600px" alt="Diagrama de Classes"/><br>  

## 📌 Detalhamento da solução
#### 📂 Estrutura de pastas do projeto
```
📦estoque
 ┗📂src
   ┣📂node_modules                 # Diretório onde o npm (Node Package Manager) instala todas as dependências do projeto.
   ┣📂controllers             		 
   ┃ ┣📜AuthController.js          # Responsável pela comunicação com o serviço auth
   ┃ ┗📜EstoqueController.js       # Responsável por atender às requisições do CRUD do serviço Estoque
   ┣📂database
   ┃ ┗📜config.js                  # Configurações do MongoDB
   ┣📂logger                      
   ┃ ┗📜index.js                   # Responsável pelo registro de logs (não utilizado)
   ┣📂models                      
   ┃ ┗📜Produto.js                 # Entidade que conterá os campos a serem manipulados pelo CRUD
   ┣📂service           	 
   ┃ ┗📜AlarmeService.js           # Responsável pela comunicação de alarmes ao serviço monitor
   ┣📜.dockerignore                # Informa ao Docker quais arquivos e pastas devem ser ignorados
   ┣📜.env                         # Aramazenamento de variáveis de ambiente
   ┣📜.gitignore                   # Informa ao Docker quais arquivos e pastas devem ser ignorados
   ┣📜Dockerfile                   # Define os passos para a criação de uma imagem Docker
   ┣📜index.js                     # Ponto de entrada principal da aplicação
   ┣📜nodemon.json                 # Configura o comportamento do Nodemon sempre que detecta mudanças nos arquivos
   ┣📜package-lock.json            # Arquivo gerado automaticamente que registra as versões das dependências instaladas
   ┣📜package.json                 # Arquivo de configuração principal. Define informações do projeto, dependências e scripts
   ┗📜routes.js                    # Define, organiza e centraliza as rotas da aplicação
```

  

## 🛠️ IDE
- **Unity 6.2**

## 💻 Linguagem
- **C#**
