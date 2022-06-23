using ApiTreino.Database;
using ApiTreino.Repositories.Implementation;
using ApiTreino.Repositories.Interfaces;
using ApiTreino.Services.Implementation;
using ApiTreino.Services.Interfaces;
using ApiTreino.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace ApiTreino
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>((DbContextOptionsBuilder options) => options
            .UseLazyLoadingProxies()
            .UseNpgsql
            ("Host=localhost;Port=5432;Database=apitreino;Username=postgres;Password=adm"));

            /* identity configuration */
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            // P: como o jwt vai decriptar/validar o token?
            // R: com nossa key
            var key = Encoding.ASCII.GetBytes(GeraKey.GetKey());

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

            services.AddScoped<ISvcPerson, SvcPerson>();
            services.AddScoped<IRepositoryPerson, RepositoryPerson>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICadastroService, CadastroService>();
            services.AddScoped<ISvcEmail, SvcEmail>();
            services.AddScoped<ISvcToken, SvcToken>();
            services.AddScoped<ISvcLogin, SvcLogin>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiTreino", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTreino v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //valida quem é vc
            app.UseAuthorization(); // valida oq vc pode fazer

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
