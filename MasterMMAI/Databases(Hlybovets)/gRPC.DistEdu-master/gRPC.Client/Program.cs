// See https://aka.ms/new-console-template for more information

// The port number must match the port of the gRPC server.

using gRPC.Client;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7152");
var client = new GreetService.GreetServiceClient(channel);

string input;
do
{
    Console.WriteLine("available commands: 'actualize', 'update'");
    input = Console.ReadLine()!;

    if (input.Equals("actualize", StringComparison.InvariantCultureIgnoreCase))
    {
        var product = new Product();
        Console.WriteLine("enter payload");

        Console.Write("enter id: ");
        product.ProductId = Console.ReadLine()!;
        
        Console.Write("enter name: ");
        product.ProductName = Console.ReadLine()!;

        var reply = await client.ActualizeDataAsync(product);

        Console.WriteLine(reply is null
            ? "empty response"
            : $"process state: {reply.IsProcessed}; product name: {reply.ProductName}");
        continue;
    }

    if (input.Equals("update", StringComparison.InvariantCultureIgnoreCase))
    {
        var request = new ProductRequest
        {
            Product = new Product()
        };

        Console.Write("enter command ('update' or 'delete'): ");
        request.Command = Console.ReadLine()!;

        if (request.Command is "update")
        {
            Console.Write("enter new product name: ");
            request.Product.ProductName = Console.ReadLine()!;
        }

        Console.Write("enter product id: ");
        request.Product.ProductId = Console.ReadLine()!;

        Console.Write("push to kafka or process? : ");
        var resp = Console.ReadLine();
       
        request.PushToKafka = !string.IsNullOrEmpty(resp);
        var res = await client.ProcessRequestAsync(request);
        
        Console.WriteLine(res.Product is null
            ? $"empty response. Process state: {res.IsProcessed}"
            : $"product : time: {res.Product.AddTime}; product name: {res.Product.ProductName}; product id : {res.Product.ProductId}");
    }
}
while(!input.Equals("q", StringComparison.InvariantCultureIgnoreCase));