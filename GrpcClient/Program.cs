using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;

string address = "http://localhost:5002";

using var channel = GrpcChannel.ForAddress(address);
var client = new Greeter.GreeterClient(channel);


var request = await client.SayHelloAsync(new HelloRequest { Name = "Maneesh" });
using var requestStream = client.GetCustomerInfo(new HelloRequest { Name = "Maneesh" });

Console.WriteLine(request.Message);


while (await requestStream.ResponseStream.MoveNext())
{
    var current = requestStream.ResponseStream.Current;

    Console.WriteLine($"{current.Name} {current.Email} {current.IsActiveCustomer}");
}

Console.ReadLine();
