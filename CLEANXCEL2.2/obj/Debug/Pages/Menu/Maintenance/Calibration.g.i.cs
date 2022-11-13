﻿#pragma checksum "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "51F30335EB5B9AC1C0DDB64EB31FA5F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLEANXCEL2._2.Pages.Menu.Maintenance;
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


namespace CLEANXCEL2._2.Pages.Menu.Maintenance {
    
    
    /// <summary>
    /// Calibration
    /// </summary>
    public partial class Calibration : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LowVacuumLevelCap;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LowVacuumLevel;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HighVacuumLevelCap;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HighVacuumLevel;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CalibrateVacuumPressure;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Recover;
        
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
            System.Uri resourceLocater = new System.Uri("/CLEANXCEL2.2;component/pages/menu/maintenance/calibration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
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
            
            #line 8 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
            ((CLEANXCEL2._2.Pages.Menu.Maintenance.Calibration)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LowVacuumLevelCap = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.LowVacuumLevel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.HighVacuumLevelCap = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.HighVacuumLevel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.CalibrateVacuumPressure = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
            this.CalibrateVacuumPressure.Click += new System.Windows.RoutedEventHandler(this.CalibrateVacuumPressure_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Recover = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\..\Pages\Menu\Maintenance\Calibration.xaml"
            this.Recover.Click += new System.Windows.RoutedEventHandler(this.Recover_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

