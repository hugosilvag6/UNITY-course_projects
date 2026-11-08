# ═══════════════ DOCUMENTAÇÃO ════════════════════════
https://docs.unity3d.com/Manual/index.html

# ═══════════════ CONCEITOS ═══════════════════════════
## Organização
```csharp
public class PlayerMovement : MonoBehaviour {
    // Configuration
    float jumpSpeed = 20f;
    // muito bom serializar em prefabs, pra mudar cada um individualmente sem mexer no código
    // O valor definido no código serve como valor inicial para novas instâncias (prefabs), ainda permitindo customização
    [SerializeField] float moveSpeed = 10f;

    // Components
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;

    // State
    bool isAlive = true;
    Vector2 currentMovementDirection;

    // Cached Values
    float originalRigidBodyGravity;

    // Unity Lifecycle
    void Awake() {
        // monta minhas referências, e roda consideravelmente antes do Start.
        // esse código aqui por exemplo quebrou quando Bullet definia o rigidBody no Start e não no Awake:
            // var newBullet = Instantiate(bulletPrefab, position, quaternion);
            // newBullet.Initialize(transform.localScale.x);
    }

    void Start() {
        // começa alguma lógica depois que tudo foi inicializado
    }

    void Update() {
        // roda uma vez por frame, então sua frequência depende do FPS
        // ideal para input, animações, UI e lógica geral
    }

    void FixedUpdate() {
        // roda em intervalos fixos (por padrão a cada 0.02s = 50x por segundo), independente do FPS
        // ideal para física: Rigidbody, AddForce, linearVelocity, MovePosition, etc
    }

    // Input
    void OnMove(InputValue value) { }
    void OnJump(InputValue value) { }

    // Movement
    void Run() { }
    void ClimbLadder() { }
    void FlipSprite() { }

    // Helpers
    bool IsGrounded() { }
}
```

## Time.deltaTime
É basicamente o intervalo entre frames. Faz com que `Update` use tempo em vez de FPS.
    - 1 segundo / 60 frames = 0.0167
    - 1 segundo / 30 frames = 0.0333
Se pegamos `float speed = 5f;` e multiplicamos por `Time.deltaTime`, o carro se move igualmente em 60 e 30 FPS, porque o intervalo entre frames é o que importa.
    - 5 × 0.0167 = 0.0835 → 0.0835 × 60 ≈ 5 metros
    - 5 × 0.0333 = 0.1665 → 0.1665 × 30 ≈ 5 metros
Basicamente dizemos:
> "Quero andar 5 metros por segundo, independentemente de quantos frames existirem."
```csharp
transform.Translate(0, 10f * Time.deltaTime, 0);
transform.Rotate(0, 0, 100f * Time.deltaTime);
```

## SerializeField
O `SerializeField` basicamente faz a variável ser "editável" na IDE da Unity, sem ter que mexer no código.
Outro jeito seria tornar a variável public, mas aí ela seria pública pra IDE e também pra outras  classes.
```csharp
[SerializeField] float moveSpeed = 10f;
[SerializeField] float steerSpeed = 100f;
```

## Tags
São labels que podemos dar a um GameObject (tipo ID), para identificá-los no código, de forma a fazer coisas diferentes de acordo com qual tag um objeto tem. Depois de adicionar, podemos "ver" a tag no código.
Para adicionar: no topo do Inspector do GameObject tem as tags. Podemos criar uma personalizada, e depois selecioná-la.
OBS:
    1) É como se a Tag fosse o ID do HTML (podendo ser usado mais de uma vez), e o Layers (tópico abaixo) fosse a Class.
    2) É uma boa prática evitar usar Tag a menos que a gente REALMENTE precise (assim como preferimos class no CSS em vez de ID).
```csharp
private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Package")) {
        Debug.Log("Entrou no Package");
    }
}
```

