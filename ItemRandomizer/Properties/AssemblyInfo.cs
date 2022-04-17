using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MelonLoader;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ItemRandomizer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ItemRandomizer")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9c9c4424-eb66-4b4a-80bd-6cdfb572ea0f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//MelonLoader relies on assembly info to get your mod description. We will have to setup them up.
//To do that, go to the Properties directory, and add these three lines to AssemblyInfo.cs
[assembly: MelonInfo(typeof(ItemRandomizer.ItemRandomizer), "ItemRandomizer", "1.0.0", "undefined", "https://www.nexusmods.com/users/80656793?tab=user+files")]
[assembly: MelonGame("Eek", "House Party")]
[assembly: VerifyLoaderVersion(0, 5, 0, true)]
