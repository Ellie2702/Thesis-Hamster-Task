﻿#pragma checksum "..\..\AddProject - Копировать.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3B86E54AA9AD6B87B88CAAEFD30EBBDD22F6BBFEFF7F1E47F9A90D4CB2F1A308"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HamsterTask;
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


namespace HamsterTask {
    
    
    /// <summary>
    /// AddProject
    /// </summary>
    public partial class AddProject : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\AddProject - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AddProjectDescrip;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\AddProject - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProjectName;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\AddProject - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskDesc;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\AddProject - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TaskDeadline;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\AddProject - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBTN;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\AddProject - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTNCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/HamsterTask;component/addproject%20-%20%d0%9a%d0%be%d0%bf%d0%b8%d1%80%d0%be%d0%b" +
                    "2%d0%b0%d1%82%d1%8c.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddProject - Копировать.xaml"
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
            this.AddProjectDescrip = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.ProjectName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TaskDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TaskDeadline = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.AddBTN = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\AddProject - Копировать.xaml"
            this.AddBTN.Click += new System.Windows.RoutedEventHandler(this.AddBTN_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BTNCancel = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\AddProject - Копировать.xaml"
            this.BTNCancel.Click += new System.Windows.RoutedEventHandler(this.BTNCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

