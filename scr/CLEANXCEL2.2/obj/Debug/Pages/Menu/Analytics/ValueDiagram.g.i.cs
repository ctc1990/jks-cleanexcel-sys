﻿#pragma checksum "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "308D224322AC4D5BC6FC9F29D6AE6CF8BC1CD368912699B49C714F0929B69C77"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLEANXCEL2._2.Pages.Menu.Analytics;
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


namespace CLEANXCEL2._2.Pages.Menu.Analytics {
    
    
    /// <summary>
    /// ValueDiagram
    /// </summary>
    public partial class ValueDiagram : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RecipeSelection;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bAnalyse;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser LocalBrowser;
        
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
            System.Uri resourceLocater = new System.Uri("/CLEANXCEL2.2;component/pages/menu/analytics/valuediagram.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml"
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
            
            #line 8 "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml"
            ((CLEANXCEL2._2.Pages.Menu.Analytics.ValueDiagram)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RecipeSelection = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.bAnalyse = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\..\Pages\Menu\Analytics\ValueDiagram.xaml"
            this.bAnalyse.Click += new System.Windows.RoutedEventHandler(this.Analyse_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LocalBrowser = ((System.Windows.Controls.WebBrowser)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

