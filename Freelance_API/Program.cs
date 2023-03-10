using Authorisation.Configuration;
using Base.ManagementService;
using Freelance_ApplicationService.Implementations.Bookmarks;
using Freelance_ApplicationService.Implementations.JobRelated;
using Freelance_ApplicationService.Implementations.Others;
using Freelance_ApplicationService.Implementations.TaskRelated;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Utils.Global;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.Configure<JwtConfig>(s => s.Secret = GlobalVariables.JWT_Encription_Key);

var key = Encoding.ASCII.GetBytes(GlobalVariables.JWT_Encription_Key);

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    RequireExpirationTime = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidationParams);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParams;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Freelance_API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
        In = ParameterLocation.Header,
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.OperationFilter<AuthResponsesOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped(typeof(IBaseManagementService), typeof(BookmarkedJobsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(BookmarkedTasksManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(BookmarkedUsersManagementService));

builder.Services.AddScoped(typeof(IBaseManagementService), typeof(FilesToJobsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(JobApplicationsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(JobsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(JobTypesManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(SkillsToJobsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(TagsToJobsManagementService));

builder.Services.AddScoped(typeof(IBaseManagementService), typeof(CategoriesManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(FreelanceFilesManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(NotesManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(ReviewsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(SkillsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(SkillsToCategoriesManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(TagsManagementService));

builder.Services.AddScoped(typeof(IBaseManagementService), typeof(FilesToTasksManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(SkillsToTasksManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(TaskBidsManagementService));
builder.Services.AddScoped(typeof(IBaseManagementService), typeof(TasksManagementService));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCookiePolicy();

app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("Open");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

internal class AuthResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                            .Union(context.MethodInfo.GetCustomAttributes(true));

        if (attributes.OfType<IAllowAnonymous>().Any())
        {
            return;
        }

        var authAttributes = attributes.OfType<IAuthorizeData>();

        if (authAttributes.Any())
        {
            operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };

            if (authAttributes.Any(att => !String.IsNullOrWhiteSpace(att.Roles) || !String.IsNullOrWhiteSpace(att.Policy)))
            {
                operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };
            }

            operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "BearerAuth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                };
        }
    }
}
