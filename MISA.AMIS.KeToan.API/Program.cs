var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Swagger là một cái tổ chức cho phép hiển thị Api 1 cách thẩm mĩ hơn
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline. Kiểm tra xem biến môi trường có phải là IsDevelopment không
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
//Mỗi lần chạy project thì file program.cs sẽ chạy đầu tiên để cấu hình lên cái ứng dụng web của mình đã
