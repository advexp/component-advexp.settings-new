###Advexp.Settings

####Details

Create cross-platform settings and make them accessible in your iOS or Android application.

- **iOS**: Storing settings in a normal form using *NSUserDefaults*
- **iOS**: Storing settings in an encrypted form using Keychain
- **Android**: Using *SharedPreferences* to store normal and confidential settings (using AES256 encryption algorithm)
- Using user storage for settings
- Using any user-defined types
- **iOS**: Ability to link settings from the Settings app with settings from Advexp.Settings
- **iOS**: The possibility of using [InAppSettingsKit component](https://components.xamarin.com/view/InAppSettingsKit) along with Advexp.Settings. Both for creating fully functional GUI of the app settings and for locating them in Settings app and accessing them from C# code. The example of using can be found in the component examples (a license of higher level than Xamarin Starter Edition is required)

#####Storage used in Advexp.Settings

- **iOS**: *NSUserDefaults* is for settings that do not require encryption or privacy
- **iOS**: Keychain is for settings that should be stored in encrypted form
- **Android**: *SharedPreferences* in the application context - for normal settings that do not require encryption and for confidential settings using AES256 encryption algorithm + user password
- Any settings storage that is user-defined or provided by third parties

This component makes it possible to create an application in Xamarin Starter Edition (that does not exceed the executable file size limits for a simple application).

**Advexp.Settings - Local** component you can find here: <http://components.xamarin.com/view/advexp-settings-local>

#####Example of a settings declaration

    :::csharp
    using Advexp;

    class SettingsSerializer : Advexp.ISettingsSerializer
    {
        public bool Load(string settingName, bool secure, out object value)
        {
            // Implementation of loading
            value = null;

            // if downloaded successfully then return true otherwise false
            return true; 
        }

        public void Save(string settingName, object settingValue, bool secure)
        {
            // Implementation of saving
        }

        public void Delete(string settingName, bool secure)
        {
            // Implementation of deletion
        }

        public void Synchronize()
        {
            // Setting synchronization
            // For the state of the settings in the application
            // to affect the state of the settings in the storage
            // If necessary
        }
    }

    class Settings : Advexp.Settings<Settings>
    {
        [Setting(Name = "StringSetting")]
        public static String StringSetting {get; set;}

        [Setting(Name = "NonStaticStringSetting")]
        public String NonStaticStringSetting {get; set;}

        [Setting(Name = "SecureStringSetting", Secure = true)]
        public static String SecureStringSetting {get; set;}

        [Setting(Name = "StringSettingWithSerializer")]
        [Serializer(SerializerType = typeof(SettingsSerializer))]
        public static String StringSettingWithSerializer {get; set;}

        // In this case, the automatic setting name in storage will be
        // "{NamespaceName}.{ClassName}.{FieldName}"
        // The name pattern can be changed 
        // using the SettingsConfiguration.SettingsNamePattern property
        // The default pattern name is: "{NamespaceName}.{ClassName}.{FieldName}"
        [Setting]
        public static String SettingWithAutoName {get; set;}
    }
    
#####Example of settings usage

    :::csharp
    class Application
    {
        static void Main(string[] args)
        {
            Settings.LoadSettings();

            // Will be saved to NSUserDefaults for iOS
            // and to SharedPreferences for Android
            Settings.StringSetting = "Data1";
            Settings.Instance.NonStaticStringSetting = "Data2";
            // Will be saved to Keychain for iOS
            // and to SharedPreferences in encrypted form for Android
            Settings.SecureStringSetting = "Date3";
            // Will be saved to the user-defined storage 
            // specified in the SettingsSerializer class
            Settings.StringSettingWithSerializer = "Data4";

            Settings.SaveSettings();
        }
    }

####Geting Started

#####Create settings

Create a new class with the name "Settings" (for example) and inherit it as follows:

    :::csharp
    class Settings : Advexp.Settings<Settings>
    {
    // settings go here
    }

Specify those settings in the definition of this class that are required by your application. They should be accompanied by required attributes:

    :::csharp
    [Setting(Name = "StringSetting")]
    public static String StringSetting {get; set;}

    [Setting(Name = "NonStaticStringSetting")]
    public String NonStaticStringSetting {get; set;}
    
    [Setting(Name = "SecureStringSetting", Secure = true)]
    public static String SecureStringSetting {get; set;}

You can use the class fields instead of properties. For example:

    :::csharp
    [Setting(Name = "StringSetting")]
    public static String StringSetting;

The settings do not have to be static. In this case, they can be accessed through the *Instance* static property, which returns the same instance of the setting class.

Call the appropriate method (*LoadSettings* / *SaveSettings* / *DeleteSettings*) in order to perform the desired actions.


#####Using user-defined types as settings

In order to allow the library to manipulate user data types, they must be labeled as *Serializable* and, if necessary, be properly implemented. This will make it possible to correctly store and retrieve them.
The library correctly handles *enum* type data, and the user does not have to perform any additional work in this area.  
Use case - CustomObjectTest in the TDD project, which can be accessed at <https://bitbucket.org/advexp/component-advexp.settings>

#####Serialization

The library user can specify their own method to save/load/delete settings using the *ISettingsSerializer* interface and the *SerializerAttribute* attribute or *MethodSerializerAttribute* attribute. This makes it possible to manipulate the settings in storage that is currently not supported by the library or has been implemented by a third-party.

#####Priorities for using serializers

The following serializers are available:

- Library level
- Class level
- Field level

Serializers are used with the following priority (the higher in the list, the higher the priority):

- Field level
- Class level
- Library level
- If none of the above is present then the built-in serializer is used depending on the attribute type or its parameters

In short, the priority for settings serialization is assigned in a cascading fashion, from the library level serializer to the field level serializer, and it can be overridden at a lower level of abstraction.

#####Description of library classes

***interface ISettingsSerializer***  
It describes the serializer interface.

***class MethodSerializerAttribute***  
Arbitrary class methods can be used as a serializer.

Method prototypes are:  
*bool Load(string settingName, bool secure, out object value);*  
Returns true if downloaded successfully, otherwise false  
*void Save(string settingName, object value, bool secure);*  
*void Delete(string settingName, bool secure);*  
*void Synchronize();*  
This attribute can be applied to a class or to a member of a class.

***class SerializerAttribute***  
The attribute specifies the serializer type that must be applied to a setting or to a class with settings.

***class SettingAttribute***  
This attribute indicates that the current class element is a setting and can be loaded/saved/deleted.  
It is applied to a class member.

***class Settings<T\>***  
The class specifies that its inheritor is a settings class and adds the appropriate functionality to the methods set.  
The class contains methods that allow you to manipulate only a particular setting while ignoring other ones. For a usage example, see the file LocalSettingsController.cs and the functions *LocalSettings.Save(s => ... )* in the component iOS example.

***class SettingsConfiguration***  
Static class. It contains parameters that define the library configuration.

**iOS:** ***class UserDefaultsSerializer***  
A class that implements a serializer for *NSUserDefaults*. It can be used when it is necessary to gain access to parameters that are not saved in the *NSUserDefaults.StandardUserDefaults* container.  
Usage example - ExternalUserDefaultsTest in the iOS TDD project, which can be accessed at <https://bitbucket.org/advexp/component-advexp.settings>

**Android:** ***class SharedPreferencesSerializer***  
The class that implements the serializer for *SharedPreferences*. It can be applied when it is necessary to access parameters that are not stored in SharedPreferences of the application context.  
Example - ExternalSaredPreferencesTest in the Android TDD project located at <https://bitbucket.org/advexp/component-advexp.settings>

#####Library configuration

All library parameters can be set through the *SettingsConfiguration* static class. These manipulations should be produced before the other library functions are first used.

- ***SettingsNamePattern***  - specifies the name pattern under which the settings are saved to the storage. The default value is *"{NamespaceName}.{ClassName}.{FieldName}"*. It supports only those substitution parameters that are used in the default value
- **iOS:** ***KeyChainServiceName*** - for settings that will be stored in the Keychain. It defines the Service Name. The default value is *"Advexp.Settings"*
- **iOS:** ***KeyChainPrefixName*** - for settings that will be stored in Keychain. It contain a prefix that is added to the beginning of the setting name. The default value is *String.Empty*. In case if value is not *String.Empty*, it is separated from the rest of the name by a '.'
- **iOS:** ***KeyChainSecAccessible*** - for settings that will be stored in the Keychain. It determines at what time the parameter can be read from the Keychain. The default value is *SecAccessible.Always*
- **Android:** ***SecureSettingsPassword*** - the password used to encrypt your confidential settings using the AES256 algorithm. Default value String.Empty
- ***Serializer*** - determines the library level serializer. The default value is *null*

#####The relationship between the settings from Settings App and the settings from Advexp.Settings and using the InAppSettingsKit component

In order to link the settings from the Settings app with settings from Advexp.Settings, you must ensure that the parameter *Name* in the *SettingAttribute* attribute matches the corresponding identifier in Settings.bundle. In this case, it is not possible to set the parameter *Secure = true*, since the setting must be stored in *NSUserDefaults*. When using along with InAppSettingsKit component, it is enough to maintain identificator compliance.
For a more detailed usage option for this scenario, see the component example.

#####Supported platforms

Xamarin.iOS (Unified)  
Xamarin.Android

Please send your questions, suggestions and impressions to <components@advexp.net> with the subject "Advexp.Settings"



