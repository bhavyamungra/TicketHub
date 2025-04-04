using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues;
using System.Text.Json;
using System.Net.Sockets;

namespace TicketHub.Controllers
    {
        // The [ApiController] attribute makes this class an API controller.
        // The [Route] attribute specifies the route for the API endpoint. In this case, it's "/api/purchase-ticket".
        [ApiController]
        [Route("api/purchase-ticket")]
        public class TicketPurchase : ControllerBase
        {
            // The IConfiguration object is used to read application settings, like the Azure Storage connection string.
            private readonly IConfiguration _configuration;

            // Constructor: The IConfiguration object is injected into the controller to access app settings.
            public TicketPurchase(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            private readonly string _queueName = "ticket-queue";
            // The [HttpPost] attribute specifies that this method handles POST requests.
            // It receives a TicketData object from the request body.
            [HttpPost]
            public async Task<IActionResult> ProcessTicketPurchase([FromBody] TicketData ticketData)
            {
                // If the ticketData object is null, return a 400 Bad Request response.
                if (ticketData == null)
                    return BadRequest("Purchase details are missing or invalid.");

                // If the data doesn't meet the validation requirements defined in TicketData, return a 400 Bad Request.
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Retrieve the Azure Storage connection string from the application's configuration.
                string? connectionString = _configuration["AzureStorageConnectionString"];

                // If the connection string is empty or not found, return a 500 Internal Server Error.
                if (string.IsNullOrEmpty(connectionString))
                    return StatusCode(500, "Internal error: Azure storage connection string is not configured.");

                // Create a QueueClient that interacts with the Azure Storage Queue.
                // "ticket-queue" is the name of the Azure Queue used to store the ticket purchase messages.
                var queueClient = new QueueClient(connectionString, _queueName);

                // Ensure the queue exists. If not, it will be created.
                await queueClient.CreateIfNotExistsAsync();

                // Serialize the ticket purchase data into a JSON string to send it to the queue.
                var purchaseMessage = JsonSerializer.Serialize(ticketData);

                // Send the serialized data as a message to the Azure Queue.
                await queueClient.SendMessageAsync(purchaseMessage);

                // If everything goes well, return a 200 OK response with a success message.
                return Ok("Ticket purchase successfully queued.");

            }
        }
    }
