using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;

namespace LevelPupper__Parser.dlls.API
{
    internal class API_Pupser_Configuration
    {
        public string? level { get; }

        public string? login { get; }
        public string? password { get; }
        public string? url_Login { get; }

        public string? url_Service { get; }
        public string? url_GameService { get; }

        public string? url_ElementOfDescription { get; }
        public string? url_RangeGradation { get; }
        public string? url_ValueOption { get; }

        public API_Pupser_Configuration(string? level, string? login, string? password, string? url_Login, string? url_Service, string? url_GameService, string? url_ElementOfDescription, string? url_RangeGradation, string? url_ValueOption)
        {
            this.level = level;

            this.login = login;
            this.password = password;
            this.url_Login = url_Login;

            this.url_Service = url_Service;
            this.url_GameService = url_GameService;

            this.url_ElementOfDescription = url_ElementOfDescription;
            this.url_RangeGradation = url_RangeGradation;
            this.url_ValueOption = url_ValueOption;
        }
    }
}
