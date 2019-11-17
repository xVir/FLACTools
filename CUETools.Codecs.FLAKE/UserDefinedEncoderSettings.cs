using System.ComponentModel;

namespace CUETools.Codecs.FLAKE
{
    public class UserDefinedEncoderSettings : AudioEncoderSettings
    {
        public UserDefinedEncoderSettings()
            : base()
        {
        }

        [DefaultValue(null)]
        public string Path
        {
            get;
            set;
        }

        [DefaultValue(null)]
        public string Parameters
        {
            get;
            set;
        }

        public string SupportedModes
        {
            get
            {
                return m_supported_modes;
            }
            set
            {
                m_supported_modes = value;
            }
        }
    }
}