## Layers
É como se fosse a Tag acima, mas tá mais pra um grupo. É como se a Tag fosse o ID do HTML (podendo ser usado mais de uma vez), e o Layers fosse a Class. Seria tipo ter 2 monstros, um com tag FemaleOrc e outro com tag MaleOrc, mas ambos com Layer Orc.
OBS:
    1) Em layer trabalhamos com index, e não com o nome direto.
```csharp
private void OnTriggerEnter2D(Collider2D other) {
    int layerIndex = LayerMask.NameToLayer("Player");
    if(other.gameObject.layer == layerIndex) {
        print("player won");
    }
}
```
Outra forma de organizar layers é pelo eixo Y, fazendo com que coisas mais abaixo do eixo Y estejam à frente, dando sensação de profundidade, como no jogo 2D RPG que fizemos. 
O ideal aqui é mudar o "Sprite Sort Point" do sprite renderer para pivot, em vez de center. Assim definimos o ponto pivot do objeto, e é esse ponto pivot que será comparado com o ponto pivot do outro objeto. o que tiver Y mais baixo, fica à frente.
          árvore
          ┌───┐
          │   │
          │   │
          │   │
          └─┬─┘ ← (se o pivot do player estivesse aqui, estaria atrás da árvore)
            ● ← Pivot na base (atrás do player)

          Player
            ● ← Pivot nos pés (player tá na frente da árvore)
Aula: https://www.udemy.com/course/unity-2d-rpg/learn/lecture/36837258

## Prefabs
É basicamente um molde de GameObject, como uma classe/interface. Ele permite salvar um GameObject já configurado com seus Components, scripts, valores, filhos, etc, e criar várias cópias dele sem precisar configurar tudo novamente. Sem isso, se quiséssemos fazer objetos iguais, teríamos que duplicar dezenas de objetos e deixá-los na tela para manipulá-los. Assim a gente tem um molde e cria quando necessário.
Para criar:
    - Arrastamos um GameObject da Hierarchy para alguma pasta dentro de Assets. Ele passa a ser um arquivo .prefab.
Para alterar todos:
    - Mudamos uma instância de prefab, e no topo do Inspector clicamos em Overrides e Apply all

## Acessando Componentes
Podemos acessar componentes de um GameObject usando `GetComponent`.
```csharp
GetComponent<ParticleSystem>().Play();
```

## Fontes Externas
1) Primeiro criamos uma pasta Fonts, e adicionamos a fonte lá dentro.
2) Botão direito no arquivo
3) Create -> TextMesh Pro -> Font Asset -> SDF
4) Isso vai gerar o arquivo no melhor formato pra nós. 
OBS: 
    - Tentei de outra forma, e deu errado e quebrou tudo, a única forma que funcionou é a acima.

## Scriptable Objects
É basicamente um container, que armazena vários dados/parâmetros.
Serve para manter os dados separados da lógica (métodos e cálculos), facilitando a manutenção.
Para criar:
    1) Botão direito dentro da aba de assets (como pra criar um MonoBehaviour) -> Scripting -> ScriptableObject Script
```csharp
// Definem o "atalho" para criar o ScriptableObject desse arquivo. Esse atalho fica no botão direito.
// No formato abaixo, vai estar na raiz do "Create" do botão direito, com nome de PowerupSO
// O arquivo criado vai ter o nome Powerup
[CreateAssetMenu(fileName = "Powerup", menuName = "PowerupSO")]
public class PowerupSO : ScriptableObject {

}
```

## Time.timeScale
Basicamente dita a velocidade em que o jogo tá rodando.
0 é 0%, 1 é 100%, 1.5f é 150%, e assim por diante.
O código abaixo basicamente para o tempo, pausa o jogo, enquanto 1.5f faria o jogo ir mais rápido.
```csharp
public class CharSelectManager: MonoBehaviour {
    void Start() {       
        Time.timeScale = 0;
    }
}
```

