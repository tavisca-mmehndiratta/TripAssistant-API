using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripAssistantSearchEngineApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.Contracts;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TripAssistantSearchEngineApi
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc();
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute
            //    {
            //        Permanent = true
            //    });
            //});
            services.AddTransient<ITripResultsService, TripResultsService>();
            services.AddTransient<IActivityTranslator, ActivityTranslator>();
            services.AddTransient<IHotelTranslator, HotelTranslator>();
            services.AddTransient<IContextAnalyzerService, ContextAnalyzerService>();
            services.AddTransient<IContextCheckerService, ContextCheckerService>();
            services.AddTransient<IContextGenerator, ContextGenerator>();
            services.AddTransient<IGeoCodeGenerator, GeoCodeGenerator>();
            services.AddTransient<ISingleAttractionProvider, SingleAttractionProvider>();
            services.AddTransient<ISingleAttractionTranslator, SingleAttractionTranslator>();
            services.AddTransient<ISingleActivityProvider, SingleActivityProvider>();
            services.AddTransient<ISingleActivityTranslator, SingleActivityTranslator>();
            services.AddTransient<IGeoCode, GeoCode>();
            services.AddTransient<IUserDetailsProvider, UserDetailsProvider>();
            //services.Add
            services.AddTransient<IActivityApi, ActivityApi>();
            services.AddTransient<IUserPreferenceService, UserPreferencesService>();
            services.AddTransient<IHotelApi, HotelsApi>();
            services.AddTransient<ICoreResponseGenerator, CoreResponseGenerator>();
            services.AddTransient<IActivityCache, ActivityCache>();
            services.AddTransient<ITrainingDataLogger, TrainingDataLogger>();
            services.AddTransient<IHotelCache, HotelCache>();
            services.AddTransient<ICoreResponseGenerator, CoreResponseGenerator>();
            services.AddAutoMapper(x => x.AddProfile(new AutoMapperInitializer()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

        }
    }
}
