﻿#pragma checksum "..\..\..\..\Pages\User\Index.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C157784AD0BFEAA5D6332ACFF57C8996700A17E356A66814E4659D84939F0EDB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLEANXCEL2._2.Pages.User;
using SharpVectors.Converters;
using SharpVectors.Runtime;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CLEANXCEL2._2.Pages.User {
    
    
    /// <summary>
    /// Index
    /// </summary>
    public partial class Index : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 160 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel Logo;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel FormPanel;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame FrameLocalContainer;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbxMainLanguage;
        
        #line default
        #line hidden
        
        
        #line 200 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel SubMenuPanel;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBSignIn;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBRegistration;
        
        #line default
        #line hidden
        
        
        #line 236 "..\..\..\..\Pages\User\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBPassword;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CLEANXCEL2.2;component/pages/user/index.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\User\Index.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Pages\User\Index.xaml"
            ((CLEANXCEL2._2.Pages.User.Index)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 31 "..\..\..\..\Pages\User\Index.xaml"
            ((System.Windows.Media.Animation.Storyboard)(target)).Completed += new System.EventHandler(this.LoadPage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Logo = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 4:
            this.FormPanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 5:
            this.FrameLocalContainer = ((System.Windows.Controls.Frame)(target));
            
            #line 179 "..\..\..\..\Pages\User\Index.xaml"
            this.FrameLocalContainer.Loaded += new System.Windows.RoutedEventHandler(this.FrameContainerLoaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CbxMainLanguage = ((System.Windows.Controls.ComboBox)(target));
            
            #line 192 "..\..\..\..\Pages\User\Index.xaml"
            this.CbxMainLanguage.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CbxMainLanguage_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SubMenuPanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 8:
            this.RBSignIn = ((System.Windows.Controls.RadioButton)(target));
            
            #line 224 "..\..\..\..\Pages\User\Index.xaml"
            this.RBSignIn.Checked += new System.Windows.RoutedEventHandler(this.RBSignIn_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.RBRegistration = ((System.Windows.Controls.RadioButton)(target));
            
            #line 233 "..\..\..\..\Pages\User\Index.xaml"
            this.RBRegistration.Checked += new System.Windows.RoutedEventHandler(this.RBRegistration_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.RBPassword = ((System.Windows.Controls.RadioButton)(target));
            
            #line 242 "..\..\..\..\Pages\User\Index.xaml"
            this.RBPassword.Checked += new System.Windows.RoutedEventHandler(this.RBPassword_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