## Tilemaps, Ruletiles e Colisões de TileMap
Plataforma 2D:
    Tilemaps: https://www.udemy.com/course/unitycourse/learn/lecture/52179827
    Rule Tiles: https://www.udemy.com/course/unitycourse/learn/lecture/52259265
    Tilemap Collisions: https://www.udemy.com/course/unitycourse/learn/lecture/52199579
    Escadas(Ladders) (inclui Colisões, Tilemaps, etc): https://www.udemy.com/course/unitycourse/learn/lecture/52314919
    Mushrooms(pula-pula) (inclui colisões, tilemaps, etc): https://www.udemy.com/course/unitycourse/learn/lecture/52315241
Top-down:
    TileMap: https://www.udemy.com/course/unity-2d-rpg/learn/lecture/36838318
    Rule Tiles: https://www.udemy.com/course/unity-2d-rpg/learn/lecture/36838320
    Animated Tiles: https://www.udemy.com/course/unity-2d-rpg/learn/lecture/36838322
OBS:
    - Pra adicionar uma nova "imagem" no tilemap (tipo um mushroom, spike, etc), arrastamos o sprite pra dentro do Tile Palette, e pra isso precisaremos salvar esse novo "tile asset", em uma pasta que chamamos de Tiles.

## Slice/cortando sprites
https://www.udemy.com/course/unitycourse/learn/lecture/52179821

## Animações
Aula 1: https://www.udemy.com/course/unitycourse/learn/lecture/52199543
Aula 2: https://www.udemy.com/course/unitycourse/learn/lecture/52199561
- Animator Component: atribui animações a GameObjects através do Animator Controller;
- Animator Controller: grupamento de animações e transições (uma máquina de estados: idle, run, etc);
- Animation: a peça de movimento em si;
Essa aula aqui faz de uma forma mais sucinta: https://www.udemy.com/course/unitycourse/learn/lecture/52315333
- Random animations: usado para que diferentes instancias de um prefab (por exemplo) comecem a animação em frames diferentes, tipo quando tem várias tochas no cenário e todas animam exatamente igual (o que pode ficar "artificial"): https://www.udemy.com/course/unity-2d-rpg/learn/lecture/37032146
OBS:
    - Se clicar com o botão direito em um espaço em branco nos assets pra criar um animation clip, será preciso adicionar cada "frame da animação" manualmente depois, definindo tempo entre eles, etc. Um ponto mais fácil é clicar na setinha pra direita que tem no asset (que abre vários "frames" do asset), selecionar todos, clicar com botão direito e aí sim criar o Animation Clip. isso já cria tuudo de uma vez, facilitando muito a vida.

## Audio/som
https://www.udemy.com/course/unitycourse/learn/lecture/52618197

## Gerenciando Scenes
File -> Build Profiles -> Scene List

# ═══════════════ UNITY IDE ═══════════════════════════

## Colliders
Para adicionar: Clica no GameObject, e na direita selecione Add Component, adicionando o "* collider" que fizer sentido (Box Collider, Capsule Collider, etc). Na aula, adicionamos um Capsule Collider no carro, e as pontas arredondadas faziam ele "desviar" pro lado do Círculo. Se a gente mudar pra um Box, ele passa a "empurrar" o círculo. Porém, a caixa como não tem as bordas arredondadas, pode afzer o carro "colidir" em pontos que não deveria. Nesse caso, podemos editar o Collider e diminuir o tamanho por exemplo.
Parâmetros
    - Is Trigger: faz o objeto funcionar como um gatilho, mas a física ignora o collider.
OBS:
    1) Sempre tem dois, um normal (`Box Collider`) e um 2D (`Box Collider 2D`). O `Box Collider` (sem 2D) é 3D.
    2) Um `Collider2D` sem `Rigidbody2D` é tratado pela física como estático: participa de colisões, mas não é movido/empurrado pela simulação.
    3) Nem todo objeto precisa ter `Rigidbody2D`. Por exemplo, uma parede pode ter apenas um `BoxCollider2D`, enquanto o personagem possui `Rigidbody2D`.

