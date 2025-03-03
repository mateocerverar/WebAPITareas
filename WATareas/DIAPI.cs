namespace WATareas
{
    public static class DIAPI
    {
        public static void AddDIAPI(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        
    }
}
