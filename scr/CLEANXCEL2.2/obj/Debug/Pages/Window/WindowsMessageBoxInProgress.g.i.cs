﻿#pragma checksum "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3745C35D6A17F9D0D37C85222D286AE65EF1B8AB97964099CA4C4B7BDC9D6CDA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLEANXCEL2._2.Pages.Window;
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


namespace CLEANXCEL2._2.Pages.Window {
    
    
    /// <summary>
    /// WindowsMessageBoxInProgress
    /// </summary>
    public partial class WindowsMessageBoxInProgress : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Caption;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Message;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ButtonGrid;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonYes;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOK;
        
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
            System.Uri resourceLocater = new System.Uri("/CLEANXCEL2.2;component/pages/window/windowsmessageboxinprogress.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
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
            
            #line 7 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
            ((CLEANXCEL2._2.Pages.Window.WindowsMessageBoxInProgress)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Caption = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Message = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ButtonGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.ButtonYes = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
            this.ButtonYes.Click += new System.Windows.RoutedEventHandler(this.ButtonYes_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonOK = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\..\Pages\Window\WindowsMessageBoxInProgress.xaml"
            this.ButtonOK.Click += new System.Windows.RoutedEventHandler(this.ButtonCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

