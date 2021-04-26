﻿namespace API.Tests
{
    public static class Routes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Entities
        {
            public static string Create() => Root + "/Entities";
            public static string Get() => Root + "/Entities";
            public static string Get(int Id) => Root + $"/Entities/{Id}";
            public static string Update(int Id) => Root + $"/Entities/{Id}";
            public static string Delete(int Id) => Root + $"/Entities/{Id}";
        }
    }
}
