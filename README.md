# Documentação do OranRouteDotNet

## Introdução
O OranRoute é um projeto voltado para a implementação de soluções inovadoras em roteamento inteligente. Utilizando tecnologias de ponta, o OranRoute visa otimizar a eficiência em sistemas de navegação e planejamento de rotas.

## Arquitetura
A arquitetura do OranRoute é baseada em um modelo modular, onde cada componente foi projetado para ser independente e interagir através de APIs. Isso facilita a manutenção e a escalabilidade do projeto. Os principais componentes incluem:
- **Módulo de Roteamento**: Responsável por calcular as melhores rotas com base em diferentes parâmetros.
- **Módulo de Interface do Usuário**: Uma interface intuitiva que permite ao usuário interagir facilmente com o sistema.
- **Módulo de Banco de Dados**: Armazena informações sobre rotas, usuários e preferências de navegação.

## Recursos
O OranRoute oferece uma variedade de recursos, incluindo:
- Cálculo de rotas em tempo real.
- Preferências personalizáveis do usuário para otimização de rotas.
- Integração com serviços de localização.
- Relatórios de desempenho e análise de rotas.

## Endpoints de Verificação de Saúde

Os endpoints de verificação de saúde permitem monitorar a integridade dos serviços:

- `GET /health`: Verifica o estado geral do serviço.
- `GET /health/live`: Verifica se o serviço está ativo.
- `GET /health/ready`: Verifica se o serviço está pronto para receber tráfego.

## Instruções de Teste

Para testar o OranRouteDotNet, siga as seguintes instruções:

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/gaaaabz/OranRouteDotNet.git
   cd OranRouteDotNet
   ```

2. **Instale as dependências:**
   ```bash
   dotnet restore
   ```

3. **Execute os testes: **
   ```bash
   dotnet test
   ```
   

## Capacidades de Monitoramento

O OranRouteDotNet inclui funcionalidades para monitoramento contínuo:
- Integração com ferramentas de monitoramento como Prometheus e Grafana.
- Execução de verificações de saúde em intervalos regulares para garantir a disponibilidade do serviço.
- Alerts configuráveis para eventos de falhas e desempenho.

## Membros 

- Julia Damasceno Busso - RM560293 - 2TDSPA
- Gabriel Gomes Cardoso - Rm559597 - 2TDSPA
- Jhonatan Quispe Torrez - rm560601 - 2TDSPA
