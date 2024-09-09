var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/products-composer/{id}", async (int id) =>
{
    var httpclient = new HttpClient();

    var productFromAAsTask = httpclient.GetFromJsonAsync<ProductFromAResponse>($"https://localhost:7292/products/{id}");
    var productFromBAsTask = httpclient.GetFromJsonAsync<ProductFromBResponse>($"https://localhost:7080/stocks/{id}");


    await Task.WhenAll(productFromAAsTask, productFromBAsTask);

    var productFromA = productFromAAsTask.Result;
    var productFromB = productFromBAsTask.Result;


    if (productFromA is null || productFromB is null)
    {
        return Results.NotFound();
    }


    var product = new Product(productFromA.Id, productFromA.Name, productFromA.Price, productFromB.Stock,
        productFromB.Size);

    return Results.Ok(product);
});


app.Run();

public record Product(int Id, string Name, decimal Price, int Stock, string Size);

public record ProductFromAResponse(int Id, string Name, decimal Price);

public record ProductFromBResponse(int Id, int Stock, string Size);