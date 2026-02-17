# 🧟 Apocalipse Zumbi

> Um jogo de sobrevivência em terceira pessoa desenvolvido na Unity com foco em Inteligência Artificial e gestão de recursos.

## 📋 Sobre o Projeto

Este projeto foi desenvolvido como parte dos estudos em **Ciência da Computação na UFAL**, com o objetivo de aplicar conceitos avançados de desenvolvimento de jogos e inteligência artificial.

O jogador deve sobreviver a hordas de zumbis, gerenciar munição escassa e enfrentar chefes com comportamentos únicos, utilizando um sistema de combate tático e movimentação fluida.

## ⚙️ Funcionalidades Técnicas (Highlights)

O projeto destaca as seguintes implementações técnicas:

- **Inteligência Artificial (NavMesh):**
  - Inimigos utilizam `NavMeshAgent` para pathfinding dinâmico, evitando obstáculos estáticos e dinâmicos.
  - Implementação de **Máquina de Estados Finita (FSM)** para comportamentos dos inimigos (Perseguir, Atacar, Vagar, Morrer).
  - O Chefe possui lógica de rotação independente do NavMesh para maior precisão nos ataques.

- **Sistema de Combate e Física:**
  - Detecção de colisão precisa usando `Raycast` e `Interfaces` (ex: `IMatavel`) para desacoplamento de código.
  - Balas com física (`Rigidbody`) ou Raycast.
  - Feedback visual e sonoro (partículas de sangue, sons de impacto) instanciados.

- **Gestão de Áudio e UI:**
  - Sistema de Áudio Centralizado (Singleton `ControlaAudio`) separando canais de Música e SFX.
  - Interface de Usuário (UI) responsiva com barras de vida, contagem de munição e menus interativos.
  - Animações procedurais no Menu (Random Idle Behaviors).

## 🎮 Controles

| Tecla | Ação |
| :---: | :--- |
| **W, A, S, D** | Movimentação do Personagem |
| **Mouse** | Mirar e Rotacionar |
| **Botão Esq. Mouse** | Atirar |
| **R** | Recarregar Arma |
| **Esc** | Pausar Jogo |

## 🛠️ Tecnologias Utilizadas

- **Engine:** Unity 6
- **Linguagem:** C#
- **Ferramentas:** Visual Studio

## 🚀 Como Baixar e Jogar

Este jogo está disponível para **Windows** (x64). Você não precisa ter a Unity instalada para jogar.

### Passo a Passo

1. Vá até a aba **[Releases](../../releases)** deste repositório.
2. Baixe o arquivo `.zip` da versão mais recente.
3. Extraia o conteúdo para uma pasta de sua preferência.
4. Execute o arquivo **`ApocalipseZumbi.exe`**.

> **Nota:** Como este é um projeto acadêmico e não possui assinatura digital, o Windows pode exibir uma tela azul dizendo "O Windows protegeu o computador". Clique em **Mais informações** > **Executar assim mesmo** para abrir o jogo.

### Requisitos Mínimos (Estimados)
- **OS:** Windows 10/11
- **Processador:** i3 ou equivalente
- **Memória:** 4GB RAM
- **Placa de Vídeo:** Integrada ou Dedicada com suporte a DX11

---
Desenvolvido por **Enrique Ferreira da Silva** com auxílio do Professor Henrique Morata (Curso Alura).
