using MetricsManager.Models.Requests;
using Newtonsoft.Json;
using MetricsManager.Agents;

namespace MetricsManager.Services.Client.Implementations
{
    public class MetricsAgentClient : IMetricsAgentClient
    {

        #region Services

        private IAgentsRepository _agentsRepository;
        private readonly HttpClient _httpClient;

        #endregion

        public MetricsAgentClient(HttpClient httpClient,
            IAgentsRepository agentsRepository)
        {
            _httpClient = httpClient;
            _agentsRepository = agentsRepository;
        }


        public CpuMetricsResponse GetCpuMetrics(CpuMetricRequest request)
        {
            AgentInfo agentInfo = _agentsRepository.GetAll().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/CPU/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new (HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse =
                    (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }

            return null;
        }

        public DotnetMetricsResponse GetDotnetMetrics(DotnetMetricRequest request)
        {
            AgentInfo agentInfo = _agentsRepository.GetAll().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/Dotnet/errors-count/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new (HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                DotnetMetricsResponse dotnetMetricsResponse =
                    (DotnetMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(DotnetMetricsResponse));
                dotnetMetricsResponse.AgentId = request.AgentId;
                return dotnetMetricsResponse;
            }

            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricRequest request)
        {
            AgentInfo agentInfo = _agentsRepository.GetAll().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/Network/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new (HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                NetworkMetricsResponse networkMetricsResponse =
                    (NetworkMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(NetworkMetricsResponse));
                networkMetricsResponse.AgentId = request.AgentId;
                return networkMetricsResponse;
            }

            return null;
        }

        public HddMetricsResponse GetHddMetrics(HddMetricRequest request)
        {
            AgentInfo agentInfo = _agentsRepository.GetAll().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/HDD/Left/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                HddMetricsResponse hddMetricsResponse =
                    (HddMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(HddMetricsResponse));
                hddMetricsResponse.AgentId = request.AgentId;
                return hddMetricsResponse;
            }

            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricRequest request)
        {
            AgentInfo agentInfo = _agentsRepository.GetAll().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/Ram/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                RamMetricsResponse ramMetricsResponse =
                    (RamMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RamMetricsResponse));
                ramMetricsResponse.AgentId = request.AgentId;
                return ramMetricsResponse;
            }

            return null;
        }
    }
}
