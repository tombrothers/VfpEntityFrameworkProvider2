﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VfpEntityFrameworkProvider.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VfpEntityFrameworkProvider.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] BlankDbc {
            get {
                object obj = ResourceManager.GetObject("BlankDbc", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] BlankDct {
            get {
                object obj = ResourceManager.GetObject("BlankDct", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] BlankDcx {
            get {
                object obj = ResourceManager.GetObject("BlankDcx", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Entity Framework 6 Provider for Visual FoxPro Data.
        /// </summary>
        internal static string Provider_Description {
            get {
                return ResourceManager.GetString("Provider_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VFP Entity Framework Provider (EF6).
        /// </summary>
        internal static string Provider_DisplayName {
            get {
                return ResourceManager.GetString("Provider_DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VfpEntityFrameworkProvider2.
        /// </summary>
        internal static string Provider_Invariant {
            get {
                return ResourceManager.GetString("Provider_Invariant", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;!-- 
        ///####################################################################################################################
        ///
        ///BE AWARE THAT THE ORDER IN WHICH TYPES ARE DESCRIBED IN THE PROVIDER MANIFEST IS RELEVANT AND HAVE IMPACT IN LOOKUP
        ///PROCESS
        ///
        ///#################################################################################################################### 
        ///--&gt;
        ///&lt;ProviderManifest Namespace=&quot;Vfp&quot;
        ///                  xmlns=&quot;http://schemas.microsoft.com/ado/ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ProviderManifest {
            get {
                return ResourceManager.GetString("ProviderManifest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;Schema Namespace=&quot;VfpEntityFrameworkProvider&quot;
        ///        Provider=&quot;VfpEntityFrameworkProvider2&quot;
        ///        ProviderManifestToken=&quot;Vfp&quot;
        ///        Alias=&quot;Self&quot;
        ///        xmlns=&quot;http://schemas.microsoft.com/ado/2006/04/edm/ssdl&quot;&gt;
        ///  &lt;EntityContainer Name=&quot;Schema&quot;&gt;
        ///    &lt;EntitySet Name=&quot;STables&quot;
        ///               EntityType=&quot;Self.Table&quot;&gt;
        ///      &lt;DefiningQuery&gt;
        ///        {{{|VFP:EF:SCHEMAHELPER_TABLES|}}}
        ///      &lt;/DefiningQuery&gt;
        ///    &lt;/EntitySet&gt;
        ///
        ///    &lt;EntitySet Name=&quot;STableColu [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StoreSchemaDefinition {
            get {
                return ResourceManager.GetString("StoreSchemaDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;Mapping Space=&quot;C-S&quot;
        ///         xmlns=&quot;urn:schemas-microsoft-com:windows:storage:mapping:CS&quot;&gt;
        ///  &lt;EntityContainerMapping StorageEntityContainer=&quot;Schema&quot;
        ///                          CdmEntityContainer=&quot;SchemaInformation&quot;&gt;
        ///    &lt;EntitySetMapping Name=&quot;Tables&quot;
        ///                      StoreEntitySet=&quot;STables&quot;
        ///                      TypeName=&quot;Store.Table&quot;&gt;
        ///      &lt;ScalarProperty Name=&quot;Id&quot;
        ///                      ColumnName=&quot;Id&quot; /&gt;
        ///      &lt;ScalarProperty Name=&quot;CatalogName&quot;
        ///    [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StoreSchemaMapping {
            get {
                return ResourceManager.GetString("StoreSchemaMapping", resourceCulture);
            }
        }
    }
}
