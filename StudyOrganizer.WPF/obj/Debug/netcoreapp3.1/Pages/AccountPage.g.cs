﻿#pragma checksum "..\..\..\..\Pages\AccountPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3CD99E90FB19F8C677F601ECDB98D078B167B056"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using StudyOrganizer.WPF;
using StudyOrganizer.WPF.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace StudyOrganizer.WPF.Pages {
    
    
    /// <summary>
    /// AccountPage
    /// </summary>
    public partial class AccountPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\..\Pages\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WarningOutput;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StudyOrganizer.WPF;component/pages/accountpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\AccountPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 35 "..\..\..\..\Pages\AccountPage.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Name_OnLostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 38 "..\..\..\..\Pages\AccountPage.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Login_OnLostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 41 "..\..\..\..\Pages\AccountPage.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Password_OnLostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 44 "..\..\..\..\Pages\AccountPage.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Study_OnLostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 47 "..\..\..\..\Pages\AccountPage.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Semester_OnLostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.WarningOutput = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

