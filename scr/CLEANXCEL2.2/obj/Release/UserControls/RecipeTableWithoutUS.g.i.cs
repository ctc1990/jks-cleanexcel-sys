﻿#pragma checksum "..\..\..\UserControls\RecipeTableWithoutUS.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5BEFD6CE2AD75C7749D9A6F3674BFC6F631DAEBD87342DA97AA489DB1929D8FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLEANXCEL2._2.UserControls;
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


namespace CLEANXCEL2._2.UserControls {
    
    
    /// <summary>
    /// RecipeTableWithoutUS
    /// </summary>
    public partial class RecipeTableWithoutUS : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CLEANXCEL2._2.UserControls.RecipeTableWithoutUS userControl;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock titleWithoutUS;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBProcessTimeTitle;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBProcessTime;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Protect_DeleteButton;
        
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
            System.Uri resourceLocater = new System.Uri("/CLEANXCEL2.2;component/usercontrols/recipetablewithoutus.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
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
            this.userControl = ((CLEANXCEL2._2.UserControls.RecipeTableWithoutUS)(target));
            return;
            case 2:
            this.titleWithoutUS = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.TBProcessTimeTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.TBProcessTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Protect_DeleteButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\UserControls\RecipeTableWithoutUS.xaml"
            this.Protect_DeleteButton.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Protect_DeleteButton_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