## Rigidbody
Para adicionar: Clica no GameObject, e na direita selecione Add Component, adicionando o "Rigidbody 2D".
Tipos:
    - Um Collider2D sem Rigidbody é tratado pela física como um objeto estático: ele participa das colisões, mas não é movido/empurrado pela simulação. Ideal para: paredes, prédios, pedras que não se movem, limites do mapa, etc;
    - `Dynamic`: tem massa, responde à simulação física, forças e colisões. Pode ser empurrado por outros objetos. Ideal para: caixas, barris, bolas, etc;
    - `Static`: continua explicitamente sendo um Rigidbody, mas é configurado para não se mover pela simulação, funcionando como um Collider2D sem Rigidbody;
    - `Kinematic`: é movimentado explicitamente pelo jogo/código. Não é movimentado normalmente por forças da simulação. Seriam portas, plataformas, elevadores, obstáculos móveis, etc.
OBS:
1) Ao adicionar, o objeto vai "cair" na tela por causa da gravidade. Para impedir, sete `Gravity Scale` pra 0 no Rigidbody.

## Rigidbody.linearVelocity vs Transform
Quando um GameObject possui `Rigidbody2D` (`Dynamic`), o ideal é movimentá-lo através do próprio Rigidbody, em vez de alterar diretamente seu `Transform`.

* `transform.Translate(...)`: altera diretamente a posição do objeto, ignorando parcialmente o sistema de física. É como se estivéssemos "teleportando" o objeto.
* `rigidBody.linearVelocity`: define a velocidade do corpo e deixa a física calcular o movimento, gravidade e colisões.

No exemplo abaixo controlamos apenas o eixo X e preservamos o Y calculado pela física, permitindo que gravidade e pulo continuem funcionando normalmente.
```csharp
float x = moveSpeed * movementVector.x;
float y = myRigidbody.linearVelocity.y;

myRigidbody.linearVelocity = new Vector2(x, y);
```

## Collider vs Rigidbody
`Collider`: define a área/formato usado para colisões.
`Rigidbody`: coloca o objeto sob controle do sistema de física.

## Package Manager
Ele começa com o "In Project", o que faz com que vejamos apenas os packages instalados. Precisamos ir para Unity Registry para baixar novos.

## Cinemachine
Pacote nativo da Udemy (precisa ser adicionado manualmente) que traz várias ferramentas de câmera, como por exemplo a necessária para fazer a câmera seguir um objeto.
Para seguir:
    - Adicionamos um novo sprite -> `Cinemachine` -> `Cinemachine Camera`
    - No novo GameObject `Cinemachine Camera`, selecionamos o `Tracking Target` (quem ela segue)
    - Precisamos selecionar um `Position Control` para dizer como a câmera deve se comportar
OBS: 
    1) Ainda precisamos da `Main Camera`. Ao adicionar a Cinemachine Camera, a Main Camera ganha um novo "Component" de `Cinemachine Brain`, que dita qual é a câmera principal, entre outras coisas.
    2) As vezes pode acontecer de a Cinemachine Camera não "mostrar nada", ficar com fundo azul. Pode ser pq ela tá bem "em cima" do jogo, então vc pode mudar a visão da IDE pra 3D e ajustar o eixo Z, pra dar pra ver o jogo.
- Explicativo básico:
https://www.udemy.com/course/unitycourse/learn/lecture/52315007
- "Confinador" de Cinemachine (usado para impedir que ela mostre fora do mapa, ou seja, pra parar nas bordas):
https://www.udemy.com/course/unitycourse/learn/lecture/52315015

## State-Driven
Usada para fazer a câmera ficar dinâmica, aproximando quando animação idle, afastando ao correr, etc:
https://www.udemy.com/course/unitycourse/learn/lecture/52315165

