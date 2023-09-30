using Grpc.Core;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task GetCustomerInfo(HelloRequest request, IServerStreamWriter<CustomerData> responseStream, ServerCallContext context)
        {
            var customer = new List<CustomerData> {

                new CustomerData
                {
                    Name = "Maneesh",
                    IsActiveCustomer = true,
                    Email = "maneesh@gmail.com",
                    ItemId = 1,
                },
                new CustomerData
                {
                    Name = "Rahul",
                    IsActiveCustomer = false,
                    Email = "rahul@gmail.com",
                    ItemId = 11,
                },
                new CustomerData
                {
                    Name = "Raj",
                    IsActiveCustomer = true,
                    Email = "maneesh@gmail.com",
                    ItemId = 2,
                },
                new CustomerData
                {
                    Name = "Maneesh",
                    IsActiveCustomer = false,
                    Email = "raj@gmail.com",
                    ItemId = 3,
                },
                new CustomerData
                {
                    Name = "shyam",
                    IsActiveCustomer = true,
                    Email = "shyam@gmail.com",
                    ItemId = 4,
                }
            };

            foreach (var item in customer)
            {
                await Task.Delay(500);
                await responseStream.WriteAsync(item);
            }
        }

    }
}

