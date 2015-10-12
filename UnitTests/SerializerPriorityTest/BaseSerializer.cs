using Advexp;

namespace TDD
{
    public class BaseSerializer : ISettingsSerializer
    {
        //------------------------------------------------------------------------------
        public BaseSerializer()
        {
        }

        #region ISettingsSerializer implementation

        //------------------------------------------------------------------------------
        public object Load(string settingName, bool secure)
        {
            return null;
        }

        //------------------------------------------------------------------------------
        public void Save(string settingName, object value, bool secure)
        {
        }

        //------------------------------------------------------------------------------
        public void Delete(string settingName, bool secure)
        {
        }

        //------------------------------------------------------------------------------
        public void Synchronize()
        {
        }

        #endregion
    }
}

