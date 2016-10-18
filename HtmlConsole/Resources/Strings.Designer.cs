﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace HtmlConsole.Resources {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    internal class Strings {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("GenerationShip.Console.ConsoleStack.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Available commands.
        /// </summary>
        internal static string AvailableCommands {
            get {
                return ResourceManager.GetString("AvailableCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown command &quot;{0}&quot;!.
        /// </summary>
        internal static string CommandNotFound {
            get {
                return ResourceManager.GetString("CommandNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command &quot;{0}&quot; requires at least {1} arguments!.
        /// </summary>
        internal static string CommandRequiresMoreArgs {
            get {
                return ResourceManager.GetString("CommandRequiresMoreArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is greater than the maximum of &quot;{1}&quot;..
        /// </summary>
        internal static string IntegerIsGreaterThanMax {
            get {
                return ResourceManager.GetString("IntegerIsGreaterThanMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is less than the minimum of &quot;{1}&quot;..
        /// </summary>
        internal static string IntegerIsLessThanMin {
            get {
                return ResourceManager.GetString("IntegerIsLessThanMin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is not a boolean value..
        /// </summary>
        internal static string InvalidBoolValue {
            get {
                return ResourceManager.GetString("InvalidBoolValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The numeric value &quot;{0}&quot; is not valid (following values are allowed: {1})..
        /// </summary>
        internal static string InvalidEnumNumericValue {
            get {
                return ResourceManager.GetString("InvalidEnumNumericValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &quot;{0}&quot; is not valid (following values are allowed: {1})..
        /// </summary>
        internal static string InvalidEnumValue {
            get {
                return ResourceManager.GetString("InvalidEnumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is not an integer..
        /// </summary>
        internal static string InvalidIntegerValue {
            get {
                return ResourceManager.GetString("InvalidIntegerValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameters.
        /// </summary>
        internal static string Parameters {
            get {
                return ResourceManager.GetString("Parameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is longer than the maximum allowed length &quot;{1}&quot;..
        /// </summary>
        internal static string StringIsLongerThanMaxLength {
            get {
                return ResourceManager.GetString("StringIsLongerThanMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is shorter than the minimum allowed length &quot;{1}&quot;..
        /// </summary>
        internal static string StringIsShorterThanMinLength {
            get {
                return ResourceManager.GetString("StringIsShorterThanMinLength", resourceCulture);
            }
        }
    }
}
