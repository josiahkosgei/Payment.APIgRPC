using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using PaymentService;

namespace Payment.APIgRPC.Services
{
    public class PaymentgRPCService : PaymentService.PaymentService.PaymentServiceBase
    {
        private readonly ILogger<PaymentgRPCService> _logger;
        public PaymentgRPCService(ILogger<PaymentgRPCService> logger)
        {
            _logger = logger;
        }

        public override Task<MakePaymentReply> MakePayment(MakePaymentRequest request, ServerCallContext context)
        {
            var transactionId = Guid.NewGuid().ToString();

            _logger.LogInformation("---------------------------------------");
            _logger.LogInformation($"Make payment {transactionId}");
            _logger.LogInformation("---------------------------------------");

            return Task.FromResult(new MakePaymentReply
            {
                TrackingId = transactionId
            });
        }
        // Mock Get Payment Details
        public override Task<GetPaymentDetailsReply> GetPaymentDetails(GetPaymentDetailsRequest request, ServerCallContext context)
        {
            var trackingId = Guid.NewGuid().ToString();
            _logger.LogInformation("---------------------------------------");
            _logger.LogInformation($"Get Payment Details {trackingId}");
            _logger.LogInformation("---------------------------------------");

            return Task.FromResult(new GetPaymentDetailsReply
            {
                TrackingId = trackingId,
                PaymentMethod="VISA",
                Reference= Guid.NewGuid().ToString(),
                PaymentStatus="COMPLETED"
                
            });
        }
    }
}
