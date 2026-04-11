# Documentação do OranRouteDotNet

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