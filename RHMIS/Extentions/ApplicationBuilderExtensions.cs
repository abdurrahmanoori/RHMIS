namespace RHMIS.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                // Uncomment if you have custom exception handling
                // app.UseMiddleware<ExceptionMiddleware>();
            }

            return app;
        }
    }
}
