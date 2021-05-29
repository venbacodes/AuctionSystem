using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppSettingsModels
{
    public class EmailSettings
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
