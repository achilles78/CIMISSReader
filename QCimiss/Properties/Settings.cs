namespace QCimiss.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        [DefaultSettingValue(""), DebuggerNonUserCode, UserScopedSetting]
        public string APIpassword
        {
            get => 
                ((string) this["APIpassword"]);
            set
            {
                this["APIpassword"] = value;
            }
        }

        [DefaultSettingValue(""), DebuggerNonUserCode, UserScopedSetting]
        public string APIuser
        {
            get => 
                ((string) this["APIuser"]);
            set
            {
                this["APIuser"] = value;
            }
        }

        public static Settings Default =>
            defaultInstance;

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string lastuser
        {
            get => 
                ((string) this["lastuser"]);
            set
            {
                this["lastuser"] = value;
            }
        }
    }
}

