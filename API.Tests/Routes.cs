namespace API.Tests
{
    public static class Routes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Entities
        {
            public const string GetAll = Root + "/Entities";

            public const string Update = Base + "/Entities/{Id}";

            public const string Delete = Base + "/Entities/{Id}";

            public const string Get = Base + "/Entities/{Id}";

            public const string Create = Base + "/Entities";
        }
    }
}
