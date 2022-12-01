
using Api.Configuration;
using Common;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCommon();

        services.AddDbContext(Configuration);

        services.AddControllers();

        services.AddSwagger();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.IntializeDatabase();

        app.UseHsts(env);

        app.UseHttpsRedirection();

        app.UseRouting();

        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseStaticFiles();

        app.UseSwaggerAndUI();

        app.UseEndpoints(config =>
        {
            config.MapControllers();
        });
    }
}