## Sprite Shape
É basicamente uma "forma", que pode ser open (tipo uma linha) ou closed (tipo um quadrado).
Para adicionar: 
    - Botão direito no espaço em branco da Hierarchy -> 2D Object -> Sprite Shape
    - Podemos criar um profile pra não ter que mexer no Default (o que impactaria todos pra sempre) -> Botão direito em uma pasta de Assets -> Create -> 2D -> Sprite Shape Profile. Para definir a textura do Sprite Shape é só clicar nele e arrastar a imagem pro primeiro "Sprites", aí também podemos arrastar uma pra definir o "fill"
OBS:
    1) Aqui pra adicionar colisão é legal o Edge Collider 2D.

## Sprites dentro de Empty GameObject
Pode ser uma boa prática, por exemplo, ao criar o objeto principal (carro, player, etc), de usar `Create Empty`, e dentro desse "grupo" colocar o sprite. Isso permite, por exemplo, que a colisão e rigid body estejam no pai (`Empty GameObject`), daí o sprite (filho) fica independente, tendo mais controle.
ISso pode ser útil por exemplo pra mexer na aparência do personagem sem mexer necessariamente no GameObject. Podemos, por exemplo, querer virar o personagem pra esquerda. A física do pai (`Empty GameObject`) continuaria igual, mexeríamos apenas no sprite que é filho.
Outro exemplo seria: Imagine que seu collider é assim:
┌─────────┐
│         │
│ player  │
│         │
└─────────┘
Mas o sprite tem um desenho meio estranho e precisa ficar 10 pixels mais para cima para parecer centralizado. Com "pai" + "filho", ajustamos o desenho sem mudar de fato a posição do personagem.

## Surface Effector 2D
Aplica uma força tangente nas superfícies de `colliders`. É adicionado como um componente normal.

## Botões/Buttons
O método deve ser definido no inspector. Tem uma sessão OnClick, que é onde definimos.
Para definir, devemos linkar um GameObject, não um script.
Aí nas opções vamos em *NomeDaClasse* e escolhemos o método.

## Sorting Group
Sorting Group serve para fazer vários SpriteRenderers serem tratados como um único bloco de ordenação. Sem ele, a Unity pode ordenar cada sprite filho separadamente.
Imagina:
    Enemy
    ├── Body SpriteRenderer
    ├── Sword SpriteRenderer
    └── Hat SpriteRenderer
    Árvore
    └── SpriteRenderer
Sem Sorting Group, pode rolar algo visualmente absurdo:
- Hat atrás da árvore
- Body na frente da árvore
- Sword atrás
# ═══════════════ CÓDIGOS ═════════════════════════════

## OnCollisionEnter2D
Pode ser adicionado em um script linkado a um GameObject, e será triggado quando esse GameObject colidir com algo (esse algo precisa ter um component collider).
```csharp
private void OnCollisionEnter2D(Collision2D other) {
    Debug.Log("Colidi");
}
```

## OnTriggerEnter2D
Pode ser adicionado em um script linkado a um GameObject, e será triggado quando esse GameObject passar por algo (esse algo precisa ter um collider com o parâmetro Is Trigger marcado).
```csharp
private void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("Passei por algo");
}
```
OBS:
    1) Normalmente usamos `other` como nome do parâmetro pq estamos referenciando o "outro" objeto, o objeto em que "eu" bati. "Eu" nesse caso sou o GameObject onde esse script tá anexado, ou seja, o objeto cujo collider(por exemplo) participou do evento. Pode ser um carro, personagem, etc. E `other` é o "outro elemento do evento".

## Invoke
Basicamente é o SetTimeout do FiveM ou o DispatchQueue.asyncAfter. Invoca um método depois de X tempo.
```csharp
void ReloadScene() {
    SceneManager.LoadScene(0);
}
Invoke("ReloadScene", 1f);
```

# ═══════════════ PROGRAMAÇÃO C# ══════════════════════

## Variáveis
```csharp
int coins = 10;
float cash = 10.2f;
bool valid = false;
string text = "random text";
```

## Conversão
```csharp
int someInt;
float someFloat = 10.5f;
someInt = (int)someFloat;
someInt = Convert.ToInt32(teste);
```