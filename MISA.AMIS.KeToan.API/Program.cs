using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Swagger hiển thị Api 1 cách thẩm mĩ hơn
builder.Services.AddSwaggerGen();

//Dependency injection
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IDepartmentDL, DepartmentDL>();
builder.Services.AddScoped<IDepartmentBL, DepartmentBL>();
builder.Services.AddScoped<IProviderGroupDL, ProviderGroupDL>();
builder.Services.AddScoped<IProviderGroupBL, ProviderGroupBL>();
builder.Services.AddScoped<IBankAccountProviderDL, BankAccountProviderDL>();
builder.Services.AddScoped<IBankAccountProviderBL, BankAccountProviderBL>();
builder.Services.AddScoped<IAnotherAddressOfProviderDL, AnotherAddressOfProviderDL>();
builder.Services.AddScoped<IAnotherAddressOfProviderBL, AnotherAddressOfProviderBL>();
builder.Services.AddScoped<IAccountPayableDL, AccountPayableDL>();
builder.Services.AddScoped<IAccountPayableBL, AccountPayableBL>();
builder.Services.AddScoped<IAccountReceivableDL, AccountReceivableDL>();
builder.Services.AddScoped<IAccountReceivableBL, AccountReceivableBL>();
builder.Services.AddScoped<IProviderDL, ProviderDL>();
builder.Services.AddScoped<IProviderBL, ProviderBL>();
builder.Services.AddScoped<IProvider_ProviderGroupDL, Provider_ProviderGroupDL>();
builder.Services.AddScoped<IProvider_ProviderGroupBL, Provider_ProviderGroupBL>();
builder.Services.AddScoped<IReceiptDL, ReceiptDL>();
builder.Services.AddScoped<IReceiptBL, ReceiptBL>();
builder.Services.AddScoped<IPaymentTermBL, PaymentTermBL>();
builder.Services.AddScoped<IPaymentTermDL, PaymentTermDL>();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

//Tắt automatic validate 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Cors
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//Lấy ConnectionString từ file appsettings.Development.json
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");
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
//Mỗi lần chạy project thì file program.cs sẽ chạy đầu tiên 
