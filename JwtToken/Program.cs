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
//Buras� jwt ayar�n� appsetting den jwtsettings s�n�f�na ba�lar
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
//Buras� kimlik do�rulama �emas�n� etkinle�tirir ve jwt do�rulama parametresini etkinle�tirir
{
    var _jwt = builder.Configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();//Yeni jwt nesnemizi olu�turduk
    o.TokenValidationParameters = new TokenValidationParameters  //Bu k�s�mda token do�rulama parametrelerini ayarlar
    {

        ValidateIssuerSigningKey = true, //imzay� do�rulas�n m�
        ValidateIssuer = true, //Token yay�mlay�c�s� do�rulans�n m� 
        ValidateAudience = true, //hedefimiz do�rulans�n m� 
        ValidateLifetime = true, //s�re do�ru mu
        ClockSkew = TimeSpan.Zero, //gecikme s�resi
        ValidIssuer = _jwt.Issuer, //Ge�erli token yay�mlay�c�s�
        ValidAudience = _jwt.Audience, //hedef
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)) //Token imza anahtar�
    };
});
builder.Services.AddScoped<IUserService, UserService>();  //interface ve onu implemente etti�imiz yeri ge�tik 

builder.Services.AddDbContext<DbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")); //sql ba�lant�m�z� yazd�k
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