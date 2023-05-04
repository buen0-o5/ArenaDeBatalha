# Arena De Batalha 🎮👾
<div>
<img src="https://user-images.githubusercontent.com/112562547/236335258-01dc1650-6d0c-4491-b12f-59f100fc214b.png" width="400px"/>
</div>

## Classe GameObject

Contém todos os métodos e características necessárias para a criação deste jogo, contendo:

Propriedades:

- Imagem (Sprite)
- Verifica se o objeto esta ativo (Active)
- Velocidade (Speed)
- Posição  (Left, Top )
- Tamanho (Width, Height )
- Limites da tela onde o objeto será renderizado  (Bounds)
- O retângulo que representa o objeto (Rectangle)
- O som que vai ser tocado em caso de colisão   (Sound)
- Referencia para a tela de pintura (Screen)
- Referencia para o tocador de som (soundPlayer)

### Métodos

- GetSprite (método abstrato que é usado para implementação do Sprite de cada objeto  )
- Construtor (método  para criação do objeto)
- UpdateObjecct (método que atualiza o  posicionamento do retângulo do objeto durante seu ciclo de vida )
- IsOutOfBounds (método que verifica se o objeto esta dentro dos limites da tela)
- IsCollidingWith(método que verifica se o objeto esta colidindo com outro objeto)
- PlaySound(método de tocar som)
- Destroy(método que destrói o objeto para não ficar ocupando a memoria)

### Métodos de coordenadas:

- MoveLeft
- MoveRight
- MoveDown
- MoveUp

## As outras classes contém as especificidades de cada Método da classe  GameObject

## FormPrincipal

Contém  algumas variáveis que controlam o jogo

### Propriedades:

- Times para disparar loop do jogo (gameLoopTimer)
- Times para disparar loop do inimigo (enemySpawnTimer)
- Propriedade onde ocorre a pintura de cada quadro do jogo(screenBuffer)
- Ferramenta de pintura(screenPainter)
- Objeto que representa o plano de fundo
- Objeto que representa o jogador
- Objeto que representa o GameOver
- Lista dos objetos que estão sendo renderizado na tela
- Gerador de numero aleatórios
- Variável que controla o disparo de tiro (verificação para ao ser acionado o botão para atirar o jogador não possa segurar botão  para gerar varios tiros )

### Métodos:

- Construtor
- StartGame
- EndGame
- SpawnEnemy
- GameLoop
- ProcessControls

### Eventos:

- FormPrincipal_Paint
- FormPrincipal_KeyDown
