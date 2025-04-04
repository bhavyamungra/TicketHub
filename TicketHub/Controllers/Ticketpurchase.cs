using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues;
using System.Text.Json;
using TicketHub;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ILogger<TicketsController> _logger;
        private readonly IConfiguration _configuration;

        // Constructor to inject configuration and logger.
        public TicketsController(IConfiguration configuration, ILogger<TicketsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private readonly string _queueName = "tickets";

        // POST API method to handle ticket purchase.
        [HttpPost]
        public async Task<IActionResult> PurchaseTicket([FromBody] TicketData ticketPurchase)
        {
            if (ticketPurchase == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return if data is invalid
            }

            string? connectionString = _configuration["AzureStorageConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                return BadRequest("An error was encountered. Connection string is missing.");
            }

            try
            {
                var queueClient = new QueueClient(connectionString, _queueName);

                // Serialize TicketData to JSON format
                string message = JsonSerializer.Serialize(ticketPurchase);

                // Send message to Azure Queue Storage
                await queueClient.SendMessageAsync(message);

                return Ok("Ticket purchase processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the ticket purchase.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
