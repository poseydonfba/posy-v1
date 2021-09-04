using System;

namespace POSY.Infra.CrossCutting.Common
{
    public class ConfigurationBase
    {
        public static DateTime DataAtual { get; private set; } = DateTime.UtcNow;
    }
}
