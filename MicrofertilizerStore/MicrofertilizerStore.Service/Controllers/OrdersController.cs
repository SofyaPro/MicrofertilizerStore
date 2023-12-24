using AutoMapper;
using MicrofertilizerStore.BL.Orders;
using MicrofertilizerStore.BL.Orders.Entities;
using MicrofertilizerStore.Service.Controllers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace MicrofertilizerStore.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;
        private readonly IOrdersManager _ordersManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrdersController(IOrdersProvider ordersProvider, IOrdersManager ordersManager, IMapper mapper, ILogger logger)
        {
            _ordersManager = ordersManager;
            _ordersProvider = ordersProvider;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet] 
        public IActionResult GetAllOrders()
        {
            var orders = _ordersProvider.GetOrders();
            return Ok(new OrdersListResponse()
            {
                Orders = orders.ToList()
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("filter")] 
        public IActionResult GetFilteredOrders([FromQuery] OrdersFilter filter)
        {
            var orders = _ordersProvider.GetOrders(_mapper.Map<OrdersModelFilter>(filter));
            return Ok(new OrdersListResponse()
            {
                Orders = orders.ToList()
            });
        }

        [HttpGet]
        [Route("{id}")] 
        public IActionResult GetOrderInfo([FromRoute] Guid id)
        {
            try
            {
                var order = _ordersProvider.GetOrderInfo(id);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString()); //stack trace + message
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request) //automatic validation
        {
            try
            {
                var order = _ordersManager.CreateOrder(_mapper.Map<CreateOrderModel>(request));
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateOrderInfo([FromRoute] Guid id, UpdateOrderRequest request)
        {
            //validator for request
            try
            {
                var order = _ordersManager.UpdateOrder(id, _mapper.Map<CreateOrderModel>(request));
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteOrder([FromRoute] Guid id)
        {
            try
            {
                _ordersManager.DeleteOrder(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
