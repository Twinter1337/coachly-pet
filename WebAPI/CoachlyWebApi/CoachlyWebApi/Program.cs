using System.Text;
using System.Text.Json.Serialization;
using CoachlyBackEnd.Config;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.Enums;
using CoachlyBackEnd.Models.Mapping;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using CoachlyBackEnd.Services.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Stripe;
using File = System.IO.File;
using PaymentMethod = CoachlyBackEnd.Models.Enums.PaymentMethod;
using Review = CoachlyBackEnd.Models.Review;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));

//Enums mapping
dataSourceBuilder.MapEnum<DocumentType>("document_type");
dataSourceBuilder.MapEnum<PaymentMethod>("payment_method");
dataSourceBuilder.MapEnum<PaymentStatus>("payment_status");
dataSourceBuilder.MapEnum<SessionStatus>("session_status");
dataSourceBuilder.MapEnum<SessionType>("session_type");
dataSourceBuilder.MapEnum<UserRole>("user_role");
dataSourceBuilder.MapEnum<CurrencyType>("currency_type");
dataSourceBuilder.EnableUnmappedTypes();
dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<CoachlyDbContext>(options =>
    options.UseLazyLoadingProxies()
        .UseNpgsql(dataSource, o =>
        {
            o.MapEnum<DocumentType>("document_type");
            o.MapEnum<PaymentMethod>("payment_method");
            o.MapEnum<PaymentStatus>("payment_status");
            o.MapEnum<SessionStatus>("session_status");
            o.MapEnum<SessionType>("session_type");
            o.MapEnum<UserRole>("user_role");
            o.MapEnum<CurrencyType>("currency_type");
        })
);

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is not configured");
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

//other services
builder.Services.AddScoped<JwtAuthService>();
builder.Services.AddScoped<PasswordHashService>();
builder.Services.AddScoped<OtpService>();

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe"));

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<StripeSettings>>().Value);

builder.Services.AddScoped<StripeService>();
//Crud Services
builder.Services.AddScoped<ICrudService<Location>, CrudService<Location>>();
builder.Services.AddScoped<ICrudService<Payment>, CrudService<Payment>>();
builder.Services.AddScoped<ICrudService<Review>, CrudService<Review>>();
builder.Services.AddScoped<ICrudService<Session>, CrudService<Session>>();
builder.Services.AddScoped<ICrudService<SessionParticipant>, CrudService<SessionParticipant>>();
builder.Services.AddScoped<ICrudService<Specialization>, CrudService<Specialization>>();
builder.Services.AddScoped<ICrudService<Subscribtion>, CrudService<Subscribtion>>();
builder.Services.AddScoped<ICrudService<TrainerAvailability>, CrudService<TrainerAvailability>>();
builder.Services.AddScoped<ICrudService<Trainer>, CrudService<Trainer>>();
builder.Services.AddScoped<ICrudService<TrainerDocument>, CrudService<TrainerDocument>>();
builder.Services.AddScoped<ICrudService<TrainerSpecialization>, CrudService<TrainerSpecialization>>();
builder.Services.AddScoped<ICrudService<User>, CrudService<User>>();
builder.Services.AddScoped<ICrudService<UserSubscribtion>, CrudService<UserSubscribtion>>();

//Swagger 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Coachly API",
        Version = "v1",
        Description = "API for Coachly Web Application",
        Contact = new OpenApiContact
        {
            Name = "Maxim Pantelii",
            Email = "pantelejmax@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coachly API v1");
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CoachlyDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();