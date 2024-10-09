using JwtIdentity.API.Services.UserServices;
using JwtToken.Models;
using JwtToken.ServicesUserServices;
using JwtToken.Settigs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.Configure<JWTSettings>(builder.Configuration.GetRequiredSection(nameof(JWTSettings)));
//Burasý jwt ayarýný appsetting den jwtsettings sýnýfýna baðlar
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
//Burasý kimlik doðrulama þemasýný etkinleþtirir ve jwt doðrulama parametresini etkinleþtirir
{
    var _jwt = builder.Configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();//Yeni jwt nesnemizi oluþturduk
    o.TokenValidationParameters = new TokenValidationParameters  //Bu kýsýmda token doðrulama parametrelerini ayarlar
    {

        ValidateIssuerSigningKey = true, //imzayý doðrulasýn mý
        ValidateIssuer = true, //Token yayýmlayýcýsý doðrulansýn mý 
        ValidateAudience = true, //hedefimiz doðrulansýn mý 
        ValidateLifetime = true, //süre doðru mu
        ClockSkew = TimeSpan.Zero, //gecikme süresi
        ValidIssuer = _jwt.Issuer, //Geçerli token yayýmlayýcýsý
        ValidAudience = _jwt.Audience, //hedef
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)) //Token imza anahtarý
    };
});
builder.Services.AddScoped<IUserService, UserService>();  //interface ve onu implemente ettiðimiz yeri geçtik 

builder.Services.AddDbContext<DbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")); //sql baðlantýmýzý yazdýk
});
builder.Services.AddIdentity<AppUserClass, IdentityRole>().AddEntityFrameworkStores<DbContext>();

builder.Services.AddControllers();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();