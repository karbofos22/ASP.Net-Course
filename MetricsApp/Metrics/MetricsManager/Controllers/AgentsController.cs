using MetricsManager.Agents;
using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _agentsRepository;

        public AgentsController(IAgentsRepository agentsRepository,
            ILogger<AgentsController> logger)
        {
            _agentsRepository = agentsRepository;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            if (agentInfo != null)
            {
                _agentsRepository.Create(agentInfo);
                _logger.LogInformation("Created agent entry");
            }
            return Ok();
        }

        //[HttpPut("enable/{agentId}")]
        //public IActionResult EnableAgentById([FromRoute] int agentId)
        //{
        //    _agentsRepository.
        //    if (_agentsRepository.Values.ContainsKey(agentId))
        //        _agentsRepository.Values[agentId].Enable = true;
        //    return Ok();
        //}

        //[HttpPut("disable/{agentId}")]
        //public IActionResult DisableAgentById([FromRoute] int agentId)
        //{
        //    if (_agentPool.Values.ContainsKey(agentId))
        //        _agentPool.Values[agentId].Enable = false;
        //    return Ok();
        //}

        //// TODO: Домашнее задание [Пункт 1]
        //// Добавьте метод в контроллер агентов проекта, относящегося к менеджеру метрик, который
        //// позволяет получить список зарегистрированных в системе объектов. - уже готово ↓


        [HttpGet("getAll")]
        public ActionResult GetallAgents()
        {
            _logger.LogInformation("Get agents base");
            return Ok(_agentsRepository.GetAll());
        } 
           

        [HttpGet("getById")]
        public ActionResult GetAgentById(int id)
        {
            _logger.LogInformation("Get single agent info");
            return Ok(_agentsRepository.GetById(id));
        } 
           



    }
}
