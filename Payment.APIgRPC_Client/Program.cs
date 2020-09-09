using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using PaymentService;
using Grpc.Core;

namespace Payment.APIgRPC_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var paymentClient = new PaymentService.PaymentService.PaymentServiceClient(channel);

            Console.WriteLine("Welcome to the gRPC client");

            var reply = await paymentClient.MakePaymentAsync(new MakePaymentRequest
            {
               Amount="2.00",
               TrackingId = Guid.NewGuid().ToString(),
               BuyerName = "John Doe",
               Email = "f.doe@example.com",
               PaymentMethod = "VISA",
               PaymentStatus = "COMPLETED",
               PhoneNumber = "422 5451 332",
               Reference = Guid.NewGuid().ToString(),
            });

            Console.WriteLine($"Made payment: {reply.TrackingId}");

            using var statusReplies = paymentClient.GetPaymentDetails(new GetPaymentDetailsRequest{ TrackingId = reply.TrackingId, Reference=reply.Reference });
            
            while (await statusReplies.ResponseStream.MoveNext())
            {
                var statusReply = statusReplies.ResponseStream.Current.PaymentMethod;
                Console.WriteLine($"Payment status: {statusReply}");
            }
            Console.ReadLine();
        }
    }
}
