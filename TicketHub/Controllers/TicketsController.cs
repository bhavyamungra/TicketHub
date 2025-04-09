using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues;
using System.Text.Json;
using TicketHub.Model;


namespace TicketHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ILogger<TicketsController> logger;
        private readonly IConfiguration _configuration;

        public TicketsController(IConfiguration configuration, ILogger<TicketsController> logger)
        {
            _configuration = configuration;
            this.logger = logger;
        }

        private readonly string _queueName = "ticket-queue";

        [HttpPost]
        public async Task<IActionResult> PurchaseTicket([FromBody] TicketPurchase ticketPurchase)
        {
            if (ticketPurchase == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? connectionString = _configuration["AzureStorageConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                return BadRequest("An error was encountered. Connection string is missing.");
            }

            try
            {
                var queueClient = new QueueClient(connectionString, _queueName);

                string message = JsonSerializer.Serialize(ticketPurchase);

                await queueClient.SendMessageAsync(message);

                return Ok("Ticket purchase processed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing the ticket purchase.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}