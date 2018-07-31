###Advexp.Settings

Settings for Xamarin

####Details

Create cross-platform settings and make them accessible in your iOS or Android projects natively.

- **iOS**: Storing settings in a normal form using *NSUserDefaults*
- **iOS**: Storing settings in an encrypted form using Keychain
- **Android**: Using *SharedPreferences* to store settings in a normal form
- **Android**: Using KeyStore to save confidential settings in an encrypted form
- Storing settings as dynamic parameters (*name* - *value* pairs)
- Using user storage for settings
- Using any build-in or user-defined types which can be saved as a setting
- **iOS**: Ability to link settings from Advexp.Settings with settings from the Settings App
- **iOS**: The possibility of using [InAppSettingsKit](https://components.xamarin.com/view/InAppSettingsKit) along with Advexp.Settings. Both for creating fully functional GUI of the app settings and for locating them in the Settings App and accessing them from C# code.
- Using library in PCL projects
- Saving or loading settings by using JSON. In this case, the additional package [Json.NET](https://www.nuget.org/packages/newtonsoft.json) is used.

NuGet package “Advexp.Settings Local” you can download from the site:  
<https://www.nuget.org/packages/Advexp.Settings.Local>
 
NuGet package “Advexp.Settings Cloud”, evaluation version, you can download from the site:  
<https://www.nuget.org/packages/Advexp.Settings.Cloud.Evaluation>

Samples, Unit Tests and additional information you can find on this site.

To purchase "Advexp.Settings Cloud", send a request to <components@advexp.net>

#####Example of a settings declaration

    :::csharp
    class Settings : Advexp.Settings<Settings>
    {
        [Advexp.
         Setting(Name = "IntSetting", Default = 3)]
        public static Int32 IntSetting {get; set;}

        [Advexp.
         Setting(Name = "NonStaticStringSetting", Default = "default string value")]
        public String NonStaticStringSetting {get; set;}

        [Advexp.
         Setting(Name = "SecureDateTimeSetting", 
                     	Secure = true, 
                     	Default = "2009-06-15T13:45:30.0000000Z")]
        public static DateTime SecureDateTimeSetting {get; set;}

        // In this case, the automatic setting name in storage will be
        // "NamespaceName.ClassName.FieldName"
        [Advexp.
         Setting]
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
            Settings.IntSetting = 5;
            Settings.Instance.NonStaticStringSetting = "Data1";
            Settings.SettingWithAutoName = "Data2";
            // Will be saved to Keychain for iOS and to KeyStore for Android
            Settings.SecureDateTimeSetting = DateTime.Now;

            Settings.SaveSettings();

            // Dynamic settings
        
            var lds = Settings.
              GetPlugin<Advexp.LocalDynamicSettings.Plugin.ILocalDynamicSettingsPlugin>();
        
            lds.LoadSettings();

            lds.SetSetting("dynamic_setting_name1", "value1");
            lds.SetSetting("dynamic_setting_name2", false);
            lds.SetSetting("dynamic_setting_name3", DateTime.Now);

            lds.SaveSettings();

            var dt = lds.GetSetting<DateTime>("dynamic_setting_name3");
        }
    }


####Getting Started
#####Create settings

Create a new class with the name "Settings" (for example) and inherit it as follows:

    :::csharp
    class Settings : Advexp.Settings<Settings>
    {
    // settings go here
    }

Specify those settings in the definition of this class that are required by your application. They should be accompanied by required attributes:

    :::csharp
    [Setting(Name = "IntSetting", Default = 3)]
    public static Int32 IntSetting {get; set;}

    [Setting(Name = "StringSetting")]
    public static String StringSetting {get; set;}
    
    [Setting(Name = "SecureDateTimeSetting", 
                 Secure = true, 
                 Default = "2009-06-15T13:45:30.0000000Z")]
    public static DateTime SecureDateTimeSetting {get; set;}


You can use the class fields instead of properties. For example:

    :::csharp
    [Setting(Name = "StringSetting")]
    public static String StringSetting;

The settings do not have to be static. In that case, they can be accessed through the *Instance* static property or through the class object which you manage independently.

Also, you can use dynamic parameters as saved settings. Their use is described below.


Call the appropriate method in order to perform the desired actions.


#####Using user-defined types as settings

The library allows the use of any user-defined types which can be saved as settings. User-defined types do not require modification and addition of special attributes. Usage case - CustomObjectTest in TDD projects on this site. 
Serializing settings is done via the [SharpSerializer](http://sharpserializer.com/en/index.html) library.
The serializer settings can be modified by using the following parameter:
*SettingsBaseConfiguration.AdvancedConfiguration.SharpSerializerSettings*


#####Settings serialization

Library users can specify their own method for storing/loading/deleting settings by using the *ISettingsSerializer* interface and the *SerializerAttribute* attribute or the *MethodSerializerAttribute* attribute.

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

#####Using proprietary attributes to specify settings

You can use proprietary attributes to specify settings. To do this, you need to register your attribute type and the corresponding serializer type:

    :::csharp
	SettingsBaseConfiguration.
	    RegisterSettingsAttribute<MyAttributeType, MySerializerType>();

#####Specifying the default settings values

You can assign a default settings value which will be used for various exceptional cases, inability to load or when deleting. This is done with the help of the *SettingBaseAttribute.Default* property. This property can be assigned any type of value allowed in C# during attribute initialization or the *DefaulValueMode.TypeDefaultValue* value. In this case, the default value will be the value used by default for this type in C#. Many parameters can also be assigned by using a string value. For example, the default value for the DateTime setting type can be assigned as a string “2009-06-15T13:45:30.0000000Z”. In case the *Default* property has not been set up, then to delete the settings or in various exceptional cases (for example, inability to convert the loaded value into a setting type), the default value utilized for this type in C# will be used; in the case of inability to load, the setting value will remains unchanged.


#####Dynamic settings

Dynamic settings are parameters of the *name* - *value* pairs.  Where "name" is the text name of the setting, and "value" is the value of any system or user-defined type.  Local dynamic settings can be accessed via the *ILocalDynamicSettingsPlugin* interface of the corresponding plugin:

    var lds = MySettingsClass.GetPlugin<ILocalDynamicSettingsPlugin>();

This plugin is built into the library and does not require registration.

To manipulate dynamic settings, the following methods are available.

Load, save or delete all dynamic settings:

    void LoadSettings();
    void SaveSettings();
    void DeleteSettings();
        

Load, save, or delete a dynamic setting with a specific name:

    void LoadSetting(string settingName);
    void SaveSetting(string settingName);
    void DeleteSetting(string settingName);


Determine whether a dynamic setting with a specific name is contained in the collection:

    bool Contains(string settingName);


Set the order of the dynamic settings. By default, the order of the settings corresponds to the order at which they have been added to the collection. But you can change it to any other. If you input *null* into this function, the order will be reset to the original one. If a setting name from the collection isn't present in the installed sequence order, then this setting will not be listed. The custom and default orders of the dynamic settings are saved and will be restored the next time the dynamic parameters are loaded.
An example of use can be found in the TDD project (UnitTests/DynamicSettings/DynamicSettingsTest.TestDynamicSettingsCustomOrder):

    void SetSettingsOrder(IEnumerable<string> settingsOrder);


Set or add (if the setting is not in the collection) a dynamic setting:

    void SetSetting<T>(string settingName, T settingValue);


Get the dynamic setting value for a specific name and bring it to the T type.
For example, if the text "10" was saved as the dynamic setting, then an attempt to get a value of int type returns a number 10 of int type:

    T GetSetting<T>(string settingName);


Set the default values. This value will be returned if there is no dynamic setting in the collection. Reset all default values by inputting *null* into the function.
An example of use can be found in the TDD project (UnitTests/DynamicSettings/DynamicSettingsTest.TestDefaultValues):

    void SetDefaultSettings(IDictionary<string, object> defaultSettings);


Listing of all the settings in the collection:

    IEnumerator<string> GetEnumerator();

 The following action is valid:
 
    var lds = MySettingsClass.GetPlugin<ILocalDynamicSettingsPlugin>();
    foreach(var dynamicSettingName in lds)
    {
    }
    
Get the number of dynamic settings in the collection:

    int Count { get; }


Examples of using dynamic settings can be seen in the corresponding example (Samples/DynamicSettings) or in the TDD project (TDD/UnitTests/DynamicSettings)


#####Saving and loading settings by using JSON

To add this capability, you need to install the [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json) nuget package, add the **Advexp.JSONSettings.Plugin.PCL** assembly to the dependencies, and register the plugin:

    :::csharp
	SettingsBaseConfiguration.
	    RegisterSettingsPlugin<IJSONSettingsPlugin, JSONSettingsPlugin>();

Next, obtain a copy of the plugin from the class or from an object from that class, depending on which one is being accessed - the static class elements or the object’s elements.

    :::csharp
	var settingsInstance = new SettingsClass();
	var jsonPlugin = settingsInstance.GetObjectPlugin<IJSONSettingsPlugin>();

or

    :::csharp
	var jsonPlugin = SettingsClass.GetPlugin<IJSONSettingsPlugin>();

You can load or save settings in JSON format by using the plugin object.

Save settings in JSON format:

    :::csharp
	var jsonSettings = jsonPlugin.SaveSettingsToJSON();

Load settings from JSON:

    :::csharp
	jsonPlugin.LoadSettingsFromJSON(jsonSettings);

The JSON parser parameters may be modified by using the parameter:  
*JSONSettingsConfiguration.JsonSerializerSettings*

The JSON Settings plugin parameters may be modified by using the parameter:  
*JSONSettingsConfiguration.PluginSettings*

A way of using this plugin may be seen in the component examples:  
Sample.JSONSettings.iOS and Sample.JSONSettings.Android

#####Previous version support and settings naming

During the development process, you need to keep in mind that Advexp.Settings version 2.0 and higher, records settings in a different format. Reading the parameters formed by the library's previous version is done by utilizing the migration mechanism. Also, in the new version, a "v2" prefix is automatically added to the setting's name. Encrypted parameters (Secure) in the library's Android version are not subject to migration. Consequently, the library does not have the capability to read the confidential settings and perform migration from the old version (encrypted SharedPreferences) to the new one (KeyStore). You can do that yourself for the simple settings type by reading them from *SharedPreferences* and decrypting them with the help of the *Cryptography* class located in the TDD library project ([here](https://bitbucket.org/advexp/component-advexp.settings/src/833b7ffdcd37244a9082d692117324c32f3b0793/TDD.Android/Cryptography.cs?at=default&fileviewer=file-view-default)). For the iOS version, all settings are readable without additional manipulations by the user. Also, for the iOS library version, the *SettingsConfiguration.KeyChainPrefixName* configuration parameter is no longer used. Previous version support can be turned on by using *SettingsBaseConfiguration.EnableFormatMigration* property.

#####iOS: The relationship between the settings from Settings App and the settings from Advexp.Settings and using the InAppSettingsKit component

In order to link the settings from the Settings app with settings from Advexp.Settings, you must ensure that the parameter *Name* in the *SettingAttribute* attribute matches the corresponding identifier in Settings.bundle. In this case, it is not possible to set the parameter *Secure = true*, since the setting must be stored in *NSUserDefaults*. When using along with [InAppSettingsKit](https://components.xamarin.com/view/InAppSettingsKit) component, it is enough to maintain identificator compliance.  
This function is available only for local settings (settings marked by the *SettingAttribute*).  
For a more detailed usage option for this scenario, see the component example.

#####Description of library classes

######Advexp.Settings library

***interface ISettingsSerializer*** (Advexp.Settings.Utils.PCL.dll)  
It describes the serializer interface.

***class MethodSerializerAttribute*** (Advexp.Settings.Utils.PCL.dll)  
Arbitrary class methods can be used as a serializer.

Method prototypes are:  
*bool Load(string settingName, bool secure, out object value);*  
Returns true if downloaded successfully, otherwise false  
*void Save(string settingName, bool secure, object value);*  
*void Delete(string settingName, bool secure);*  
*void Contains(string settingName, bool secure);*  
*void Synchronize();*  
This attribute can be applied to a class or to a member of a class.

***class SerializerAttribute*** (Advexp.Settings.Utils.PCL.dll)  
The attribute specifies the serializer type that must be applied to a setting or to a class with settings.

***class SettingAttribute*** (Advexp.Settings.Utils.PCL.dll)  
This attribute indicates that the current class element is a setting and can be loaded/saved/deleted.  
It is applied to a class member.

***class Settings<T>*** (Advexp.Settings.dll for iOS, Android and PCL)  
The class specifies that its inheritor is a settings class and adds the appropriate functionality to the methods set.

***class SettingsConfiguration*** (Advexp.Settings.dll for iOS and Android)  
Static class. It contains parameters that define the library configuration.

**iOS:** ***class UserDefaultsSerializer*** (Advexp.Settings.dll for iOS)  
A class that implements a serializer for *NSUserDefaults*. It can be used when it is necessary to gain access to parameters that are not saved in the *NSUserDefaults.StandardUserDefaults* container.  
Example - ExternalUserDefaultsTest in the iOS TDD project, which can be accessed at <https://bitbucket.org/advexp/component-advexp.settings>

**Android:** ***class SharedPreferencesSerializer*** (Advexp.Settings.dll for Android)  
The class that implements the serializer for *SharedPreferences*. It can be applied when it is necessary to access parameters that are not stored in SharedPreferences of the application context.
Example - ExternalSaredPreferencesTest in the Android TDD project, which can be accessed at <https://bitbucket.org/advexp/component-advexp.settings> 

***class AdvancedConfiguration*** (Advexp.Settings.Utils.PCL.dll)  
Extended library configuration parameters. Objects of this type belong to the *SettingsBaseConfiguration* class.

######Dynamic Settings plugin

***interface ILocalDynamicSettingsPlugin*** (Advexp.Settings.Utils.PCL.dll)  
Interface definition of the Dynamic Settings plugin


######JSON Settings plugin

***interface IJSONSettingsPlugin*** (Advexp.JSONSettings.Plugin.PCL)  
Interface definition of the JSON Settings plugin

***class JSONSettingsPlugin*** (Advexp.JSONSettings.Plugin.PCL)  
Implementation of the JSON Settings plugin

***class JSONSettingsConfiguration*** (Advexp.JSONSettings.Plugin.PCL)  
Static class. It contains parameters that define the JSON Settings plugin configuration

***class PluginSettings*** (Advexp.JSONSettings.Plugin.PCL)  
Class which contains the JSON Settings plugin parameters. Objects of this type belong to the JSONSettingsConfiguration class.

#####Library configuration

######Advexp.Settings library

All library parameters can be set through the *SettingsConfiguration* static class. These manipulations should be produced before the other library functions are first used.

- ***SettingsNamePattern***  - specifies the name pattern under which the settings are saved to the storage. The default value is "{NamespaceName}.{ClassName}.{FieldName}". It supports only those substitution parameters that are used by default
- ***EncryptionServiceID*** - for settings that will be stored in the Keychain or KeyStore. It defines the Service Name. The default value is "Advexp.Settings"
- **iOS:** ***KeyChainSecAccessible*** - for settings that will be stored in the Keychain. It determines at what time the parameter can be read from the Keychain. The default value is *SecAccessible.Always*
- **Android:** ***KeyStoreFileProtectionPassword*** - the password used to protect KeyStore file. The default value is *null*
- **Android:** ***KeyStoreFileName*** - KeyStore file name. The default value is *null*. In this case file name is “.keystore”
- ***Serializer*** - determines the library level serializer. The default value is *null*  
- ***AdvancedConfiguration.SharpSerializerSettings*** - parameters of the *SharpSerializer* serializer which are used for saving settings
- ***RegisterSettingsAttribute*** - a method designed for registering the settings indicator attribute
- ***RegisterSettingsPlugin*** - a method designed for the registration of plugins, for example, *JSONSettingsPlugin*
- **EnableFormatMigration** - enable or disable previous version support. The default value is *false*
- **LogLevel** - set log level

######JSON Settings plugin

All JSON Settings plugin parameters can be set through the JSONSettingsConfiguration static class. These manipulations should be produced before the other library functions are first used.  
***JsonSerializerSettings*** - The JSON parser parameters  
***PluginSettings*** - The JSON Settings plugin parameters


####Supported platforms

Xamarin.iOS (Unified)  
Xamarin.Android

PCL projects

NuGet package “Advexp.Settings Local” you can download from the site:  
<https://www.nuget.org/packages/Advexp.Settings.Local>
 
NuGet package “Advexp.Settings Cloud”, evaluation version, you can download from the site:  
<https://www.nuget.org/packages/Advexp.Settings.Cloud.Evaluation>

Samples, Unit Tests and additional information you can find on this site.

To purchase "Advexp.Settings Cloud", send a request to <components@advexp.net>

Please send your questions, suggestions and impressions to <components@advexp.net> with the subject "Advexp.Settings"